using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム全体の設定を統括する
/// </summary>
public class GameScene : MonoBehaviour
{

    public const int _boardWidth = 8; //const ... 数値を定数にする
    public const int _boardHeight = 8;
    const int _playersMax = 2; //プレイ時の最大人数

    //チェス盤の配列
    public GameObject[] board;
    //駒選択のためのカーソル ... 駒を選択した時に、移動可能な範囲を表示する
    public GameObject cursor;

    //内部データ
    GameObject[,] boards;
    PieceController[,] units;

    //駒のプレハブ(白黒それぞれ)
    public List<GameObject> prefabWhitePieces;
    public List<GameObject> prefabBlackPieces;

    //Queen = 5, Rook = 4, Bishop = 3, Knight = 2, Pawn = 1 と数字を振る
    //下記は初期配置
    public int[,] pieceType =
    {
        {1, 0, 0, 0, 0, 0, 0, 11}, // 1...白ポーン
        {1, 0, 0, 0, 0, 0, 0, 11}, // 0...何も置かれていない
        {1, 0, 0, 0, 0, 0, 0, 11}, //11...黒ポーン
        {1, 0, 0, 0, 0, 0, 0, 11},
        {1, 0, 0, 0, 0, 0, 0, 11},
        {1, 0, 0, 0, 0, 0, 0, 11},
        {1, 0, 0, 0, 0, 0, 0, 11},
        {1, 0, 0, 0, 0, 0, 0, 11},
    };

    //UI関連
    //GameObject _textTurnInfo; //アンカー左上
    //GameObject _textResultInfo; //アンカー真上
    //GameObject _buttonApply; //Retry
    //GameObject _buttonCancel; //ToTitle

    //選択中の駒
    PieceController _selectPiece;

    // Start is called before the first frame update
    void Start()
    {
    //    UIオブジェクト取得
    //    _textTurnInfo = GameObject.Find("TextTurnInfo");
    //    _textResultInfo = GameObject.Find("TextResultInfo");
    //    _buttonApply = GameObject.Find("ButtonApply");
    //    _buttonCancel = GameObject.Find("ButtonCancel");

    //    Result関連のものは最初は消しておく
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
                GameObject tile = Instantiate(board[_idx], _pos, Quaternion.identity); //盤のプレハブを生成

                boards[i, j] = tile;

                //駒の作成
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
                piece = Instantiate(prefab, _pos, Quaternion.identity); //駒のプレハブを生成(ポーンが1つ生成されている)

                //初期設定
                ctrl = piece.GetComponent<PieceController>();
                ctrl.SetPiece(_player, (PieceController.Type)_pieceType, tile);

                //内部データのセット

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject _tile = null;
        PieceController _piece = null;

        //PLAYER
        if (Input.GetMouseButtonUp(0)) //MouseButton(0)...左クリック　Up...離れた時
        {
            Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition); //マウス座標からのRayの設定

            //駒にも当たり判定がある ... ヒットした全てのオブジェクト情報を取得
            foreach (RaycastHit hit in Physics.RaycastAll(_ray))
            {
                if (hit.transform.name.Contains("Board"))
                {
                    _tile = hit.transform.gameObject;
                    break;
                }
            }
        }

        //タイルが押されていない(選択されていない)ならば、処理を行わない
        if (null == _tile)
        {
            return;
        }

        //選んだタイルから駒を取得
        Vector2Int _tilePos = new Vector2Int(
            (int)_tile.transform.position.x + _boardWidth / 2,
            (int)_tile.transform.position.z + _boardHeight / 2);

        //タイルにのっている駒
        _piece = units[_tilePos.x, _tilePos.y];

        //駒選択
        if (null != _piece && _selectPiece != _piece)
        {
            SetSelectCursors(_piece);
        }
        //駒の移動
        else if (null != _selectPiece)
        {
            MovePiece(_selectPiece, _tilePos);
        }
    }

    void SetSelectCursors(PieceController piece = null, bool setPiece = true)
    {
        //カーソル解除


        //駒の非選択状態
        if (null != _selectPiece)
        {
            _selectPiece.SelectPiece(false);
            _selectPiece = null;
        }

        //駒を何もセットしないなら、終わり
        if (null == piece)
        {
            return;
        }

        //カーソル作成


        //駒の選択状態
        if (setPiece)
        {
            _selectPiece = piece;
            _selectPiece.SelectPiece(setPiece);
        }
    }

    bool MovePiece(PieceController piece, Vector2Int tilePos)
    {
        Vector2Int _piecePos = piece.Pos;

        //駒を新しい位置に移動
        piece.MovePiece(boards[tilePos.x, tilePos.y]);

        //配列データの更新(もともと駒がいた位置)
        units[_piecePos.x, _piecePos.y] = null;

        //駒があったら消す
        if (null != units[tilePos.x, tilePos.y])
        {
            Destroy(units[tilePos.x, tilePos.y].gameObject);
        }

        //配列データの更新(新しく置いた位置)
        units[tilePos.x, tilePos.y] = piece;

        return true;
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
