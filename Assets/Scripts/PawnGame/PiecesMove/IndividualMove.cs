using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��(Individual)�̓���
/// </summary>
public class IndividualMove : PieceMove
{
    //���I���������Ƀ}�X��Ray���΂��A�ړ��s�̃}�X��Collider��off�ɂ��遨��I���ňړ����Ȃ��Ȃ�?
    //�����ꂾ�ƑS���̃}�X��Ray���΂����ƂɂȂ�?��Ray���΂��A����ȊO�̃}�X�͋����I��off�ɂ���?
    //�^�[���̐؂�ւ�薈��off��on�ɂ���K�v������
    //�ړ��\�͈͂̃}�X����
    //�I�������}�X��tag�Ń}�X�̐F��ω�������(��Ƃ��Ȃ物�F�A"Tile"tag�Ȃ�F�Ȃ�)

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    /// <summary>
    /// ��̌ʂ̈ړ�
    /// </summary>
    public void MovableSpace()
    {
        //�|�[���̓���
        if (this.gameObject.GetComponent<PieceMove>()._type == PieceType.Pawn)
        {
            Debug.Log("�|�[�����I������܂���");
        }
        //�i�C�g�̓���
        else if (this.gameObject.GetComponent<PieceMove>()._type == PieceType.Knight)
        {
            Debug.Log("�i�C�g���I������܂���");
        }
        //�r�V���b�v�̓���
        else if (this.gameObject.GetComponent<PieceMove>()._type == PieceType.Bishop)
        {
            Debug.Log("�r�V���b�v���I������܂���");
        }
        //���[�N�̓���
        else if (this.gameObject.GetComponent<PieceMove>()._type == PieceType.Rook)
        {
            Debug.Log("���[�N���I������܂���");
        }
        //�N�C�[���̓���
        else if (this.gameObject.GetComponent<PieceMove>()._type == PieceType.Queen)
        {
            Debug.Log("�N�C�[�����I������܂���");
        }
    }
}
