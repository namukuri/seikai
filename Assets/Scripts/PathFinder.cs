using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathFinder : MonoBehaviour
{
    // グリッドの有効範囲
    public Vector3Int gridMin = new Vector3Int(0, 0, 0);
    public Vector3Int gridMax = new Vector3Int(10, 10, 0);
    public Tilemap tilemap; // Unity の Tilemap コンポーネント

    /// <summary>
    /// BFSを用いて、startからtargetまでの最短経路を計算する。
    /// movePower（移動可能タイル数）以上の経路は探索せず、経路が見つからなければ null を返す。
    /// </summary>
    /// <param name="start">開始タイル座標</param>
    /// <param name="target">目標タイル座標</param>
    /// <param name="movePower">WarShipData.movePower に相当する移動可能タイル数</param>
    /// <returns>開始から目標までのタイル座標リスト（先頭が開始、末尾が目標）</returns>
    public List<Vector3Int> FindPath(Vector3Int start, Vector3Int target, int movePower)
    {
        // BFS用のキュー：タイル座標とその時点での移動コスト（距離）
        Queue<(Vector3Int pos, int cost)> queue = new Queue<(Vector3Int pos, int cost)>();
        // 経路復元用の辞書：あるタイルに到達する直前のタイルを記録
        Dictionary<Vector3Int, Vector3Int> cameFrom = new Dictionary<Vector3Int, Vector3Int>();
        // 訪問済み管理
        HashSet<Vector3Int> visited = new HashSet<Vector3Int>();

        queue.Enqueue((start, 0));
        visited.Add(start);

        // 隣接セルの探索（上下左右）
        Vector3Int[] directions = new Vector3Int[]
        {
            new Vector3Int(0, 1, 0), // 上
            new Vector3Int(0, -1, 0), // 下
            new Vector3Int(1, 0, 0), // 右
            new Vector3Int(-1, 0, 0) // 左
        };

        bool found = false;
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            Vector3Int pos = current.pos;
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
                 Vector3Int next = pos + d;
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
         List<Vector3Int> path = new List<Vector3Int>();
         Vector3Int currentPos = target;
         path.Add(currentPos);
         while (currentPos != start)
         {
             currentPos = cameFrom[currentPos];
             path.Add(currentPos);
         }
         path.Reverse();
         return path;
       }
    
    public List<Vector3Int> CalculateMoveRange(Vector3Int startPosition, int maxMoveRange)
    {
        Debug.Log(startPosition);
        List<Vector3Int> moveRange = new List<Vector3Int>();

        // BFS を使って移動範囲を計算（例: 最大 maxMoveRange マス）
        Queue<Vector3Int> queue = new Queue<Vector3Int>();
        HashSet<Vector3Int> visited = new HashSet<Vector3Int>();

        queue.Enqueue(startPosition);
        visited.Add(startPosition);

        while (queue.Count > 0)
        {
            Vector3Int current = queue.Dequeue();

            // 上下左右の隣接タイルを確認
            foreach (Vector3Int direction in GetAdjacentDirections())
            {
                Vector3Int neighbor = current + direction;

                if (!visited.Contains(neighbor) && tilemap.HasTile(neighbor))
                {
                    int moveCost = GetTileMoveCost(neighbor); // タイルの移動コストを取得
                    if (GetManhattanDistance(startPosition, neighbor) <= maxMoveRange)
                    {
                        moveRange.Add(neighbor);
                        queue.Enqueue(neighbor);
                        visited.Add(neighbor);
                    }
                }
            }
        }

        return moveRange;
    }

    private List<Vector3Int> GetAdjacentDirections()
    {
        return new List<Vector3Int>
    {
        Vector3Int.up,
        Vector3Int.down,
        Vector3Int.left,
        Vector3Int.right
    };
    }

    private int GetTileMoveCost(Vector3Int position)
    {
        // タイルの移動コストを取得（デフォルト 1）
        // 必要に応じて TileBase やカスタムデータから取得
        return 1;
    }

    private int GetManhattanDistance(Vector3Int a, Vector3Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

}
