using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 
/// 全ての駒に統一された動き
/// </summary>
public class PieceController : MonoBehaviour
{
    ///<summary> プレイヤー1(白番) </summary>
    private const int _playerOne = 1;
    ///<summary> プレイヤー2(黒番) </summary>
    private const int _playerTwo = 2;
    ///<summary> 現在のプレイヤー </summary>
    private int _currentPlayer = _playerOne; //current...現在(の)
    /// <summary> 駒の種類 </summary>
    public Type _type;
    /// <summary> 移動判定をとるためのフラグ </summary>
    public bool _select;
    /// <summary> Rayの衝突を確かめる </summary>
    public RaycastHit _hitTile;
    /// <summary> レイヤーマスク </summary>
    private LayerMask _pieceLay = 6;

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

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(_select);
    }

    // Update is called once per frame
    void Update()
    {
        //マウス左クリックで駒を選び、二度目のクリックで配置場所を確定、移動する
        if (Input.GetMouseButtonDown(0))
        {
            //マウスの位置を取得し、Rayに代入
            Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(_ray.origin, _ray.direction * 30, Color.green, 10, false); //Rayが黒番のカメラの方から出ている...Tag変えたらMainCameraの方に変わった
            Debug.Log(_ray);
            /*↑ここまでは呼ばれている*/

            //マウスのポジションからRayを伸ばし、何かに当たったら_hitTileに代入する
            //↓このif文が呼ばれてない...どの引数がダメ？
            //if (Physics.Raycast( _ray /*投射対象のRay*/, out _hitTile /*衝突した相手オブジェクトの情報*/, 50 /*Rayの長さ(省略した場合、無限長)*/, _pieceLay /*衝突対象になるレイヤー(レイヤーマスク)*/ ))
            //{
                _select = !_select; //「!」...現在の値とは反対の値を代入している(今回の場合、false→true)
                Debug.Log("SelectPosition");

                //駒を選択中、かつRayがヒットしたオブジェクトが盤面のマスならば移動する
                if (_select/*(== true)*/ && _hitTile.collider.gameObject.tag == "Tile")//...NullReferenceException
                {
                    Vector3 _newPos = Input.mousePosition;
                    transform.position = new Vector3(_newPos.x, _newPos.y, _newPos.z);
                    _select = false;
                }
            //}
        }
    }
}
