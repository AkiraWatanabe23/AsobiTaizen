using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �}�X�ɔz�u������I��
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
                if (_select.SelectPieceCount[0] != 0)
                {
                    _select.setPiece = _selectPieces[0];
                    _select.SelectPieceCount[0]--;
                    print($"�����[�N�͂��� {_select.SelectPieceCount[0]} ��");
                    UISwitch();
                }
                else if (_select.SelectPieceCount[0] == 0)
                {
                    Debug.LogError("���̋�͂����u�����Ƃ��o���܂���");
                }
            }
            else if (gameObject.name == "Bishop")
            {
                if (_select.SelectPieceCount[1] != 0)
                {
                    _select.setPiece = _selectPieces[1];
                    _select.SelectPieceCount[1]--;
                    print($"���r�V���b�v�͂��� {_select.SelectPieceCount[1]} ��");
                    UISwitch();
                }
                else if (_select.SelectPieceCount[1] == 0)
                {
                    Debug.LogError("���̋�͂����u�����Ƃ��o���܂���");
                }
            }
            else if (gameObject.name == "Knight")
            {
                if (_select.SelectPieceCount[2] != 0)
                {
                    _select.setPiece = _selectPieces[2];
                    _select.SelectPieceCount[2]--;
                    print($"���i�C�g�͂��� {_select.SelectPieceCount[2]} ��");
                    UISwitch();
                }
                else if (_select.SelectPieceCount[2] == 0)
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
                if (_select.SelectPieceCount[3] != 0)
                {
                    _select.setPiece = _selectPieces[3];
                    _select.SelectPieceCount[3]--;
                    print($"�����[�N�͂��� {_select.SelectPieceCount[3]} ��");
                    UISwitch();
                }
                else if (_select.SelectPieceCount[3] == 0)
                {
                    Debug.LogError("���̋�͂����u�����Ƃ��o���܂���");
                }
            }
            else if (gameObject.name == "Bishop")
            {
                if (_select.SelectPieceCount[4] != 0)
                {
                    _select.setPiece = _selectPieces[4];
                    _select.SelectPieceCount[4]--;
                    print($"�����[�N�͂��� {_select.SelectPieceCount[4]} ��");
                    UISwitch();
                }
                else if (_select.SelectPieceCount[4] == 0)
                {
                    Debug.LogError("���̋�͂����u�����Ƃ��o���܂���");
                }
            }
            else if (gameObject.name == "Knight")
            {
                if (_select.SelectPieceCount[5] != 0)
                {
                    _select.setPiece = _selectPieces[5];
                    _select.SelectPieceCount[5]--;
                    print($"�����[�N�͂��� {_select.SelectPieceCount[5]} ��");
                    UISwitch();
                }
                else if (_select.SelectPieceCount[5] == 0)
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
