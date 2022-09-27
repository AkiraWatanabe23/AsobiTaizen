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
    [Header("���I��Panel")]
    [SerializeField] Image _whitePanel;
    [SerializeField] Image _blackPanel;
    SelectTile _select;

    // Start is called before the first frame update
    void Start()
    {
        _select = GameObject.Find("GameManager").GetComponent<SelectTile>();
    }

    /// <summary>
    /// Panel�̃{�^���N���b�N�ŋ��z�u
    /// </summary>
    public void OnClick()
    {
        //���^�[��
        if (GameManager.Player == 1)
        {
            if (gameObject.name == "Rook")
            {
                if (_select._selectPieceCount[0] != 0)
                {
                    _select._set = _selectPieces[0];
                    _select._selectPieceCount[0]--;
                    print($"�����[�N�͂��� {_select._selectPieceCount[0]} ��");
                    UISwitch();
                }
                else if (_select._selectPieceCount[0] == 0)
                {
                    Debug.LogError("���̋�͂����u�����Ƃ��o���܂���");
                }
            }
            else if (gameObject.name == "Bishop")
            {
                if (_select._selectPieceCount[1] != 0)
                {
                    _select._set = _selectPieces[1];
                    _select._selectPieceCount[1]--;
                    print($"���r�V���b�v�͂��� {_select._selectPieceCount[1]} ��");
                    UISwitch();
                }
                else if (_select._selectPieceCount[1] == 0)
                {
                    Debug.LogError("���̋�͂����u�����Ƃ��o���܂���");
                }
            }
            else if (gameObject.name == "Knight")
            {
                if (_select._selectPieceCount[2] != 0)
                {
                    _select._set = _selectPieces[2];
                    _select._selectPieceCount[2]--;
                    print($"���i�C�g�͂��� {_select._selectPieceCount[2]} ��");
                    UISwitch();
                }
                else if (_select._selectPieceCount[2] == 0)
                {
                    Debug.LogError("���̋�͂����u�����Ƃ��o���܂���");
                }
            }
        }
        //���^�[��
        else if (GameManager.Player == 2)
        {
            if (gameObject.name == "Rook")
            {
                if (_select._selectPieceCount[3] != 0)
                {
                    _select._set = _selectPieces[3];
                    _select._selectPieceCount[3]--;
                    print($"�����[�N�͂��� {_select._selectPieceCount[3]} ��");
                    UISwitch();
                }
                else if (_select._selectPieceCount[3] == 0)
                {
                    Debug.LogError("���̋�͂����u�����Ƃ��o���܂���");
                }
            }
            else if (gameObject.name == "Bishop")
            {
                if (_select._selectPieceCount[4] != 0)
                {
                    _select._set = _selectPieces[4];
                    _select._selectPieceCount[4]--;
                    print($"�����[�N�͂��� {_select._selectPieceCount[4]} ��");
                    UISwitch();
                }
                else if (_select._selectPieceCount[4] == 0)
                {
                    Debug.LogError("���̋�͂����u�����Ƃ��o���܂���");
                }
            }
            else if (gameObject.name == "Knight")
            {
                if (_select._selectPieceCount[5] != 0)
                {
                    _select._set = _selectPieces[5];
                    _select._selectPieceCount[5]--;
                    print($"�����[�N�͂��� {_select._selectPieceCount[5]} ��");
                    UISwitch();
                }
                else if (_select._selectPieceCount[5] == 0)
                {
                    Debug.LogError("���̋�͂����u�����Ƃ��o���܂���");
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
