using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    [SerializeField] PieceManager _piece;
    [Tooltip("��̈ړ���")] public int _moveCount = 0;
    public GameObject _currentPieceTile;
    public GameObject _movedPieceTile;
    [SerializeField] Promotion _promQ;
    [SerializeField] Promotion _promR;
    [SerializeField] Promotion _promB;
    [SerializeField] Promotion _promK;

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

        print($"{ name } ��I��");
        go.GetComponent<PieceMove>().ChangeState();

        //_movedPieceTile = null �ŋ�̍đI�����o����悤�ɂ���
        if (_status == Status.Normal && _currentPieceTile == _movedPieceTile)
        {
            _movedPieceTile = null;
        }
    }

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _camera = GameObject.Find("Camera(black)").GetComponent<Camera>();

        if (Physics.Raycast(gameObject.transform.position, Vector3.down, out _hit, 10))
        {
            _currentPieceTile = _hit.collider.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //���N���b�N�őI���A�ړ�
        if (Input.GetMouseButtonDown(0))
        {
            //��ړ���ԂɂȂ��Ă�����
            if (_status == Status.Move)
            {
                //�ړ�����
                if (Move()) /*��return �̒l�� true ��������*/
                {
                    //�ړ���ԁ��ʏ���
                    ChangeState();
                }
            }
        }
        //�E�N���b�N�Ŕ�I����ԂɕύX
        else if (Input.GetMouseButtonDown(1) && _status == Status.Move)
        {
            Debug.Log("�I�ђ���");

            //�}�X�����̏�Ԃɖ߂�
            for (int i = 0; i < _search._movableTile.Count; i++)
            {
                if (_search._movableTile[i].tag == "Tile")
                {
                    _search._movableTile[i].GetComponent<MeshRenderer>().enabled = false;
                    _search._tile.Add(_search._movableTile[i]);
                }
            }
            foreach (var tiles in _search._tile)
            {
                tiles.GetComponent<Collider>().enabled = true;
            }

            foreach (var piece in _search._immovablePieces)
            {
                piece.GetComponent<Collider>().enabled = true;
            }
            _search._immovablePieces.Clear();

            if (gameObject.tag == "WhitePiece")
            {
                foreach (var pieces in _piece._whitePieces)
                {
                    pieces.GetComponent<Collider>().enabled = true;
                }
                _piece._whitePieces.Add(gameObject);
            }
            else if (gameObject.tag == "BlackPiece")
            {
                foreach (var pieces in _piece._blackPieces)
                {
                    pieces.GetComponent<Collider>().enabled = true;
                }
                _piece._blackPieces.Add(gameObject);
            }

            //�l������l��Ȃ������ꍇ��List�ɖ߂�
            if (_piece._getablePieces != null)
            {
                _piece.UnGetPiece();
            }

            //��̏�Ԃ����Ƃɖ߂�
            _status = Status.Normal;
            _renderer.material = _normalMaterial;
            _search._movableTile.Clear();
            _search._piece = null;
            _search._pieceInfo = null;
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
            SetCursorPos(Screen.width / 2, Screen.height / 2);
            GameManager._phase = Phase.Black;

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
            foreach (var i in _search._movableTile)
            {
                GameObject _target = _hit.collider.gameObject;
                if (_target == i.gameObject)
                {
                    this.transform.position = _target.transform.position + _offset;
                    GameManager._player = GameManager.Player_Two;

                    PhaseChange(_target);
                    SetCursorPos(Screen.width / 2, Screen.height / 2);
                    GameManager._phase = Phase.Black;

                    print($"��� {_target.name} �Ɉړ�����");
                }
                else
                {
                    Debug.Log("�w�肵���}�X�ɂ͓����܂���");
                }
            }
            _movedPieceTile = _hit.collider.gameObject;
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
            GameManager._phase = Phase.White;

            print($"��� {_target.name} ���Ƃ���");

            if (Physics.Raycast(_ray, out RaycastHit hitTile, _rayDistance, _tileLayer))
            {
                GameObject _hitTile = hitTile.collider.gameObject;
                print($"��� {_hitTile.name} �Ɉړ�����");
            }

            if (Physics.Raycast(gameObject.transform.position, Vector3.down, out _hit, 10))
            {
                _movedPieceTile = _hit.collider.gameObject;
            }
            return true;
        }
        //���Ԗڐ���Ray�̏���(�ړ�����)
        else if (Physics.Raycast(_ray2, out _hit, _rayDistance, _tileLayer))
        {
            foreach (var i in _search._movableTile)
            {
                GameObject _target = _hit.collider.gameObject;
                if (_target == i.gameObject)
                {
                    this.transform.position = _target.transform.position + _offset;
                    GameManager._player = GameManager.Player_One;

                    PhaseChange(_target);
                    SetCursorPos(Screen.width / 2, Screen.height / 2);
                    GameManager._phase = Phase.White;

                    print($"��� {_target.name} �Ɉړ�����");
                }
                else
                {
                    Debug.Log("�w�肵���}�X�ɂ͓����܂���");
                }
            }
            _movedPieceTile = _hit.collider.gameObject;
            return true;
        }
        return false;
    }

    /// <summary> 
    /// �}�E�X�N���b�N������(���I�񂾁A��������)���Ɏ��s����鏈��
    /// </summary>
    public void ChangeState() //����E�N���b�N������ƑI����ԁ��ʏ��Ԃɂł���
    {
        //�ʏ��ԁ��I�����(��)
        if (_status == Status.Normal && _color == PieceColor.White && GameManager._phase == Phase.White && _currentPieceTile != _movedPieceTile)
        {
            if (Physics.Raycast(gameObject.transform.position, Vector3.down, out _hit, 10))
            {
                _currentPieceTile = _movedPieceTile = _hit.collider.gameObject;
            }
            _status = Status.Move;
            _renderer.material = _moveMaterial;
            _search._piece = this;
            _search._pieceInfo = gameObject;
            _piece._whitePieces.Remove(gameObject);
        }
        //�ʏ��ԁ��I�����(��)
        else if (_status == Status.Normal && _color == PieceColor.Black && GameManager._phase == Phase.Black && _currentPieceTile != _movedPieceTile)
        {
            if (Physics.Raycast(gameObject.transform.position, Vector3.down, out _hit, 10))
            {
                _currentPieceTile = _movedPieceTile = _hit.collider.gameObject;
            }
            _status = Status.Move;
            _renderer.material = _moveMaterial;
            _search._piece = this;
            _search._pieceInfo = gameObject;
            _piece._blackPieces.Remove(gameObject);
        }
        //�I����ԁ��ʏ���(��ړ�������̋��ʏ���)
        else if (_status == Status.Move)
        {
            //��̈ړ��񐔂����Z����(�|�[���̈ړ��p)
            if (_currentPieceTile != _movedPieceTile && _movedPieceTile.tag == "Tile")
            {
                _moveCount++;
            }

            //�v�����[�V�����ւ̈ڍs(�|�[���̂�)
            if (gameObject.name.Contains("pawn"))
            {
                if (gameObject.tag == "WhitePiece" && int.Parse(_movedPieceTile.name[1].ToString()) == 8)
                {
                    _promQ._promWhite = _promR._promWhite = _promB._promWhite = _promK._promWhite = gameObject;
                    _piece._promImage.gameObject.SetActive(true);
                }
                else if (gameObject.tag == "BlackPiece" && int.Parse(_movedPieceTile.name[1].ToString()) == 1)
                {
                    _promQ._promBlack = _promR._promBlack = _promB._promBlack = _promK._promBlack = gameObject;
                    _piece._promImage.gameObject.SetActive(true);
                }
            }

            //�}�X�����̏�Ԃɖ߂�
            for (int i = 0; i < _search._movableTile.Count; i++)
            {
                if (_search._movableTile[i].tag == "Tile")
                {
                    _search._movableTile[i].GetComponent<MeshRenderer>().enabled = false;
                    _search._tile.Add(_search._movableTile[i]);
                }
            }
            foreach (var tiles in _search._tile)
            {
                tiles.GetComponent<Collider>().enabled = true;
            }

            foreach (var piece in _search._immovablePieces)
            {
                piece.GetComponent<Collider>().enabled = true;
            }
            _search._immovablePieces.Clear();

            //Collider off �ɂ���������Ƃɖ߂�
            foreach (var pieces in _piece._whitePieces)
            {
                if (pieces != null)
                {
                    pieces.GetComponent<Collider>().enabled = true;
                }
            }
            foreach (var pieces in _piece._blackPieces)
            {
                if (pieces != null)
                {
                    pieces.GetComponent<Collider>().enabled = true;
                }
            }
            if (gameObject.tag == "WhitePiece")
            {
                _piece._whitePieces.Add(gameObject);
            }
            else if (gameObject.tag == "BlackPiece")
            {
                _piece._blackPieces.Add(gameObject);
            }

            //����l�������Ɉړ������}�X��_movedPieceTile�ɑ������
            if (Physics.Raycast(gameObject.transform.position, Vector3.down, out _hit, 10))
            {
                _movedPieceTile = _hit.collider.gameObject;
            }
            else
            {
                Debug.Log("none");
            }

            //�l������l��Ȃ������ꍇ��List�ɖ߂�
            if (_piece._getablePieces != null)
            {
                _piece.UnGetPiece();
            }

            //��̏�Ԃ����Ƃɖ߂�
            _status = Status.Normal;
            _renderer.material = _normalMaterial;
            _search._movableTile.Clear();
            _search._piece = null;
            _search._pieceInfo = null;
        }
    }

    /// <summary> 
    /// �v�����[�V������Instantiate���ꂽ��ɃX�N���v�g���A�T�C������
    /// </summary>
    public void PromAssign()
    {
        //�ϐ��ɒ��ڑ������...�~�A�T�C��,�Z�ϐ��ɒ��ڑ�� �̍l�����̕���(�l�I�ɂ�)�������₷��
        _search = GameObject.Find("Board,Tile").GetComponent<MasuSearch>();
        _piece = GameObject.Find("Piece").GetComponent<PieceManager>();
        _promQ = GameObject.Find("Queen").GetComponent<Promotion>();
        _promR = GameObject.Find("Rook").GetComponent<Promotion>();
        _promB = GameObject.Find("Bishop").GetComponent<Promotion>();
        _promK = GameObject.Find("Knight").GetComponent<Promotion>();
    }

    /// <summary>
    /// �^�[���\���̐؂�ւ��A��̑I����ԁ��ʏ���
    /// </summary>
    /// <param name="_target"></param>
    public void PhaseChange(GameObject _target)
    {
        switch (_color)
        {
            case PieceColor.White:
                _piece.SwitchTurnWhite();
                if (_target.tag == "WhitePiece")
                {
                    _status = Status.Normal;
                }
                break;

            case PieceColor.Black:
                _piece.SwitchTurnBlack();
                if (_target.tag == "BlackPiece")
                {
                    _status = Status.Normal;
                }
                break;
        }
    }

    /// <summary> �ʏ��ԁA�I����� </summary>
    public enum Status
    {
        Normal,
        Move,
    }

    /// <summary> ����or���� </summary>
    public enum PieceColor
    {
        White = 0,
        Black = 1,
    }

    /// <summary> ��̎�� </summary>
    public enum PieceType
    {
        Pawn = 1,
        Knight,
        Bishop,
        Rook,
        Queen,
    }
}