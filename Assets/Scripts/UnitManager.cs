using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UnitManager : MonoBehaviour
{
    public WarShipController selectWarShip;
    public List<WarShipController> warshipList = new();

    // �^�C���}�b�v���Q�Ƃ���i�͑����ǂ̃Z���ɂ��邩�𔻒肷�邽�߁j
    public Tilemap tilemap;

    /// <summary>
    /// �w�肵���Z�����W�Ɋ͑�������΁A���̊͑��� selectWarShip �ɐݒ肷��
    /// </summary>
    /// <param name="cellPos">�N���b�N�Ȃǂŋ��߂��Z�����W</param>
    public bool SelectWarShipAtCell(Vector3Int cellPos)
    {
        // ��������I��������
        //selectWarShip = null;

        // warshipList �Ɋ܂܂�邷�ׂĂ̊͑����`�F�b�N
        foreach (var warship in warshipList)
        {
            // �͑��̌��݈ʒu���^�C�����W�ɕϊ�
            Vector3Int warshipCellPos = tilemap.WorldToCell(warship.transform.position);

            // �����N���b�N�����Z���ƈ�v���Ă���΁A���̊͑���I��
            if (warshipCellPos == cellPos)
            {
                selectWarShip = warship;
                Debug.Log($"Warship {warship.name} ��I�����܂��� (�Z�����W: {cellPos})");
                return true; // �ŏ��Ɍ��������͑������I�����ďI��
            }
        }
        return false;
    }

    // �K�v�ɉ����� Start / Update �͂��̂܂�
    void Start()
    {
    }

    void Update()
    {
    }
}
