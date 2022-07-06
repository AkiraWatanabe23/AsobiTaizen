using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Q�[���S�̂̐ݒ�𓝊�����
/// </summary>
public class GameScene : MonoBehaviour
{

    public const int _boardWidth = 8; //const ... ���l��萔�ɂ���
    public const int _boardHeight = 8;
    const int _playersMax = 2; //�v���C���̍ő�l��

    //�`�F�X�Ղ̔z��
    public GameObject[] board;
    //��I���̂��߂̃J�[�\�� ... ���I���������ɁA�ړ��\�Ȕ͈͂�\������
    public GameObject cursor;

    //�����f�[�^
    GameObject[,] boards;
    PieceController[,] units;

    //��̃v���n�u(�������ꂼ��)
    public List<GameObject> prefabWhitePieces;
    public List<GameObject> prefabBlackPieces;

    //Queen = 5, Rook = 4, Bishop = 3, Knight = 2, Pawn = 1 �Ɛ�����U��
    //���L�͏����z�u
    public int[,] pieceType =
    {
        {1, 0, 0, 0, 0, 0, 0, 11}, // 1...���|�[��
        {1, 0, 0, 0, 0, 0, 0, 11}, // 0...�����u����Ă��Ȃ�
        {1, 0, 0, 0, 0, 0, 0, 11}, //11...���|�[��
        {1, 0, 0, 0, 0, 0, 0, 11},
        {1, 0, 0, 0, 0, 0, 0, 11},
        {1, 0, 0, 0, 0, 0, 0, 11},
        {1, 0, 0, 0, 0, 0, 0, 11},
        {1, 0, 0, 0, 0, 0, 0, 11},
    };

    //UI�֘A
    //GameObject _textTurnInfo; //�A���J�[����
    //GameObject _textResultInfo; //�A���J�[�^��
    //GameObject _buttonApply; //Retry
    //GameObject _buttonCancel; //ToTitle

    //�I�𒆂̋�
    PieceController _selectPiece;

    // Start is called before the first frame update
    void Start()
    {
    //    UI�I�u�W�F�N�g�擾
    //    _textTurnInfo = GameObject.Find("TextTurnInfo");
    //    _textResultInfo = GameObject.Find("TextResultInfo");
    //    _buttonApply = GameObject.Find("ButtonApply");
    //    _buttonCancel = GameObject.Find("ButtonCancel");

    //    Result�֘A�̂��͍̂ŏ��͏����Ă���
    //    _buttonApply.SetActive(false);
    //    _buttonCancel.SetActive(false);

        boards = new GameObject[_boardWidth, _boardHeight];
        units = new PieceController[_boardWidth, _boardHeight];

        for (int i = 0; i < _boardWidth; i++)
        {
            for (int j = 0; j < _boardHeight; j++)
            {
                float x = i - _boardWidth / 2;
                float z = j - _boardHeight / 2;

                Vector3 _pos = new Vector3(x, 0, z);

                int _idx = (i + j) % 2;
                GameObject tile = Instantiate(board[_idx], _pos, Quaternion.identity); //�Ղ̃v���n�u�𐶐�

                boards[i, j] = tile;

                //��̍쐬
                int _pieceType = pieceType[i, j] % 10;
                int _player = pieceType[i, j] / 10;

                GameObject prefab = getPrefabPiece(_player, _pieceType);
                GameObject piece = null;
                PieceController ctrl = null;

                if (null == prefab)
                {
                    continue;
                }

                _pos.y += 1.5f;
                piece = Instantiate(prefab, _pos, Quaternion.identity); //��̃v���n�u�𐶐�(�|�[����1��������Ă���)

                //�����ݒ�
                ctrl = piece.GetComponent<PieceController>();
                ctrl.SetPiece(_player, (PieceController.Type)_pieceType, tile);

                //�����f�[�^�̃Z�b�g

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject _tile = null;
        PieceController _piece = null;

        //PLAYER
        if (Input.GetMouseButtonUp(0)) //MouseButton(0)...���N���b�N�@Up...���ꂽ��
        {
            Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition); //�}�E�X���W�����Ray�̐ݒ�

            //��ɂ������蔻�肪���� ... �q�b�g�����S�ẴI�u�W�F�N�g�����擾
            foreach (RaycastHit hit in Physics.RaycastAll(_ray))
            {
                if (hit.transform.name.Contains("Board"))
                {
                    _tile = hit.transform.gameObject;
                    break;
                }
            }
        }

        //�^�C����������Ă��Ȃ�(�I������Ă��Ȃ�)�Ȃ�΁A�������s��Ȃ�
        if (null == _tile)
        {
            return;
        }

        //�I�񂾃^�C���������擾
        Vector2Int _tilePos = new Vector2Int(
            (int)_tile.transform.position.x + _boardWidth / 2,
            (int)_tile.transform.position.z + _boardHeight / 2);

        //�^�C���ɂ̂��Ă����
        _piece = units[_tilePos.x, _tilePos.y];

        //��I��
        if (null != _piece && _selectPiece != _piece)
        {
            SetSelectCursors(_piece);
        }
        //��̈ړ�
        else if (null != _selectPiece)
        {
            MovePiece(_selectPiece, _tilePos);
        }
    }

    void SetSelectCursors(PieceController piece = null, bool setPiece = true)
    {
        //�J�[�\������


        //��̔�I�����
        if (null != _selectPiece)
        {
            _selectPiece.SelectPiece(false);
            _selectPiece = null;
        }

        //��������Z�b�g���Ȃ��Ȃ�A�I���
        if (null == piece)
        {
            return;
        }

        //�J�[�\���쐬


        //��̑I�����
        if (setPiece)
        {
            _selectPiece = piece;
            _selectPiece.SelectPiece(setPiece);
        }
    }

    bool MovePiece(PieceController piece, Vector2Int tilePos)
    {
        Vector2Int _piecePos = piece.Pos;

        //���V�����ʒu�Ɉړ�
        piece.MovePiece(boards[tilePos.x, tilePos.y]);

        //�z��f�[�^�̍X�V(���Ƃ��Ƌ�����ʒu)
        units[_piecePos.x, _piecePos.y] = null;

        //������������
        if (null != units[tilePos.x, tilePos.y])
        {
            Destroy(units[tilePos.x, tilePos.y].gameObject);
        }

        //�z��f�[�^�̍X�V(�V�����u�����ʒu)
        units[tilePos.x, tilePos.y] = piece;

        return true;
    }

    //��̃v���n�u��Ԃ�
    GameObject getPrefabPiece(int _player, int _type)
    {
        int idx = _type - 1;

        if (0 > idx)
        {
            return null;
        }

        GameObject _prefab = prefabWhitePieces[idx];
        if (1 == _player)
        {
            _prefab = prefabBlackPieces[idx];
        }
        return _prefab;
    }
}
