using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //[System.Serializable]�����̒ǉ��B�N���X�̏����C���X�y�N�^�[�ɕ\�����邱�Ƃ��o���A�f�[�^���C���X�y�N�^�[����o�^���邱�Ƃ��o����B

public class OfficerData
{
    public int officerNo;
    public string OfficerName;
    public Sprite OfficerSprite;
    public string OfficerStatus;
    public OfficerRank officerRank;�@//OfficerRank.cs��enum�ɓo�^����Ă���K�����g�p
    public int meritValue;
    public int attackPower;
    public int defensePower;

}
