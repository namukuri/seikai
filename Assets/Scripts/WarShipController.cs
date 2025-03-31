using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarShipController : MonoBehaviour
{
    public int movePower = 4; // 移動力
    public Vector2Int currentPos; //2次元マップのマス座標の現在の位置
    public Vector2Int tempTargetPos; // 一時的に選んだ移動先
    public Vector2 direction; // 艦の向き
    public int warshipNo;
    [SerializeField]
    private WarshipData warshipData;

    void Start()
    {
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
    }

    // 方向を決める
    public void SetDirection(Vector2 dir)
    {
        if (dir.sqrMagnitude > 0.01f) //（ベクトルの長さの2乗）が 0.01 よりも大きいかどうかをチェック
        {
            direction = dir.normalized;　//ベクトルの向きはそのままで大きさを 1 にする
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg; //Mathf.Atan2(direction.y, direction.x) で direction の角度（ラジアン）を求め
                                                                                 // * Mathf.Rad2Deg でその値を度に変換 結果が、変数 angle に代入される
            transform.rotation = Quaternion.Euler(0, 0, angle); //指定した Euler 角（ここでは X:0, Y:0, Z:angle）をもとに、クォータニオン（回転情報）を生成
        }
        
    }

    // 実際に移動する処理
    public void MoveTo(Vector2Int tilePos)
    {
        currentPos = tilePos; //新しいタイルの座標を現在の位置に反映
        // タイル座標をワールド座標に変換して移動
        Vector3 worldPos = ConvertTileToWorldPos(tilePos);
        transform.position = worldPos;
    }

    private Vector3 ConvertTileToWorldPos(Vector2Int tilePos) //タイル座標をワールド座標に変換する処理を行うメソッド
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
            SetDirection(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetDirection(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetDirection(Vector2.right);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetDirection(Vector2.left);
        }
    }

}
