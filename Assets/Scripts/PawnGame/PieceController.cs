using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary> 
/// �S�Ă̋�ɓ��ꂳ�ꂽ����(��̑I���A�ړ�)
/// </summary>
public class PieceController : MonoBehaviour
{
    /// <summary> ��̎�� </summary>
    public Type _type;
    /// <summary> �ʏ��ԁA�ړ���Ԃ̋�̃}�e���A�� </summary>
    [SerializeField] Material _normalMaterial;
    [SerializeField] Material _moveMaterial;
    Renderer _renderer;
    ///// <summary> ���Ԗڐ��̃J���� </summary>
    //Camera _camera;
    /// <summary> ��̏�� </summary>
    public Status _status = Status.Normal;

    bool isMove = false;

    /// <summary> �ǂ����̃^�[����(��) </summary>
    Text _whiteTurn;
    /// <summary> �ǂ����̃^�[����(��) </summary>
    Text _blackTurn;
    /// <summary> ������ </summary>
    [SerializeField] public Color _state = Color.White;
    /// <summary> ��̃}�l�[�W���[ </summary>
    PieceManager _managerMove;

    /// <summary>
    /// �}�E�X�N���b�N���������Ɏ��s����鏈��
    /// </summary>
    public void ChangeState() //�E�N���b�N������ƈړ���ԁ��ʏ��Ԃɂł���
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
    /// �t�F�[�Y���̈ړ�����
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
        _renderer = GetComponent<Renderer>(); //���Renderer(�R���|�[�l���g)���Ƃ��Ă���

        _whiteTurn = GameObject.Find("WhiteText").GetComponent<Text>();
        _blackTurn = GameObject.Find("BlackText").GetComponent<Text>();
        _whiteTurn.color = UnityEngine.Color.yellow; //���Ԃ���n�߂�

        _managerMove = GameObject.Find("Piece").GetComponent<PieceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //���ڂ̃}�E�X�N���b�N�ŋ��I�сA���ڂ̃N���b�N�Ŕz�u�ꏊ���m��A�ړ�����
        //�}�E�X�N���b�N�̒��ł��A���N���b�N���s��ꂽ�ꍇ�Ɉȉ��̏������s��
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
    /// Queen = 5, Rook = 4, Bishop = 3, Knight = 2, Pawn = 1 �Ɛ�����U��
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
    /// �ʏ��ԁA�ړ����
    /// </summary>
    public enum Status
    {
        Normal, //�ʏ���
        Move,   //�ړ����
    }

    /// <summary>
    /// ����or����
    /// </summary>
    public enum Color
    {
        White = 0,
        Black = 1,
    }
}