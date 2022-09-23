using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ポーンの特別な動き
/// </summary>
public class Promotion : MonoBehaviour
{
    //プロモーションの駒(白)
    [SerializeField] GameObject _promQueenWhite;
    [SerializeField] GameObject _promRookWhite;
    [SerializeField] GameObject _promBishopWhite;
    [SerializeField] GameObject _promKnightWhite;
    //プロモーションの駒(黒)
    [SerializeField] GameObject _promQueenBlack;
    [SerializeField] GameObject _promRookBlack;
    [SerializeField] GameObject _promBishopBlack;
    [SerializeField] GameObject _promKnightBlack;
    public GameObject _promWhite;
    public GameObject _promBlack;
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
                GameObject _promPiece = Instantiate(_promQueenWhite, _currentPos.position, _currentPos.rotation);
                _piece._whitePieces.Remove(_promWhite);
                _piece._whitePieces.Add(_promPiece);
                Debug.Log("クイーンにプロモーションします");
            }
            else if (gameObject.name == "Rook")
            {
                PromPos();
                GameObject _promPiece = Instantiate(_promRookWhite, _currentPos.position, _currentPos.rotation);
                _piece._whitePieces.Remove(_promWhite);
                _piece._whitePieces.Add(_promPiece);
                Debug.Log("ルークにプロモーションします");
            }
            else if (gameObject.name == "Bishop")
            {
                PromPos();
                GameObject _promPiece = Instantiate(_promBishopWhite, _currentPos.position, _currentPos.rotation);
                _piece._whitePieces.Remove(_promWhite);
                _piece._whitePieces.Add(_promPiece);
                Debug.Log("ビショップにプロモーションします");
            }
            else if (gameObject.name == "Knight")
            {
                PromPos();
                GameObject _promPiece = Instantiate(_promKnightWhite, _currentPos.position, _currentPos.rotation);
                _piece._whitePieces.Remove(_promWhite);
                _piece._whitePieces.Add(_promPiece);
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
                GameObject _promPiece = Instantiate(_promQueenBlack, _currentPos.position, _currentPos.rotation);
                _piece._blackPieces.Remove(_promBlack);
                _piece._blackPieces.Add(_promPiece);
                Debug.Log("クイーンにプロモーションします");
            }
            else if (gameObject.name == "Rook")
            {
                PromPos();
                GameObject _promPiece = Instantiate(_promRookBlack, _currentPos.position, _currentPos.rotation);
                _piece._blackPieces.Remove(_promBlack);
                _piece._blackPieces.Add(_promPiece);
                Debug.Log("ルークにプロモーションします");
            }
            else if (gameObject.name == "Bishop")
            {
                PromPos();
                GameObject _promPiece = Instantiate(_promBishopBlack, _currentPos.position, _currentPos.rotation);
                _piece._blackPieces.Remove(_promBlack);
                _piece._blackPieces.Add(_promPiece);
                Debug.Log("ビショップにプロモーションします");
            }
            else if (gameObject.name == "Knight")
            {
                PromPos();
                GameObject _promPiece = Instantiate(_promKnightBlack, _currentPos.position, _currentPos.rotation);
                _piece._blackPieces.Remove(_promBlack);
                _piece._blackPieces.Add(_promPiece);
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
