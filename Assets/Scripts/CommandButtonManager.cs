using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandButtonManager : MonoBehaviour
{
    [SerializeField]
    private Button btnMove;
    [SerializeField]
    private Button btnEscape;
    public GameManager gameManager;
    public UnitManager unitManager;    // UnitManager �Q��
    public MapManager mapManager;      // MapManager �Q��
    public PathFinder pathFinder;      // BFS�Ȃǂ̌o�H�T���p
    // Start is called before the first frame update
    void Start()
    {
        // Move �{�^���̃N���b�N������o�^
        btnMove.onClick.AddListener(OnClickMoveBtn);

        // Escape �{�^���̃N���b�N������o�^
        btnEscape.onClick.AddListener(OnClickEscapeBtn);

    }

    private void OnClickMoveBtn()
    {
        Debug.Log("MoveButton");
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
        List<Vector3Int> moveRange = pathFinder.CalculateMoveRange
            ( 
            startPos,
            warship.warshipData.movePower
            );

        // �^�C���}�b�v��ňړ��\�͈͂��n�C���C�g
        mapManager.ShowRoute(moveRange);

        //�Q�[���t�F�C�Y��ShowingMoveRange�ɐ؂�ւ���
        gameManager.ChangeCurrentGamePhase(GamePhase.ShowingMoveRange);
                       
    }

    private void OnClickEscapeBtn()
    {
        gameManager.ChangeCurrentGamePhase(GamePhase.MoveCurrsor);
    }


}
