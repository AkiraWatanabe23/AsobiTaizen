using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��(Individual)�̓���
/// </summary>
public class IndividualMove : PieceMove
{
    [Tooltip("�|�[���̈ړ���")] int _moveCount = 0;

    //�K�v�ȃ}�X��Ray���΂�(���ł͂Ȃ��A�J�������_�����Ray�ł���Ă݂�)�A����ȊO�̃}�X�͋����I��off�ɂ���?����I���ňړ����Ȃ��Ȃ�(?)
    //�^�[���̐؂�ւ�薈��off��on�ɂ���K�v������
    //�������}�X��Collider off(�����BoardInfo()�ŏ����Ă�)
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
    /// ��̌ʂ̈ړ�(PieceMove��Move�����s���Ă��镔���Ŏ��s����)
    /// </summary>
    public void MovableSpace()
    {
        //�|�[���̓���
        if (this.gameObject.GetComponent<PieceMove>()._type == PieceType.Pawn)
        {
            Debug.Log("�|�[�����I������܂���");
            //1,1��ڂ̓������A�����łȂ���
            //�@1��ڂ̏ꍇ��2�}�X�ړ���
            if (_moveCount == 0)
            {

                _moveCount++;
            }
            //2,2��ڈȍ~��1�}�X�ړ�
            else if (_moveCount != 0)
            {

            }
            //�@��Ɏ΂�1�R�O�͒T��(�A���p�b�T���Ɏg����?)
            //3,�A���p�b�T��...�^�ׂ̃}�X�T��
        }
        //�i�C�g�̓���
        else if (this.gameObject.GetComponent<PieceMove>()._type == PieceType.Knight)
        {
            Debug.Log("�i�C�g���I������܂���");
            //�ړ��\�ȃ}�X(�j�n4����)�ɋ���邩�A�Ȃ����̔���(�������甒or���A�Ȃ���Έړ���)
            //���������Έړ��s��
        }
        //�r�V���b�v�̓���
        else if (this.gameObject.GetComponent<PieceMove>()._type == PieceType.Bishop)
        {
            Debug.Log("�r�V���b�v���I������܂���");
            //�΂ߕ�����T��(�����΂�����stop)
            //����������̃}�X�́~�A�G���Z
        }
        //���[�N�̓���
        else if (this.gameObject.GetComponent<PieceMove>()._type == PieceType.Rook)
        {
            Debug.Log("���[�N���I������܂���");
            //�㉺���E��T��(�����΂�����stop)
            //����������̃}�X�́~�A�G���Z
        }
        //�N�C�[���̓���
        else if (this.gameObject.GetComponent<PieceMove>()._type == PieceType.Queen)
        {
            Debug.Log("�N�C�[�����I������܂���");
            //�΂ߕ��� + �㉺���E��T��(�����΂�����stop)
            //����������̃}�X�́~�A�G���Z
        }
    }
}
