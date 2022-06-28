using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessManager : MonoBehaviour
{
    [SerializeField] GameObject _pawn;
    [SerializeField] GameObject _knight;
    [SerializeField] GameObject _bishop;
    [SerializeField] GameObject _rook;
    [SerializeField] GameObject _queen;
    [SerializeField] GameObject[] _board;

    [SerializeField] bool _choosePiece; //bool の既定値 ... false
    GameObject _choose;
    List<Piece> _piecesPlayer = new List<Piece>();
    List<Piece> _piecesEnemy = new List<Piece>();

    List<GameObject> _piece = new List<GameObject>();
    List<GameObject> _areaBoard = new List<GameObject>();

    readonly int _width = 8;
    readonly int _height = 8;

    enum PieceID
    {
        None = 0,
        Pawn,
        Knight,
        Bishop,
        Rook,
        Queen,

        MoveArea = 9
    }
    PieceID _chooseID = PieceID.None;

    enum Turn
    {
        Player = 0,
        Enemy,
    }
    Turn _turn = Turn.Player;

    GameObject[,] board = new GameObject[8, 8];

    int[][,] boardPlaying = new int[2][,] //プレイ時点での駒の配置を記憶する
    {
        new int[,]
        {
            {1,1,1,1,1,1,1,1},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0}
        },
        new int[,]
        {
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {0,0,0,0,0,0,0,0},
            {1,1,1,1,1,1,1,1}
        }
    };

    // Start is called before the first frame update
    void Start()
    {
        InitPiece();
    }

    // Update is called once per frame
    void Update()
    {
        InputRay();
    }

    void InitPiece()
    {
        int boardIndex = 0;
        Piece _piece;

        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                _piece = SpawnPiece(boardPlaying[(int)Turn.Player],i,j,false);
                if (_piece)
                {
                    _piecesPlayer.Add(_piece);
                }

                _piece = SpawnPiece(boardPlaying[(int)Turn.Enemy],i,j,true);
                if (_piece)
                {
                    _piecesEnemy.Add(_piece);
                }

                board[i, j] = Instantiate(_board[_boardIndex % 2], new Vector3(i, -1, j), Quaternion.identity);
                _boardIndex++;
            }
            _boardIndex++;
        }
    }

    Piece SpawnPiece(int[,] board, int w, int h, bool isEnemy)
    {
        switch ((PieceID)board[w,h])
        {
            case PieceID.Pawn:
                _piece = Instantiate(_pawn, new Vector3(w, 0, h), Quaternion.identity).GetComponent<Piece>();
                break;
            case PieceID.Knight:
                _piece = Instantiate(_knight, new Vector3(w, 0, h), Quaternion.identity).GetComponent<Piece>();
                break;
            case PieceID.Bishop:
                _piece = Instantiate(_bishop, new Vector3(w, 0, h), Quaternion.identity).GetComponent<Piece>();
                break;
            case PieceID.Rook:
                _piece = Instantiate(_rook, new Vector3(w, 0, h), Quaternion.identity).GetComponent<Piece>();
                break;
            case PieceID.Queen:
                _piece = Instantiate(_queen, new Vector3(w, 0, h), Quaternion.identity).GetComponent<Piece>();
                break;
        }

        if (_piece)
        {
            _piece.isEnemy = isEnemy;
            return _piece;
        }
    }

    /// <summary> 選択した場所を取得 </summary>
    void InputRay()
    {
        if (Input.GetMouseButtonDown(0)) //MouseButton(0) ... 左ボタン
        {
            Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(_ray,out hit,100f,1 << 6))
            {
                OnClick(hit.transform);
            }
        }
    }

    void OnClick(Transform hitTrans)
    {
        if (isChoosePiece)
        {
            //移動不可能なエリアを選んだ場合、無視
            if (boardPlaying[(int)_turn][(int)hitTrans.localPosition.x, (int)hitTrans.localPosition.z] == (int)PieceID.None)
            // position ... ワールド座標を基準にしている　localPosition ... 親子関係にあるオブジェクトの、親オブジェクトの座標を基準にしている
            {
                return;
            }
            //選択する駒を変更した場合、移動可能エリアを再取得
            else if (boardPlaying[(int)_turn][(int)hitTrans.localPosition.x, (int)hitTrans.localPosition.z] != (int)PieceID.MoveArea)
            {
                boardPlaying[(int)_turn][(int)_choose.transform.localPosition.x, (int)_choose.transform.localPosition.z] = (int)_chooseID;
                ResetMovePlace();
                if (_turn == Turn.Player)
                {
                    _choosePiece(_piecesPlayer, hitTrans);
                }
                else
                {
                    _choosePiece(_piecesEnemy, hitTrans);
                }
                return;
            }

            if (_chooseID == PieceID.Pawn)
            {
                _choose.GetComponent<Piece>().isPawnFirstMove = false;
            }

            //クリックされた位置に駒を移動
            var pos = _choose.transform.localPosition;
            pos.x = hitTrans.localPosition.x;
            pos.z = hitTrans.localPosition.z;
            _choose.transform.localPosition = pos;

            //敵駒なら削除する
            foreach (var v in _takePiece)
            {
                if (v.transform.localPosition.x == _choose.transform.localPosition.x &&
                    v.transform.localPosition.z == _choose.transform.localPosition.z)
                {
                    if (_turn == Turn.Player)
                    {
                        _piecesEnemy.Remove(v.GetComponent<Piece>());
                    }
                    else
                    {
                        _piecesPlayer.Remove(v.GetComponent<Piece>());
                    }
                    Destroy(v);
                }
            }

            boardPlaying[(int)_turn][(int)_choose.transform.localPosition.x, (int)_choose.transform.localPosition.z] = (int)_chooseID;
            _chooseID = PieceID.None;

            _choose = null;
            isChoosePiece = false;
            _takePiece.Clear();
            ResetMovePlace();

            if (_turn == Turn.Player)
            {
                _turn = Turn.Enemy;
            }
            else
            {
                _turn = Turn.Player;
            }
        }
        else
        {
            //クリックした位置に駒があるか調べる
            if (_turn == Turn.Player)
            {
                ChoosePiece(_piecesPlayer, hitTrans);
            }
            else
            {
                ChoosePiece(_piecesEnemy, hitTrans);
            }
        }
    }

    /// <param name="pieces"></param>
    /// <param name="hitTrans"></param>
    void ChoosePiece(List<Piece> pieces, Transform hitTrans)
    {
        foreach (var v in pieces)
        {
            if (v.transform.localPosition.x == hitTrans.localPosition.x &&
                v.transform.localPosition.z == hitTrans.localPosition.z)
            {
                _choose = v.gameObject;
                isChoosePiece = true;
                MoveAreaCheck();
                return;
            }
        }
    }

    GameObject SearchEnemyPiece (int width, int height, List<Piece> enemy)
    {
        foreach (var v in enemy)
        {
            if (v.transform.localPosition.x == width &&
                v.transform.localPosition.z == height)
            {
                return v.gameObject;
            }
        }
        return null;
    }

    void MoveAreaCheck()
    {
        _chooseID = (PieceID)boardPlaying[(int)_turn][(int)_choose.transform.localPosition.x, (int)_choose.transform.localPosition.z];
        boardPlaying[(int)_turn][(int)_choose.transform.localPosition.x, (int)_choose.transform.localPosition.z] = (int)PieceID.None;
        _takePiece.Clear();
        _areaBoard.Clear();

        switch (_chooseID)
        {
            case PieceID.Pawn:
                Pawn();
                break;

            case PieceID.Knight:
                Knight( 2, 1);
                Knight( 1, 2);
                Knight(-1, 2);
                Knight(-2, 1);
                Knight( 2,-1);
                Knight( 1,-2);
                Knight(-1,-2);
                Knight(-2,-1);
                break;

            case PieceID.Bishop:
                Bishop(false);
                break;

            case PieceID.Rook:
                Rook(false);
                break;

            case PieceID.Queen:
                Queen(false);
                break;
        }
        AreaBoardChangeMaterial();
    }

    void AreaBoardChangeMaterial()
    {
        if (_areaBoard.Count == 0)
        {
            return;
        }

        foreach (var v in _areaBoard)
        {
            v.GetComponent<MeshRenderer>().material = boardMaterial[2];
        }
    }

    /// <summary>
    /// 移動する位置のリセット
}
