using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//���}�E�X�J�[�\���|�W�V�����̋����ړ�������̂ɐ錾����K�v������(�炵��)
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
    /// <summary> ���C���[�}�X�N(Inspector��Layer����I������) </summary>
    [SerializeField] LayerMask _tileLayer;
    [SerializeField] LayerMask _whiteLayer;
    [SerializeField] LayerMask _blackLayer;
    /// <summary> ���Ԗڐ��̃J���� </summary>
    public static Camera _camera;
    /// <summary> ����ړ���������collider�̏�ɒu�� </summary>
    [SerializeField] Vector3 _offset = Vector3.up;
    /// <summary> �ʏ��ԁA�ړ���Ԃ̋�̃}�e���A�� </summary>
    [SerializeField] Material _normalMaterial;
    [SerializeField] Material _moveMaterial;
    Renderer _renderer;
    /// <summary> �ǂ����̃^�[�����̕\��(��) </summary>
    Text _whiteTurn;
    /// <summary> �ǂ����̃^�[�����̕\��(��) </summary>
    Text _blackTurn;
    /// <summary> ������ </summary>
    public PieceColor _state = PieceColor.White;
    /// <summary> ��̎�� </summary>
    public Type _type;
    /// <summary> ��̏�� </summary>
    public Status _status = Status.Normal;
    bool isMove = false;

    //extern...Unity��VisualStudio�ɂ͂Ȃ��@�\(�֐�)���Ƃ��Ă���(C++�ł����Ɓu::�v�Ɠ����炵��)
    //��L����� : extern...�O���t�@�C��(dll�t�@�C��)�Œ�`����Ă���֐���ϐ����g�p����A�Ƃ�������
    //[DllImport("user32.dll")]...�O�̂ǂ̃t�@�C��(����́uuser32.dll�v)����Ƃ��Ă���̂�
    //SetCursorPos(�֐�)...�w�肵���t�@�C�����̂ǂ̋@�\(�֐�)���g���̂�
    //�ȉ�2�s�̓Z�b�g�ŏ����Ȃ��ƃR���p�C���G���[����
    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int x, int y);

    /// <summary>
    /// �}�E�X�N���b�N���s��ꂽ(�ǂ̃}�E�X�N���b�N�ł����s�����)���̏���
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        var go = eventData.pointerCurrentRaycast.gameObject;
        //���J�������猻�݂̃}�E�X�J�[�\���̈ʒu��Ray���΂��A���������I�u�W�F�N�g��������
        var piece = go.GetComponent<PieceController>();
        //�����������I�u�W�F�N�g�́uPieceController�v�������Ă���

        print($"{ name } ��I��");
        piece.ChangeState();
    }

    void Start()
    {
        _renderer = GetComponent<Renderer>(); //���Renderer(�R���|�[�l���g)���Ƃ��Ă���
        _camera = GameObject.Find("Camera(black)").GetComponent<Camera>(); //���Ԗڐ��̃J�������擾
        //���^�[���\����Text
        _whiteTurn = GameObject.Find("WhiteText").GetComponent<Text>();
        _blackTurn = GameObject.Find("BlackText").GetComponent<Text>();
        _whiteTurn.color = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {
        //���ڂ̃}�E�X�N���b�N�ŋ��I�сA���ڂ̃N���b�N�Ŕz�u�ꏊ���m��A�ړ�����
        //���N���b�N���s��ꂽ�ꍇ�Ɉȉ��̏������s��
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
        //�}�E�X�̈ʒu���擾���ARay�ɑ��
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition); //���ԃJ��������Ray���Ƃ΂�
        Ray _ray2 = _camera.ScreenPointToRay(Input.mousePosition);    //���ԃJ��������Ray���Ƃ΂�
        float _rayDistance = 100; //Ray�̒���

        //���Ԗڐ��̋�̈ړ�
        //���Ԗڐ���Ray�̏���(���D���ꍇ)
        if (Physics.Raycast(_ray, out RaycastHit hit, _rayDistance, _blackLayer))
        {
            GameObject _target = hit.collider.gameObject;

            //Ray�����������I�u�W�F�N�g���G�̋�����ꍇ�A���D���Ă��̃}�X�Ɉړ�����
            if (_target.tag == "BlackPiece")
            {
                //���̃X�R�A�����Z
                //�Ƃ�����̎�ނɂ���Ċl������_�����قȂ�
                //word.Contains(string)...word�̒��ɁAstring(������)���܂܂�Ă��邩
                //�Ƃ�����|�[���Ȃ�
                if (_target.name.Contains("pawn"))
                {
                    GameManager._scoreWhite += 1;
                    Debug.Log("���� " + GameManager._scoreWhite + " �_");
                }
                //�i�C�g�Ȃ�
                else if (_target.name.Contains("knight"))
                {
                    GameManager._scoreWhite += 2;
                    Debug.Log("���� " + GameManager._scoreWhite + " �_");
                }
                //�r�V���b�v�Ȃ�
                else if (_target.name.Contains("bishop"))
                {
                    GameManager._scoreWhite += 3;
                    Debug.Log("���� " + GameManager._scoreWhite + " �_");
                }
                //���[�N�Ȃ�
                else if (_target.name.Contains("rook"))
                {
                    GameManager._scoreWhite += 4;
                    Debug.Log("���� " + GameManager._scoreWhite + " �_");
                }
                //�N�C�[���Ȃ�
                else if (_target.name.Contains("queen"))
                {
                    GameManager._scoreWhite += 5;
                    Debug.Log("���� " + GameManager._scoreWhite + " �_");
                }
                //�Տ�ɂ���G��̃J�E���g�����炷
                GameManager._bPieceCount--;
                Destroy(_target);
            }

            this.transform.position = _target.transform.position + _offset;
            _currentPlayer = _playerTwo;

            PhaseChange(_target);
            SetCursorPos(950, 400); //����ړ���������A�}�E�X�J�[�\�����Q�[����ʂ̒����ӂ�ɋ����ړ�������

            //_state = Color.Black;
            GameManager._state = Phase.Black;
            print($"��� {_target.name} ���Ƃ���");

            if (Physics.Raycast(_ray, out RaycastHit hitTile, _rayDistance, _tileLayer))
            {
                GameObject _hitTile = hitTile.collider.gameObject;
                print($"��� {_hitTile.name} �Ɉړ�����");
            }
            return true;
        }
        //���Ԗڐ���Ray�̏���(�ړ��̂�)
        else if (Physics.Raycast(_ray, out RaycastHit hit2, _rayDistance, _tileLayer))
        {
            GameObject _target = hit2.collider.gameObject;
            this.transform.position = _target.transform.position + _offset;
            _currentPlayer = _playerTwo;

            PhaseChange(_target);
            SetCursorPos(950, 400); //����ړ���������A�}�E�X�J�[�\�����Q�[����ʂ̒����ӂ�ɋ����ړ�������

            //_state = Color.Black;
            GameManager._state = Phase.Black;

            print($"��� {_target.name} �Ɉړ�����"); // print($"..."); ���� Debug.Log("..."); �Ɠ���
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
                //���̃X�R�A�����Z
                //�Ƃ�����̎�ނɂ���Ċl������_�����قȂ�
                //�Ƃ�����|�[���Ȃ�
                if (_target.name.Contains("pawn"))
                {
                    GameManager._scoreBlack += 1;
                    Debug.Log("���� " + GameManager._scoreBlack + " �_");
                }
                //�i�C�g�Ȃ�
                else if (_target.name.Contains("knight"))
                {
                    GameManager._scoreBlack += 2;
                    Debug.Log("���� " + GameManager._scoreBlack + " �_");
                }
                //�r�V���b�v�Ȃ�
                else if (_target.name.Contains("bishop"))
                {
                    GameManager._scoreBlack += 3;
                    Debug.Log("���� " + GameManager._scoreBlack + " �_");
                }
                //���[�N�Ȃ�
                else if (_target.name.Contains("rook"))
                {
                    GameManager._scoreBlack += 4;
                    Debug.Log("���� " + GameManager._scoreBlack + " �_");
                }
                //�N�C�[���Ȃ�
                else if (_target.name.Contains("queen"))
                {
                    GameManager._scoreBlack += 5;
                    Debug.Log("���� " + GameManager._scoreBlack + " �_");
                }

                //�Տ�ɂ����̃J�E���g�����炷
                GameManager._wPieceCount--;
                Destroy(_target);
            }

            this.transform.position = _target.transform.position + _offset;
            _currentPlayer = _playerOne;

            PhaseChange(_target);
            SetCursorPos(950, 400); //����ړ���������A�}�E�X�J�[�\�����Q�[����ʂ̒����ӂ�ɋ����ړ�������

            //_state = Color.White;
            GameManager._state = Phase.White;
            print($"��� {_target.name} ���Ƃ���");

            if (Physics.Raycast(_ray, out RaycastHit hitTile, _rayDistance, _tileLayer))
            {
                GameObject _hitTile = hitTile.collider.gameObject;
                print($"��� {_hitTile.name} �Ɉړ�����");
            }
            return true;
        }
        //���Ԗڐ���Ray�̏���(�ړ��̂�)
        else if (Physics.Raycast(_ray2, out RaycastHit hit4, _rayDistance, _tileLayer))
        {
            GameObject _target = hit4.collider.gameObject;
            this.transform.position = _target.transform.position + _offset;
            _currentPlayer = _playerOne;

            PhaseChange(_target);
            SetCursorPos(950, 400); //����ړ���������A�}�E�X�J�[�\�����Q�[����ʂ̒����ӂ�ɋ����ړ�������

            //_state = Color.White;
            GameManager._state = Phase.White;

            print($"��� {_target.name} �Ɉړ�����");
            return true;
        }
        return false;
    }
    /// <summary>
    /// �}�E�X�N���b�N������(���I�񂾁A��������)���Ɏ��s����鏈��
    /// </summary>
    public void ChangeState() //�E�N���b�N������ƈړ���ԁ��ʏ��Ԃɂł���
    {
        //����̐؂�ւ�
        if (_status == Status.Normal && _state == PieceColor.White && GameManager._state == Phase.White)
        {
            _status = Status.Move;
            _renderer.material = _moveMaterial;
            isMove = true;
        }
        //����̐؂�ւ�
        else if (_status == Status.Normal && _state == PieceColor.Black && GameManager._state == Phase.Black)
        {
            _status = Status.Move;
            _renderer.material = _moveMaterial;
            isMove = true;
        }
        //�ړ���ԁ��ʏ���(��ړ�������̏���)
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
        switch (_state)
        {
            case 0: //Color.White
                _whiteTurn.color = Color.white;
                _blackTurn.color = Color.yellow;
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

            case (PieceColor)1: //Color.Black
                _whiteTurn.color = Color.yellow;
                _blackTurn.color = Color.white;
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
    public enum PieceColor
    {
        White = 0,
        Black = 1,
    }
}