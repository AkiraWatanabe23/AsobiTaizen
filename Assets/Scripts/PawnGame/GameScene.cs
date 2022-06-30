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
    //��I���̂��߂̃J�[�\��
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


        for (int i = 0; i < _boardWidth; i++)
        {
            for (int j = 0; j < _boardHeight; j++)
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

                //GameObject prefab = getPrefabPiece(_player, (PieceController.Type)_pieceType);
                GameObject piece = null;
                PieceController ctrl = null;


            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
