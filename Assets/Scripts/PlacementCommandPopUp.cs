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

    private bool hideCommandButtons = false;

    // Start is called before the first frame update
    void Start()
    {
        // ゲーム開始時は非表示にしておく
        commandButtonsCanvasGroup.alpha = 0f;

    }

    // Update is called once per frame
    void Update()
    {
        if (!hideCommandButtons)
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
        else
        {
            // hideCommandButtons が true の場合は常に非表示にする
            commandButtonsCanvasGroup.alpha = 0f;
        }
    }
    /// <summary>
    /// 外部から呼び出して、移動ボタン等を非表示にする
    /// </summary>
    public void HideCommandButtons()
    {
        hideCommandButtons = true;
        commandButtonsCanvasGroup.alpha -= 0f;
    }
}
