using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��(Individual)�̓���
/// </summary>
public class IndividualMove : MonoBehaviour
{
    BoardManager _managerB;
    MeshRenderer _renderer;

    public GameObject[] _masu;
    public int[] _movablePos;

    // Start is called before the first frame update
    void Start()
    {
        _managerB = GameObject.Find("Board,Tile").GetComponent<BoardManager>();
    }

    public void MovableSpace(bool Move, int Space)
    {
        //�|�[���̓���
        if (Move == false && Space == 0)
        {

        }
        //�i�C�g�̓���
        //�r�V���b�v�̓���
        //���[�N�̓���
        else if (Move == false && Space == 4)
        {

        }
        //�N�C�[���̓���
    }
}
