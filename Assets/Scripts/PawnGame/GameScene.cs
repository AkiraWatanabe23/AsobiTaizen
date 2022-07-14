using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム全体の設定を統括する...マスの生成、配置
/// </summary>
public class GameScene : MonoBehaviour
{
    //盤を二次元配列で管理する
    public const int _boardWidth  = 8; //const...数値を定数にする
    public const int _boardHeight = 8;

    /// <summary> チェス盤(マス)の配列 </summary>
    public GameObject boardTile;

    //内部データ
    GameObject[,] boards;

    /// <summary> 駒のプレハブ(白) </summary>
    public List<GameObject> prefabWhitePieces;
    /// <summary> 駒のプレハブ(黒) </summary>
    public List<GameObject> prefabBlackPieces;

    [SerializeField, Range(1, 5)] float _boardMagnification; //magnification...倍率

    //Queen = 5, Rook = 4, Bishop = 3, Knight = 2, Pawn = 1 と数字を振る
    //下記は初期配置
    public int[,] pieceType =
    {
        // 1...白ポーン、0...何も置かれていない、11...黒ポーン

        {1, 0, 0, 0, 0, 0, 0, 11},  // a1～a8
        {1, 0, 0, 0, 0, 0, 0, 11},  // b1～b8
        {1, 0, 0, 0, 0, 0, 0, 11},  // c1～c8
        {1, 0, 0, 0, 0, 0, 0, 11},  // d1～d8
        {1, 0, 0, 0, 0, 0, 0, 11},  // e1～e8
        {1, 0, 0, 0, 0, 0, 0, 11},  // f1～f8
        {1, 0, 0, 0, 0, 0, 0, 11},  // g1～g8
        {1, 0, 0, 0, 0, 0, 0, 11},  // h1～h8
    };

    // Start is called before the first frame update
    void Start()
    {
        boards = new GameObject[_boardWidth, _boardHeight];
        SetPrefab();
    }

    //マスにキューブのプレハブを配置する
    private void SetPrefab()
    {
        //各マスの中心に空のオブジェクトを配置する
        for (int i = 0; i < _boardWidth; i++)      //横のインデックス
        {
            for (int j = 0; j < _boardHeight; j++) //縦のインデックス
            {
                float x = i; //横の座標を設定
                float z = j; //縦の座標を設定

                Vector3 _posTile = new Vector3(x, 8, z) * _boardMagnification;

                //タイルを生成
                //盤のマスに空のオブジェクトを置き、そこに二次元配列を設定し、保存する
                _posTile.y += 0.6f;
                GameObject tile = Instantiate(boardTile, _posTile, Quaternion.identity);
                boards[i, j] = tile;

                //駒の作成
                int _pieceType = pieceType[i, j] % 10;
                int _player = pieceType[i, j] / 10;

                GameObject prefab = getPrefabPiece(_player, _pieceType);

                if (null == prefab)
                {
                    continue;
                }
            }
        }
    }

    //駒のプレハブを返す
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
