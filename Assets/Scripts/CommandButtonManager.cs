using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandButtonManager : MonoBehaviour
{
    [SerializeField]
    private Button btnMove;
    [SerializeField]
    private Button btnEscape, btnCancel;
    public GameManager gameManager;
    public UnitManager unitManager;    // UnitManager 参照
    public MapManager mapManager;      // MapManager 参照
    public PathFinder pathFinder;      // BFSなどの経路探索用
    public PlacementCommandPopUp placementCommandPopUp;

    // Start is called before the first frame update
    void Start()
    {
        // Move ボタンのクリック処理を登録
        btnMove.onClick.AddListener(OnClickMoveBtn);

        // Escape ボタンのクリック処理を登録
        btnEscape.onClick.AddListener(OnClickEscapeBtn);

        btnCancel.onClick.AddListener(OnClickCancelBtn);
        // 最初はCancelを隠しておく
        HideCancelBtn();

    }

    /// <summary>Cancelボタンを隠してクリック不可にする</summary>
    public void HideCancelBtn()
    {
        btnCancel.gameObject.SetActive(false);
        btnCancel.interactable = false;
    }

    /// <summary>Cancelボタンを再表示してクリック可能にする</summary>
    public void ShowCancelBtn()
    {
        btnCancel.gameObject.SetActive(true);
        btnCancel.interactable = true;
    }

    private void OnClickMoveBtn()
    {
        Debug.Log("MoveButton clicked");
        // 現在選択中の WarShip を取得
        var warship = unitManager.selectWarShip;
        if (warship == null)
        {
            Debug.LogWarning("艦が選択されていません");
            return;
        }

        Vector3Int startPos = warship.GetWarshipPos();
        //warship.OnTargetTileSelected
        // 移動可能範囲を BFS などで計算
        //   ここでは「warship.currentPos から warship.warshipData.movePower タイル分」
        List<Vector3Int> moveRange = pathFinder.CalculateRoute
            (
            startPos,
            warship.warshipData.movePower
            );

        // タイルマップ上で移動可能範囲をハイライト
        mapManager.ShowRoute(moveRange);

        placementCommandPopUp.HideCommandButtons();

        //ゲームフェイズをShowingMoveRangeに切り替える
        gameManager.ChangeCurrentGamePhase(GamePhase.ShowingMoveRange);

        // ボタンを消したいので、選択中の艦を null にしておく
        //unitManager.selectWarShip = null;

    }

    private void OnClickEscapeBtn()
    {
        gameManager.ChangeCurrentGamePhase(GamePhase.MoveCurrsor);
        // ボタンを消したいので、選択中の艦を null にしておく
        unitManager.selectWarShip = null;
    }

    private void OnClickCancelBtn()
    {
        Debug.Log("Cancel clicked");
        Debug.Log("unitmanager" + unitManager);
        Debug.Log("selectWarShip" + unitManager.selectWarShip);

        // WarShipController を取り出す
        var warship = unitManager.selectWarShip;
        if (warship == null)
        {
            return;
        }

        // 「移動先を決めたあとのキャンセル」も拾う
        if (warship.isMoveEnd)
        {
            //元のマスに戻す
            warship.MoveCancel(unitManager);
            //もし矢印UIが出ていれば隠す
            warship.DisableDirectionSelection();
            // ３）コマンドUIを再表示
            placementCommandPopUp.ShowCommandButtons();
            ShowMoveBtn();
            // ４）Cancelは隠す
            HideCancelBtn();
            //フェイズをコマンド選択（SelectingUnit）に戻す
            gameManager.ChangeCurrentGamePhase(GamePhase.SelectingUnit);
        }

            // 「いま方向選択中」かどうかで分岐
            if (gameManager.currentGamePhase == GamePhase.SelectingDirection)
        {
            //移動と矢印UIを取り消す
            warship.MoveCancel(unitManager); //// 元のマスに戻す
            warship.DisableDirectionSelection(); //矢印UIを隠す

            //コマンドボタン（移動／戻る）を再表示
            placementCommandPopUp.ShowCommandButtons();
            ShowMoveBtn(); // 移動ボタンを出す
            //ShowEscapeBtn();

            // Cancel は隠す
            HideCancelBtn();

            //フェイズを元に戻す
            gameManager.ChangeCurrentGamePhase(GamePhase.SelectingUnit);
        }
        else
        {
            // （もし他のフェイズで Cancel 動作があるならここに書く）
        }
    }

    public void ShowMoveBtn()
    {
        btnMove.interactable = true;
        btnMove.gameObject.SetActive(true);
    }
    public void HideMoveBtn()
    {
        btnMove.interactable = false;
        btnMove.gameObject.SetActive(false);
    }
}

    