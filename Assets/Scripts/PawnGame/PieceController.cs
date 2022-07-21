using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//using System.Runtime.InteropServices;

/// <summary> 
/// �S�Ă̋�ɓ��ꂳ�ꂽ����(��̑I���A�ړ�)
/// </summary>
public class PieceController : MonoBehaviour //IPointerClickHandler
{
    ///// <summary> �v���C���[(��) </summary>
    //public const int _playerOne = 1;
    ///// <summary> �v���C���[(��) </summary>
    //public const int _playerTwo = 2;
    ///// <summary> current(���݂�)�v���C���[ </summary>
    //public int _currentPlayer;
    /// <summary> ��̎�� </summary>
    public Type _type;
    ///// <summary> ���C���[�}�X�N(Inspector��Layer����I������) </summary>
    //[SerializeField] LayerMask _tileLayer;
    //[SerializeField] LayerMask _whiteLayer;
    //[SerializeField] LayerMask _blackLayer;
    ///// <summary> ����ړ���������collider�̏�ɒu�� </summary>
    //[SerializeField] Vector3 _offset = Vector3.up;
    /// <summary> �ʏ��ԁA�ړ���Ԃ̋�̃}�e���A�� </summary>
    [SerializeField] Material _normalMaterial;
    [SerializeField] Material _moveMaterial;
    Renderer _renderer;
    ///// <summary> ���Ԗڐ��̃J���� </summary>
    //Camera _camera;
    /// <summary> ��̏�� </summary>
    public Status _status = Status.Normal;

    bool isMove;

    /// <summary> �ǂ����̃^�[����(��) </summary>
    Text _whiteTurn;
    /// <summary> �ǂ����̃^�[����(��) </summary>
    Text _blackTurn;

    [SerializeField] public Color _state = Color.White;

    PieceManager _managerMove;

    //extern...Unity��VisualStudio�ɂ͂Ȃ��@�\(�֐�)���Ƃ��Ă���(C++�ł����Ɓu::�v�Ɠ����炵��)
    //[DllImport("user32.dll")]...�O�̂ǂ̃t�@�C��(����́uuser32.dll�v)����Ƃ��Ă���̂�
    //SetCursorPos(�֐�)...�w�肵���t�@�C�����̂ǂ̋@�\(�֐�)���g���̂�
    //[DllImport("user32.dll")]
    //public static extern bool SetCursorPos(int x, int y);

    /// <summary>
    /// �}�E�X�N���b�N���s��ꂽ(�ǂ̃}�E�X�N���b�N�ł����s�����)���̏���
    /// </summary>
    /// <param name="eventData"></param>
    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    print($"{ name } ��I��");
    //    ChangeState();
    //}

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

    //public bool Move()
    //{
    //    //�}�E�X�̈ʒu���擾���ARay�ɑ��
    //    Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition); //���C���J����(���Ԗڐ�)����Ray���Ƃ΂�
    //    Ray _ray2 = _camera.ScreenPointToRay(Input.mousePosition);    //second�J����(���Ԗڐ�)����Ray���Ƃ΂�
    //    //Ray�̒���
    //    float _rayDistance = 100;

    //    //���Ԗڐ��̋�̈ړ�
    //    //���Ԗڐ���Ray�̏���(���D���ꍇ)
    //    if (Physics.Raycast(_ray, out RaycastHit hit, _rayDistance, _blackLayer))
    //    {
    //        GameObject _target = hit.collider.gameObject;

    //        //Ray�����������I�u�W�F�N�g���G�̋�����ꍇ�A���D���Ă��̃}�X�Ɉړ�����
    //        if (_target.tag == "BlackPiece")
    //        {
    //            _target.SetActive(false);
    //        }

    //        this.transform.position = _target.transform.position + _offset;
    //        _currentPlayer = _playerTwo;

    //        PhaseChange(_target);
    //        SetCursorPos(950, 400);

    //        print($"Ray �� {_target.name} �Ɉړ�����");
    //        Debug.Log(_currentPlayer);
    //        return true;
    //    }
    //    //���Ԗڐ���Ray�̏���(�ړ��̂�)
    //    else if (Physics.Raycast(_ray, out RaycastHit hit2, _rayDistance, _tileLayer))
    //    {
    //        GameObject _target = hit2.collider.gameObject;
    //        this.transform.position = _target.transform.position + _offset;
    //        _currentPlayer = _playerTwo;

    //        PhaseChange(_target);
    //        SetCursorPos(950, 400);

    //        print($"Ray �� {_target.name} �Ɉړ�����"); // print($"..."); ���� Debug.Log("..."); �Ɠ���
    //        Debug.Log(_currentPlayer);
    //        return true;
    //    }

    //    //���Ԗڐ��̋�̈ړ�
    //    //���Ԗڐ���Ray�̏���(���D���ꍇ)
    //    if (Physics.Raycast(_ray2, out RaycastHit hit3, _rayDistance, _whiteLayer))
    //    {
    //        GameObject _target = hit3.collider.gameObject;

    //        //Ray�����������I�u�W�F�N�g���G�̋�����ꍇ�A���D���Ă��̃}�X�Ɉړ�����
    //        if (_target.tag == "WhitePiece")
    //        {
    //            _target.SetActive(false);
    //        }

    //        this.transform.position = _target.transform.position + _offset;
    //        _currentPlayer = _playerOne;

    //        PhaseChange(_target);
    //        SetCursorPos(950, 400);

    //        print($"Ray �� {_target.name} �Ɉړ�����");
    //        Debug.Log(_currentPlayer);
    //        return true;
    //    }
    //    //���Ԗڐ���Ray�̏���(�ړ��̂�)
    //    else if (Physics.Raycast(_ray2, out RaycastHit hit4, _rayDistance, _tileLayer))
    //    {
    //        GameObject _target = hit4.collider.gameObject;
    //        this.transform.position = _target.transform.position + _offset;
    //        _currentPlayer = _playerOne;

    //        PhaseChange(_target);
    //        SetCursorPos(950, 400);

    //        print($"Ray �� {_target.name} �Ɉړ�����");
    //        Debug.Log(_currentPlayer);
    //        return true;
    //    }

    //    return false;
    //}

    /// <summary>
    /// �t�F�[�Y���̈ړ�����
    /// </summary>
    /// <param name="_target"></param>
    public void PhaseChange(GameObject _target)
    {
        switch (_state)
        {
            case 0: //white
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
            case (Color)1: //black
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

    void Start()
    {
        _renderer = GetComponent<Renderer>(); //���Renderer(�R���|�[�l���g)���Ƃ��Ă���

        _whiteTurn = GameObject.Find("WhiteText").GetComponent<Text>();
        _blackTurn = GameObject.Find("BlackText").GetComponent<Text>();
        _whiteTurn.color = UnityEngine.Color.yellow; //���Ԃ���n�߂�
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

    public enum Color
    {
        White = 0,
        Black = 1,
    }
}