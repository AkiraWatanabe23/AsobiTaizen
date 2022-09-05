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

    private void Update()
    {
        OnClick();
    }

    /// <summary>
    /// パネルのボタンクリックでプロモーション
    /// </summary>
    public void OnClick()
    {
        if (_promWhite != null && _promBlack == null)
        {
            if (_promWhite.tag == "WhitePiece")
            {
                if (gameObject.name == "Queen")
                {
                    _promWhite.SetActive(false);
                    Instantiate(_promQueenWhite, _promWhite.transform.position, Quaternion.identity);
                    Debug.Log("クイーンにプロモーションします");
                }
                else if (gameObject.name == "Rook")
                {
                    _promWhite.SetActive(false);
                    Instantiate(_promRookWhite, _promWhite.transform.position, Quaternion.identity);
                    Debug.Log("ルークにプロモーションします");
                }
                else if (gameObject.name == "Bishop")
                {
                    _promWhite.SetActive(false);
                    Instantiate(_promBishopWhite, _promWhite.transform.position, Quaternion.identity);
                    Debug.Log("ビショップにプロモーションします");
                }
                else if (gameObject.name == "Knight")
                {
                    _promWhite.SetActive(false);
                    Instantiate(_promKnightWhite, _promWhite.transform.position, Quaternion.identity);
                    Debug.Log("ナイトにプロモーションします");
                }
                _promWhite = null;
                _promBlack = null;
            }
        }
        else if (_promBlack != null && _promWhite == null)
        {
            if (_promBlack.tag == "BlackPiece")
            {
                if (gameObject.name == "Queen")
                {
                    _promBlack.SetActive(false);
                    Instantiate(_promQueenBlack, _promBlack.transform.position, Quaternion.identity);
                    Debug.Log("クイーンにプロモーションします");
                }
                else if (gameObject.name == "Rook")
                {
                    _promBlack.SetActive(false);
                    Instantiate(_promRookBlack, _promBlack.transform.position, Quaternion.identity);
                    Debug.Log("ルークにプロモーションします");
                }
                else if (gameObject.name == "Bishop")
                {
                    _promBlack.SetActive(false);
                    Instantiate(_promBishopBlack, _promBlack.transform.position, Quaternion.identity);
                    Debug.Log("ビショップにプロモーションします");
                }
                else if (gameObject.name == "Knight")
                {
                    _promBlack.SetActive(false);
                    Instantiate(_promKnightBlack, _promBlack.transform.position, Quaternion.identity);
                    Debug.Log("ナイトにプロモーションします");
                }
                _promWhite = null;
                _promBlack = null;
            }
        }
    }
}
