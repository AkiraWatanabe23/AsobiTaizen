using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [Header("駒を選ぶPanel")]
    [SerializeField] Image _whitePanel;
    [SerializeField] Image _blackPanel;
    [SerializeField] Text _whereText;
    SelectTile _select;

    // Start is called before the first frame update
    void Start()
    {
        _select = GameObject.Find("GameManager").GetComponent< SelectTile > ();
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
                    _select._set = _selectWhite_One;
                    _whiteRookCount--;
                    print($"白ルークはあと {_whiteRookCount} 個");
                    UISwitch();
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
                    _select._set = _selectWhite_Two;
                    _whiteBishopCount--;
                    print($"白ビショップはあと {_whiteBishopCount} 個");
                    UISwitch();
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
                    _select._set = _selectWhite_Three;
                    _whiteKnightCount--;
                    print($"白ナイトはあと {_whiteKnightCount} 個");
                    UISwitch();
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
                    _select._set = _selectBlack_One;
                    _blackRookCount--;
                    print($"黒ルークはあと {_blackRookCount} 個");
                    UISwitch();
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
                    _select._set = _selectBlack_Two;
                    _blackBishopCount--;
                    print($"黒ルークはあと {_blackBishopCount} 個");
                    UISwitch();
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
                    _select._set = _selectBlack_Three;
                    _blackKnightCount--;
                    print($"黒ルークはあと {_blackKnightCount} 個");
                    UISwitch();
                }
                else if (_blackKnightCount == 0)
                {
                    Debug.LogError("この駒はもう置くことが出来ません");
                }
            }
        }
    }

    void UISwitch()
    {
        if (GameManager._player == 1)
        {
            _whitePanel.gameObject.SetActive(false);
        }
        else if (GameManager._player == 2)
        {
            _blackPanel.gameObject.SetActive(false);
        }
        _whereText.gameObject.SetActive(true);
        _select.SetPiece();
    }
}
