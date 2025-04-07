using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    // �O���b�h�̗L���͈�
    public Vector2Int gridMin = new Vector2Int(0, 0);
    public Vector2Int gridMax = new Vector2Int(10, 10);

    /// <summary>
    /// BFS��p���āAstart����target�܂ł̍ŒZ�o�H���v�Z����B
    /// movePower�i�ړ��\�^�C�����j�ȏ�̌o�H�͒T�������A�o�H��������Ȃ���� null ��Ԃ��B
    /// </summary>
    /// <param name="start">�J�n�^�C�����W</param>
    /// <param name="target">�ڕW�^�C�����W</param>
    /// <param name="movePower">WarShipData.movePower �ɑ�������ړ��\�^�C����</param>
    /// <returns>�J�n����ڕW�܂ł̃^�C�����W���X�g�i�擪���J�n�A�������ڕW�j</returns>
    public List<Vector2Int> FindPath(Vector2Int start, Vector2Int target, int movePower)
    {
        // BFS�p�̃L���[�F�^�C�����W�Ƃ��̎��_�ł̈ړ��R�X�g�i�����j
        Queue<(Vector2Int pos, int cost)> queue = new Queue<(Vector2Int, int)>();
        // �o�H�����p�̎����F����^�C���ɓ��B���钼�O�̃^�C�����L�^
        Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();
        // �K��ς݊Ǘ�
        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();

        queue.Enqueue((start, 0));
        visited.Add(start);

        // �אڃZ���̒T���i�㉺���E�j
        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(0, 1), // ��
            new Vector2Int(0, -1), // ��
            new Vector2Int(1, 0), // �E
            new Vector2Int(-1, 0) // ��
        };

        bool found = false;
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            Vector2Int pos = current.pos;
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
                 Vector2Int next = pos + d;
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
