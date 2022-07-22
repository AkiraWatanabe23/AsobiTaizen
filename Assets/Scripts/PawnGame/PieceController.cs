using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Runtime.InteropServices;

/// <summary> 
/// 全ての駒に統一された動き(駒の選択、移動)
/// </summary>
public class PieceController : MonoBehaviour
{
    /// <summary> プレイヤー(白) </summary>
    public const int _playerOne = 1;
    /// <summary> プレイヤー(黒) </summary>
    public const int _playerTwo = 2;
    /// <summary> current(現在の)プレイヤー </summary>
    public int _currentPlayer;
    /// <summary> レイヤーマスク(InspectorのLayerから選択する) </summary>
    [SerializeField] LayerMask _tileLayer;
    [SerializeField] LayerMask _whiteLayer;
    [SerializeField] LayerMask _blackLayer;
    /// <summary> 黒番目線のカメラ </summary>
    Camera _camera;
    /// <summary> 駒を移動した時にcolliderの上に置く </summary>
    [SerializeField] Vector3 _offset = Vector3.up;
    /// <summary> 駒の種類 </summary>
    public Type _type;
    /// <summary> 通常状態、移動状態の駒のマテリアル </summary>
    [SerializeField] Material _normalMaterial;
    [SerializeField] Material _moveMaterial;
    Renderer _renderer;
    ///// <summary> 黒番目線のカメラ </summary>
    //Camera _camera;
    /// <summary> 駒の状態 </summary>
    public Status _status = Status.Normal;

    bool isMove = false;

    /// <summary> どっちのターンか(白) </summary>
    Text _whiteTurn;
    /// <summary> どっちのターンか(黒) </summary>
    Text _blackTurn;
    /// <summary> 白駒か黒駒か </summary>
    [SerializeField] public Color _state = Color.White;

    //extern...UnityやVisualStudioにはない機能(関数)をとってくる(C++でいうと「::」と同じらしい)
    //上記を訂正 : extern...外部ファイル(dllファイル)で定義されている関数や変数を使用する、という命令
    //[DllImport("user32.dll")]...外のどのファイル(今回は「user32.dll」)からとってくるのか
    //SetCursorPos(関数)...指定したファイル内のどの機能(関数)を使うのか
    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int x, int y);

    void Start()
    {
        _renderer = GetComponent<Renderer>(); //駒のRenderer(コンポーネント)をとってくる

        _whiteTurn = GameObject.Find("WhiteText").GetComponent<Text>();
        _blackTurn = GameObject.Find("BlackText").GetComponent<Text>();
        _whiteTurn.color = UnityEngine.Color.yellow; //白番から始める

        _camera = GameObject.Find("Camera(black)").GetComponent<Camera>();
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
    public bool Move()
    {
        //マウスの位置を取得し、Rayに代入
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition); //メインカメラ(白番目線)からRayをとばす
        Ray _ray2 = _camera.ScreenPointToRay(Input.mousePosition);    //secondカメラ(黒番目線)からRayをとばす
        //Rayの長さ
        float _rayDistance = 100;

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

            PhaseChange(_target);
            SetCursorPos(950, 400); //駒を移動させた後、マウスカーソルをゲーム画面の中央辺りに強制移動させる

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

            PhaseChange(_target);
            SetCursorPos(950, 400); //駒を移動させた後、マウスカーソルをゲーム画面の中央辺りに強制移動させる

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

            PhaseChange(_target);
            SetCursorPos(950, 400); //駒を移動させた後、マウスカーソルをゲーム画面の中央辺りに強制移動させる

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

            PhaseChange(_target);
            SetCursorPos(950, 400); //駒を移動させた後、マウスカーソルをゲーム画面の中央辺りに強制移動させる

            print($"Ray は {_target.name} に移動した");
            Debug.Log(_currentPlayer);
            return true;
        }

        return false;
    }
    /// <summary>
    /// マウスクリックをした時に実行される処理
    /// </summary>
    public void ChangeState() //右クリックをすると移動状態→通常状態にできる
    {
        if (_status == Status.Normal && _state == Color.White && GameManager._state == Phase.White)
        {
            _status = Status.Move;
            _renderer.material = _moveMaterial;
            isMove = true;
        }
        else if (_status == Status.Move && isMove == true)
        {
            _status = Status.Normal;
            _renderer.material = _normalMaterial;
        }
    }

    /// <summary>
    /// フェーズ毎の移動制御
    /// </summary>
    /// <param name="_target"></param>
    public void PhaseChange(GameObject _target)
    {
        switch (_state)
        {
            case 0: //Color.White
                _whiteTurn.color = UnityEngine.Color.white;
                _blackTurn.color = UnityEngine.Color.yellow;
                if (_target.tag == "WhitePiece")
                {
                    _status = Status.Normal;
                    isMove = false;
                }
                else
                {
                    isMove = true;
                }
                break;

            case (Color)1: //Color.Black
                _whiteTurn.color = UnityEngine.Color.yellow;
                _blackTurn.color = UnityEngine.Color.white;
                if (_target.tag == "BlackPiece")
                {
                    _status = Status.Normal;
                    isMove = false;
                }
                else
                {
                    isMove = true;
                }
                break;
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

    /// <summary>
    /// 白駒or黒駒
    /// </summary>
    public enum Color
    {
        White = 0,
        Black = 1,
    }
}