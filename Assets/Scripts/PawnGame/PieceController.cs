using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Runtime.InteropServices;

/// <summary> 
/// 全ての駒に統一された動き(駒の選択、移動)
/// </summary>
public class PieceController : MonoBehaviour, IPointerClickHandler
{
    /// <summary> プレイヤー(白) </summary>
    public const int _playerOne = 1;
    /// <summary> プレイヤー(黒) </summary>
    public const int _playerTwo = 2;
    /// <summary> current(現在の)プレイヤー </summary>
    public int _currentPlayer;
    /// <summary> 駒の種類 </summary>
    public Type _type;
    /// <summary> Rayの長さ </summary>
    [SerializeField] float _rayDistance = 100;
    /// <summary> レイヤーマスク(InspectorのLayerから選択する) </summary>
    [SerializeField] LayerMask _tileLayer;
    [SerializeField] LayerMask _whiteLayer;
    [SerializeField] LayerMask _blackLayer;
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

    /// <summary> どっちのターンか(白) </summary>
    Text _whiteTurn;
    /// <summary> どっちのターンか(黒) </summary>
    Text _blackTurn;

    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int x, int y);

    /// <summary>
    /// マウスクリックが行われた(どのマウスクリックでも実行される)時の処理
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        print($"{ name } を選んだ");
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

        //白番目線の駒の移動
        //白番目線のRayの処理(駒を奪う場合)
        if (Physics.Raycast(_ray, out RaycastHit hit, _rayDistance, _blackLayer))
        {
            GameObject _target = hit.collider.gameObject;

            //Rayが当たったオブジェクトが敵の駒だった場合、駒を奪ってそのマスに移動する
            if (_target.tag == "BlackPiece")
            {
                _target.SetActive(false);
            }

            this.transform.position = _target.transform.position + _offset;
            _currentPlayer = _playerTwo;
            _whiteTurn.color = Color.white;
            _blackTurn.color = Color.yellow;
            SetCursorPos(950, 400);

            print($"Ray は {_target.name} に移動した");
            Debug.Log(_currentPlayer);
            return true;
        }
        //白番目線のRayの処理(移動のみ)
        else if (Physics.Raycast(_ray, out RaycastHit hit2, _rayDistance, _tileLayer))
        {
            GameObject _target = hit2.collider.gameObject;
            this.transform.position = _target.transform.position + _offset;
            _currentPlayer = _playerTwo;
            _whiteTurn.color = Color.white;
            _blackTurn.color = Color.yellow;
            SetCursorPos(950, 400);

            print($"Ray は {_target.name} に移動した"); // print($"..."); ←→ Debug.Log("..."); と同じ
            Debug.Log(_currentPlayer);
            return true;
        }

        //黒番目線の駒の移動
        //黒番目線のRayの処理(駒を奪う場合)
        if (Physics.Raycast(_ray2, out RaycastHit hit3, _rayDistance, _whiteLayer))
        {
            GameObject _target = hit3.collider.gameObject;

            //Rayが当たったオブジェクトが敵の駒だった場合、駒を奪ってそのマスに移動する
            if (_target.tag == "WhitePiece")
            {
                _target.SetActive(false);
            }

            this.transform.position = _target.transform.position + _offset;
            _currentPlayer = _playerOne;
            _whiteTurn.color = Color.yellow;
            _blackTurn.color = Color.white;
            SetCursorPos(950, 400);

            print($"Ray は {_target.name} に移動した");
            Debug.Log(_currentPlayer);
            return true;
        }
        //黒番目線のRayの処理(移動のみ)
        else if (Physics.Raycast(_ray2, out RaycastHit hit4, _rayDistance, _tileLayer))
        {
            GameObject _target = hit4.collider.gameObject;
            this.transform.position = _target.transform.position + _offset;
            _currentPlayer = _playerOne;
            _whiteTurn.color = Color.yellow;
            _blackTurn.color = Color.white;
            SetCursorPos(950, 400);

            print($"Ray は {_target.name} に移動した");
            Debug.Log(_currentPlayer);
            return true;
        }

        return false;
    }

    void Start()
    {
        _currentPlayer = _playerOne;                                       //白番から始める
        _camera = GameObject.Find("Camera(black)").GetComponent<Camera>(); //黒番目線のカメラを見つけてくる
        _renderer = GetComponent<Renderer>();                              //駒のRenderer(コンポーネント)をとってくる

        _whiteTurn = GameObject.Find("WhiteText").GetComponent<Text>();
        _blackTurn = GameObject.Find("BlackText").GetComponent<Text>();
        _whiteTurn.color = Color.yellow;                                   //白番から始める
        Debug.Log(Input.mousePosition);
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

    /// <summary>
    /// Queen = 5, Rook = 4, Bishop = 3, Knight = 2, Pawn = 1 と数字を振る
    /// </summary>
    public enum Type
    {
        Pawn = 1,
        Knight,
        Bishop,
        Rook,
        Queen,
    }

    /// <summary>
    /// 通常状態、移動状態
    /// </summary>
    public enum Status
    {
        Normal, //通常状態
        Move,   //移動状態
    }
}