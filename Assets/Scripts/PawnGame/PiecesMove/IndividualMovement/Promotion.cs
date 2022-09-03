using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ポーンの特別な動き
/// </summary>
public class Promotion : MonoBehaviour
{
    MasuSearch _search;
    [SerializeField] GameObject _promQueenWhite;
    [SerializeField] GameObject _promRookWhite;
    [SerializeField] GameObject _promBishopWhite;
    [SerializeField] GameObject _promKnightWhite;

    [SerializeField] GameObject _promQueenBlack;
    [SerializeField] GameObject _promRookBlack;
    [SerializeField] GameObject _promBishopBlack;
    [SerializeField] GameObject _promKnightBlack;

    // Start is called before the first frame update
    void Start()
    {
        _search = GameObject.Find("Board,Tile").GetComponent<MasuSearch>();
    }

    public void OnClick()
    {
        if (_search._pieceInfo.tag == "WhitePiece")
        {
            if (gameObject.name == "Queen")
            {
                Instantiate(_promQueenWhite, _search._pieceInfo.transform.position, Quaternion.identity);
                Debug.Log("クイーンにプロモーションします");
            }
            else if (gameObject.name == "Rook")
            {
                Instantiate(_promRookWhite, _search._pieceInfo.transform.position, Quaternion.identity);
                Debug.Log("ルークにプロモーションします");
            }
            else if (gameObject.name == "Bishop")
            {
                Instantiate(_promBishopWhite, _search._pieceInfo.transform.position, Quaternion.identity);
                Debug.Log("ビショップにプロモーションします");
            }
            else if (gameObject.name == "Knight")
            {
                Instantiate(_promKnightWhite, _search._pieceInfo.transform.position, Quaternion.identity);
                Debug.Log("ナイトにプロモーションします");
            }
        }
        else if (_search._pieceInfo.tag == "BlackPiece")
        {
            if (gameObject.name == "Queen")
            {
                Instantiate(_promQueenBlack, _search._pieceInfo.transform.position, Quaternion.identity);
                Debug.Log("クイーンにプロモーションします");
            }
            else if (gameObject.name == "Rook")
            {
                Instantiate(_promRookBlack, _search._pieceInfo.transform.position, Quaternion.identity);
                Debug.Log("ルークにプロモーションします");
            }
            else if (gameObject.name == "Bishop")
            {
                Instantiate(_promBishopBlack, _search._pieceInfo.transform.position, Quaternion.identity);
                Debug.Log("ビショップにプロモーションします");
            }
            else if (gameObject.name == "Knight")
            {
                Instantiate(_promKnightBlack, _search._pieceInfo.transform.position, Quaternion.identity);
                Debug.Log("ナイトにプロモーションします");
            }
        }
    }
}
