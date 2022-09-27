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
    [Tooltip("��������enum")] public PieceColor color = PieceColor.White;
    [Tooltip("��̏�Ԃ�enum")] public PieceState state = PieceState.Normal;
    [Tooltip("��̎�ނ�enum")] public PieceType type;
    RaycastHit _hit;
    [SerializeField] Vector3 _offset = Vector3.up;
    //���C���[�}�X�N(Inspector��Layer����I������)
    [SerializeField] LayerMask _tileLayer;
    [SerializeField] LayerMask _whiteLayer;
    [SerializeField] LayerMask _blackLayer;
    //�ړ��\�͈͂̒T��
    [SerializeField] MasuSearch search;
    [SerializeField] PieceManager piece;
    GameManager _manager;
    [Tooltip("��̈ړ���")] public int moveCount = 0;
    public GameObject currentPieceTile;
    public GameObject movedPieceTile;

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
        if (state == PieceState.Normal && currentPieceTile == movedPieceTile)
        {
            movedPieceTile = null;
        }
    }

    void Start()
    {
        _manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _renderer = GetComponent<Renderer>();

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
            if (state == PieceState.Move)
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
        else if (Input.GetMouseButtonDown(1) && state == PieceState.Move)
        {
            Debug.Log("�I�ђ���");

            //�}�X�����̏�Ԃɖ߂�
            for (int i = 0; i < search.MovableTile.Count; i++)
            {
                if (search.MovableTile[i].tag == "Tile")
                {
                    search.MovableTile[i].GetComponent<MeshRenderer>().enabled = false;
                    search.Tile.Add(search.MovableTile[i]);
                }
            }
            foreach (var tiles in search.Tile)
            {
                tiles.GetComponent<Collider>().enabled = true;
            }

            foreach (var piece in search.ImmovablePieces)
            {
                piece.GetComponent<Collider>().enabled = true;
            }
            search.ImmovablePieces.Clear();

            if (gameObject.tag == "WhitePiece")
            {
                foreach (var pieces in piece.WhitePieces)
                {
                    pieces.GetComponent<Collider>().enabled = true;
                }
                piece.WhitePieces.Add(gameObject);
            }
            else if (gameObject.tag == "BlackPiece")
            {
                foreach (var pieces in piece.BlackPieces)
                {
                    pieces.GetComponent<Collider>().enabled = true;
                }
                piece.BlackPieces.Add(gameObject);
            }

            //�l������l��Ȃ������ꍇ��List�ɖ߂�
            if (piece.GetablePieces != null)
            {
                piece.UnGetPiece();
            }

            //��̏�Ԃ����Ƃɖ߂�
            state = PieceState.Normal;
            _renderer.material = _normalMaterial;
            search.MovableTile.Clear();
            search.piece = null;
            search.pieceInfo = null;
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
                if (target.tag == "BlackPiece")
                {
                    Destroy(target);
                }
                transform.position = target.transform.position;

                PhaseChange(target);
                SetCursorPos(Screen.width / 2, Screen.height / 2);
                GameManager.Player = GameManager.Player_Two;
                GameManager.phase = Phase.Black;

                print($"��� {target.name} ���Ƃ���");

                if (Physics.Raycast(ray, out RaycastHit hitTile, rayDistance, _tileLayer))
                {
                    GameObject _hitTile = hitTile.collider.gameObject;
                    print($"��� {_hitTile.name} �Ɉړ�����");
                }
                _manager._getPiece = target.name;
                return true;
            }
        }
        //�������̋��D������
        else if (Physics.Raycast(ray, out _hit, rayDistance, _whiteLayer))
        {
            GameObject target = _hit.collider.gameObject;

            if (gameObject.tag == "BlackPiece")
            {
                if (target.tag == "WhitePiece")
                {
                    Destroy(target);
                }

                transform.position = target.transform.position;

                PhaseChange(target);
                SetCursorPos(Screen.width / 2, Screen.height / 2);
                GameManager.Player = GameManager.Player_One;
                GameManager.phase = Phase.White;

                print($"��� {target.name} ���Ƃ���");

                if (Physics.Raycast(ray, out RaycastHit hitTile, rayDistance, _tileLayer))
                {
                    GameObject _hitTile = hitTile.collider.gameObject;
                    print($"��� {_hitTile.name} �Ɉړ�����");
                }

                if (Physics.Raycast(gameObject.transform.position, Vector3.down, out _hit, 10))
                {
                    movedPieceTile = _hit.collider.gameObject;
                }
                _manager._getPiece = target.name;
                return true;
            }
        }
        //�������ʂ̈ړ�����
        else if (Physics.Raycast(ray, out _hit, rayDistance, _tileLayer))
        {
            foreach (var i in search.MovableTile)
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
                        GameManager.Player = GameManager.Player_Two;
                        GameManager.phase = Phase.Black;
                    }
                    else if (gameObject.tag == "BlackPiece")
                    {
                        GameManager.Player = GameManager.Player_One;
                        GameManager.phase = Phase.White;
                    }

                    print($"��� {target.name} �Ɉړ�����");
                }
                else
                {
                    Debug.Log("�w�肵���}�X�ɂ͓����܂���");
                }
            }
            movedPieceTile = _hit.collider.gameObject;
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
        if (state == PieceState.Normal && color == PieceColor.White && GameManager.phase == Phase.White && currentPieceTile != movedPieceTile)
        {
            if (Physics.Raycast(gameObject.transform.position, Vector3.down, out _hit, 10))
            {
                currentPieceTile = movedPieceTile = _hit.collider.gameObject;
            }
            state = PieceState.Move;
            _renderer.material = _moveMaterial;
            search.piece = this;
            search.pieceInfo = gameObject;
            piece.WhitePieces.Remove(gameObject);
        }
        //�ʏ��ԁ��I�����(��)
        else if (state == PieceState.Normal && color == PieceColor.Black && GameManager.phase == Phase.Black && currentPieceTile != movedPieceTile)
        {
            if (Physics.Raycast(gameObject.transform.position, Vector3.down, out _hit, 10))
            {
                currentPieceTile = movedPieceTile = _hit.collider.gameObject;
            }
            state = PieceState.Move;
            _renderer.material = _moveMaterial;
            search.piece = this;
            search.pieceInfo = gameObject;
            piece.BlackPieces.Remove(gameObject);
        }
        //�I����ԁ��ʏ���(��ړ�������̏���)
        else if (state == PieceState.Move)
        {
            //��̈ړ��񐔂����Z����(�|�[���̈ړ������p)
            if (currentPieceTile != movedPieceTile && movedPieceTile.tag == "Tile")
            {
                moveCount++;
            }

            //�}�X�����̏�Ԃɖ߂�
            for (int i = 0; i < search.MovableTile.Count; i++)
            {
                if (search.MovableTile[i].tag == "Tile")
                {
                    search.MovableTile[i].GetComponent<MeshRenderer>().enabled = false;
                    search.Tile.Add(search.MovableTile[i]);
                }
            }
            foreach (var tiles in search.Tile)
            {
                tiles.GetComponent<Collider>().enabled = true;
            }
            foreach (var piece in search.ImmovablePieces)
            {
                piece.GetComponent<Collider>().enabled = true;
            }
            search.ImmovablePieces.Clear();

            //ColliderOff�ɂ���������Ƃɖ߂�
            foreach (var pieces in piece.WhitePieces)
            {
                if (pieces != null)
                {
                    pieces.GetComponent<Collider>().enabled = true;
                }
            }
            foreach (var pieces in piece.BlackPieces)
            {
                if (pieces != null)
                {
                    pieces.GetComponent<Collider>().enabled = true;
                }
            }
            if (gameObject.tag == "WhitePiece")
            {
                piece.WhitePieces.Add(gameObject);
            }
            else if (gameObject.tag == "BlackPiece")
            {
                piece.BlackPieces.Add(gameObject);
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
            if (piece.GetablePieces != null)
            {
                piece.UnGetPiece();
            }

            //��̏�Ԃ����Ƃɖ߂�
            state = PieceState.Normal;
            _renderer.material = _normalMaterial;
            search.MovableTile.Clear();
            search.piece = null;
            search.pieceInfo = null;
        }
    }

    /// <summary>
    /// �^�[���\���̐؂�ւ��A��̑I����ԁ��ʏ���
    /// </summary>
    /// <param name="_target"></param>
    public void PhaseChange(GameObject _target)
    {
        switch (color)
        {
            case PieceColor.White:
                _manager.SwitchTurnWhite();
                if (_target.tag == "WhitePiece")
                {
                    state = PieceState.Normal;
                }
                break;

            case PieceColor.Black:
                _manager.SwitchTurnBlack();
                if (_target.tag == "BlackPiece")
                {
                    state = PieceState.Normal;
                }
                break;
        }
    }

    /// <summary>
    /// ���z�u�������ɓ��������Ԃɂ���(�ϐ��ɃX�N���v�g�𒼐ڑ������)
    /// </summary>
    public void SelectAssign()
    {
        search = GameObject.Find("Board,Tile").GetComponent<MasuSearch>();
        piece = GameObject.Find("Piece").GetComponent<PieceManager>();
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