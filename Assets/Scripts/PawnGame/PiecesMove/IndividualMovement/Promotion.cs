using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ポーンの特別な動き
/// </summary>
public class Promotion : MonoBehaviour
{
    //プロモーションの駒
    [SerializeField] GameObject[] _promPieces = new GameObject[8];
    GameObject _promWhite;
    GameObject _promBlack;
    Transform _currentPos;
    PieceManager _piece;

    private void Start()
    {
        _piece = GameObject.Find("Piece").GetComponent<PieceManager>();
    }

    /// <summary>
    /// パネルのボタンクリックでプロモーション
    /// </summary>
    public void OnClick()
    {
        //白駒のプロモーション
        if (_promWhite != null && _promBlack == null)
        {
            //↓(this.)gameObject...クリックするボタンのこと
            if (gameObject.name == "Queen")
            {
                PromPos();
                GameObject _promPiece = Instantiate(_promPieces[0], _currentPos.position, _currentPos.rotation);
                _piece.WhitePieces.Remove(_promWhite);
                _piece.WhitePieces.Add(_promPiece);
                Debug.Log("クイーンにプロモーションします");
            }
            else if (gameObject.name == "Rook")
            {
                PromPos();
                GameObject _promPiece = Instantiate(_promPieces[1], _currentPos.position, _currentPos.rotation);
                _piece.WhitePieces.Remove(_promWhite);
                _piece.WhitePieces.Add(_promPiece);
                Debug.Log("ルークにプロモーションします");
            }
            else if (gameObject.name == "Bishop")
            {
                PromPos();
                GameObject _promPiece = Instantiate(_promPieces[2], _currentPos.position, _currentPos.rotation);
                _piece.WhitePieces.Remove(_promWhite);
                _piece.WhitePieces.Add(_promPiece);
                Debug.Log("ビショップにプロモーションします");
            }
            else if (gameObject.name == "Knight")
            {
                PromPos();
                GameObject _promPiece = Instantiate(_promPieces[3], _currentPos.position, _currentPos.rotation);
                _piece.WhitePieces.Remove(_promWhite);
                _piece.WhitePieces.Add(_promPiece);
                Debug.Log("ナイトにプロモーションします");
            }
            _promWhite = null;
            _promBlack = null;
        }
        //黒駒のプロモーション
        else if (_promWhite == null && _promBlack != null)
        {
            if (gameObject.name == "Queen")
            {
                PromPos();
                GameObject _promPiece = Instantiate(_promPieces[4], _currentPos.position, _currentPos.rotation);
                _piece.BlackPieces.Remove(_promBlack);
                _piece.BlackPieces.Add(_promPiece);
                Debug.Log("クイーンにプロモーションします");
            }
            else if (gameObject.name == "Rook")
            {
                PromPos();
                GameObject _promPiece = Instantiate(_promPieces[5], _currentPos.position, _currentPos.rotation);
                _piece.BlackPieces.Remove(_promBlack);
                _piece.BlackPieces.Add(_promPiece);
                Debug.Log("ルークにプロモーションします");
            }
            else if (gameObject.name == "Bishop")
            {
                PromPos();
                GameObject _promPiece = Instantiate(_promPieces[6], _currentPos.position, _currentPos.rotation);
                _piece.BlackPieces.Remove(_promBlack);
                _piece.BlackPieces.Add(_promPiece);
                Debug.Log("ビショップにプロモーションします");
            }
            else if (gameObject.name == "Knight")
            {
                PromPos();
                GameObject _promPiece = Instantiate(_promPieces[7], _currentPos.position, _currentPos.rotation);
                _piece.BlackPieces.Remove(_promBlack);
                _piece.BlackPieces.Add(_promPiece);
                Debug.Log("ナイトにプロモーションします");
            }
            _promWhite = null;
            _promBlack = null;
        }
    }

    /// <summary>
    /// プロモーション時の位置情報
    /// </summary>
    private void PromPos()
    {
        if (_promWhite != null && _promBlack == null)
        {
            _currentPos = _promWhite.transform;
            Destroy(_promWhite);
        }
        else if (_promWhite == null && _promBlack != null)
        {
            _currentPos = _promBlack.transform;
            Destroy(_promBlack);
        }

        Vector3 _pos = _currentPos.position;
        _pos.y = 1f;
        _currentPos.position = _pos;
    }
}
