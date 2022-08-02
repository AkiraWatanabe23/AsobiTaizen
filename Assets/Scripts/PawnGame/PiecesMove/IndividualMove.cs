using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 個別(Individual)の動き
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
        //ポーンの動き
        if (Move == false && Space == 0)
        {

        }
        //ナイトの動き
        //ビショップの動き
        //ルークの動き
        else if (Move == false && Space == 4)
        {

        }
        //クイーンの動き
    }
}
