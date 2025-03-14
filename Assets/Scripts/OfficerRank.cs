using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OfficerRank //enum のみでスクリプトを作成する場合、using の宣言や、MonoBehaviour(モノビヘイビア) クラスの継承は不要。
                        //どのスクリプトからでも変数の代入なしで利用可能
{
    少尉,
    中尉,
    大尉,
    少佐,
    中佐,
    大佐,
    少将,
    中将,
    大将,
    元帥,
    国王
}