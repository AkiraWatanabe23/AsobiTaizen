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
        if (Input.GetMouseButtonDown(0))
        {
            if (_status == Status.Normal)
            {
                MovableSpace();
            }
        }
    }

    public void MovableSpace()
    {
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
