using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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
    private Vector3 offset = new Vector3(0.5f, 0.5f, 0);

    void Start()
    {
        // Rigidbody2D�R���|�[�l���g���擾
        //TryGetComponent<Rigidbody2D>(out rb);
    }

    // Update is called once per frame
    void Update()
    {
        if(EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        // ��ɃJ�[�\���̈ʒu���X�V����
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition + offset);
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
                if (tilemap.HasTile(cellPos)
                    && unitManager.selectWarShip != null
                    && unitManager.selectWarShip.mapManager.IsCellHiglighted(cellPos)) 
                {
                    var warship = unitManager.selectWarShip;

                    warship.MoveTo(cellPos, unitManager);
                    warship.mapManager.HideRoute();

                    //�ړ������������I��UI���o��
                    warship.EnableDirectionSelection();
                }

            }                                                
                        
        }
    }
}



                           