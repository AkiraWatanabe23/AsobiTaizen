using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//���}�E�X�J�[�\���|�W�V�����̋����ړ������鏈��������̂ɐ錾����K�v������炵��
using System.Runtime.InteropServices;

/// <summary> 
/// ��̈ړ��Ɋւ���X�N���v�g
/// </summary>
public class PieceMove : MonoBehaviour, IPointerClickHandler
{
    //���C���[�}�X�N(Inspector��Layer����I������)
    [SerializeField] LayerMask _tileLayer;
    [SerializeField] LayerMask _whiteLayer;
    [SerializeField] LayerMask _blackLayer;
    /// <summary> ���Ԗڐ��̃J���� </summary>
    public static Camera _camera;
    /// <summary> ����ړ��������Ƀ}�X��collider�̏�ɒu�� </summary>
    [SerializeField] Vector3 _offset = Vector3.up;
    /// <summary> �ʏ��Ԃ̋�̃}�e���A�� </summary>
    [SerializeField] Material _normalMaterial;
    /// <summary> �ړ���Ԃ̋�̃}�e���A�� </summary>
    [SerializeField] Material _moveMaterial;
    Renderer _renderer;
    [Tooltip("�ǂ����̃^�[�����̕\��(��)")] Text _whiteTurn;
    [Tooltip("�ǂ����̃^�[�����̕\��(��)")] Image _whiteTurnPanel;
    [Tooltip("�ǂ����̃^�[�����̕\��(��)")] Text _blackTurn;
    [Tooltip("�ǂ����̃^�[�����̕\��(��)")] Image _blackTurnPanel;
    /// <summary> ������ </summary>
    public PieceColor _color = PieceColor.White;
    /// <summary> ��̏�� </summary>
    public Status _status = Status.Normal;

    //extern...Unity��VisualStudio�ɂ͂Ȃ��@�\(�֐�)���Ƃ��Ă���
    //��L����� : extern...�O���t�@�C��(dll�t�@�C��)�Œ�`����Ă���֐���ϐ����g�p����A�Ƃ�������
    //[DllImport("user32.dll")]...�O�̂ǂ̃t�@�C��(�����[user32.dll])����Ƃ��Ă���̂�
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
        var piece = go.GetComponent<PieceMove>();

        print($"{ name } ��I��");
        piece.ChangeState();
    }

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _camera = GameObject.Find("Camera(black)").GetComponent<Camera>();
        //���^�[���\����Text
        _whiteTurn = GameObject.Find("WhiteText").GetComponent<Text>();
        _blackTurn = GameObject.Find("BlackText").GetComponent<Text>();
        _whiteTurn.color = Color.yellow;
        //���^�[���\����Panel
        _whiteTurnPanel = GameObject.Find("WhiteTurnPanel").GetComponent<Image>();
        _blackTurnPanel = GameObject.Find("BlackTurnPanel").GetComponent<Image>();
        _blackTurnPanel.gameObject.GetComponent<Image>().enabled = false;
        /*��enabled...�I�u�W�F�N�g�̎w�肵��[�R���|�[�l���g]�̃A�N�e�B�u�A��A�N�e�B�u��ύX����
         *  (SetActive�ŃI�u�W�F�N�g���Ƃ��Ă���Ȃ��̂��������)*/
    }

    // Update is called once per frame
    void Update()
    {
        //���N���b�N���s��ꂽ�ꍇ�Ɉȉ��̏������s��
        if (Input.GetMouseButtonDown(0))
        {
            //��ړ���ԂɂȂ��Ă�����
            if (_status == Status.Move)
            {
                //�ړ�����
                if (Move())
                {
                    //�ړ���ԁ��ʏ���
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
        float _rayDistance = 100;
        /* �����̊֐���[Move()]�S�̂Ŏg���ϐ� */

        //���Ԗڐ��̋�̈ړ�
        //���Ԗڐ���Ray�̏���(���D���ꍇ)
        if (Physics.Raycast(_ray, out RaycastHit hit, _rayDistance, _blackLayer))
        {
            GameObject _target = hit.collider.gameObject;

            //Ray�����������I�u�W�F�N�g����������ꍇ�A���D���Ă��̃}�X�Ɉړ�����
            if (_target.tag == "BlackPiece")
            {
                //���̃X�R�A�����Z
                //word.Contains(string)...word�̒��ɁAstring(�w��̕�����)���܂܂�Ă��邩
                if (_target.name.Contains("pawn"))
                {
                    GameManager._scoreWhite += 1;
                    Debug.Log("���� " + GameManager._scoreWhite + " �_");
                }
                else if (_target.name.Contains("knight"))
                {
                    GameManager._scoreWhite += 2;
                    Debug.Log("���� " + GameManager._scoreWhite + " �_");
                }
                else if (_target.name.Contains("bishop"))
                {
                    GameManager._scoreWhite += 3;
                    Debug.Log("���� " + GameManager._scoreWhite + " �_");
                }
                else if (_target.name.Contains("rook"))
                {
                    GameManager._scoreWhite += 4;
                    Debug.Log("���� " + GameManager._scoreWhite + " �_");
                }
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
            GameManager._player = GameManager.Player_Two;

            PhaseChange(_target);
            SetCursorPos(Screen.width/2, Screen.height/2);
            GameManager._state = Phase.Black;

            print($"��� {_target.name} ���Ƃ���");

            if (Physics.Raycast(_ray, out RaycastHit hitTile, _rayDistance, _tileLayer))
            {
                GameObject _hitTile = hitTile.collider.gameObject;
                print($"��� {_hitTile.name} �Ɉړ�����");
            }
            return true;
        }
        //���Ԗڐ���Ray�̏���(�ړ�����)
        else if (Physics.Raycast(_ray, out RaycastHit hit2, _rayDistance, _tileLayer))
        {
            GameObject _target = hit2.collider.gameObject;
            this.transform.position = _target.transform.position + _offset;
            GameManager._player = GameManager.Player_Two;

            PhaseChange(_target);
            SetCursorPos(Screen.width / 2, Screen.height / 2);
            GameManager._state = Phase.Black;

            print($"��� {_target.name} �Ɉړ�����");
            return true;
        }

        //���Ԗڐ��̋�̈ړ�
        //���Ԗڐ���Ray�̏���(���D���ꍇ)
        if (Physics.Raycast(_ray2, out RaycastHit hit3, _rayDistance, _whiteLayer))
        {
            GameObject _target = hit3.collider.gameObject;

            //Ray�����������I�u�W�F�N�g����������ꍇ�A���D���Ă��̃}�X�Ɉړ�����
            if (_target.tag == "WhitePiece")
            {
                //���̃X�R�A�����Z
                if (_target.name.Contains("pawn"))
                {
                    GameManager._scoreBlack += 1;
                    Debug.Log("���� " + GameManager._scoreBlack + " �_");
                }
                else if (_target.name.Contains("knight"))
                {
                    GameManager._scoreBlack += 2;
                    Debug.Log("���� " + GameManager._scoreBlack + " �_");
                }
                else if (_target.name.Contains("bishop"))
                {
                    GameManager._scoreBlack += 3;
                    Debug.Log("���� " + GameManager._scoreBlack + " �_");
                }
                else if (_target.name.Contains("rook"))
                {
                    GameManager._scoreBlack += 4;
                    Debug.Log("���� " + GameManager._scoreBlack + " �_");
                }
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
            GameManager._player = GameManager.Player_One;

            PhaseChange(_target);
            SetCursorPos(Screen.width / 2, Screen.height / 2);
            GameManager._state = Phase.White;

            print($"��� {_target.name} ���Ƃ���");

            if (Physics.Raycast(_ray, out RaycastHit hitTile, _rayDistance, _tileLayer))
            {
                GameObject _hitTile = hitTile.collider.gameObject;
                print($"��� {_hitTile.name} �Ɉړ�����");
            }
            return true;
        }
        //���Ԗڐ���Ray�̏���(�ړ�����)
        else if (Physics.Raycast(_ray2, out RaycastHit hit4, _rayDistance, _tileLayer))
        {
            GameObject _target = hit4.collider.gameObject;
            this.transform.position = _target.transform.position + _offset;
            GameManager._player = GameManager.Player_One;

            PhaseChange(_target);
            SetCursorPos(Screen.width / 2, Screen.height / 2);
            GameManager._state = Phase.White;

            print($"��� {_target.name} �Ɉړ�����");
            return true;
        }
        return false;
    }
    /// <summary>
    /// �}�E�X�N���b�N������(���I�񂾁A��������)���Ɏ��s����鏈��
    /// </summary>
    public void ChangeState() //����E�N���b�N������ƈړ���ԁ��ʏ��Ԃɂł���
    {
        //�ʏ��ԁ��ړ����(��)
        if (_status == Status.Normal && _color == PieceColor.White && GameManager._state == Phase.White)
        {
            _status = Status.Move;
            _renderer.material = _moveMaterial;
        }
        //�ʏ��ԁ��ړ����(��)
        else if (_status == Status.Normal && _color == PieceColor.Black && GameManager._state == Phase.Black)
        {
            _status = Status.Move;
            _renderer.material = _moveMaterial;
        }
        //�ړ���ԁ��ʏ���(��ړ�������̏���)
        else if (_status == Status.Move)
        {
            _status = Status.Normal;
            _renderer.material = _normalMaterial;
        }
    }

    /// <summary>
    /// �^�[���\���̐؂�ւ��A��̈ړ���ԁ��ʏ���
    /// </summary>
    /// <param name="_target"></param>
    public void PhaseChange(GameObject _target)
    {
        switch (_color)
        {
            //case int: �̉�[break;]�܂Ŏ��s�����({ }�͏����Ȃ�)
            case 0: //Color.White                                                                                                                       
                _whiteTurn.color = Color.black;
                _blackTurn.color = Color.yellow;
                _whiteTurnPanel.gameObject.GetComponent<Image>().enabled = false;
                _blackTurnPanel.gameObject.GetComponent<Image>().enabled = true;

                if (_target.tag == "WhitePiece")
                {
                    _status = Status.Normal;
                }
                break;

            case (PieceColor)1: //Color.Black
                _whiteTurn.color = Color.yellow;
                _blackTurn.color = Color.black;
                _whiteTurnPanel.gameObject.GetComponent<Image>().enabled = true;
                _blackTurnPanel.gameObject.GetComponent<Image>().enabled = false;

                if (_target.tag == "BlackPiece")
                {
                    _status = Status.Normal;
                }
                break;
        }
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