using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    // グリッドの有効範囲
    public Vector2Int gridMin = new Vector2Int(0, 0);
    public Vector2Int gridMax = new Vector2Int(10, 10);

    /// <summary>
    /// BFSを用いて、startからtargetまでの最短経路を計算する。
    /// movePower（移動可能タイル数）以上の経路は探索せず、経路が見つからなければ null を返す。
    /// </summary>
    /// <param name="start">開始タイル座標</param>
    /// <param name="target">目標タイル座標</param>
    /// <param name="movePower">WarShipData.movePower に相当する移動可能タイル数</param>
    /// <returns>開始から目標までのタイル座標リスト（先頭が開始、末尾が目標）</returns>
    public List<Vector2Int> FindPath(Vector2Int start, Vector2Int target, int movePower)
    {
        // BFS用のキュー：タイル座標とその時点での移動コスト（距離）
        Queue<(Vector2Int pos, int cost)> queue = new Queue<(Vector2Int, int)>();
        // 経路復元用の辞書：あるタイルに到達する直前のタイルを記録
        Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();
        // 訪問済み管理
        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();

        queue.Enqueue((start, 0));
        visited.Add(start);

        // 隣接セルの探索（上下左右）
        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(0, 1), // 上
            new Vector2Int(0, -1), // 下
            new Vector2Int(1, 0), // 右
            new Vector2Int(-1, 0) // 左
        };

        bool found = false;
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            Vector2Int pos = current.pos;
            int cost = current.cost;

             // 目標に到達した場合
             if(pos == target)
             {
                found = true;
                break;
             }

             // 移動可能距離を超えていれば探索を打ち切る
             if (cost >= movePower)
                 continue;

             // 隣接セルを探索
             foreach (var d in directions)
             {
                 Vector2Int next = pos + d;
                 // グリッド範囲外は除外
                 if (next.x < gridMin.x || next.x > gridMax.x || next.y < gridMin.y || next.y > gridMax.y)
                     continue;
                 if (!visited.Contains(next))
                 {    
                     visited.Add(next);
                     queue.Enqueue((next, cost + 1));
                     cameFrom[next] = pos;
                  }
             }
         }
            
         // 目標に到達できなかった場合
         if (!found)
             return null;

         // 経路復元：targetからstartまで辿ってリストを作成し、逆順にして返す
         List<Vector2Int> path = new List<Vector2Int>();
         Vector2Int currentPos = target;
         path.Add(currentPos);
         while (currentPos != start)
         {
             currentPos = cameFrom[currentPos];
             path.Add(currentPos);
         }
         path.Reverse();
         return path;
       }
}
