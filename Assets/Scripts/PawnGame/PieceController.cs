using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : MonoBehaviour
{
    //プレイヤー
    public int Player;
    //駒の種類
    public Type _type;
    //何手経過したか
    public int _turnCount;
    //駒の配置位置
    public Vector2Int Pos, _oldPos;
    //移動状態
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
        Check,
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

    public void MovePiece(GameObject tile)
    {
        //boardの番号から配列番号に変換
        Vector2Int idx = new Vector2Int
            ((int)tile.transform.position.x + GameScene._boardWidth / 2,
             (int)tile.transform.position.z + GameScene._boardHeight / 2);

        //場所の移動
        Vector3 _pos = tile.transform.localPosition;
        _pos.y = 21.25f;
        transform.localPosition = _pos;

        //移動状態をセット
        _status.Clear();

        //アンパッサンの処理


        //インデックスの更新
        _oldPos = Pos;
        Pos = idx;

        //配置してからの経過ターンをリセット
        _turnCount = 0;
    }
}
