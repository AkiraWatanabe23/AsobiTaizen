using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Panelに設置したボタンの駒を選ぶスクリプト
/// </summary>
public class SelectPiece : MonoBehaviour
{
    [SerializeField] GameObject _selectWhite_One;
    [SerializeField] GameObject _selectWhite_Two;
    [SerializeField] GameObject _selectWhite_Three;
    [SerializeField] GameObject _selectBlack_One;
    [SerializeField] GameObject _selectBlack_Two;
    [SerializeField] GameObject _selectBlack_Three;
    int _whiteRookCount = 4;
    int _whiteBishopCount = 4;
    int _whiteKnightCount = 4;
    int _blackRookCount = 4;
    int _blackBishopCount = 4;
    int _blackKnightCount = 4;

    public GameObject _selectTile;
    Transform _currentPos;
    PieceManager _piece;

    // Start is called before the first frame update
    void Start()
    {
        _piece = GameObject.Find("Piece").GetComponent<PieceManager>();
    }

    /// <summary>
    /// Panelのボタンクリックで駒を配置
    /// </summary>
    public void OnClick()
    {
        //白ターン
        if (GameManager._player == 1)
        {
            if (gameObject.name == "Rook")
            {
                if (_whiteRookCount != 0)
                {
                    SetPiece();
                    GameObject _selectPiece = Instantiate(_selectWhite_One, _currentPos.position, _currentPos.rotation);
                    _piece._whitePieces.Add(_selectPiece);
                    PieceMove _pieceInfo = _selectPiece.GetComponent<PieceMove>();
                    _pieceInfo.SelectAssign();
                    _whiteRookCount--;
                }
                else if (_whiteRookCount == 0)
                {
                    Debug.LogError("この駒はもう置くことが出来ません");
                }
            }
            else if (gameObject.name == "Bishop")
            {
                if (_whiteBishopCount != 0)
                {
                    SetPiece();
                    GameObject _selectPiece = Instantiate(_selectWhite_Two, _currentPos.position, _currentPos.rotation);
                    _piece._whitePieces.Add(_selectPiece);
                    PieceMove _pieceInfo = _selectPiece.GetComponent<PieceMove>();
                    _pieceInfo.SelectAssign();
                    _whiteBishopCount--;
                }
                else if (_whiteBishopCount == 0)
                {
                    Debug.LogError("この駒はもう置くことが出来ません");
                }
            }
            else if (gameObject.name == "Knight")
            {
                if (_whiteKnightCount != 0)
                {
                    SetPiece();
                    GameObject _selectPiece = Instantiate(_selectWhite_Three, _currentPos.position, _currentPos.rotation);
                    _piece._whitePieces.Add(_selectPiece);
                    PieceMove _pieceInfo = _selectPiece.GetComponent<PieceMove>();
                    _pieceInfo.SelectAssign();
                    _whiteKnightCount--;
                }
                else if (_whiteKnightCount == 0)
                {
                    Debug.LogError("この駒はもう置くことが出来ません");
                }
            }
        }
        //黒ターン
        else if (GameManager._player == 2)
        {
            if (gameObject.name == "Rook")
            {
                if (_blackRookCount != 0)
                {
                    SetPiece();
                    GameObject _selectPiece = Instantiate(_selectBlack_One, _currentPos.position, _currentPos.rotation);
                    _piece._blackPieces.Add(_selectPiece);
                    PieceMove _pieceInfo = _selectPiece.GetComponent<PieceMove>();
                    _pieceInfo.SelectAssign();
                    _blackRookCount--;
                }
                else if (_blackRookCount == 0)
                {
                    Debug.LogError("この駒はもう置くことが出来ません");
                }
            }
            else if (gameObject.name == "Bishop")
            {
                if (_blackBishopCount != 0)
                {
                    SetPiece();
                    GameObject _selectPiece = Instantiate(_selectBlack_Two, _currentPos.position, _currentPos.rotation);
                    _piece._blackPieces.Add(_selectPiece);
                    PieceMove _pieceInfo = _selectPiece.GetComponent<PieceMove>();
                    _pieceInfo.SelectAssign();
                    _blackBishopCount--;
                }
                else if (_blackBishopCount == 0)
                {
                    Debug.LogError("この駒はもう置くことが出来ません");
                }
            }
            else if (gameObject.name == "Knight")
            {
                if (_blackKnightCount != 0)
                {
                    SetPiece();
                    GameObject _selectPiece = Instantiate(_selectBlack_Three, _currentPos.position, _currentPos.rotation);
                    _piece._blackPieces.Add(_selectPiece);
                    PieceMove _pieceInfo = _selectPiece.GetComponent<PieceMove>();
                    _pieceInfo.SelectAssign();
                    _blackKnightCount--;
                }
                else if (_blackKnightCount == 0)
                {
                    Debug.LogError("この駒はもう置くことが出来ません");
                }
            }
        }
    }

    private void SetPiece()
    {
        _currentPos = _selectTile.transform;
        Vector3 _pos = _currentPos.position;
        _pos.y = 1f;
        _currentPos.position = _pos;
    }
}
