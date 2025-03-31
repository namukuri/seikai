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

    // Start is called before the first frame update
    void Start()
    {
        // �Q�[���J�n���͔�\���ɂ��Ă���
        commandButtonsCanvasGroup.alpha = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        // UnitManager ���ێ����Ă���I�𒆂� WarShip �� null �łȂ���Ε\���Anull �Ȃ��\��
        if (unitManager != null && unitManager.selectWarShip != null)
        {
            commandButtonsCanvasGroup.alpha = 1f;
        }
        else
        {
            commandButtonsCanvasGroup.alpha = 0f;
        }
    }
}
