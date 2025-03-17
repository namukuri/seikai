using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro�𗘗p���邽�߂̖��O���
using UnityEngine.UI;

public class PlacementPlayerPopUp : MonoBehaviour
{
    [Header("ScriptableObject�Q��")]
    //�v���C���[�i��l���j���܂�OfficerData�̃��X�g��ێ����Ă���ScriptableObject
    public VagaOfficerDataSO vagaOfficerDataSO;

    //meritValue�ɉ�����OfficerRank���X�V���邽�߂̃N���X
    public OfficerRankUpDater rankUpDater;

    [Header("UI�Q��")]
    //�u���ђl�ǉ��{�^���v
    public Button btnPlayerMVUp;

    // ��ʂɕ\��������ђl�p�e�L�X�g�iTextMeshPro�j
    public TMP_Text txtMeritValue;

    // ��ʂɕ\�������l���K���p�e�L�X�g�iTextMeshPro�j
    public TMP_Text txtPlayerRank;

    // ��l���̃X�v���C�g��\������UI Image
    public Image ImgPlayer;

    // ��l���̖��O��\������e�L�X�g
    public TMP_Text txtPlayerName;

    // ��l���̃X�e�[�^�X��\������e�L�X�g
    public TMP_Text txtPlayerStatus;

    // ��l���̍U���͂�\������e�L�X�g
    public TMP_Text txtPlayerAttackPower;

    // ��l���̖h��͂�\������e�L�X�g
    public TMP_Text txtPlayerDeffensePower;

    [Header("�v���C���[��OfficerData���X�g��̃C���f�b�N�X")]
    //��l���i�v���C���[�j��vagaOfficerDataSO.vagaOfficerDataList�̉��ԖڂɊi�[����Ă��邩
    public int playerIndex = 0;

    //�����ň�����l����OfficerData�Q��
    private OfficerData playerOfficerData;

    private void Start()
    {
        // ScriptableObject ���������A�T�C������Ă��邩�m�F
        if (vagaOfficerDataSO == null)
        {
            Debug.Log("VagaOfficerDataSO���A�T�C������Ă��܂���B");
            return; //�������I������
        }
        // rankUpDater ���������A�T�C������Ă��邩�m�F
        if (rankUpDater == null)
        {
            Debug.Log("OfficerRankUpData���A�T�C������Ă��܂���B");
            return; //�������I������
        }
        // playerIndex �͈̔͂����X�g�͈͓̔����m�F
        if (playerIndex < 0 || playerIndex >= vagaOfficerDataSO.vagaOfficerDataList.Count) //playerIndex���O�ȉ��������̓��X�g�̐��l�ȏ�ł����
        {
            Debug.Log("playerIndex�����X�g�͈̔͊O�ł��B");
            return; //�������I������
        }

        //��l����OfficerData���擾
        playerOfficerData = vagaOfficerDataSO.vagaOfficerDataList[playerIndex]; //vagaOfficerDataList����playerIndex���擾�i�O�Ԗځj

        //�����\�����Z�b�g
        txtMeritValue.text = playerOfficerData.meritValue.ToString();�@//�v���C���[�̌��ђl�̏����l�����X�g�v���C���[����ݒ�BToString�^�ɂ��ēǂ߂�悤�ɂ���
        txtPlayerRank.text = playerOfficerData.officerRank.ToString();�@//�v���C���[�̊K���̏����l�����X�g�v���C���[����ݒ�BToString�^�͕K�v�炵���B
        ImgPlayer.sprite = playerOfficerData.OfficerSprite;
        txtPlayerName.text = playerOfficerData.OfficerName;
        txtPlayerStatus.text = playerOfficerData.OfficerStatus;
        txtPlayerAttackPower.text = playerOfficerData.attackPower.ToString();
        txtPlayerDeffensePower.text = playerOfficerData.defensePower.ToString();

        // �{�^���N���b�N���̏�����o�^
        btnPlayerMVUp.onClick.AddListener(OnClickPlayerMVUp); //AddListener�ɂ��OnClickPlayerMVUp���\�b�h���ǉ������
    }

    // �u���ђl�ǉ��v�{�^���������ꂽ�Ƃ��̏���
    private void OnClickPlayerMVUp()
    {
        // ��l���̌��ђl��50���Z
        playerOfficerData.meritValue += 50;

        // �S���Z�̃����N���Čv�Z�iOfficerRankUpDater �𗘗p�j
        rankUpDater.UpdateOfficerRanks();

        // �Čv�Z��̎�l���f�[�^�� UI �ɔ��f
        txtMeritValue.text = playerOfficerData.meritValue.ToString();
        txtPlayerRank.text = playerOfficerData.officerRank.ToString();
    }



}

