using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム全体の設定
/// </summary>
public class GameScene : MonoBehaviour
{

    public const int _boardWidth = 8; //const ... 数値の定数化
    public const int _boardHeight = 8;
    const int _players = 2; //プレイ時の最大人数

    //チェス盤の配列
    public GameObject[] board;
    //駒選択のためのカーソル
    public GameObject cursor;

    GameObject[,] boards;
    PieceController[,] units;

    //駒のプレハブ(白黒それぞれ)
    public List<GameObject> prefabWhitePieces;
    public List<GameObject> prefabBlackPieces;

    //Queen = 5, Rook = 4, Bishop = 3, Knight = 2, Pawn = 1 と数字を振る
    //下記は初期配置
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

                //駒の作成
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
