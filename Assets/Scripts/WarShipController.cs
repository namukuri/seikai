using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarShipController : MonoBehaviour
{
    //public int movePower = 4; // �ړ���
    public Vector3Int currentPos; //2�����}�b�v�̃}�X���W�̌��݂̈ʒu
    public Vector3Int tempTargetPos; // �ꎞ�I�ɑI�񂾈ړ���
    public Vector3 direction; // �͂̌���
    public int warshipNo;
    [SerializeField]
    public WarshipData warshipData;

    // PathFinder �ւ̎Q�Ɓi�C���X�y�N�^�[�Őݒ�j
    public PathFinder pathFinder;
    // �o�H�̃n�C���C�g�\�����s�� MapManager �ւ̎Q��
    public MapManager mapManager;

    void Start()
    {
        // DataBaseManager ����w�肵�� warshipNo �ɍ��v���� WarshipData ���擾
        warshipData = DataBaseManager.instance.warshipDataSO.warshipDataList.Find(data => data.warshipNo == warshipNo);
        if (warshipData == null)
        {
            Debug.LogError("WarShipData�� warshipNo:" + warshipNo + " �Ō�����܂���");
        }
        else
        {
            // �K�v�ł���΁AwarshipData �̏��𗘗p���ď������Ȃǂ��s��
            Debug.Log("WarShipData���擾: " + warshipData.warshipName);
        }
    }

    // ���[�U���͂�R�}���h�� tempTargetPos ���X�V������ɌĂяo���o�H�v�Z���\�b�h
    public void OnTargetTileSelected(Vector3Int targetTile)
    {
        tempTargetPos = targetTile;
        // BFS��p���Čo�H���v�Z����BwarshipData.movePower ������Ƃ���
        List<Vector3Int> route = CalculateRoute();
        if (route == null)
        {
            Debug.Log("�ڕW�^�C�� " + tempTargetPos + " �͈ړ��\�͈͊O�ł�");
        }
        else
        {
            Debug.Log("�o�H��������܂���: " + string.Join(" -> ", route));
            // �o�H���n�C���C�g�\��
            mapManager.ShowRoute(route);
        }
    }

    // BFS�Ōo�H���v�Z����
    public List<Vector3Int> CalculateRoute()
    {
        if (warshipData == null)
        {
            Debug.LogError("WarShipData���ݒ肳��Ă��܂���");
            return null;
        }
        if (pathFinder == null)
        {
            Debug.LogError("PathFinder���Q�Ƃ���Ă��܂���");
            return null;
        }
        // warshipData.movePower ������Ƃ��Čo�H���擾
        List<Vector3Int> route = pathFinder.FindPath(currentPos, tempTargetPos, warshipData.movePower);
        return route;
    }

    // ���������߂�
    public void SetDirection(Vector2 dir)
    {
        if (dir.sqrMagnitude > 0.01f) //�i�x�N�g���̒�����2��j�� 0.01 �����傫�����ǂ������`�F�b�N
        {
            direction = dir.normalized;�@//�x�N�g���̌����͂��̂܂܂ő傫���� 1 �ɂ���
            float angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg; //Mathf.Atan2(direction.y, direction.x) �� direction �̊p�x�i���W�A���j������
                                                                                 // * Mathf.Rad2Deg �ł��̒l��x�ɕϊ� ���ʂ��A�ϐ� angle �ɑ�������
            transform.rotation = Quaternion.Euler(0, 0, angle); //�w�肵�� Euler �p�i�����ł� X:0, Y:0, Z:angle�j�����ƂɁA�N�H�[�^�j�I���i��]���j�𐶐�
        }
        
    }

    // ���ۂɈړ����鏈��
    public void MoveTo(Vector3Int tilePos)
    {
        currentPos = tilePos; //�V�����^�C���̍��W�����݂̈ʒu�ɔ��f
        // �^�C���̒��S�̃��[���h���W���擾���A�����ֈړ�
        Vector3 centerPos = mapManager.tilemap.GetCellCenterWorld(tilePos);
        transform.position = centerPos;
    }

    private Vector3 ConvertTileToWorldPos(Vector3Int tilePos) //�^�C�����W�����[���h���W�ɕϊ����鏈�����s�����\�b�h
    {
        // �^�C���T�C�Y�Ȃǂ��l�����ă��[���h���W���Z�o
        float x = tilePos.x;
        float y = tilePos.y;
        return new Vector3(x, y, 0);
    }

        void Update()
    {
        // �L�[�{�[�h�̏㉺���E�̓��͂ɉ����ĉ�]������
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetDirection(Vector2.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetDirection(Vector2.down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetDirection(Vector2.right);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetDirection(Vector2.left);
        }
    }
    
    public Vector3Int GetWarshipPos()
    {
        return mapManager.tilemap.WorldToCell(transform.position);
    }
}
