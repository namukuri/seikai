using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarShipController : MonoBehaviour
{
    public int movePower = 4; // �ړ���
    public Vector2Int currentPos; //2�����}�b�v�̃}�X���W�̌��݂̈ʒu
    public Vector2Int tempTargetPos; // �ꎞ�I�ɑI�񂾈ړ���
    public Vector2 direction; // �͂̌���
    public int warshipNo;
    [SerializeField]
    private WarshipData warshipData;

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
    public void MoveTo(Vector2Int tilePos)
    {
        currentPos = tilePos; //�V�����^�C���̍��W�����݂̈ʒu�ɔ��f
        // �^�C�����W�����[���h���W�ɕϊ����Ĉړ�
        Vector3 worldPos = ConvertTileToWorldPos(tilePos);
        transform.position = worldPos;
    }

    private Vector3 ConvertTileToWorldPos(Vector2Int tilePos) //�^�C�����W�����[���h���W�ɕϊ����鏈�����s�����\�b�h
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

}
