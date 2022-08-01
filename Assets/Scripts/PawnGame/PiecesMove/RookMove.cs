using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RookMove : PieceManager
{
    BoardManager _board;
    public int[] _movableSpace;

    // Start is called before the first frame update
    void Start()
    {
        _board = GameObject.Find("Board,Tile").GetComponent<BoardManager>();
    }

    public void MovableSpace(bool Move, int Space)
    {
        if (Move == false && Space == 0)
        {
            //«PieceManager‚ÌAwake‚Ìˆ—‚ğÀs‚·‚é
            PieceManager._manager.Awake();
        }
    }
}
