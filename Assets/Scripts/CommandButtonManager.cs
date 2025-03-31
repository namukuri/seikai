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
        
    }

    private void OnClickMoveBtn()
    {
        //ゲームフェイズをShowingMoveRangeに切り替える
        gameManager.ChangeCurrentGamePhase(GamePhase.ShowingMoveRange);

        //艦隊の移動範囲を表示する。
        
    }
}
