using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary> 
/// 全ての駒に統一された動き
/// </summary>
public class PieceController : MonoBehaviour, IPointerClickHandler
{
    ///<summary> プレイヤー(白番) </summary>
    private const int _playerOne = 1;
    ///<summary> プレイヤー(黒番) </summary>
    private const int _playerTwo = 2;
    ///<summary> current(現在の)プレイヤー </summary>
    private int _currentPlayer = _playerOne;
    /// <summary> 駒の種類 </summary>
    public Type _type;
    /// <summary> 移動判定をとるためのフラグ </summary>
    public bool _select;
    /// <summary> Rayの長さ </summary>
    [SerializeField] public float _rayDistance = 100;
    /// <summary> レイヤーマスク(Inspector内のLayerの番号) </summary>
    // ※レイヤーマスクの値は2bit値(2進数)で管理しているため、10進数で表示は×
    [SerializeField] LayerMask _tileLayer;
    /// <summary> 駒を移動した時にcolliderの上に置く </summary>
    [SerializeField] Vector3 _offset = Vector3.up;

    Status _status = Status.Normal;


    public void OnPointerClick(PointerEventData eventData)
    {
        print($"{ name } をクリックした");
        ChangeState();
    }

    void ChangeState()
    {
        if (_status == Status.Normal)
        {
            _status = Status.Move;
        }
        else if (_status == Status.Move)
        {
            _status = Status.Normal;
        }
    }

    bool Move()
    {
        //マウスの位置を取得し、Rayに代入
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(_ray.origin, _ray.direction * 30, Color.green, 30/*実行時間(秒)*/, false);
        Debug.Log(_ray);

        if (Physics.Raycast(_ray, out RaycastHit hit, _rayDistance, _tileLayer))
        {
            GameObject _target = hit.collider.gameObject;
            print($"Ray は {_target.name} に当たった");
            this.transform.position = _target.transform.position + _offset;
            return true;
        }

        return false;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //一回目のマウス左クリックで駒を選び、二度目のクリックで配置場所を確定、移動する
        if (Input.GetMouseButtonDown(0))
        {
            if (_status == Status.Move)
            {
                if (Move())
                {
                    ChangeState();
                }
            }
        }
    }

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

    //通常状態、移動状態
    public enum Status
    {
        Normal, //通常状態
        Move,   //移動状態
    }
}