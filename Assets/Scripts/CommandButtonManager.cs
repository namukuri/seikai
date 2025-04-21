using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandButtonManager : MonoBehaviour
{
    [SerializeField]
    private Button btnMove;
    [SerializeField]
    private Button btnEscape, btnCancel;
    public GameManager gameManager;
    public UnitManager unitManager;    // UnitManager �Q��
    public MapManager mapManager;      // MapManager �Q��
    public PathFinder pathFinder;      // BFS�Ȃǂ̌o�H�T���p
    public PlacementCommandPopUp placementCommandPopUp;

    // Start is called before the first frame update
    void Start()
    {
        // Move �{�^���̃N���b�N������o�^
        btnMove.onClick.AddListener(OnClickMoveBtn);

        // Escape �{�^���̃N���b�N������o�^
        btnEscape.onClick.AddListener(OnClickEscapeBtn);

        btnCancel.onClick.AddListener(OnClickCancelBtn);
        // �ŏ���Cancel���B���Ă���
        HideCancelBtn();

    }

    /// <summary>Cancel�{�^�����B���ăN���b�N�s�ɂ���</summary>
    public void HideCancelBtn()
    {
        btnCancel.gameObject.SetActive(false);
        btnCancel.interactable = false;
    }

    /// <summary>Cancel�{�^�����ĕ\�����ăN���b�N�\�ɂ���</summary>
    public void ShowCancelBtn()
    {
        btnCancel.gameObject.SetActive(true);
        btnCancel.interactable = true;
    }

    private void OnClickMoveBtn()
    {
        Debug.Log("MoveButton clicked");
        // ���ݑI�𒆂� WarShip ���擾
        var warship = unitManager.selectWarShip;
        if (warship == null)
        {
            Debug.LogWarning("�͂��I������Ă��܂���");
            return;
        }

        Vector3Int startPos = warship.GetWarshipPos();
        //warship.OnTargetTileSelected
        // �ړ��\�͈͂� BFS �ȂǂŌv�Z
        //   �����ł́uwarship.currentPos ���� warship.warshipData.movePower �^�C�����v
        List<Vector3Int> moveRange = pathFinder.CalculateRoute
            (
            startPos,
            warship.warshipData.movePower
            );

        // �^�C���}�b�v��ňړ��\�͈͂��n�C���C�g
        mapManager.ShowRoute(moveRange);

        placementCommandPopUp.HideCommandButtons();

        //�Q�[���t�F�C�Y��ShowingMoveRange�ɐ؂�ւ���
        gameManager.ChangeCurrentGamePhase(GamePhase.ShowingMoveRange);

        // �{�^�������������̂ŁA�I�𒆂̊͂� null �ɂ��Ă���
        //unitManager.selectWarShip = null;

    }

    private void OnClickEscapeBtn()
    {
        gameManager.ChangeCurrentGamePhase(GamePhase.MoveCurrsor);
        // �{�^�������������̂ŁA�I�𒆂̊͂� null �ɂ��Ă���
        unitManager.selectWarShip = null;
    }

    private void OnClickCancelBtn()
    {
        Debug.Log("Cancel clicked");
        Debug.Log("unitmanager" + unitManager);
        Debug.Log("selectWarShip" + unitManager.selectWarShip);

        // WarShipController �����o��
        var warship = unitManager.selectWarShip;
        if (warship == null)
        {
            return;
        }

        // �u�ړ�������߂����Ƃ̃L�����Z���v���E��
        if (warship.isMoveEnd)
        {
            //���̃}�X�ɖ߂�
            warship.MoveCancel(unitManager);
            //�������UI���o�Ă���ΉB��
            warship.DisableDirectionSelection();
            // �R�j�R�}���hUI���ĕ\��
            placementCommandPopUp.ShowCommandButtons();
            ShowMoveBtn();
            // �S�jCancel�͉B��
            HideCancelBtn();
            //�t�F�C�Y���R�}���h�I���iSelectingUnit�j�ɖ߂�
            gameManager.ChangeCurrentGamePhase(GamePhase.SelectingUnit);
        }

            // �u���ܕ����I�𒆁v���ǂ����ŕ���
            if (gameManager.currentGamePhase == GamePhase.SelectingDirection)
        {
            //�ړ��Ɩ��UI��������
            warship.MoveCancel(unitManager); //// ���̃}�X�ɖ߂�
            warship.DisableDirectionSelection(); //���UI���B��

            //�R�}���h�{�^���i�ړ��^�߂�j���ĕ\��
            placementCommandPopUp.ShowCommandButtons();
            ShowMoveBtn(); // �ړ��{�^�����o��
            //ShowEscapeBtn();

            // Cancel �͉B��
            HideCancelBtn();

            //�t�F�C�Y�����ɖ߂�
            gameManager.ChangeCurrentGamePhase(GamePhase.SelectingUnit);
        }
        else
        {
            // �i�������̃t�F�C�Y�� Cancel ���삪����Ȃ炱���ɏ����j
        }
    }

    public void ShowMoveBtn()
    {
        btnMove.interactable = true;
        btnMove.gameObject.SetActive(true);
    }
    public void HideMoveBtn()
    {
        btnMove.interactable = false;
        btnMove.gameObject.SetActive(false);
    }
}

    