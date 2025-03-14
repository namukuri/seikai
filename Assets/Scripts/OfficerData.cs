using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //[System.Serializable]属性の追加。クラスの情報をインスペクターに表示することが出来、データをインスペクターから登録することが出来る。

public class OfficerData
{
    public int officerNo;
    public string OfficerName;
    public Sprite OfficerSprite;
    public string OfficerStatus;
    public OfficerRank officerRank;　//OfficerRank.csのenumに登録されている階級を使用
    public int meritValue;
    public int attackPower;
    public int defensePower;

}
