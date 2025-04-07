using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapManager : MonoBehaviour
{
    // タイルマップの参照
    public Tilemap tilemap;

    // 現在ハイライト中のセル座標を記録しておくリスト
    private List<Vector3Int> highlightedCells = new List<Vector3Int>();

    /// <summary>
    /// 経路（タイル座標のリスト）に沿って各セルの色を青半透明に変更してハイライト表示する。
    /// </summary>
    /// <param name="route">経路のタイル座標（Vector2Int のリスト）</param>
    public void ShowRoute(List<Vector3Int> route)
    {
        Debug.Log("ルートの数" +  route.Count);
        // 既にハイライト中のセルがあればリセットする
        HideRoute();

        // 経路上の各タイルをハイライト
        foreach (Vector3Int tile in route)
        {
            // タイルマップAPIは Vector3Int を使用するので変換（z=0）
            Vector3Int cellPos = new Vector3Int(tile.x, tile.y, 0);
            // そのセルにタイルが存在するか確認
            if (tilemap.HasTile(cellPos))
            {
                // ハイライト用の色：青（RGB:0,0,1）に半透明（アルファ0.5）
                Color highlightColor = new Color(0f, 0f, 1f, 0.5f);
                tilemap.SetTileFlags(cellPos, TileFlags.None);
                tilemap.SetColor(cellPos, highlightColor);
                // 後で元に戻すため、ハイライト済みセルとして記録
                highlightedCells.Add(cellPos);
            }
        }
    }

    /// <summary>
    /// 以前にハイライトしたセルの色をリセット（デフォルトは白）する
    /// </summary>
    public void HideRoute()
    {
        foreach (Vector3Int cellPos in highlightedCells)
        {
            tilemap.SetColor(cellPos, Color.white);
        }
        highlightedCells.Clear();
    }

}
