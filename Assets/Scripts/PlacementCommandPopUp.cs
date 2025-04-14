using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // CanvasGroup ���������߂ɕK�v

public class PlacementCommandPopUp : MonoBehaviour
{
    [Header("CommandButtons��CanvasGroup")]
    [SerializeField]
    private CanvasGroup commandButtonsCanvasGroup;

    [Header("UnitManager�ւ̎Q��")]
    [SerializeField]
    private UnitManager unitManager;

    private bool hideCommandButtons = false;

    // Start is called before the first frame update
    void Start()
    {
        // �Q�[���J�n���͔�\���ɂ��Ă���
        commandButtonsCanvasGroup.alpha = 0f;

    }

    // Update is called once per frame
    /// <summary>
    /// �ړ��{�^�����������ۂȂǁA�O������Ăяo����
    /// �ړ��{�^���E�߂�{�^�����\���ɂ���
    /// </summary>
    public void HideCommandButtons()
    {
        commandButtonsCanvasGroup.alpha = 0f;
    }

    /// <summary>
    /// �ړ�����������ȂǁA�O������Ăяo����
    /// �ړ��{�^���E�߂�{�^����\������
    /// </summary>
    public void ShowCommandButtons()
    {
        commandButtonsCanvasGroup.alpha = 1f;
    }

    
}
