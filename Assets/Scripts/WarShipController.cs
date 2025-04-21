using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public bool isMoveEnd;

    [Header("�����I��UI")]
    [SerializeField]
    private GameObject directionsButtonsObj; // Canvas��DirectionsButtons
    [SerializeField]
    private Button btnUp;
    [SerializeField]
    private Button btnDown;
    [SerializeField]
    private Button btnLeft;
    [SerializeField]
    private Button btnRight;

    public Vector3Int prewPos; //�ړ��O�̍��W

    [Header("�Q�[���Ǘ�")]
    [SerializeField]
    private�@GameManager gameManager; // �t�F�C�Y�؂�ւ��p


    void Start()
    {
        isMoveEnd = false;
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
        //�Q�[���J�n���� �����{�^�����B��
        directionsButtonsObj.SetActive(false);
        //�e�����{�^���������ꂽ�Ƃ��̃��X�i�[�o�^
        btnUp.onClick.AddListener(() => OnDirectionClicked(Vector3.up));
        btnRight.onClick.AddListener(() => OnDirectionClicked(Vector3.right));
        btnDown.onClick.AddListener(() => OnDirectionClicked(Vector3.down));
        btnLeft.onClick.AddListener(() => OnDirectionClicked(Vector3.left));
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

    // �ړ�������������ɌĂԃ��\�b�h
    public void EnableDirectionSelection()
    {
        // �t�F�C�Y�� SelectingDirection �ɐ؂�ւ�
        gameManager.ChangeCurrentGamePhase(GamePhase.SelectingDirection);
        // ���{�^����\��
        directionsButtonsObj.SetActive(true);
    }

    // �{�^�����������Ƃ��̏���
    private void OnDirectionClicked(Vector3 dir)
    {
        //��]����
        SetDirection(dir);

        //�����{�^�����B���āA�t�F�C�Y��߂�
        directionsButtonsObj.SetActive(false);
        gameManager.ChangeCurrentGamePhase(GamePhase.MoveCurrsor);
    }

    // ���������߂�
    public void SetDirection(Vector3 dir)
    {
        if (dir.sqrMagnitude > 0.01f) //�i�x�N�g���̒�����2��j�� 0.01 �����傫�����ǂ������`�F�b�N
        {
            direction = dir.normalized;�@//�x�N�g���̌����͂��̂܂܂ő傫���� 1 �ɂ���
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg
                -90f; //�E������0�x�ɂȂ��Ă���̂ŁA������ 90 �x����
            //Mathf.Atan2(direction.y, direction.x) �� direction �̊p�x�i���W�A���j������
            // * Mathf.Rad2Deg �ł��̒l��x�ɕϊ� ���ʂ��A�ϐ� angle �ɑ�������
            transform.rotation = Quaternion.Euler(0, 0, angle); //�w�肵�� Euler �p�i�����ł� X:0, Y:0, Z:angle�j�����ƂɁA�N�H�[�^�j�I���i��]���j�𐶐�
        }
        
    }

    // ���ۂɈړ����鏈��
    public void MoveTo(Vector3Int tilePos, UnitManager unitManager)
    {
        Vector3 tempPos = transform.position;
        Vector3Int gridPos = new Vector3Int((int)tempPos.x, (int)tempPos.y, 0);
        prewPos = gridPos;
        currentPos = tilePos; //�V�����^�C���̍��W�����݂̈ʒu�ɔ��f
        // �^�C���̒��S�̃��[���h���W���擾���A�����ֈړ�
        Vector3 centerPos = mapManager.tilemap.GetCellCenterWorld(tilePos);
        transform.position = centerPos;
        isMoveEnd = true;
        unitManager.selectWarShip = null;
    }

    public void MoveCancel(UnitManager unitManager)
    {
        transform.position = prewPos;
        isMoveEnd = false;
        unitManager.selectWarShip = null;
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
            SetDirection(Vector3.up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetDirection(Vector3.down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SetDirection(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SetDirection(Vector3.right);
        }
    }
    
    public Vector3Int GetWarshipPos()
    {
        return mapManager.tilemap.WorldToCell(transform.position);
    }
}
