using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// マスの情報を取得、駒の個別の移動処理に移行
/// </summary>
public class MasuSearch : MonoBehaviour
{
    [Tooltip("盤面のマス"), SerializeField] public List<Collider> Tile = new List<Collider>();
    [Tooltip("移動可能マス"), SerializeField] public List<Collider> MovableTile = new List<Collider>();
    [Tooltip("探索先にいた味方駒"), SerializeField] public List<GameObject> ImmovablePieces = new List<GameObject>();
    [SerializeField] public PieceMove piece = default;
    [SerializeField] public PuzzlePiece puzzle = default;
    [SerializeField] public GameObject pieceInfo;
    [Tooltip("駒のいるマスのファイル(縦) a〜h")] public int _tileFile = 0;
    [Tooltip("駒のいるマスのランク(横) 1〜8")] public int _tileRank = 0;
    RaycastHit _hit;
    [SerializeField] public PieceManager manager;
    [SerializeField] public Pawn _pawn;
    [SerializeField] public Knight _knight;
    [SerializeField] public Bishop _bishop;
    [SerializeField] public Rook _rook;
    [SerializeField] public Queen _queen;
    [SerializeField] public King _king;

    // Start is called before the first frame update
    void Start()
    {
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
        if (piece != null)
        {
            GetTileNum();
            var _pieceNum = piece.gameObject.GetComponent<PieceMove>().type;
            Search((int)_pieceNum);
        }

        if (puzzle != null)
        {
            GetTileNum(); 
            var _pieceNum = puzzle.gameObject.GetComponent<PuzzlePiece>()._type;
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
        foreach (var pieces in manager.WhitePieces)
        {
            //Listの要素がMissingだった場合、無視する
            if (pieces != null)
            {
                pieces.GetComponent<Collider>().enabled = false;
            }
        }
        foreach (var pieces in manager.BlackPieces)
        {
            //Listの要素がMissingだった場合、無視する
            if (pieces != null)
            {
                pieces.GetComponent<Collider>().enabled = false;
            }
        }
    }

    /// <summary>
    /// マスの番号を取得する
    /// </summary>
    void GetTileNum()
    {
        if (pieceInfo != null)
        {
            if (Physics.Raycast(pieceInfo.transform.position, Vector3.down, out _hit, 5))
            {
                //マス番号取得(ランク)
                _tileRank = int.Parse(_hit.collider.gameObject.name[1].ToString());
                //マス番号取得(ファイル)
                if (_hit.collider.gameObject.name[0] == 'a')
                {
                    _tileFile = 1;
                }
                else if (_hit.collider.gameObject.name[0] == 'b')
                {
                    _tileFile = 2;
                }
                else if (_hit.collider.gameObject.name[0] == 'c')
                {
                    _tileFile = 3;
                }
                else if (_hit.collider.gameObject.name[0] == 'd')
                {
                    _tileFile = 4;
                }
                else if (_hit.collider.gameObject.name[0] == 'e')
                {
                    _tileFile = 5;
                }
                else if (_hit.collider.gameObject.name[0] == 'f')
                {
                    _tileFile = 6;
                }
                else if (_hit.collider.gameObject.name[0] == 'g')
                {
                    _tileFile = 7;
                }
                else if (_hit.collider.gameObject.name[0] == 'h')
                {
                    _tileFile = 8;
                }
            }
        }
    }
}
