using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UnitManager : MonoBehaviour
{
    public WarShipController selectWarShip;
    public List<WarShipController> warshipList = new();

    // タイルマップを参照する（艦隊がどのセルにいるかを判定するため）
    public Tilemap tilemap;

    /// <summary>
    /// 指定したセル座標に艦隊がいれば、その艦隊を selectWarShip に設定する
    /// </summary>
    /// <param name="cellPos">クリックなどで求めたセル座標</param>
    public bool SelectWarShipAtCell(Vector3Int cellPos)
    {
        // いったん選択を解除
        //selectWarShip = null;

        // warshipList に含まれるすべての艦隊をチェック
        foreach (var warship in warshipList)
        {
            // 艦隊の現在位置をタイル座標に変換
            Vector3Int warshipCellPos = tilemap.WorldToCell(warship.transform.position);

            // もしクリックしたセルと一致していれば、その艦隊を選択
            if (warshipCellPos == cellPos)
            {
                selectWarShip = warship;
                Debug.Log($"Warship {warship.name} を選択しました (セル座標: {cellPos})");
                return true; // 最初に見つかった艦隊だけ選択して終了
            }
        }
        return false;
    }

    // 必要に応じて Start / Update はそのまま
    void Start()
    {
    }

    void Update()
    {
    }
}
