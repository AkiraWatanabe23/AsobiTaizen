using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Q�[���S�̂̐ݒ�𓝊�����...���}�X�̐����A�z�u
/// </summary>
public class GameScene : MonoBehaviour
{
    //�Ղ�񎟌��z��ŊǗ�����
    public const int _boardWidth  = 8; //const...���l��萔�ɂ���
    public const int _boardHeight = 8;
    public const int _playersMax  = 2; //�v���C���̍ő�l��

    /// <summary> �`�F�X��(�}�X)�̔z�� </summary>
    public GameObject boardTile;

    //�����f�[�^
    GameObject[,] boards;

    /// <summary> ��̃v���n�u(��) </summary>
    public List<GameObject> prefabWhitePieces;
    /// <summary> ��̃v���n�u(��) </summary>
    public List<GameObject> prefabBlackPieces;

    [SerializeField, Range(1, 5)] float _boardMagnification; //magnification...�{��

    //Queen = 5, Rook = 4, Bishop = 3, Knight = 2, Pawn = 1 �Ɛ�����U��
    //���L�͏����z�u
    public int[,] pieceType =
    {
        // 1...���|�[���A0...�����u����Ă��Ȃ��A11...���|�[��

        {1, 0, 0, 0, 0, 0, 0, 11},  // a1�`a8
        {1, 0, 0, 0, 0, 0, 0, 11},  // b1�`b8
        {1, 0, 0, 0, 0, 0, 0, 11},  // c1�`c8
        {1, 0, 0, 0, 0, 0, 0, 11},  // d1�`d8
        {1, 0, 0, 0, 0, 0, 0, 11},  // e1�`e8
        {1, 0, 0, 0, 0, 0, 0, 11},  // f1�`f8
        {1, 0, 0, 0, 0, 0, 0, 11},  // g1�`g8
        {1, 0, 0, 0, 0, 0, 0, 11},  // h1�`h8
    };

    // Start is called before the first frame update
    void Start()
    {
        boards = new GameObject[_boardWidth, _boardHeight];

        //�e�}�X�̒��S�ɋ�̃I�u�W�F�N�g��z�u����
        for (int i = 0; i < _boardWidth; i++)      //���̃C���f�b�N�X
        {
            for (int j = 0; j < _boardHeight; j++) //�c�̃C���f�b�N�X
            {
                //
                float x = i; //���̍��W��ݒ�
                float z = j; //�c�̍��W��ݒ�

                Vector3 _posTile = new Vector3(x, 8, z) * _boardMagnification;
                Vector3 _posPiece = new Vector3(x, (float)7.5, z) * _boardMagnification;
                

                //�^�C���𐶐�
                //�Ղ̃}�X�ɋ�̃I�u�W�F�N�g��u���A�����ɓ񎟌��z���ݒ肵�A�ۑ�����
                GameObject tile = Instantiate(boardTile, _posTile, Quaternion.identity);
                boards[i, j] = tile;

                //��̍쐬
                int _pieceType = pieceType[i, j] % 10;
                int _player = pieceType[i, j] / 10;

                GameObject prefab = getPrefabPiece(_player, _pieceType);

                if (null == prefab)
                {
                    continue;
                }

                _posPiece.y += 1.5f;
                GameObject piece = Instantiate(prefab, _posPiece, Quaternion.identity); //��(�|�[��)�̃v���n�u�������ʒu�ɐ���
            }
        }
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
