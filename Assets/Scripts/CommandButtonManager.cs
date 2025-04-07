using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandButtonManager : MonoBehaviour
{
    [SerializeField]
    private Button btnMove;
    [SerializeField]
    private Button btnEscape;
    public GameManager gameManager;
    public UnitManager unitManager;    // UnitManager 参照
    public MapManager mapManager;      // MapManager 参照
    public PathFinder pathFinder;      // BFSなどの経路探索用
    // Start is called before the first frame update
    void Start()
    {
        // Move ボタンのクリック処理を登録
        btnMove.onClick.AddListener(OnClickMoveBtn);

        // Escape ボタンのクリック処理を登録
        btnEscape.onClick.AddListener(OnClickEscapeBtn);

    }

    private void OnClickMoveBtn()
    {
        Debug.Log("MoveButton");
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
        List<Vector3Int> moveRange = pathFinder.CalculateMoveRange
            ( 
            startPos,
            warship.warshipData.movePower
            );

        // タイルマップ上で移動可能範囲をハイライト
        mapManager.ShowRoute(moveRange);

        //ゲームフェイズをShowingMoveRangeに切り替える
        gameManager.ChangeCurrentGamePhase(GamePhase.ShowingMoveRange);
                       
    }

    private void OnClickEscapeBtn()
    {
        gameManager.ChangeCurrentGamePhase(GamePhase.MoveCurrsor);
    }


}
