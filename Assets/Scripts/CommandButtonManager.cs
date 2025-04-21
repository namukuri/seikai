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
        Debug.Log("unitmanager" + unitManager);
        Debug.Log("selectWarShip" +unitManager.selectWarShip);
        unitManager.selectWarShip.MoveCancel(unitManager);
    }

    public void ShowMoveBtn()
    {
        btnMove.interactable = true;
    }
    public void HideMoveBtn()
    {
        btnMove.interactable = false;
    }
}
