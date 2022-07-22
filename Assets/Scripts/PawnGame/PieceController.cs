using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary> 
/// 全ての駒に統一された動き(駒の選択、移動)
/// </summary>
public class PieceController : MonoBehaviour
{
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
    /// <summary> 駒のマネージャー </summary>
    PieceManager _managerMove;

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
        if (_state == Color.White)
        {
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
        }
        else if (_state == Color.Black)
        {
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
        }

        //switch (_state)
        //{
        //    case 0: //Color.White
        //        _whiteTurn.color = UnityEngine.Color.white;
        //        _blackTurn.color = UnityEngine.Color.yellow;
        //        if (_target.tag == "WhitePiece")
        //        {
        //            _status = Status.Normal;
        //            isMove = false;
        //        }
        //        else
        //        {
        //            isMove = true;
        //        }
        //        break;

        //    case (Color)1: //Color.Black
        //        _whiteTurn.color = UnityEngine.Color.yellow;
        //        _blackTurn.color = UnityEngine.Color.white;
        //        if (_target.tag == "BlackPiece")
        //        {
        //            _status = Status.Normal;
        //            isMove = false;
        //        }
        //        else
        //        {
        //            isMove = true;
        //        }
        //        break;
        //}
    }

    void Start()
    {
        _renderer = GetComponent<Renderer>(); //駒のRenderer(コンポーネント)をとってくる

        _whiteTurn = GameObject.Find("WhiteText").GetComponent<Text>();
        _blackTurn = GameObject.Find("BlackText").GetComponent<Text>();
        _whiteTurn.color = UnityEngine.Color.yellow; //白番から始める

        _managerMove = GameObject.Find("Piece").GetComponent<PieceManager>();
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
                if (_managerMove.Move())
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

    /// <summary>
    /// 白駒or黒駒
    /// </summary>
    public enum Color
    {
        White = 0,
        Black = 1,
    }
}