using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary> 
/// 全ての駒に統一された動き(駒の選択、移動)
/// </summary>
public class PieceController : MonoBehaviour, IPointerClickHandler
{
    ///<summary> プレイヤー(白番) </summary>
    private const int _playerOne = 1;
    ///<summary> プレイヤー(黒番) </summary>
    private const int _playerTwo = 2;
    ///<summary> current(現在の)プレイヤー </summary>
    private int _currentPlayer;
    /// <summary> 駒の種類 </summary>
    public Type _type;
    /// <summary> Rayの長さ </summary>
    [SerializeField] float _rayDistance = 100;
    /// <summary> レイヤーマスク(InspectorのLayerから選択する) </summary>
    [SerializeField] LayerMask _tileLayer;
    /// <summary> 駒を移動した時にcolliderの上に置く </summary>
    [SerializeField] Vector3 _offset = Vector3.up;
    /// <summary> 通常状態、移動状態の駒のマテリアル </summary>
    [SerializeField] Material _normalMaterial;
    [SerializeField] Material _moveMaterial;
    Renderer _renderer;
    /// <summary> 黒番目線のカメラ </summary>
    Camera _camera;
    /// <summary> 駒の状態 </summary>
    public Status _status = Status.Normal;

    /// <summary>
    /// マウスクリックが行われた(どのマウスクリックでも実行される)時の処理
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        print($"{ name } をクリックした");
        ChangeState();
    }

    /// <summary>
    /// マウスクリックをした時に実行される処理
    /// </summary>
    void ChangeState() //右クリックをすると移動状態→通常状態にできる
    {
        if (_status == Status.Normal)
        {
            _status = Status.Move;
            _renderer.material = _moveMaterial;
        }
        else if (_status == Status.Move)
        {
            _status = Status.Normal;
            _renderer.material = _normalMaterial;
        }
    }

    bool Move()
    {
        //マウスの位置を取得し、Rayに代入
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition); //メインカメラ(白番目線)からRayをとばす
        Ray _ray2 = _camera.ScreenPointToRay(Input.mousePosition);    //secondカメラ(黒番目線)からRayをとばす

        //白番目線のRayの処理
        if (Physics.Raycast(_ray, out RaycastHit hit, _rayDistance, _tileLayer))
        {
            GameObject _target = hit.collider.gameObject;
            this.transform.position = _target.transform.position + _offset;
            print($"Ray は {_target.name} に当たった"); // print($"..."); = Debug.Log("..."); と同じ
            return true;
        }
        //黒番目線のRayの処理
        else if (Physics.Raycast(_ray2, out RaycastHit hit2, _rayDistance, _tileLayer))
        {
            GameObject _target = hit2.collider.gameObject;
            this.transform.position = _target.transform.position + _offset;
            print($"Ray は {_target.name} に当たった");
            return true;
        }

        return false;
    }

    void Start()
    {
        _currentPlayer = _playerOne;                                       //最初は白番から
        _camera = GameObject.Find("Camera(black)").GetComponent<Camera>(); //黒番目線のカメラを見つけてくる
        _renderer = GetComponent<Renderer>();                              //駒のRenderer(コンポーネント)をとってくる
    }

    // Update is called once per frame
    void Update()
    {
        //一回目のマウスクリックで駒を選び、二回目のクリックで配置場所を確定、移動する
        //マウスクリックの中でも、左クリックが行われた場合に以下の処理を行う
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
        None,
        Pawn,
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