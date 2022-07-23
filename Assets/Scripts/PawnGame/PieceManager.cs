using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PieceManager : MonoBehaviour, IPointerClickHandler
{
    /// <summary> 駒をまとめている親オブジェクトを取得 </summary>
    GameObject _pieceParent;
    /// <summary> 駒(子オブジェクト達)を配列として取得 </summary>
    Transform[] _pieceChildrens;

    /// <summary>
    /// マウスクリックが行われた(どのマウスクリックでも実行される)時の処理
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        var go = eventData.pointerCurrentRaycast.gameObject;
        //↑カメラから現在のマウスカーソルの位置にRayを飛ばし、当たったオブジェクトを代入する
        var piece = go.GetComponent<PieceController>();
        //↑当たったオブジェクトの「PieceController」を見つけてくる

        print($"{ name } を選んだ");
        piece.ChangeState();
    }

    // Start is called before the first frame update
    void Start()
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
