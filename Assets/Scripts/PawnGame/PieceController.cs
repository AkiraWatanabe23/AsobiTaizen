using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 
/// 駒の動きに関する class
/// </summary>
public class PieceController : MonoBehaviour
{
    ///<summary>プレイヤー </summary>
    public int Player;
    /// <summary> 駒の種類 </summary>
    public Type _type;
    /// <summary> 何手経過したか </summary>
    public int _turnCount;
    /// <summary> 駒の配置されている位置 </summary>
    public Vector2Int Pos, _oldPos;
    /// <summary> 移動状態 </summary>
    public List<Status> _status;


    //Queen = 5, Rook = 4, Bishop = 3, Knight = 2, Pawn = 1 と数字を振る
    public enum Type
    {
        None = -1,
        Pawn = 1,
        Knight,
        Bishop,
        Rook,
        Queen,
    }

    //移動状態
    public enum Status
    {
        None = -1,
        EnPassant = 1, //アンパッサン
        Check, //チェック(クイーンに対して)
    }

    // Start is called before the first frame update
    void Start()
    {
        _turnCount = -1;
        _status = new List<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //初期設定
    public void SetPiece(int player, Type type, GameObject tile)
    {
        Player = player;
        _type = type;
        MovePiece(tile);
        _turnCount = -1; //初動に戻す
    }

    //駒が選択された時の処理
    public void SelectPiece(bool select = true)
    {
        Vector3 _pos = transform.localPosition;
        _pos.y += 2;
        GetComponent<Rigidbody>().isKinematic = true;

        //駒の選択解除
        if (!select)
        {
            _pos.y = 1.25f;
            GetComponent<Rigidbody>().isKinematic = false;
        }

        transform.localPosition = _pos;
    }

    //移動処理
    public void MovePiece(GameObject tile)
    {
        //駒の移動時は、非選択状態にする
        SelectPiece(false);

        //boardの番号から配列番号に変換
        Vector2Int idx = new Vector2Int
            ((int)tile.transform.position.x + GameScene._boardWidth / 2,
             (int)tile.transform.position.z + GameScene._boardHeight / 2);

        //場所の移動
        Vector3 _pos = tile.transform.position;
        _pos.y = 21.25f;
        transform.position = _pos;

        //移動状態をリセット
        _status.Clear();

        //アンパッサン(ポーンの特別な動き)の処理
        if (Type.Pawn == _type)
        {
            //前に2マス進んだ時
            if (1 < Mathf.Abs(idx.y - Pos.y))
            {
                _status.Add(Status.EnPassant);
            }

            //2マス進んだ一歩前に、ポーンの残像(イメージ)が残る
            int _direction = -1;
            if (1 == Player)
            {
                _direction = 1;
            }

            Pos.y = idx.y + _direction;
        }

        //インデックスの更新
        _oldPos = Pos;
        Pos = idx;

        //配置してからの経過ターンをリセット
        _turnCount = 0;
    }
}
