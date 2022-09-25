using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Panelに設置したボタンの駒を選ぶスクリプト
/// </summary>
public class SelectPiece : MonoBehaviour
{
    [SerializeField] GameObject[] _selectPieces = new GameObject[6];
    int[] _selectPieceCount = new int[6];
    [SerializeField] Text[] _countText = new Text[6];
    [Header("駒を選ぶPanel")]
    [SerializeField] Image _whitePanel;
    [SerializeField] Image _blackPanel;
    SelectTile _select;

    // Start is called before the first frame update
    void Start()
    {
        _select = GameObject.Find("GameManager").GetComponent<SelectTile>();
        for (int i = 0; i < _selectPieceCount.Length; i++)
        {
            _selectPieceCount[i] = 4;
        }
    }

    void Update()
    {
        for (int i = 0; i < 6; i++)
        {
            _countText[i].text = _selectPieceCount[i].ToString();
        }
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
                if (_selectPieceCount[0] != 0)
                {
                    _select._set = _selectPieces[0];
                    _selectPieceCount[0]--;
                    print($"白ルークはあと {_selectPieceCount[0]} 個");
                    UISwitch();
                }
                else if (_selectPieceCount[0] == 0)
                {
                    Debug.LogError("この駒はもう置くことが出来ません");
                }
            }
            else if (gameObject.name == "Bishop")
            {
                if (_selectPieceCount[1] != 0)
                {
                    _select._set = _selectPieces[1];
                    _selectPieceCount[1]--;
                    print($"白ビショップはあと {_selectPieceCount[1]} 個");
                    UISwitch();
                }
                else if (_selectPieceCount[1] == 0)
                {
                    Debug.LogError("この駒はもう置くことが出来ません");
                }
            }
            else if (gameObject.name == "Knight")
            {
                if (_selectPieceCount[2] != 0)
                {
                    _select._set = _selectPieces[2];
                    _selectPieceCount[2]--;
                    print($"白ナイトはあと {_selectPieceCount[2]} 個");
                    UISwitch();
                }
                else if (_selectPieceCount[2] == 0)
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
                if (_selectPieceCount[3] != 0)
                {
                    _select._set = _selectPieces[3];
                    _selectPieceCount[3]--;
                    print($"黒ルークはあと {_selectPieceCount[3]} 個");
                    UISwitch();
                }
                else if (_selectPieceCount[3] == 0)
                {
                    Debug.LogError("この駒はもう置くことが出来ません");
                }
            }
            else if (gameObject.name == "Bishop")
            {
                if (_selectPieceCount[4] != 0)
                {
                    _select._set = _selectPieces[4];
                    _selectPieceCount[4]--;
                    print($"黒ルークはあと {_selectPieceCount[4]} 個");
                    UISwitch();
                }
                else if (_selectPieceCount[4] == 0)
                {
                    Debug.LogError("この駒はもう置くことが出来ません");
                }
            }
            else if (gameObject.name == "Knight")
            {
                if (_selectPieceCount[5] != 0)
                {
                    _select._set = _selectPieces[5];
                    _selectPieceCount[5]--;
                    print($"黒ルークはあと {_selectPieceCount[5]} 個");
                    UISwitch();
                }
                else if (_selectPieceCount[5] == 0)
                {
                    Debug.LogError("この駒はもう置くことが出来ません");
                }
            }
        }
        _select.SetPiece();
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
        _select._whereText.gameObject.SetActive(true);
    }
}
