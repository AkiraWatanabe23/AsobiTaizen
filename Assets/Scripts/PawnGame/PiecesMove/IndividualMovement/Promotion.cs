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
    AfterPromotion _aftProm;

    private void Start()
    {
        _aftProm = GameObject.Find("PromotionPanel").GetComponent<AfterPromotion>();
    }


    /// <summary>
    /// パネルのボタンクリックでプロモーション
    /// </summary>
    public void OnClick()
    {
        //白駒のプロモーション
        if (_promWhite != null && _promBlack == null)
        {
            if (gameObject.name == "Queen")
            {
                _promWhite.SetActive(false);
                GameObject _promPiece = Instantiate(_promQueenWhite, _promWhite.transform.position, Quaternion.identity);
                PieceMove _piece = _promPiece.GetComponent<PieceMove>();
                _piece.TestAsign();
                Debug.Log("クイーンにプロモーションします");
            }
            else if (gameObject.name == "Rook")
            {
                _promWhite.SetActive(false);
                GameObject _promPiece = Instantiate(_promRookWhite, _promWhite.transform.position, Quaternion.identity);
                PieceMove _piece = _promPiece.GetComponent<PieceMove>();
                _piece.TestAsign();
                Debug.Log("ルークにプロモーションします");
            }
            else if (gameObject.name == "Bishop")
            {
                _promWhite.SetActive(false);
                GameObject _promPiece = Instantiate(_promBishopWhite, _promWhite.transform.position, Quaternion.identity);
                PieceMove _piece = _promPiece.GetComponent<PieceMove>();
                _piece.TestAsign();
                Debug.Log("ビショップにプロモーションします");
            }
            else if (gameObject.name == "Knight")
            {
                _promWhite.SetActive(false);
                GameObject _promPiece = Instantiate(_promKnightWhite, _promWhite.transform.position, Quaternion.identity);
                PieceMove _piece = _promPiece.GetComponent<PieceMove>();
                _piece.TestAsign();
                Debug.Log("ナイトにプロモーションします");
            }
            _aftProm.PromReturn();
        }
        //黒駒のプロモーション
        else if (_promBlack != null && _promWhite == null)
        {
            if (gameObject.name == "Queen")
            {
                _promBlack.SetActive(false);
                GameObject _promPiece = Instantiate(_promQueenBlack, _promBlack.transform.position, Quaternion.identity);
                PieceMove _piece = _promPiece.GetComponent<PieceMove>();
                _piece.TestAsign();
                Debug.Log("クイーンにプロモーションします");
            }
            else if (gameObject.name == "Rook")
            {
                _promBlack.SetActive(false);
                GameObject _promPiece = Instantiate(_promRookBlack, _promBlack.transform.position, Quaternion.identity);
                PieceMove _piece = _promPiece.GetComponent<PieceMove>();
                _piece.TestAsign();
                Debug.Log("ルークにプロモーションします");
            }
            else if (gameObject.name == "Bishop")
            {
                _promBlack.SetActive(false);
                GameObject _promPiece = Instantiate(_promBishopBlack, _promBlack.transform.position, Quaternion.identity);
                PieceMove _piece = _promPiece.GetComponent<PieceMove>();
                _piece.TestAsign();
                Debug.Log("ビショップにプロモーションします");
            }
            else if (gameObject.name == "Knight")
            {
                _promBlack.SetActive(false);
                GameObject _promPiece = Instantiate(_promKnightBlack, _promBlack.transform.position, Quaternion.identity);
                PieceMove _piece = _promPiece.GetComponent<PieceMove>();
                _piece.TestAsign();
                Debug.Log("ナイトにプロモーションします");
            }
            _aftProm.PromReturn();
        }
    }
}
