using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Runtime.InteropServices;

/// <summary> 
/// �S�Ă̋�ɓ��ꂳ�ꂽ����(��̑I���A�ړ�)
/// </summary>
public class PieceController : MonoBehaviour, IPointerClickHandler
{
    /// <summary> �v���C���[(��) </summary>
    public const int _playerOne = 1;
    /// <summary> �v���C���[(��) </summary>
    public const int _playerTwo = 2;
    /// <summary> current(���݂�)�v���C���[ </summary>
    public int _currentPlayer;
    /// <summary> ��̎�� </summary>
    public Type _type;
    /// <summary> Ray�̒��� </summary>
    [SerializeField] float _rayDistance = 100;
    /// <summary> ���C���[�}�X�N(Inspector��Layer����I������) </summary>
    [SerializeField] LayerMask _tileLayer;
    [SerializeField] LayerMask _whiteLayer;
    [SerializeField] LayerMask _blackLayer;
    /// <summary> ����ړ���������collider�̏�ɒu�� </summary>
    [SerializeField] Vector3 _offset = Vector3.up;
    /// <summary> �ʏ��ԁA�ړ���Ԃ̋�̃}�e���A�� </summary>
    [SerializeField] Material _normalMaterial;
    [SerializeField] Material _moveMaterial;
    Renderer _renderer;
    /// <summary> ���Ԗڐ��̃J���� </summary>
    Camera _camera;
    /// <summary> ��̏�� </summary>
    public Status _status = Status.Normal;

    /// <summary> �ǂ����̃^�[����(��) </summary>
    Text _whiteTurn;
    /// <summary> �ǂ����̃^�[����(��) </summary>
    Text _blackTurn;

    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int x, int y);

    /// <summary>
    /// �}�E�X�N���b�N���s��ꂽ(�ǂ̃}�E�X�N���b�N�ł����s�����)���̏���
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        print($"{ name } ��I��");
        ChangeState();
    }

    /// <summary>
    /// �}�E�X�N���b�N���������Ɏ��s����鏈��
    /// </summary>
    void ChangeState() //�E�N���b�N������ƈړ���ԁ��ʏ��Ԃɂł���
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
        //�}�E�X�̈ʒu���擾���ARay�ɑ��
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition); //���C���J����(���Ԗڐ�)����Ray���Ƃ΂�
        Ray _ray2 = _camera.ScreenPointToRay(Input.mousePosition);    //second�J����(���Ԗڐ�)����Ray���Ƃ΂�

        //���Ԗڐ��̋�̈ړ�
        //���Ԗڐ���Ray�̏���(���D���ꍇ)
        if (Physics.Raycast(_ray, out RaycastHit hit, _rayDistance, _blackLayer))
        {
            GameObject _target = hit.collider.gameObject;

            //Ray�����������I�u�W�F�N�g���G�̋�����ꍇ�A���D���Ă��̃}�X�Ɉړ�����
            if (_target.tag == "BlackPiece")
            {
                _target.SetActive(false);
            }

            this.transform.position = _target.transform.position + _offset;
            _currentPlayer = _playerTwo;
            _whiteTurn.color = Color.white;
            _blackTurn.color = Color.yellow;
            SetCursorPos(950, 400);

            print($"Ray �� {_target.name} �Ɉړ�����");
            Debug.Log(_currentPlayer);
            return true;
        }
        //���Ԗڐ���Ray�̏���(�ړ��̂�)
        else if (Physics.Raycast(_ray, out RaycastHit hit2, _rayDistance, _tileLayer))
        {
            GameObject _target = hit2.collider.gameObject;
            this.transform.position = _target.transform.position + _offset;
            _currentPlayer = _playerTwo;
            _whiteTurn.color = Color.white;
            _blackTurn.color = Color.yellow;
            SetCursorPos(950, 400);

            print($"Ray �� {_target.name} �Ɉړ�����"); // print($"..."); ���� Debug.Log("..."); �Ɠ���
            Debug.Log(_currentPlayer);
            return true;
        }

        //���Ԗڐ��̋�̈ړ�
        //���Ԗڐ���Ray�̏���(���D���ꍇ)
        if (Physics.Raycast(_ray2, out RaycastHit hit3, _rayDistance, _whiteLayer))
        {
            GameObject _target = hit3.collider.gameObject;

            //Ray�����������I�u�W�F�N�g���G�̋�����ꍇ�A���D���Ă��̃}�X�Ɉړ�����
            if (_target.tag == "WhitePiece")
            {
                _target.SetActive(false);
            }

            this.transform.position = _target.transform.position + _offset;
            _currentPlayer = _playerOne;
            _whiteTurn.color = Color.yellow;
            _blackTurn.color = Color.white;
            SetCursorPos(950, 400);

            print($"Ray �� {_target.name} �Ɉړ�����");
            Debug.Log(_currentPlayer);
            return true;
        }
        //���Ԗڐ���Ray�̏���(�ړ��̂�)
        else if (Physics.Raycast(_ray2, out RaycastHit hit4, _rayDistance, _tileLayer))
        {
            GameObject _target = hit4.collider.gameObject;
            this.transform.position = _target.transform.position + _offset;
            _currentPlayer = _playerOne;
            _whiteTurn.color = Color.yellow;
            _blackTurn.color = Color.white;
            SetCursorPos(950, 400);

            print($"Ray �� {_target.name} �Ɉړ�����");
            Debug.Log(_currentPlayer);
            return true;
        }

        return false;
    }

    void Start()
    {
        _currentPlayer = _playerOne;                                       //���Ԃ���n�߂�
        _camera = GameObject.Find("Camera(black)").GetComponent<Camera>(); //���Ԗڐ��̃J�����������Ă���
        _renderer = GetComponent<Renderer>();                              //���Renderer(�R���|�[�l���g)���Ƃ��Ă���

        _whiteTurn = GameObject.Find("WhiteText").GetComponent<Text>();
        _blackTurn = GameObject.Find("BlackText").GetComponent<Text>();
        _whiteTurn.color = Color.yellow;                                   //���Ԃ���n�߂�
        Debug.Log(Input.mousePosition);
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
                if (Move())
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
}