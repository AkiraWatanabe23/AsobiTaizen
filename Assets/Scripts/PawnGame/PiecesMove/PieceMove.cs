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
    [Tooltip("�ʏ��Ԃ̃}�e���A��"), SerializeField] Material _normalMaterial;
    [Tooltip("�ړ���Ԃ̃}�e���A��"), SerializeField] Material _moveMaterial;
    Renderer _renderer;
    [Tooltip("��������enum"), SerializeField] PieceColor _color = PieceColor.White;
    [Tooltip("��̎�ނ�enum"), SerializeField] PieceType _type;
    [Tooltip("��̏�Ԃ�enum")] PieceState _state = PieceState.Normal;
    [SerializeField] Vector3 _offset = Vector3.up;
    //���C���[�}�X�N(Inspector��Layer����I������)
    [SerializeField] LayerMask _tileLayer;
    [SerializeField] LayerMask _whiteLayer;
    [SerializeField] LayerMask _blackLayer;
    RaycastHit _hit;
    //�ړ��\�͈͂̒T��
    MasuSearch _search;
    PieceManager _piece;
    GameManager _manager;
    GameObject _currentPieceTile;
    GameObject _movedPieceTile;
    /// <summary> ��̈ړ��� </summary>
    public int MoveCount { get; set; }
    public PieceType Type { get => _type; set => _type = value; }

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
        if (_state == PieceState.Normal && _currentPieceTile == _movedPieceTile)
        {
            _movedPieceTile = null;
        }
    }

    void Start()
    {
        _manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _search = GameObject.Find("Board,Tile").GetComponent<MasuSearch>();
        _piece = GameObject.Find("Piece").GetComponent<PieceManager>();
        _renderer = GetComponent<Renderer>();

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
            if (_state == PieceState.Move)
            {
                //�ړ�����
                if (Move()) /*��return �̒l�� true ��������[Move()��bool�^�̊֐�]*/
                {
                    //�ړ���ԁ��ʏ���
                    ChangeState();
                }
            }
        }
        //�E�N���b�N�Ŕ�I����ԂɕύX
        else if (Input.GetMouseButtonDown(1) && _state == PieceState.Move)
        {
            Debug.Log("�I�ђ���");

            //�}�X�����̏�Ԃɖ߂�
            for (int i = 0; i < _search.MovableTile.Count; i++)
            {
                if (_search.MovableTile[i].tag == "Tile")
                {
                    _search.MovableTile[i].GetComponent<MeshRenderer>().enabled = false;
                    _search.Tile.Add(_search.MovableTile[i]);
                }
            }
            foreach (var tiles in _search.Tile)
            {
                tiles.GetComponent<Collider>().enabled = true;
            }

            foreach (var piece in _search.ImmovablePieces)
            {
                piece.GetComponent<Collider>().enabled = true;
            }
            _search.ImmovablePieces.Clear();

            if (gameObject.tag == "WhitePiece")
            {
                foreach (var pieces in _piece.WhitePieces)
                {
                    pieces.GetComponent<Collider>().enabled = true;
                }
                _piece.WhitePieces.Add(gameObject);
            }
            else if (gameObject.tag == "BlackPiece")
            {
                foreach (var pieces in _piece.BlackPieces)
                {
                    pieces.GetComponent<Collider>().enabled = true;
                }
                _piece.BlackPieces.Add(gameObject);
            }

            //�l������l��Ȃ������ꍇ��List�ɖ߂�
            if (_piece.GetablePieces != null)
            {
                _piece.UnGetPiece();
            }

            //��̏�Ԃ����Ƃɖ߂�
            _state = PieceState.Normal;
            _renderer.material = _normalMaterial;
            _search.MovableTile.Clear();
            _search.Piece = null;
            _search.PieceInfo = null;
        }
    }

    /// <summary> �ړ�����}�X(�܂��͒D����)���}�E�X�N���b�N�őI�сA���̈ʒu�Ɉړ����� </summary>
    public bool Move()
    {
        //�}�E�X�̈ʒu���擾���ARay�ɑ��
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float rayDistance = 100;

        //�������̋��D������
        if (Physics.Raycast(ray, out _hit, rayDistance, _blackLayer))
        {
            GameObject target = _hit.collider.gameObject;

            if (gameObject.tag == "WhitePiece")
            {
                Destroy(target);
                transform.position = target.transform.position;

                PhaseChange(target);
                SetCursorPos(Screen.width / 2, Screen.height / 2);
                GameManager.Player = GameManager.PLAYER_TWO;
                _manager.Phase = GamePhase.Black;

                print($"��� {target.name} ���Ƃ���");

                if (Physics.Raycast(ray, out RaycastHit hitTile, rayDistance, _tileLayer))
                {
                    GameObject _hitTile = hitTile.collider.gameObject;
                    print($"��� {_hitTile.name} �Ɉړ�����");
                }
                _manager.GetPiece = target.name;
                return true;
            }
        }
        //�������̋��D������
        else if (Physics.Raycast(ray, out _hit, rayDistance, _whiteLayer))
        {
            GameObject target = _hit.collider.gameObject;

            if (gameObject.tag == "BlackPiece")
            {
                Destroy(target);
                transform.position = target.transform.position;

                PhaseChange(target);
                SetCursorPos(Screen.width / 2, Screen.height / 2);
                GameManager.Player = GameManager.PLAYER_ONE;
                _manager.Phase = GamePhase.White;

                print($"��� {target.name} ���Ƃ���");

                if (Physics.Raycast(ray, out RaycastHit hitTile, rayDistance, _tileLayer))
                {
                    GameObject _hitTile = hitTile.collider.gameObject;
                    print($"��� {_hitTile.name} �Ɉړ�����");
                }

                if (Physics.Raycast(gameObject.transform.position, Vector3.down, out _hit, 10))
                {
                    _movedPieceTile = _hit.collider.gameObject;
                }
                _manager.GetPiece = target.name;
                return true;
            }
        }
        //�������ʂ̈ړ�����
        else if (Physics.Raycast(ray, out _hit, rayDistance, _tileLayer))
        {
            foreach (var i in _search.MovableTile)
            {
                GameObject target = _hit.collider.gameObject;
                if (target == i.gameObject)
                {
                    transform.position = target.transform.position + _offset;

                    PhaseChange(target);
                    SetCursorPos(Screen.width / 2, Screen.height / 2);
                    //�^�[���؂�ւ�
                    if (gameObject.tag == "WhitePiece")
                    {
                        GameManager.Player = GameManager.PLAYER_TWO;
                        _manager.Phase = GamePhase.Black;
                    }
                    else if (gameObject.tag == "BlackPiece")
                    {
                        GameManager.Player = GameManager.PLAYER_ONE;
                        _manager.Phase = GamePhase.White;
                    }

                    print($"��� {target.name} �Ɉړ�����");
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
        if (_state == PieceState.Normal && _color == PieceColor.White && _manager.Phase == GamePhase.White && _currentPieceTile != _movedPieceTile)
        {
            if (Physics.Raycast(gameObject.transform.position, Vector3.down, out _hit, 10))
            {
                _currentPieceTile = _movedPieceTile = _hit.collider.gameObject;
            }
            _state = PieceState.Move;
            _renderer.material = _moveMaterial;
            _search.Piece = this;
            _search.PieceInfo = gameObject;
            _piece.WhitePieces.Remove(gameObject);
        }
        //�ʏ��ԁ��I�����(��)
        else if (_state == PieceState.Normal && _color == PieceColor.Black && _manager.Phase == GamePhase.Black && _currentPieceTile != _movedPieceTile)
        {
            if (Physics.Raycast(gameObject.transform.position, Vector3.down, out _hit, 10))
            {
                _currentPieceTile = _movedPieceTile = _hit.collider.gameObject;
            }
            _state = PieceState.Move;
            _renderer.material = _moveMaterial;
            _search.Piece = this;
            _search.PieceInfo = gameObject;
            _piece.BlackPieces.Remove(gameObject);
        }
        //�I����ԁ��ʏ���(��ړ�������̏���)
        else if (_state == PieceState.Move)
        {
            //��̈ړ��񐔂����Z����(�|�[���̈ړ������p)
            if (_currentPieceTile != _movedPieceTile && _movedPieceTile.tag == "Tile")
            {
                MoveCount++;
            }

            //�}�X�����̏�Ԃɖ߂�
            for (int i = 0; i < _search.MovableTile.Count; i++)
            {
                if (_search.MovableTile[i].tag == "Tile")
                {
                    _search.MovableTile[i].GetComponent<MeshRenderer>().enabled = false;
                    _search.Tile.Add(_search.MovableTile[i]);
                }
            }
            foreach (var tiles in _search.Tile)
            {
                tiles.GetComponent<Collider>().enabled = true;
            }
            foreach (var piece in _search.ImmovablePieces)
            {
                piece.GetComponent<Collider>().enabled = true;
            }
            _search.ImmovablePieces.Clear();

            //ColliderOff�ɂ���������Ƃɖ߂�
            foreach (var pieces in _piece.WhitePieces)
            {
                if (pieces != null)
                {
                    pieces.GetComponent<Collider>().enabled = true;
                }
            }
            foreach (var pieces in _piece.BlackPieces)
            {
                if (pieces != null)
                {
                    pieces.GetComponent<Collider>().enabled = true;
                }
            }
            if (gameObject.tag == "WhitePiece")
            {
                _piece.WhitePieces.Add(gameObject);
            }
            else if (gameObject.tag == "BlackPiece")
            {
                _piece.BlackPieces.Add(gameObject);
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
            if (_piece.GetablePieces != null)
            {
                _piece.UnGetPiece();
            }

            //��̏�Ԃ����Ƃɖ߂�
            _state = PieceState.Normal;
            _renderer.material = _normalMaterial;
            _search.MovableTile.Clear();
            _search.Piece = null;
            _search.PieceInfo = null;
        }
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
                _manager.SwitchTurnWhite();
                if (_target.tag == "WhitePiece")
                {
                    _state = PieceState.Normal;
                }
                break;

            case PieceColor.Black:
                _manager.SwitchTurnBlack();
                if (_target.tag == "BlackPiece")
                {
                    _state = PieceState.Normal;
                }
                break;
        }
    }

    /// <summary>
    /// ���z�u�������ɓ��������Ԃɂ���(�ϐ��ɃX�N���v�g�𒼐ڑ������)
    /// </summary>
    public void SelectAssign()
    {
        _search = GameObject.Find("Board,Tile").GetComponent<MasuSearch>();
        _piece = GameObject.Find("Piece").GetComponent<PieceManager>();
    }

    /// <summary> �ʏ��ԁA�I����� </summary>
    public enum PieceState
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
        King,
    }
}