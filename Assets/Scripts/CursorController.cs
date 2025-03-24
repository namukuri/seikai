using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps; //Tilemap���������߂ɕK�v

[RequireComponent(typeof(Rigidbody2D))]

public class CursorController : MonoBehaviour
{
    [Header("�^�C���}�b�v�Q��")]
    public Tilemap tilemap; // �}�E�X�N���b�N��̃Z�����W���擾���邽�߂Ɏg��
    //public float moveSpeed;
    private Rigidbody2D rb; // �R���|�[�l���g�̎擾�p
    //private float horizontal; // x ��(�����E��)�����̓��͂̒l�̑���p
    //private float vertical; // y ��(�����E�c)�����̓��͂̒l�̑���p

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody2D�R���|�[�l���g���擾
        TryGetComponent<Rigidbody2D>(out rb);
    }

    // Update is called once per frame
    void Update()
    {
        // ���N���b�N�����m
        if(Input.GetMouseButtonDown(0))
        {
            //  1.�N���b�N���ꂽ�X�N���[�����W�����[���h���W�ɕϊ�
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0f; //2D�Ȃ̂�Z��0�ɂ��낦��

            // 2.���[���h���W���^�C���}�b�v�̃Z�����W�ɕϊ�
            Vector3Int cellPos = tilemap.WorldToCell(mouseWorldPos);

            // 3. �Z�����W����A���̃Z���̒��S�̃��[���h���W���擾
            Vector3 targetPosition = tilemap.GetCellCenterWorld(cellPos);

            // �u�Ԉړ�
            transform.position = targetPosition;
        }
        // InputManager �� Horizontal �ɓo�^���Ă���L�[�����͂��ꂽ��A����(��)�����̓��͒l�Ƃ��đ��
        //horizontal = Input.GetAxis("Horizontal");
        // InputManager �� Vertical �ɓo�^���Ă���L�[�����͂��ꂽ��A����(��)�����̓��͒l�Ƃ��đ��
        //vertical = Input.GetAxis("Vertical");
    }
    //private void FixedUpdate()
    //{
    // �ړ�
    //Move();
    //}
    /// <summary>
    /// �ړ�
    /// </summary>
    //private void Move()
    //{
    // �΂߈ړ��̋����������Ȃ��悤�ɐ��K���������s���A�P�ʃx�N�g���Ƃ���(�����̏��͎����A�����ɂ�鑬�x�����Ȃ����Ĉ��l�ɂ���)
    //Vector3 dir = new Vector3(horizontal, vertical, 0).normalized;
    // velocity(���x)�ɐV�����l�������āA�Q�[���I�u�W�F�N�g���ړ�������
    //rb.velocity = dir * moveSpeed;

}
