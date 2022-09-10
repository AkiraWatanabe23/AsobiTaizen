using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// start時に駒を取得
/// </summary>
public class PieceManager : MonoBehaviour
{
    [Tooltip("プロモーション時に表示する")] Image _promImage;
    [Tooltip("どっちのターンか(白)")] Text _whiteTurn;
    [Tooltip("どっちのターンか(黒)")] Text _blackTurn;
    [Tooltip("どっちのターンか(白)")] Image _whiteTurnPanel;
    [Tooltip("どっちのターンか(黒)")] Image _blackTurnPanel;
    [SerializeField] public List<GameObject> _whitePieces = new List<GameObject>();
    [SerializeField] public List<GameObject> _blackPieces = new List<GameObject>();
    [Tooltip("探索範囲にいた獲ることが出来る駒")] public List<GameObject> _getablePieces = new List<GameObject>();
    MasuSearch _search;

    // Start is called before the first frame update
    public void Start()
    {
        _search = GameObject.Find("Board,Tile").GetComponent<MasuSearch>();

        for (int i = 0; i < 16; i++)
        {
            if (transform.GetChild(i).gameObject.tag == "WhitePiece")
            {
                _whitePieces.Add(transform.GetChild(i).gameObject);
            }
            else if (transform.GetChild(i).gameObject.tag == "BlackPiece")
            {
                _blackPieces.Add(transform.GetChild(i).gameObject);
            }
        }
        _promImage = GameObject.Find("PromotionPanel").GetComponent<Image>();
        _promImage.gameObject.SetActive(false);
        //↓ターン表示のText
        _whiteTurn = GameObject.Find("WhiteText").GetComponent<Text>();
        _blackTurn = GameObject.Find("BlackText").GetComponent<Text>();
        _whiteTurn.color = Color.yellow;
        //↓ターン表示のPanel
        _whiteTurnPanel = GameObject.Find("WhiteTurnPanel").GetComponent<Image>();
        _blackTurnPanel = GameObject.Find("BlackTurnPanel").GetComponent<Image>();
        _blackTurnPanel.gameObject.SetActive(false);
    }

    public void ActivePanel()
    {
        _promImage.gameObject.SetActive(true);
    }

    public void SwitchTurnWhite()
    {
        _whiteTurn.color = Color.black;
        _blackTurn.color = Color.yellow;
        _whiteTurnPanel.gameObject.SetActive(false);
        _blackTurnPanel.gameObject.SetActive(true);
    }

    public void SwitchTurnBlack()
    {
        _whiteTurn.color = Color.yellow;
        _blackTurn.color = Color.black;
        _whiteTurnPanel.gameObject.SetActive(true);
        _blackTurnPanel.gameObject.SetActive(false);
    }

    /// <summary>
    /// 獲れる駒を獲らなかった場合にListに戻す
    /// </summary>
    public void UnGetPiece()
    {
        foreach (var i in _getablePieces)
        {
            if (_search._pieceInfo.tag == "BlackPiece")
            {
                _whitePieces.Add(i);
            }
            else if (_search._pieceInfo.gameObject.tag == "WhitePiece")
            {
                _blackPieces.Add(i);
            }
        }
        _getablePieces.Clear();
    }
}
