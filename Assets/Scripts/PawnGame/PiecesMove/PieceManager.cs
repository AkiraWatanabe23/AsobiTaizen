using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    /// <summary> 駒をまとめている親オブジェクトを取得 </summary>
    GameObject _pieceParent;
    /// <summary> 駒(子オブジェクト達)を一次元配列として取得 </summary>
    Transform[] _pieceChildrens;

    // Start is called before the first frame update
    public void Start()
    {
        ///<summary> 駒をまとめている親オブジェクトを検索する </summary>
        _pieceParent = GameObject.Find("Piece");
        ///<summary> 子オブジェクト達の配列を初期化 </summary>
        _pieceChildrens = new Transform[_pieceParent.transform.childCount];
        ///<summary> 子オブジェクトを取得 </summary>
        for (int i = 0; i < _pieceParent.transform.childCount; i++)
        {
            _pieceChildrens[i] = _pieceParent.transform.GetChild(i);
            Debug.Log(i + "番目の駒は" + _pieceChildrens[i].name + "です");
        }
    }
}
