using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// マスの情報を取得、駒の個別の移動処理に移行
/// </summary>
public class MasuSearch : MonoBehaviour
{
    [Tooltip("盤面のマス"), SerializeField] List<Collider> _tile = new List<Collider>();
    [Tooltip("移動可能マス"), SerializeField] List<Collider> _movableTile = new List<Collider>();
    [Tooltip("探索先にいた味方駒"), SerializeField] List<GameObject> _immovablePieces = new List<GameObject>();
    PieceManager _manager;
    [Header("駒の個別移動処理")]
    [SerializeField] Pawn _pawn;
    [SerializeField] Knight _knight;
    [SerializeField] Bishop _bishop;
    [SerializeField] Rook _rook;
    [SerializeField] Queen _queen;
    [SerializeField] King _king;
    public List<Collider> Tile { get => _tile; set => _tile = value; }
    public List<Collider> MovableTile { get => _movableTile; set => _movableTile = value; }
    public List<GameObject> ImmovablePieces { get => _immovablePieces; set => _immovablePieces = value; }
    public PieceMove Piece { get; set; }
    public PuzzlePiece Puzzle { get; set; }
    public GameObject PieceInfo { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        _manager = GameObject.Find("Piece").GetComponent<PieceManager>();
        //マスを取得し、色を非表示にする
        for (int i = 0; i < 64; i++)
        {
            Tile.Add(gameObject.transform.GetChild(i).GetComponent<Collider>());
            Tile[i].GetComponent<MeshRenderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Piece != null)
        {
            var _pieceNum = Piece.gameObject.GetComponent<PieceMove>().Type;
            Search((int)_pieceNum);
        }

        if (Puzzle != null)
        {
            var _pieceNum = Puzzle.gameObject.GetComponent<PuzzlePiece>().Type;
            Search((int)_pieceNum);
        }
    }

    /// <summary>
    /// 駒の個別の移動処理
    /// </summary>
    /// <param name="pieceType"> 選んだ駒の種類 </param>
    public void Search(int pieceType)
    {
        switch (pieceType)
        {
            case 1:
                _pawn.PawnMovement();
                break;
            case 2:
                _knight.KnightMovement();
                break;
            case 3:
                _bishop.BishopMovement();
                break;
            case 4:
                _rook.RookMovement();
                break;
            case 5:
                _queen.QueenMovement();
                break;
            case 6:
                _king.KingMovement();
                break;
        }
        //移動範囲外の駒のColliderをoffにする処理
        foreach (var pieces in _manager.WhitePieces)
        {
            //Listの要素がMissingだった場合、無視する
            if (pieces != null)
            {
                pieces.GetComponent<Collider>().enabled = false;
            }
        }
        foreach (var pieces in _manager.BlackPieces)
        {
            //Listの要素がMissingだった場合、無視する
            if (pieces != null)
            {
                pieces.GetComponent<Collider>().enabled = false;
            }
        }
    }
}
