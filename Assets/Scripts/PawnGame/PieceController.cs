using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 
/// 駒の動きに関するクラス...「選んで、動く」「奪われたら、破棄」の駒全体で統一された動き
/// </summary>
public class PieceController : MonoBehaviour
{
    ///<summary>プレイヤー </summary>
    public int _player;
    /// <summary> 駒の種類 </summary>
    public Type _type;
    /// <summary> 移動状態 </summary>
    public List<Status> _status;
    /// <summary> 移動判定をとるためのフラグ </summary>
    public bool _select;
    /// <summary> 移動時のマス選択 </summary>
    public RaycastHit _hitTile;

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
        _status = new List<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        //マウス左クリックで駒を選び、二度目のクリックで配置場所を確定する
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 _mousePos = Input.mousePosition;
            Vector3 _cameraPos = Camera.main.transform.position;

            if (Physics.Linecast(_cameraPos, _mousePos)) //ここ呼ばれてない...
            {
                //Rayを飛ばし、タグが自分自身と同じだった場合選択状態の変更
                if (_hitTile.collider.gameObject.tag == gameObject.tag)
                {
                    _select = true;
                }
                //駒を選択中かつ、Rayがヒットしたオブジェクトがマスだった場合移動
                if (_select && _hitTile.collider.gameObject.tag == "Tile")
                {
                    Vector3 _newPiecePos = _hitTile.collider.gameObject.transform.position;
                    transform.position = new Vector3(_newPiecePos.x, transform.position.y, _newPiecePos.z);

                    //移動後は駒の選択状態を非選択にする
                    _select = false;
                }
            }
            Debug.Log(_mousePos);
            Debug.Log(_cameraPos);
        }
    }

    //初期設定
    public void SetPiece(int player, Type type)
    {
        _player = player;
        _type = type;
    }
}
