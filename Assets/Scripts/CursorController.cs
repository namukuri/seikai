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
    void Start()
    {
        // Rigidbody2Dコンポーネントを取得
        //TryGetComponent<Rigidbody2D>(out rb);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.currentGamePhase != GamePhase.MoveCurrsor)
        {
            return;
        }
        // 左クリックを検知
        if(Input.GetMouseButtonDown(0))
        {
            //  1.クリックされたスクリーン座標をワールド座標に変換
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f; //2DなのでZは0にそろえる

            // 2.ワールド座標をタイルマップのセル座標に変換
            Vector3Int cellPos = tilemap.WorldToCell(mouseWorldPos);
            if (tilemap.HasTile(cellPos))
            {
                // 3. セル座標から、そのセルの中心のワールド座標を取得
                Vector3 targetPosition = tilemap.GetCellCenterWorld(cellPos);

                // 瞬間移動
                transform.position = targetPosition;
                // 4. UnitManager に「このセル上に艦隊がいれば選択する」処理を呼び出す
                if (unitManager != null)
                {
                    //カーソルを移動した地点に艦隊がいるかどうかをbool型で受け取る
                    bool isSelectWarship = unitManager.SelectWarShipAtCell(cellPos);

                    //艦隊がいた場合にはゲームフェイズを変更する。
                    if (isSelectWarship)
                    {
                        gameManager.ChangeCurrentGamePhase(GamePhase.SelectingUnit);
                    }
                }
            }
        }
            // InputManager の Horizontal に登録してあるキーが入力されたら、水平(横)方向の入力値として代入
            //horizontal = Input.GetAxis("Horizontal");
            // InputManager の Vertical に登録してあるキーが入力されたら、水平(横)方向の入力値として代入
            //vertical = Input.GetAxis("Vertical");
        }
    //private void FixedUpdate()
    //{
    // 移動
    //Move();
    //}
    /// <summary>
    /// 移動
    /// </summary>
    //private void Move()
    //{
    // 斜め移動の距離が増えないように正規化処理を行い、単位ベクトルとする(方向の情報は持ちつつ、距離による速度差をなくして一定値にする)
    //Vector3 dir = new Vector3(horizontal, vertical, 0).normalized;
    // velocity(速度)に新しい値を代入して、ゲームオブジェクトを移動させる
    //rb.velocity = dir * moveSpeed;

}
