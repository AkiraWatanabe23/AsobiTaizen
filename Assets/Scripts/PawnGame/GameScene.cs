using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Q�[���S�̂̐ݒ�
/// </summary>
public class GameScene : MonoBehaviour
{

    public const int _boardWidth = 8; //const ... ���l�̒萔��
    public const int _boardHeight = 8;
    const int _players = 2; //�v���C���̍ő�l��

    //�`�F�X�Ղ̔z��
    public GameObject[] board;
    //��I���̂��߂̃J�[�\�� ... ���I���������ɁA�ړ��\�Ȕ͈͂�\������
    public GameObject cursor;

    GameObject[,] boards;
    PieceController[,] units;

    //��̃v���n�u(�������ꂼ��)
    public List<GameObject> prefabWhitePieces;
    public List<GameObject> prefabBlackPieces;

    //Queen = 5, Rook = 4, Bishop = 3, Knight = 2, Pawn = 1 �Ɛ�����U��
    //���L�͏����z�u
    public int[,] pieceType =
    {
        {1, 0, 0, 0, 0, 0, 0, 11},
        {1, 0, 0, 0, 0, 0, 0, 11},
        {1, 0, 0, 0, 0, 0, 0, 11},
        {1, 0, 0, 0, 0, 0, 0, 11},
        {1, 0, 0, 0, 0, 0, 0, 11},
        {1, 0, 0, 0, 0, 0, 0, 11},
        {1, 0, 0, 0, 0, 0, 0, 11},
        {1, 0, 0, 0, 0, 0, 0, 11},
    };

    // Start is called before the first frame update
    void Start()
    {
        boards = new GameObject[_boardWidth, _boardHeight];
        units = new PieceController[_boardWidth, _boardHeight];


        for (int i = 1; i <= _boardWidth; i++)
        {
            for (int j = 1; j <= _boardHeight; j++)
            {
                float x = i - _boardWidth / 2;
                float z = j - _boardHeight / 2;

                Vector3 _pos = new Vector3(x, 0, z);

                int _idx = (i + j) % 2;
                GameObject tile = Instantiate(board[_idx], _pos, Quaternion.identity);

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
                piece = Instantiate(prefab, _pos, Quaternion.identity);

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
