using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VagaOfficerDataSO", menuName = "Create VagaOfficerDataSO")]�@//CreateAssetMenu �����̋@�\�BVagaOfficerDataSO �X�N���v�^�u���E�I�u�W�F�N�g���쐬


public class VagaOfficerDataSO : ScriptableObject //ScriptableObject�Ƃ���VagaOfficerDataSO���쐬
{
    public List<OfficerData> vagaOfficerDataList = new List<OfficerData>(); //List �^�ɂ��āA�P�̕ϐ����ɕ����� OfficerData ���Ǘ�����Ă��邱�Ƃɂ���āA�P�̃f�[�^�Q�Ƃ��ė��p�ł���悤�ɂ��Ă���
}

