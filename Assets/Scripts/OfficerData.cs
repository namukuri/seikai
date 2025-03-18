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

    /// <summary>
    /// �R���X�g���N�^�ƌĂ΂�郁�\�b�h
    /// </summary>
    /// <param name="officerData">vagaOfficerDataSO.vagaOfficerDataList[playerIndex]</param>
    public OfficerData(OfficerData officerData) 
    {
        officerNo = officerData.officerNo;
        OfficerName = officerData.OfficerName;
        OfficerSprite = officerData.OfficerSprite;
        OfficerStatus = officerData.OfficerStatus;
        officerRank = officerData.officerRank;
        meritValue = officerData.meritValue;
        attackPower = officerData.attackPower;
        defensePower = officerData.defensePower;
    }
}
