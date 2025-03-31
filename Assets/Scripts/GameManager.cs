using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GamePhase currentGamePhase = GamePhase.MoveCurrsor;
    // Start is called before the first frame update
    void Start()
    {

    }
    public void ChangeCurrentGamePhase(GamePhase nextGamePhase)
    {
        currentGamePhase = nextGamePhase;
    }
}
