using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficerRankUpDater : MonoBehaviour
{
    // OfficerDataSO��Inspector����A�T�C���ł���悤�ɂ���
    public VagaOfficerDataSO vagaofficerDataSO;

    // meritValue�ɉ����������N���X�V���郁�\�b�h
    public void UpdateOfficerRanks() //OfficerDataList��meritValue�ɉ����ĊK����ύX����
    {
        if (vagaofficerDataSO == null) //officerDataSO���Ȃ����
        {
            Debug.LogWarning("vagaOfficerDataSO���A�T�C������Ă��܂���");�@// Debug.LogWarning�Ōx�����̏o�́@Debug.Log�ł��悢�H
            return; //�ȉ��̏��������s�����I������B
        }

         foreach (OfficerData officer in vagaofficerDataSO.vagaOfficerDataList)�@//foreach�őS�v�f�������B���̂���vagaofficerDataSO�͒�`���ꂽOfficerDataSO�^�̕ϐ��ł��邱�Ƃ��m�F�B���ނ̂Ƃ���for���ŏ����̂Ȃ�ȉ��̒ʂ�ɂȂ�
            //for(int i = 0; i < vagaofficerDataSO.vagaOfficerDataList.Cout; i++)
        {
            //OfficerData officer = vagaofficerDataSO.vagaOfficerDataList[i];
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
