using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Panel�ɐݒu�����{�^���̋��I�ԃX�N���v�g
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
    [Header("���I��Panel")]
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
    /// Panel�̃{�^���N���b�N�ŋ��z�u
    /// </summary>
    public void OnClick()
    {
        //���^�[��
        if (GameManager._player == 1)
        {
            if (gameObject.name == "Rook")
            {
                if (_whiteRookCount != 0)
                {
                    _select._set = _selectWhite_One;
                    _whiteRookCount--;
                    print($"�����[�N�͂��� {_whiteRookCount} ��");
                    UISwitch();
                }
                else if (_whiteRookCount == 0)
                {
                    Debug.LogError("���̋�͂����u�����Ƃ��o���܂���");
                }
            }
            else if (gameObject.name == "Bishop")
            {
                if (_whiteBishopCount != 0)
                {
                    _select._set = _selectWhite_Two;
                    _whiteBishopCount--;
                    print($"���r�V���b�v�͂��� {_whiteBishopCount} ��");
                    UISwitch();
                }
                else if (_whiteBishopCount == 0)
                {
                    Debug.LogError("���̋�͂����u�����Ƃ��o���܂���");
                }
            }
            else if (gameObject.name == "Knight")
            {
                if (_whiteKnightCount != 0)
                {
                    _select._set = _selectWhite_Three;
                    _whiteKnightCount--;
                    print($"���i�C�g�͂��� {_whiteKnightCount} ��");
                    UISwitch();
                }
                else if (_whiteKnightCount == 0)
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
                if (_blackRookCount != 0)
                {
                    _select._set = _selectBlack_One;
                    _blackRookCount--;
                    print($"�����[�N�͂��� {_blackRookCount} ��");
                    UISwitch();
                }
                else if (_blackRookCount == 0)
                {
                    Debug.LogError("���̋�͂����u�����Ƃ��o���܂���");
                }
            }
            else if (gameObject.name == "Bishop")
            {
                if (_blackBishopCount != 0)
                {
                    _select._set = _selectBlack_Two;
                    _blackBishopCount--;
                    print($"�����[�N�͂��� {_blackBishopCount} ��");
                    UISwitch();
                }
                else if (_blackBishopCount == 0)
                {
                    Debug.LogError("���̋�͂����u�����Ƃ��o���܂���");
                }
            }
            else if (gameObject.name == "Knight")
            {
                if (_blackKnightCount != 0)
                {
                    _select._set = _selectBlack_Three;
                    _blackKnightCount--;
                    print($"�����[�N�͂��� {_blackKnightCount} ��");
                    UISwitch();
                }
                else if (_blackKnightCount == 0)
                {
                    Debug.LogError("���̋�͂����u�����Ƃ��o���܂���");
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
