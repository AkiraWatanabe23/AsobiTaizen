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
        //マウス左クリックで駒を選び、二度目のクリックで配置場所を確定する
        if (Input.GetMouseButtonDown(0))
        {
            //マウスの位置を取得し、Rayに代入
            Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(_ray.origin, _ray.direction * 10, Color.green, 10, false); //Rayが黒番のカメラの方から出ている...Tag変えたらMainCameraの方に変わった
            _select = true;
            Debug.Log(_select);
            /*↑ここまでは呼ばれている*/

            if (_hitTile.collider.gameObject.tag == "WhitePiece")
            {
                Debug.Log("SelectWhitePiece");
            }

            //マウスのポジションからRayを伸ばし、何かに当たったら_hitTileに代入する
            if (Physics.Raycast(_ray, out _hitTile)) //←多分ここがダメ...
            {
                Debug.Log("SelectPosition");

                //駒を選択中、かつRayがヒットしたオブジェクトが盤面のマスならば移動する
                if (_select && _hitTile.collider.gameObject.tag == "Tile")
                {
                    Vector3 _newPos = _hitTile.collider.gameObject.transform.position;
                    transform.position = new Vector3(_newPos.x, _newPos.y, _newPos.z);

                    _select = false;
                }
            }
        }
    }
}
