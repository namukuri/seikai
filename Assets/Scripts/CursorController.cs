using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps; //Tilemapを扱うために必要

[RequireComponent(typeof(Rigidbody2D))]

public class CursorController : MonoBehaviour
{
    [Header("タイルマップ参照")]
    public Tilemap tilemap; // マウスクリック先のセル座標を取得するために使う
                            //public float moveSpeed;
                            //private Rigidbody2D rb; // コンポーネントの取得用
                            //private float horizontal; // x 軸(水平・横)方向の入力の値の代入用
                            //private float vertical; // y 軸(垂直・縦)方向の入力の値の代入用

    // Start is called before the first frame update
    // UnitManager への参照
    public UnitManager unitManager;
    public GameManager gameManager;
    [SerializeField]
    public PlacementCommandPopUp placementcommandPopUp;
    public CommandButtonManager commandbuttonManager;
    void Start()
    {
        // Rigidbody2Dコンポーネントを取得
        //TryGetComponent<Rigidbody2D>(out rb);
    }

    // Update is called once per frame
    void Update()
    {
        // 常にカーソルの位置を更新する
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        Vector3Int cellPos = tilemap.WorldToCell(mouseWorldPos);
        transform.position = tilemap.GetCellCenterWorld(cellPos);

        // 現在のゲームフェイズをログ出力しておく
        Debug.Log("CurrentGamePhase: " + gameManager.currentGamePhase);
        Debug.Log("Selected WarShip: " + unitManager.selectWarShip);


        // フェイズに応じたクリック処理の分岐
        if (gameManager.currentGamePhase == GamePhase.MoveCurrsor)
        {
            // ユニット選択中の処理（例: マウスクリックで戦艦選択）
            if (Input.GetMouseButtonDown(0))
            {                            
                if (tilemap.HasTile(cellPos))
                {
                    bool isSelectWarship = unitManager.SelectWarShipAtCell(cellPos);
                    if (isSelectWarship)
                    {
                        if (unitManager.selectWarShip.isMoveEnd == true)
                        {
                            commandbuttonManager.HideMoveBtn();
                        }
                        else
                        {
                            commandbuttonManager.ShowMoveBtn();
                        }
                        gameManager.ChangeCurrentGamePhase(GamePhase.SelectingUnit);
                        placementcommandPopUp.ShowCommandButtons(); 
                    }
                }
            }
        }
        // 移動可能範囲表示中のフェイズ（ShowingMoveRange）の間、ユーザーがクリックしたら…
        else if (gameManager.currentGamePhase == GamePhase.ShowingMoveRange)
        {
            // 移動先を選択するフェイズ
            if (Input.GetMouseButtonDown(0))
            {
                // クリックされたセルがタイルとして存在しているか確認
                if (tilemap.HasTile(cellPos)) 
                {
                    if(unitManager != null && unitManager.selectWarShip != null)
                    {
                        if(unitManager.selectWarShip.mapManager.IsCellHiglighted(cellPos)) 
                        {
                            // ここで MapManager 側でハイライト済みセルを判定するメソッド IsCellHighlighted() が正しく実装されている前提
                            if (unitManager.selectWarShip.mapManager.IsCellHiglighted(cellPos))
                            {
                                // フェイズを SelectingMoveTile に変更
                                gameManager.ChangeCurrentGamePhase(GamePhase.SelectingMoveTile);

                                // 移動先として処理する
                                var warship = unitManager.selectWarShip;
                                warship.MoveTo(cellPos);
                                warship.mapManager.HideRoute();
                                //placementcommandPopUp.ShowCommandButtons();

                                // 次のフェイズへ、またはフェイズを戻す
                                gameManager.ChangeCurrentGamePhase(GamePhase.MoveCurrsor);
                            }
                        }                                                
                           
                        else
                        {
                            Debug.Log("このセルは移動可能範囲外です");
                        }
                    }
                }
            }
        }
    }
}



                           