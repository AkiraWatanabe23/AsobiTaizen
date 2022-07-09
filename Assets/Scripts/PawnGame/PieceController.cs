using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 
/// 駒の動きに関するクラス...「選んで、動く」「奪われたら、破棄」の駒全体で統一された動き
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
    /// <summary> 移動判定をとるためのフラグ </summary>
    public bool _isSelect;
    /// <summary> 移動時のマス選択 </summary>
    RaycastHit _hitTile;

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
        //マウス左クリックで駒を選び、二度目のクリックで配置場所を確定する
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 _mousePos = Input.mousePosition;
            Ray _ray = Camera.main.ScreenPointToRay(_mousePos);
            Debug.Log(_isSelect);

            if (Physics.Raycast(_ray, out _hitTile, 50))
              //Physics.Raycast(Rayの開始地点(ワールド座標), Rayの方向, Rayが衝突を検知する最大の距離)
            {
                //Rayを飛ばし、タグが自分自身と同じだった場合選択状態の変更
                if (_hitTile.collider.gameObject.tag == gameObject.tag)
                {
                    _isSelect = !_isSelect;
                }
                //駒を選択中かつ、Rayがヒットしたオブジェクトがマスだった場合移動
                if (_isSelect && _hitTile.collider.gameObject.tag == "Tile")
                {
                    Vector3 _newPiecePos = _hitTile.collider.gameObject.transform.position;
                    transform.position = new Vector3(_newPiecePos.x, transform.position.y, _newPiecePos.z);

                    //移動後は駒の選択状態を非選択にする
                    _isSelect = false;
                }
            }
            Debug.Log(_mousePos);
        }
    }

    //初期設定
    public void SetPiece(int player, Type type)
    {
        Player = player;
        _type = type;
        _turnCount = -1; //初動に戻す
    }
}
