using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��(Individual)�̓���
/// </summary>
public class IndividualMove : PieceMove
{
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
    /// <param name="x">x���W</param>
    /// <param name="z">z���W</param>
    public void MovableSpace(float x, float z)
    {
        List<float> BlockXPos = new List<float>();
        List<float> BlockZPos = new List<float>();
        BlockXPos.Add(z);
        BlockZPos.Add(x);

        //�|�[���̓���
        if (_type == PieceType.Pawn)
        {
            Debug.Log("�|�[�����I������܂���");
        }
        //�i�C�g�̓���
        else if (_type == PieceType.Knight)
        {
            Debug.Log("�i�C�g���I������܂���");
        }
        //�r�V���b�v�̓���
        else if (_type == PieceType.Bishop)
        {
            Debug.Log("�r�V���b�v���I������܂���");
        }
        //���[�N�̓���
        else if (_type == PieceType.Rook)
        {
            Debug.Log("���[�N���I������܂���");
        }
        //�N�C�[���̓���
        else if (_type == PieceType.Queen)
        {
            Debug.Log("�N�C�[�����I������܂���");
        }
    }
}
