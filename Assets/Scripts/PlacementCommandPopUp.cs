using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // CanvasGroup を扱うために必要

public class PlacementCommandPopUp : MonoBehaviour
{
    [Header("CommandButtonsのCanvasGroup")]
    [SerializeField]
    private CanvasGroup commandButtonsCanvasGroup;

    [Header("UnitManagerへの参照")]
    [SerializeField]
    private UnitManager unitManager;

    // Start is called before the first frame update
    void Start()
    {
        // ゲーム開始時は非表示にしておく
        commandButtonsCanvasGroup.alpha = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        // UnitManager が保持している選択中の WarShip が null でなければ表示、null なら非表示
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
