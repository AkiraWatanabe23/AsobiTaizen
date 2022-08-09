using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 個別(Individual)の動き
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
        //ポーンの動き
        if (_type == PieceType.Pawn)
        {
            Debug.Log("ポーンが選択されました");
        }
        //ナイトの動き
        else if (_type == PieceType.Knight)
        {
            Debug.Log("ナイトが選択されました");
        }
        //ビショップの動き
        else if (_type == PieceType.Bishop)
        {
            Debug.Log("ビショップが選択されました");
        }
        //ルークの動き
        else if (_type == PieceType.Rook)
        {
            Debug.Log("ルークが選択されました");
        }
        //クイーンの動き
        else if (_type == PieceType.Queen)
        {
            Debug.Log("クイーンが選択されました");
        }
    }
}
