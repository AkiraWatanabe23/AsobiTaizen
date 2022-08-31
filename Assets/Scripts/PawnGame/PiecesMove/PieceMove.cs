using System;
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
    [Tooltip("���Ԗڐ��̃J����")] public static Camera _camera;
    [Tooltip("�ʏ��Ԃ̃}�e���A��"), SerializeField] Material _normalMaterial;
    [Tooltip("�ړ���Ԃ̃}�e���A��"), SerializeField] Material _moveMaterial;
    Renderer _renderer;
    [Tooltip("�ǂ����̃^�[����(��)")] Text _whiteTurn;
    [Tooltip("�ǂ����̃^�[����(��)")] Image _whiteTurnPanel;
    [Tooltip("�ǂ����̃^�[����(��)")] Text _blackTurn;
    [Tooltip("�ǂ����̃^�[����(��)")] Image _blackTurnPanel;
    [Tooltip("��������enum")] public PieceColor _color = PieceColor.White;
    [Tooltip("��̏�Ԃ�enum")] public Status _status = Status.Normal;
    [Tooltip("��̎�ނ�enum")] public PieceType _type;
    RaycastHit _hit;
    [SerializeField] Vector3 _offset = Vector3.up;
    //���C���[�}�X�N(Inspector��Layer����I������)
    [SerializeField] LayerMask _tileLayer;
    [SerializeField] LayerMask _whiteLayer;
    [SerializeField] LayerMask _blackLayer;
    //��̓��_(Inspector�Őݒ�)
    [SerializeField] public int _getScore;
    //�ړ��\�͈͂̒T��
    [SerializeField] MasuSearch _search;
    Pawn _pawn;
    Knight _knight;
    Bishop _bishop;
    Rook _rook;
    Queen _queen;
    public int _moveCount = 0;

    //extern...Unity��VisualStudio�ɂ͂Ȃ��@�\(�֐�)���Ƃ��Ă���{����:�O���t�@�C��(dll�t�@�C��)�Œ�`����Ă���֐���ϐ����g�p����A�Ƃ�������}
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
        var piece = go.GetComponent<PieceMove>();

        print($"{ name } ��I��");
        piece.ChangeState();
    }

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        //���^�[���\����Text
        _whiteTurn = GameObject.Find("WhiteText").GetComponent<Text>();
        _blackTurn = GameObject.Find("BlackText").GetComponent<Text>();
        _whiteTurn.color = Color.yellow;
        //���^�[���\����Panel
        _whiteTurnPanel = GameObject.Find("WhiteTurnPanel").GetComponent<Image>();
        _blackTurnPanel = GameObject.Find("BlackTurnPanel").GetComponent<Image>();
        _blackTurnPanel.gameObject.GetComponent<Image>().enabled = false;
        _camera = GameObject.Find("Camera(black)").GetComponent<Camera>();
        /*��enabled...�I�u�W�F�N�g�̎w�肵��[�R���|�[�l���g(�����Image)]�̃A�N�e�B�u�A��A�N�e�B�u��ύX����
         *  SetActive(false)�ŃI�u�W�F�N�g���Ƃ��Ă���Ȃ��̂��������*/
    }

    // Update is called once per frame
    void Update()
    {
        //���N���b�N���s��ꂽ�ꍇ�̏���
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
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Ray _ray2 = _camera.ScreenPointToRay(Input.mousePosition);
        float _rayDistance = 100;

        //���Ԗڐ��̋�̈ړ�
        //���Ԗڐ���Ray�̏���(���D���ꍇ)
        if (Physics.Raycast(_ray, out _hit, _rayDistance, _blackLayer))
        {
            GameObject _target = _hit.collider.gameObject;
            int _targetScore = _target.GetComponent<PieceMove>()._getScore; //�Ƃ���������Ă���_getScore���擾

            if (_target.tag == "BlackPiece")
            {
                //���̃X�R�A�����Z
                GameManager._scoreWhite += _targetScore;
                //�Տ�ɂ���G��̃J�E���g�����炵�āA���j�󂷂�
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
        else if (Physics.Raycast(_ray, out _hit, _rayDistance, _tileLayer))
        {
            GameObject _target = _hit.collider.gameObject;
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
        if (Physics.Raycast(_ray2, out _hit, _rayDistance, _whiteLayer))
        {
            GameObject _target = _hit.collider.gameObject;
            int _targetScore = _target.GetComponent<PieceMove>()._getScore; //�Ƃ���������Ă���_getScore���擾

            if (_target.tag == "WhitePiece")
            {
                //���̃X�R�A�����Z
                GameManager._scoreBlack += _targetScore;
                //�Տ�ɂ����̃J�E���g�����炵�āA���j�󂷂�
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
        else if (Physics.Raycast(_ray2, out _hit, _rayDistance, _tileLayer))
        {
            GameObject _target = _hit.collider.gameObject;
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
            _search.gameObject.GetComponent<MasuSearch>()._piece = this;
            _search.gameObject.GetComponent<MasuSearch>()._pieceInfo = gameObject;
            _pawn.gameObject.GetComponent<Pawn>()._pieceInfo = gameObject;
            _knight.gameObject.GetComponent<Knight>()._pieceInfo = gameObject;
            _bishop.gameObject.GetComponent<Bishop>()._pieceInfo = gameObject;
            _rook.gameObject.GetComponent<Rook>()._pieceInfo = gameObject;
            _queen.gameObject.GetComponent<Queen>()._pieceInfo = gameObject;
        }
        //�ʏ��ԁ��ړ����(��)
        else if (_status == Status.Normal && _color == PieceColor.Black && GameManager._state == Phase.Black)
        {
            _status = Status.Move;
            _renderer.material = _moveMaterial;
            _search.gameObject.GetComponent<MasuSearch>()._piece = this;
            _search.gameObject.GetComponent<MasuSearch>()._pieceInfo = gameObject;
            _pawn.gameObject.GetComponent<Pawn>()._pieceInfo = gameObject;
            _knight.gameObject.GetComponent<Knight>()._pieceInfo = gameObject;
            _bishop.gameObject.GetComponent<Bishop>()._pieceInfo = gameObject;
            _rook.gameObject.GetComponent<Rook>()._pieceInfo = gameObject;
            _queen.gameObject.GetComponent<Queen>()._pieceInfo = gameObject;
        }
        //�ړ���ԁ��ʏ���(��ړ�������̋��ʏ���)
        else if (_status == Status.Move)
        {
            _moveCount++;
            _status = Status.Normal;
            _renderer.material = _normalMaterial;
            for (int i = 0; i < _search.gameObject.GetComponent<MasuSearch>()._movableTile.Count; i++)
            {
                if (_search.gameObject.GetComponent<MasuSearch>()._movableTile[i].tag == "Tile")
                {
                    _search.gameObject.GetComponent<MasuSearch>()._tile.Add(_search.gameObject.GetComponent<MasuSearch>()._movableTile[i]);
                }
            }
            _search.gameObject.GetComponent<MasuSearch>()._movableTile.Clear();
            _search.gameObject.GetComponent<MasuSearch>()._piece = null;
            _search.gameObject.GetComponent<MasuSearch>()._pieceInfo = null;
            _pawn.gameObject.GetComponent<Pawn>()._pieceInfo = null;
            _knight.gameObject.GetComponent<Knight>()._pieceInfo = null;
            _bishop.gameObject.GetComponent<Bishop>()._pieceInfo = null;
            _rook.gameObject.GetComponent<Rook>()._pieceInfo = null;
            _queen.gameObject.GetComponent<Queen>()._pieceInfo = null;
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
            case PieceColor.White:
                _whiteTurn.color = Color.black;
                _blackTurn.color = Color.yellow;
                _whiteTurnPanel.gameObject.GetComponent<Image>().enabled = false;
                _blackTurnPanel.gameObject.GetComponent<Image>().enabled = true;
                if (_target.tag == "WhitePiece")
                {
                    _status = Status.Normal;
                }
                break;

            case PieceColor.Black:
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
        Normal,
        Move,
    }

    /// <summary>
    /// ����or����
    /// </summary>
    public enum PieceColor
    {
        White = 0,
        Black = 1,
    }

    /// <summary>
    /// ��̎��
    /// </summary>
    public enum PieceType
    {
        Pawn = 1,
        Knight,
        Bishop,
        Rook,
        Queen,
    }
}