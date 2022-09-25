using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Panel�ɐݒu�����{�^���̋��I�ԃX�N���v�g
/// </summary>
public class SelectPiece : MonoBehaviour
{
    [SerializeField] GameObject[] _selectPieces = new GameObject[6];
    int[] _selectPieceCount = new int[6];
    [SerializeField] Text[] _countText = new Text[6];
    [Header("���I��Panel")]
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
    /// Panel�̃{�^���N���b�N�ŋ��z�u
    /// </summary>
    public void OnClick()
    {
        //���^�[��
        if (GameManager._player == 1)
        {
            if (gameObject.name == "Rook")
            {
                if (_selectPieceCount[0] != 0)
                {
                    _select._set = _selectPieces[0];
                    _selectPieceCount[0]--;
                    print($"�����[�N�͂��� {_selectPieceCount[0]} ��");
                    UISwitch();
                }
                else if (_selectPieceCount[0] == 0)
                {
                    Debug.LogError("���̋�͂����u�����Ƃ��o���܂���");
                }
            }
            else if (gameObject.name == "Bishop")
            {
                if (_selectPieceCount[1] != 0)
                {
                    _select._set = _selectPieces[1];
                    _selectPieceCount[1]--;
                    print($"���r�V���b�v�͂��� {_selectPieceCount[1]} ��");
                    UISwitch();
                }
                else if (_selectPieceCount[1] == 0)
                {
                    Debug.LogError("���̋�͂����u�����Ƃ��o���܂���");
                }
            }
            else if (gameObject.name == "Knight")
            {
                if (_selectPieceCount[2] != 0)
                {
                    _select._set = _selectPieces[2];
                    _selectPieceCount[2]--;
                    print($"���i�C�g�͂��� {_selectPieceCount[2]} ��");
                    UISwitch();
                }
                else if (_selectPieceCount[2] == 0)
                {
                    Debug.LogError("���̋�͂����u�����Ƃ��o���܂���");
                }
            }
        }
        //���^�[��
        else if (GameManager._player == 2)
        {
            if (gameObject.name == "Rook")
            {
                if (_selectPieceCount[3] != 0)
                {
                    _select._set = _selectPieces[3];
                    _selectPieceCount[3]--;
                    print($"�����[�N�͂��� {_selectPieceCount[3]} ��");
                    UISwitch();
                }
                else if (_selectPieceCount[3] == 0)
                {
                    Debug.LogError("���̋�͂����u�����Ƃ��o���܂���");
                }
            }
            else if (gameObject.name == "Bishop")
            {
                if (_selectPieceCount[4] != 0)
                {
                    _select._set = _selectPieces[4];
                    _selectPieceCount[4]--;
                    print($"�����[�N�͂��� {_selectPieceCount[4]} ��");
                    UISwitch();
                }
                else if (_selectPieceCount[4] == 0)
                {
                    Debug.LogError("���̋�͂����u�����Ƃ��o���܂���");
                }
            }
            else if (gameObject.name == "Knight")
            {
                if (_selectPieceCount[5] != 0)
                {
                    _select._set = _selectPieces[5];
                    _selectPieceCount[5]--;
                    print($"�����[�N�͂��� {_selectPieceCount[5]} ��");
                    UISwitch();
                }
                else if (_selectPieceCount[5] == 0)
                {
                    Debug.LogError("���̋�͂����u�����Ƃ��o���܂���");
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
