using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CommandButtonManager : MonoBehaviour
{
    [SerializeField]
    private Button btnMove;
    [SerializeField]
    private Button btnEscape;
    public GameManager gameManager;
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
        //�Q�[���t�F�C�Y��ShowingMoveRange�ɐ؂�ւ���
        gameManager.ChangeCurrentGamePhase(GamePhase.ShowingMoveRange);

        //�͑��̈ړ��͈͂�\������B
        
    }

    private void OnClickEscapeBtn()
    {
        gameManager.ChangeCurrentGamePhase(GamePhase.MoveCurrsor);
    }
}
