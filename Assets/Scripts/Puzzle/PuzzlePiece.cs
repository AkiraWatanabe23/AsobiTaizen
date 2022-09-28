using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// ��̈ړ��Ɋւ������(�p�Y����)
/// </summary>
public class PuzzlePiece : MonoBehaviour, IPointerClickHandler
{
    [Tooltip("�ʏ��Ԃ̃}�e���A��"), SerializeField] Material _normalMaterial;
    [Tooltip("�ړ���Ԃ̃}�e���A��"), SerializeField] Material _moveMaterial;
    Renderer _renderer;
    [Tooltip("��̎�ނ�enum"), SerializeField] PieceType _type;
    [Tooltip("��������enum")] PieceColor _color = PieceColor.White;
    [Tooltip("��̏�Ԃ�enum")] Status _status = Status.Normal;
    [Header("�ړ���̈ʒu����")]
    [SerializeField] Vector3 _movedOffset = Vector3.up;
    [SerializeField] Vector3 _gotOffset = Vector3.down;
    //���C���[�}�X�N(Inspector��Layer����I������)
    [SerializeField] LayerMask _tileLayer;
    [SerializeField] LayerMask _whiteLayer;
    [SerializeField] LayerMask _blackLayer;
    RaycastHit _hit;
    //�ړ��\�͈͂̒T��
    MasuSearch _search;
    PieceManager _piece;
    [Tooltip("��̈ړ���")] int moveCount = 0;
    GameObject currentPieceTile;
    GameObject movedPieceTile;
    public PieceType Type { get => _type; set => _type = value; }

    /// <summary>
    /// �I�u�W�F�N�g���N���b�N�������̏���
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        var go = eventData.pointerCurrentRaycast.gameObject;

        print($"{ name } ��I��");
        //���̋�(�l��ׂ���͑I���ł��Ȃ�)
        if (go.gameObject.tag == "WhitePiece")
        {
            go.GetComponent<PuzzlePiece>().ChangeState();
        }

        //_movedPieceTile = null �ŋ�̍đI�����o����悤�ɂ���
        if (_status == Status.Normal && currentPieceTile == movedPieceTile)
        {
            movedPieceTile = null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _search = GameObject.Find("Board,Tile").GetComponent<MasuSearch>();
        _piece = GameObject.Find("Piece").GetComponent<PieceManager>();

        if (Physics.Raycast(gameObject.transform.position, Vector3.down, out _hit, 10))
        {
            currentPieceTile = _hit.collider.gameObject;
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
            _status = Status.Normal;
            _renderer.material = _normalMaterial;
            _search.MovableTile.Clear();
            _search.Piece = null;
            _search.PieceInfo = null;
        }
    }

    public bool Move()
    {
        //�}�E�X�̈ʒu���擾���ARay�ɑ��
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float rayDistance = 100;

        //������l��Ƃ��̏���
        if (Physics.Raycast(ray, out _hit, rayDistance, _blackLayer))
        {
            GameObject target = _hit.collider.gameObject;

            if (target.tag == "BlackPiece")
            {
                PuzzleManager._getPieceCount++;
                Destroy(target);
            }

            this.transform.position = target.transform.position + _gotOffset;

            print($"��� {target.name} ���Ƃ���");

            if (Physics.Raycast(ray, out RaycastHit hitTile, rayDistance, _tileLayer))
            {
                GameObject _hitTile = hitTile.collider.gameObject;
                print($"��� {_hitTile.name} �Ɉړ�����");
            }
            PuzzleManager.moveCount--;

            return true;
        }
        //����ړ����鏈��
        else if (Physics.Raycast(ray, out _hit, rayDistance, _tileLayer))
        {
            foreach (var i in _search.MovableTile)
            {
                GameObject _target = _hit.collider.gameObject;
                if (_target == i.gameObject)
                {
                    this.transform.position = _target.transform.position + _movedOffset;
                    print($"��� {_target.name} �Ɉړ�����");
                }
                else
                {
                    Debug.Log("�w�肵���}�X�ɂ͓����܂���");
                }
            }
            movedPieceTile = _hit.collider.gameObject;

            PuzzleManager.moveCount--;
            Debug.Log(moveCount);

            return true;
        }
        return false;
    }

    public void ChangeState()
    {
        //�ʏ��ԁ��I�����(��)
        if (_status == Status.Normal && _color == PieceColor.White && currentPieceTile != movedPieceTile)
        {
            if (Physics.Raycast(gameObject.transform.position, Vector3.down, out _hit, 10))
            {
                currentPieceTile = movedPieceTile = _hit.collider.gameObject;
            }
            _status = Status.Move;
            _renderer.material = _moveMaterial;
            _search.Puzzle = this;
            _search.PieceInfo = gameObject;
            _piece.WhitePieces.Remove(gameObject);
        }
        //�ʏ��ԁ��I�����(��)
        else if (_status == Status.Normal && _color == PieceColor.Black && currentPieceTile != movedPieceTile)
        {
            if (Physics.Raycast(gameObject.transform.position, Vector3.down, out _hit, 10))
            {
                currentPieceTile = movedPieceTile = _hit.collider.gameObject;
            }
            _status = Status.Move;
            _renderer.material = _moveMaterial;
            _search.Puzzle = this;
            _search.PieceInfo = gameObject;
            _piece.BlackPieces.Remove(gameObject);
        }
        //�I����ԁ��ʏ���(��ړ�������̏���)
        else if (_status == Status.Move)
        {
            //��̈ړ��񐔂����Z����(�|�[���̈ړ������p)
            if (currentPieceTile != movedPieceTile && movedPieceTile.tag == "Tile")
            {
                moveCount++;
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
                movedPieceTile = _hit.collider.gameObject;
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
            _status = Status.Normal;
            _renderer.material = _normalMaterial;
            _search.MovableTile.Clear();
            _search.Piece = null;
            _search.PieceInfo = null;
            _search.Puzzle = null;
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
        King,
    }
}
