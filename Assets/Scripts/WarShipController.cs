using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarShipController : MonoBehaviour
{
    //public int movePower = 4; // 移動力
    public Vector3Int currentPos; //2次元マップのマス座標の現在の位置
    public Vector3Int tempTargetPos; // 一時的に選んだ移動先
    public Vector3 direction; // 艦の向き
    public int warshipNo;
    [SerializeField]
    public WarshipData warshipData;

    // PathFinder への参照（インスペクターで設定）
    public PathFinder pathFinder;
    // 経路のハイライト表示を行う MapManager への参照
    public MapManager mapManager;
    public bool isMoveEnd;

    [Header("方向選択UI")]
    [SerializeField]
    private GameObject directionsButtonsObj; // Canvas→DirectionsButtons
    [SerializeField]
    private Button btnUp;
    [SerializeField]
    private Button btnDown;
    [SerializeField]
    private Button btnLeft;
    [SerializeField]
    private Button btnRight;

    public Vector3Int prewPos; //移動前の座標

    [Header("ゲーム管理")]
    [SerializeField]
    private　GameManager gameManager; // フェイズ切り替え用


    void Start()
    {
        isMoveEnd = false;
        // DataBaseManager から指定した warshipNo に合致する WarshipData を取得
        warshipData = DataBaseManager.instance.warshipDataSO.warshipDataList.Find(data => data.warshipNo == warshipNo);
        if (warshipData == null)
        {
            Debug.LogError("WarShipDataが warshipNo:" + warshipNo + " で見つかりません");
        }
        else
        {
            // 必要であれば、warshipData の情報を利用して初期化などを行う
            Debug.Log("WarShipDataを取得: " + warshipData.warshipName);
        }
        //ゲーム開始時は 方向ボタンを隠す
        directionsButtonsObj.SetActive(false);
        //各方向ボタンが押されたときのリスナー登録
        btnUp.onClick.AddListener(() => OnDirectionClicked(Vector3.up));
        btnRight.onClick.AddListener(() => OnDirectionClicked(Vector3.right));
        btnDown.onClick.AddListener(() => OnDirectionClicked(Vector3.down));
        btnLeft.onClick.AddListener(() => OnDirectionClicked(Vector3.left));
    }

    // ユーザ入力やコマンドで tempTargetPos を更新した後に呼び出す経路計算メソッド
    public void OnTargetTileSelected(Vector3Int targetTile)
    {
        tempTargetPos = targetTile;
        // BFSを用いて経路を計算する。warshipData.movePower を上限とする
        List<Vector3Int> route = CalculateRoute();
        if (route == null)
        {
            Debug.Log("目標タイル " + tempTargetPos + " は移動可能範囲外です");
        }
        else
        {
            Debug.Log("経路が見つかりました: " + string.Join(" -> ", route));
            // 経路をハイライト表示
            mapManager.ShowRoute(route);
        }
    }

    // BFSで経路を計算する
    public List<Vector3Int> CalculateRoute()
    {
        if (warshipData == null)
        {
            Debug.LogError("WarShipDataが設定されていません");
            return null;
        }
        if (pathFinder == null)
        {
            Debug.LogError("PathFinderが参照されていません");
            return null;
        }
        // warshipData.movePower を上限として経路を取得
        List<Vector3Int> route = pathFinder.FindPath(currentPos, tempTargetPos, warshipData.movePower);
        return route;
    }

    // 移動が完了した後に呼ぶメソッド
    public void EnableDirectionSelection()
    {
        // フェイズを SelectingDirection に切り替え
        gameManager.ChangeCurrentGamePhase(GamePhase.SelectingDirection);
        // 矢印ボタンを表示
        directionsButtonsObj.SetActive(true);
    }

    // ボタンを押したときの処理
    private void OnDirectionClicked(Vector3 dir)
    {
        //回転処理
        SetDirection(dir);

        //方向ボタンを隠して、フェイズを戻す
        directionsButtonsObj.SetActive(false);
        gameManager.ChangeCurrentGamePhase(GamePhase.MoveCurrsor);
    }

    // 方向を決める
    public void SetDirection(Vector3 dir)
    {
        if (dir.sqrMagnitude > 0.01f) //（ベクトルの長さの2乗）が 0.01 よりも大きいかどうかをチェック
        {
            direction = dir.normalized;　//ベクトルの向きはそのままで大きさを 1 にする
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg
                -90f; //右向きが0度になっているので、ここで 90 度引く
            //Mathf.Atan2(direction.y, direction.x) で direction の角度（ラジアン）を求め
            // * Mathf.Rad2Deg でその値を度に変換 結果が、変数 angle に代入される
            transform.rotation = Quaternion.Euler(0, 0, angle); //指定した Euler 角（ここでは X:0, Y:0, Z:angle）をもとに、クォータニオン（回転情報）を生成
        }
        
    }

    // 実際に移動する処理
    public void MoveTo(Vector3Int tilePos, UnitManager unitManager)
    {
        Vector3 tempPos = transform.position;
        Vector3Int gridPos = new Vector3Int((int)tempPos.x, (int)tempPos.y, 0);
        prewPos = gridPos;
        currentPos = tilePos; //新しいタイルの座標を現在の位置に反映
        // タイルの中心のワールド座標を取得し、そこへ移動
        Vector3 centerPos = mapManager.tilemap.GetCellCenterWorld(tilePos);
        transform.position = centerPos;
        isMoveEnd = true;
        unitManager.selectWarShip = null;
    }

    public void MoveCancel(UnitManager unitManager)
    {
        transform.position = prewPos;
        isMoveEnd = false;
        unitManager.selectWarShip = null;
    }


    private Vector3 ConvertTileToWorldPos(Vector3Int tilePos) //タイル座標をワールド座標に変換する処理を行うメソッド
    {
        // タイルサイズなどを考慮してワールド座標を算出
        float x = tilePos.x;
        float y = tilePos.y;
        return new Vector3(x, y, 0);
    }

        void Update()
    {
        // キーボードの上下左右の入力に応じて回転させる
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetDirection(Vector3.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetDirection(Vector3.down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetDirection(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetDirection(Vector3.right);
        }
    }
    
    public Vector3Int GetWarshipPos()
    {
        return mapManager.tilemap.WorldToCell(transform.position);
    }
}
