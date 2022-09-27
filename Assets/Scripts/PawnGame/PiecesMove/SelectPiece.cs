using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// マスに配置する駒を選ぶ
/// </summary>
public class SelectPiece : MonoBehaviour
{
    [SerializeField] GameObject[] _selectPieces = new GameObject[6];
    [Header("駒を選ぶPanel")]
    [SerializeField] Image _whitePanel;
    [SerializeField] Image _blackPanel;
    SelectTile _select;

    // Start is called before the first frame update
    void Start()
    {
        _select = GameObject.Find("GameManager").GetComponent<SelectTile>();
    }

    /// <summary>
    /// Panelのボタンクリックで駒を配置
    /// </summary>
    public void OnClick()
    {
        //白ターン
        if (GameManager.Player == 1)
        {
            if (gameObject.name == "Rook")
            {
                if (_select.SelectPieceCount[0] != 0)
                {
                    _select.setPiece = _selectPieces[0];
                    _select.SelectPieceCount[0]--;
                    print($"白ルークはあと {_select.SelectPieceCount[0]} 個");
                    UISwitch();
                }
                else if (_select.SelectPieceCount[0] == 0)
                {
                    Debug.LogError("この駒はもう置くことが出来ません");
                }
            }
            else if (gameObject.name == "Bishop")
            {
                if (_select.SelectPieceCount[1] != 0)
                {
                    _select.setPiece = _selectPieces[1];
                    _select.SelectPieceCount[1]--;
                    print($"白ビショップはあと {_select.SelectPieceCount[1]} 個");
                    UISwitch();
                }
                else if (_select.SelectPieceCount[1] == 0)
                {
                    Debug.LogError("この駒はもう置くことが出来ません");
                }
            }
            else if (gameObject.name == "Knight")
            {
                if (_select.SelectPieceCount[2] != 0)
                {
                    _select.setPiece = _selectPieces[2];
                    _select.SelectPieceCount[2]--;
                    print($"白ナイトはあと {_select.SelectPieceCount[2]} 個");
                    UISwitch();
                }
                else if (_select.SelectPieceCount[2] == 0)
                {
                    Debug.LogError("この駒はもう置くことが出来ません");
                }
            }
        }
        //黒ターン
        else if (GameManager.Player == 2)
        {
            if (gameObject.name == "Rook")
            {
                if (_select.SelectPieceCount[3] != 0)
                {
                    _select.setPiece = _selectPieces[3];
                    _select.SelectPieceCount[3]--;
                    print($"黒ルークはあと {_select.SelectPieceCount[3]} 個");
                    UISwitch();
                }
                else if (_select.SelectPieceCount[3] == 0)
                {
                    Debug.LogError("この駒はもう置くことが出来ません");
                }
            }
            else if (gameObject.name == "Bishop")
            {
                if (_select.SelectPieceCount[4] != 0)
                {
                    _select.setPiece = _selectPieces[4];
                    _select.SelectPieceCount[4]--;
                    print($"黒ルークはあと {_select.SelectPieceCount[4]} 個");
                    UISwitch();
                }
                else if (_select.SelectPieceCount[4] == 0)
                {
                    Debug.LogError("この駒はもう置くことが出来ません");
                }
            }
            else if (gameObject.name == "Knight")
            {
                if (_select.SelectPieceCount[5] != 0)
                {
                    _select.setPiece = _selectPieces[5];
                    _select.SelectPieceCount[5]--;
                    print($"黒ルークはあと {_select.SelectPieceCount[5]} 個");
                    UISwitch();
                }
                else if (_select.SelectPieceCount[5] == 0)
                {
                    Debug.LogError("この駒はもう置くことが出来ません");
                }
            }
        }
        _select.SetPiece();
    }

    void UISwitch()
    {
        if (GameManager.Player == 1)
        {
            _whitePanel.gameObject.SetActive(false);
        }
        else if (GameManager.Player == 2)
        {
            _blackPanel.gameObject.SetActive(false);
        }
        _select._whereText.gameObject.SetActive(true);
    }
}
