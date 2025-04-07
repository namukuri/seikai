using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathFinder : MonoBehaviour
{
    // �O���b�h�̗L���͈�
    public Vector3Int gridMin = new Vector3Int(0, 0, 0);
    public Vector3Int gridMax = new Vector3Int(10, 10, 0);
    public Tilemap tilemap; // Unity �� Tilemap �R���|�[�l���g

    /// <summary>
    /// BFS��p���āAstart����target�܂ł̍ŒZ�o�H���v�Z����B
    /// movePower�i�ړ��\�^�C�����j�ȏ�̌o�H�͒T�������A�o�H��������Ȃ���� null ��Ԃ��B
    /// </summary>
    /// <param name="start">�J�n�^�C�����W</param>
    /// <param name="target">�ڕW�^�C�����W</param>
    /// <param name="movePower">WarShipData.movePower �ɑ�������ړ��\�^�C����</param>
    /// <returns>�J�n����ڕW�܂ł̃^�C�����W���X�g�i�擪���J�n�A�������ڕW�j</returns>
    public List<Vector3Int> FindPath(Vector3Int start, Vector3Int target, int movePower)
    {
        // BFS�p�̃L���[�F�^�C�����W�Ƃ��̎��_�ł̈ړ��R�X�g�i�����j
        Queue<(Vector3Int pos, int cost)> queue = new Queue<(Vector3Int pos, int cost)>();
        // �o�H�����p�̎����F����^�C���ɓ��B���钼�O�̃^�C�����L�^
        Dictionary<Vector3Int, Vector3Int> cameFrom = new Dictionary<Vector3Int, Vector3Int>();
        // �K��ς݊Ǘ�
        HashSet<Vector3Int> visited = new HashSet<Vector3Int>();

        queue.Enqueue((start, 0));
        visited.Add(start);

        // �אڃZ���̒T���i�㉺���E�j
        Vector3Int[] directions = new Vector3Int[]
        {
            new Vector3Int(0, 1, 0), // ��
            new Vector3Int(0, -1, 0), // ��
            new Vector3Int(1, 0, 0), // �E
            new Vector3Int(-1, 0, 0) // ��
        };

        bool found = false;
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            Vector3Int pos = current.pos;
            int cost = current.cost;

             // �ڕW�ɓ��B�����ꍇ
             if(pos == target)
             {
                found = true;
                break;
             }

             // �ړ��\�����𒴂��Ă���ΒT����ł��؂�
             if (cost >= movePower)
                 continue;

             // �אڃZ����T��
             foreach (var d in directions)
             {
                 Vector3Int next = pos + d;
                 // �O���b�h�͈͊O�͏��O
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
            
         // �ڕW�ɓ��B�ł��Ȃ������ꍇ
         if (!found)
             return null;

         // �o�H�����Ftarget����start�܂ŒH���ă��X�g���쐬���A�t���ɂ��ĕԂ�
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

        // BFS ���g���Ĉړ��͈͂��v�Z�i��: �ő� maxMoveRange �}�X�j
        Queue<Vector3Int> queue = new Queue<Vector3Int>();
        HashSet<Vector3Int> visited = new HashSet<Vector3Int>();

        queue.Enqueue(startPosition);
        visited.Add(startPosition);

        while (queue.Count > 0)
        {
            Vector3Int current = queue.Dequeue();

            // �㉺���E�̗אڃ^�C�����m�F
            foreach (Vector3Int direction in GetAdjacentDirections())
            {
                Vector3Int neighbor = current + direction;

                if (!visited.Contains(neighbor) && tilemap.HasTile(neighbor))
                {
                    int moveCost = GetTileMoveCost(neighbor); // �^�C���̈ړ��R�X�g���擾
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
        // �^�C���̈ړ��R�X�g���擾�i�f�t�H���g 1�j
        // �K�v�ɉ����� TileBase ��J�X�^���f�[�^����擾
        return 1;
    }

    private int GetManhattanDistance(Vector3Int a, Vector3Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

}
