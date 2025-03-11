using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficerRankUpDater : MonoBehaviour
{
    // V_OfficerDataSO��Inspector����A�T�C���ł���悤�ɂ���
    public V_OfficerDataSO officerDataSO;

    // meritValue�ɉ����������N���X�V���郁�\�b�h
    public void UpdateOfficerRanks()
    {
        if (officerDataSO == null)
        {
            Debug.LogWarning("OfficerDataSO���A�T�C������Ă��܂���");
            return;
        }

         foreach (V_OfficerData officer in officerDataSO.v_OfficerDataList)
        {
            if (officer.meritValue >= 12800)
            {
                officer.officerRank = OfficerRank.����;
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
