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
    [Tooltip("��������enum")] public PieceColor _color = PieceColor.White;
    [Tooltip("��̏�Ԃ�enum")] public Status _status = Status.Normal;
    [Tooltip("��̎�ނ�enum")] public PieceType _type;
    RaycastHit _hit;
    [Header("�ړ���̈ʒu����")]
    [SerializeField] Vector3 _movedOffset = Vector3.up;
    [SerializeField] Vector3 _gotOffset = Vector3.down;
    //���C���[�}�X�N(Inspector��Layer����I������)
    [SerializeField] LayerMask _tileLayer;
    [SerializeField] LayerMask _whiteLayer;
    [SerializeField] LayerMask _blackLayer;
    //�ړ��\�͈͂̒T��
    [SerializeField] MasuSearch _search;
    [SerializeField] PieceManager _piece;
    [Tooltip("��̈ړ���")] public int _moveCount = 0;
    public GameObject _currentPieceTile;
    public GameObject _movedPieceTile;

    /// <summary>
    /// �I�u�W�F�N�g���N���b�N�������̏���
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        var go = eventData.pointerCurrentRaycast.gameObject;

        print($"{ name } ��I��");
        go.GetComponent<PuzzlePiece>().ChangeState();

        //_movedPieceTile = null �ŋ�̍đI�����o����悤�ɂ���
        if (_status == Status.Normal && _currentPieceTile == _movedPieceTile)
        {
            _movedPieceTile = null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
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
        float _rayDistance = 100;

        //���Ԗڐ��̋�̈ړ�
        //���Ԗڐ���Ray�̏���(���D���ꍇ)
        if (Physics.Raycast(_ray, out _hit, _rayDistance, _blackLayer))
        {
            GameObject _target = _hit.collider.gameObject;
            //int _targetScore = _target.GetComponent<PieceMove>()._getScore; //�Ƃ���������Ă���_getScore���擾

            if (_target.tag == "BlackPiece")
            {
                //���̃X�R�A�����Z
                //GameManager._scoreWhite += _targetScore;
                PuzzleManager._getPieceCount++;
                Destroy(_target);
            }

            this.transform.position = _target.transform.position + _gotOffset;
            GameManager._player = GameManager.Player_Two;
            GameManager._phase = Phase.Black;

            print($"��� {_target.name} ���Ƃ���");

            if (Physics.Raycast(_ray, out RaycastHit hitTile, _rayDistance, _tileLayer))
            {
                GameObject _hitTile = hitTile.collider.gameObject;
                print($"��� {_hitTile.name} �Ɉړ�����");
            }

            PuzzleManager._moveCount--;

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
                    this.transform.position = _target.transform.position + _movedOffset;
                    GameManager._player = GameManager.Player_Two;
                    GameManager._phase = Phase.Black;

                    print($"��� {_target.name} �Ɉړ�����");
                }
                else
                {
                    Debug.Log("�w�肵���}�X�ɂ͓����܂���");
                }
            }
            _movedPieceTile = _hit.collider.gameObject;

            PuzzleManager._moveCount--;
            Debug.Log(_moveCount);

            return true;
        }
        //���Ԗڐ��̋�̈ړ�
        //���Ԗڐ���Ray�̏���(���D���ꍇ)
        //if (Physics.Raycast(_ray2, out _hit, _rayDistance, _whiteLayer))
        //{
        //    GameObject _target = _hit.collider.gameObject;
        //    int _targetScore = _target.GetComponent<PieceMove>()._getScore; //�Ƃ���������Ă���_getScore���擾

        //    if (_target.tag == "WhitePiece")
        //    {
        //        //���̃X�R�A�����Z
        //        GameManager._scoreBlack += _targetScore;
        //        //�Տ�ɂ����̃J�E���g�����炵�āA���j�󂷂�
        //        GameManager._wPieceCount--;
        //        Destroy(_target);
        //    }

        //    this.transform.position = _target.transform.position;
        //    GameManager._player = GameManager.Player_One;
        //    GameManager._phase = Phase.White;

        //    print($"��� {_target.name} ���Ƃ���");

        //    if (Physics.Raycast(_ray, out RaycastHit hitTile, _rayDistance, _tileLayer))
        //    {
        //        GameObject _hitTile = hitTile.collider.gameObject;
        //        print($"��� {_hitTile.name} �Ɉړ�����");
        //    }

        //    if (Physics.Raycast(gameObject.transform.position, Vector3.down, out _hit, 10))
        //    {
        //        _movedPieceTile = _hit.collider.gameObject;
        //    }
        //    return true;
        //}
        //���Ԗڐ���Ray�̏���(�ړ�����)
        //else if (Physics.Raycast(_ray2, out _hit, _rayDistance, _tileLayer))
        //{
        //    foreach (var i in _search._movableTile)
        //    {
        //        GameObject _target = _hit.collider.gameObject;
        //        if (_target == i.gameObject)
        //        {
        //            this.transform.position = _target.transform.position + _offset;
        //            GameManager._player = GameManager.Player_One;
        //            GameManager._phase = Phase.White;

        //            print($"��� {_target.name} �Ɉړ�����");
        //        }
        //        else
        //        {
        //            Debug.Log("�w�肵���}�X�ɂ͓����܂���");
        //        }
        //    }
        //    _movedPieceTile = _hit.collider.gameObject;
        //    return true;
        //}
        return false;
    }

    public void ChangeState()
    {
        //�ʏ��ԁ��I�����(��)
        if (_status == Status.Normal && _color == PieceColor.White && _currentPieceTile != _movedPieceTile)
        {
            if (Physics.Raycast(gameObject.transform.position, Vector3.down, out _hit, 10))
            {
                _currentPieceTile = _movedPieceTile = _hit.collider.gameObject;
            }
            _status = Status.Move;
            _renderer.material = _moveMaterial;
            _search._puzzle = this;
            _search._pieceInfo = gameObject;
            _piece._whitePieces.Remove(gameObject);
        }
        //�ʏ��ԁ��I�����(��)
        else if (_status == Status.Normal && _color == PieceColor.Black && _currentPieceTile != _movedPieceTile)
        {
            if (Physics.Raycast(gameObject.transform.position, Vector3.down, out _hit, 10))
            {
                _currentPieceTile = _movedPieceTile = _hit.collider.gameObject;
            }
            _status = Status.Move;
            _renderer.material = _moveMaterial;
            _search._puzzle = this;
            _search._pieceInfo = gameObject;
            _piece._blackPieces.Remove(gameObject);
        }
        //�I����ԁ��ʏ���(��ړ�������̏���)
        else if (_status == Status.Move)
        {
            //��̈ړ��񐔂����Z����(�|�[���̈ړ������p)
            if (_currentPieceTile != _movedPieceTile && _movedPieceTile.tag == "Tile")
            {
                _moveCount++;
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

            //ColliderOff�ɂ���������Ƃɖ߂�
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
            _search._puzzle = null;
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
