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
    /// <summary>
    /// 移動ボタンを押した際など、外部から呼び出して
    /// 移動ボタン・戻るボタンを非表示にする
    /// </summary>
    public void HideCommandButtons()
    {
        commandButtonsCanvasGroup.alpha = 0f;
    }

    /// <summary>
    /// 移動処理完了後など、外部から呼び出して
    /// 移動ボタン・戻るボタンを表示する
    /// </summary>
    public void ShowCommandButtons()
    {
        commandButtonsCanvasGroup.alpha = 1f;
    }

    
}
