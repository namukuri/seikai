using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficerRankUpDater : MonoBehaviour
{
    // V_OfficerDataSO��Inspector����A�T�C���ł���悤�ɂ���
    public V_OfficerDataSO officerDataSO;

    // meritValue�ɉ����������N���X�V���郁�\�b�h
    public void UpdateOfficerRanks() //V_OfficerDataList��meritValue�ɉ����ĊK����ύX����
    {
        if (officerDataSO == null) //officerDataSO���Ȃ����
        {
            Debug.LogWarning("OfficerDataSO���A�T�C������Ă��܂���");�@// Debug.LogWarning�Ōx�����̏o�́@Debug.Log�ł��悢�H
            return; //�ȉ��̏��������s�����I������B
        }

         foreach (V_OfficerData officer in officerDataSO.v_OfficerDataList)�@//foreach�őS�v�f�������B���̂���officerDataSO�͒�`���ꂽV_OfficerDataSO�^�̕ϐ��ł��邱�Ƃ��m�F�B���ނ̂Ƃ���for���ŏ����̂Ȃ�ȉ��̒ʂ�ɂȂ�
            //for(int i = 0; i < officerDataSO.v_OfficerDataList.Cout; i++)
        {
            //V_OfficerData officer = officerDataSO.v_OfficerDataList[i];
            if (officer.meritValue >= 12800) //����v_OfficerDataList�ł���ꂽV_OfficerData�̕ϐ�meritValue��12800�ȏ�Ȃ�
            {
                officer.officerRank = OfficerRank.����;�@//v_OfficerDataList�ł���ꂽ�ϐ�officerRank��OfficerRnak�ō�����ʂ�̌����ɂ��Ȃ����B���ȉ���
            }
            else if (officer.meritValue >= 6400)
            {
                officer.officerRank = OfficerRank.�叫;
            }
            else if (officer.meritValue >= 3200)
            {
                officer.officerRank = OfficerRank.����;
            }
            else if (officer.meritValue >= 1600)
            {
                officer.officerRank = OfficerRank.����;
            }
            else if (officer.meritValue >= 800)
            {
                officer.officerRank = OfficerRank.�卲;
            }
            else if (officer.meritValue >= 400)
            {
                officer.officerRank = OfficerRank.����;
            }
            else if (officer.meritValue >= 200)
            {
                officer.officerRank = OfficerRank.����;
            }
            else if (officer.meritValue >= 100)
            {
                officer.officerRank = OfficerRank.���;
            }
            else if (officer.meritValue >= 50)
            {
                officer.officerRank = OfficerRank.����;
            }
            else
            {
                // 50�����̏ꍇ�͏�����ԂƂ��ď��тɐݒ�
                officer.officerRank = OfficerRank.����;
            }
        }

        Debug.Log("�S���Z�̃����N���X�V���܂���");
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateOfficerRanks();
    }

    
}
