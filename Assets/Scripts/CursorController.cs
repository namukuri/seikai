using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps; //Tilemap���������߂ɕK�v

[RequireComponent(typeof(Rigidbody2D))]

public class CursorController : MonoBehaviour
{
    [Header("�^�C���}�b�v�Q��")]
    public Tilemap tilemap; // �}�E�X�N���b�N��̃Z�����W���擾���邽�߂Ɏg��
                            //public float moveSpeed;
                            //private Rigidbody2D rb; // �R���|�[�l���g�̎擾�p
                            //private float horizontal; // x ��(�����E��)�����̓��͂̒l�̑���p
                            //private float vertical; // y ��(�����E�c)�����̓��͂̒l�̑���p

    // Start is called before the first frame update
    // UnitManager �ւ̎Q��
    public UnitManager unitManager;
    public GameManager gameManager;
    [SerializeField]
    public PlacementCommandPopUp placementcommandPopUp;
    public CommandButtonManager commandbuttonManager;
    void Start()
    {
        // Rigidbody2D�R���|�[�l���g���擾
        //TryGetComponent<Rigidbody2D>(out rb);
    }

    // Update is called once per frame
    void Update()
    {
        // ��ɃJ�[�\���̈ʒu���X�V����
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0f;
        Vector3Int cellPos = tilemap.WorldToCell(mouseWorldPos);
        transform.position = tilemap.GetCellCenterWorld(cellPos);

        // ���݂̃Q�[���t�F�C�Y�����O�o�͂��Ă���
        Debug.Log("CurrentGamePhase: " + gameManager.currentGamePhase);
        Debug.Log("Selected WarShip: " + unitManager.selectWarShip);


        // �t�F�C�Y�ɉ������N���b�N�����̕���
        if (gameManager.currentGamePhase == GamePhase.MoveCurrsor)
        {
            // ���j�b�g�I�𒆂̏����i��: �}�E�X�N���b�N�Ő�͑I���j
            if (Input.GetMouseButtonDown(0))
            {                            
                if (tilemap.HasTile(cellPos))
                {
                    bool isSelectWarship = unitManager.SelectWarShipAtCell(cellPos);
                    if (isSelectWarship)
                    {
                        if (unitManager.selectWarShip.isMoveEnd == true)
                        {
                            commandbuttonManager.HideMoveBtn();
                        }
                        else
                        {
                            commandbuttonManager.ShowMoveBtn();
                        }
                        gameManager.ChangeCurrentGamePhase(GamePhase.SelectingUnit);
                        placementcommandPopUp.ShowCommandButtons(); 
                    }
                }
            }
        }
        // �ړ��\�͈͕\�����̃t�F�C�Y�iShowingMoveRange�j�̊ԁA���[�U�[���N���b�N������c
        else if (gameManager.currentGamePhase == GamePhase.ShowingMoveRange)
        {
            // �ړ����I������t�F�C�Y
            if (Input.GetMouseButtonDown(0))
            {
                // �N���b�N���ꂽ�Z�����^�C���Ƃ��đ��݂��Ă��邩�m�F
                if (tilemap.HasTile(cellPos)) 
                {
                    if(unitManager != null && unitManager.selectWarShip != null)
                    {
                        if(unitManager.selectWarShip.mapManager.IsCellHiglighted(cellPos)) 
                        {
                            // ������ MapManager ���Ńn�C���C�g�ς݃Z���𔻒肷�郁�\�b�h IsCellHighlighted() ����������������Ă���O��
                            if (unitManager.selectWarShip.mapManager.IsCellHiglighted(cellPos))
                            {
                                // �t�F�C�Y�� SelectingMoveTile �ɕύX
                                gameManager.ChangeCurrentGamePhase(GamePhase.SelectingMoveTile);

                                // �ړ���Ƃ��ď�������
                                var warship = unitManager.selectWarShip;
                                warship.MoveTo(cellPos);
                                warship.mapManager.HideRoute();
                                //placementcommandPopUp.ShowCommandButtons();

                                // ���̃t�F�C�Y�ցA�܂��̓t�F�C�Y��߂�
                                gameManager.ChangeCurrentGamePhase(GamePhase.MoveCurrsor);
                            }
                        }                                                
                           
                        else
                        {
                            Debug.Log("���̃Z���͈ړ��\�͈͊O�ł�");
                        }
                    }
                }
            }
        }
    }
}



                           