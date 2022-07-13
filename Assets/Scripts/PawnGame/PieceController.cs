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
    ///<summary> current(現在の)プレイヤー </summary>
    private int _currentPlayer = _playerOne;
    /// <summary> 駒の種類 </summary>
    public Type _type;
    /// <summary> 移動判定をとるためのフラグ </summary>
    public bool _select;
    /// <summary> レイヤーマスク(Inspector内のLayerの番号) </summary>
    // ※レイヤーマスクの値は2bit値(2進数)で管理しているため、10進数で表示は×
    private LayerMask _pieceLay = 1 << 8; //2進数で「1000」
                              //= LayerMask.NameToLayer(Layer名(string)); でもOK

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
        
    }

    // Update is called once per frame
    void Update()
    {
        //マウス左クリックで駒を選び、二度目のクリックで配置場所を確定、移動する
        if (Input.GetMouseButtonDown(0))
        {
            //マウスの位置を取得し、Rayに代入
            Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //Rayの衝突を確かめる
            RaycastHit _hitTile;

            Physics.Raycast(_ray.origin, _ray.direction * 30, out _hitTile, Mathf.Infinity, _pieceLay);
            Debug.DrawRay(_ray.origin, _ray.direction * 30, Color.green, 30/*実行時間*/, false);
            Debug.Log(_ray);

            //マウスのポジションからRayを伸ばし、何かに当たったら_hitTileに代入する
            _select = !_select;
            Debug.Log(_select);
            Debug.Log("SelectPosition");

            //  ↓選択中　↓白番　　　　　　　　　　　　　↓Rayが"WhitePiece"タグのオブジェクトに当たった時
            if (_select && _currentPlayer == _playerOne && _hitTile.collider.gameObject.tag == "WhitePiece")
            {
                Vector3 _newPos = _hitTile.collider.gameObject.transform.position;
                transform.position = new Vector3(_newPos.x, _newPos.y, _newPos.z);
                _currentPlayer = _playerTwo;
                _select = false;
            }
            //       ↓選択中　↓黒番　　　　　　　　　　　　　↓Rayが"BlackPiece"タグのオブジェクトに当たった時
            else if (_select && _currentPlayer == _playerTwo && _hitTile.collider.gameObject.tag == "BlackPiece")
            {
                Vector3 _newPos = _hitTile.collider.gameObject.transform.position;
                transform.position = new Vector3(_newPos.x, _newPos.y, _newPos.z);
                _currentPlayer = _playerOne;
                _select = false;
            }
        }
    }
}
