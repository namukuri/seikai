using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GamePhase
{
   MoveCurrsor, //ゲームが始まったときはこの状態
   SelectingUnit, // ユニット選択中
   ShowingMoveRange, // 移動可能範囲表示中
   SelectingMoveTile, // 移動先タイル選択中
   SelectingDirection // 進行方向選択中
}
