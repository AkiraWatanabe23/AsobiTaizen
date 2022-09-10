using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// start���ɋ���擾
/// </summary>
public class PieceManager : MonoBehaviour
{
    [Tooltip("�v�����[�V�������ɕ\������")] Image _promImage;
    [Tooltip("�ǂ����̃^�[����(��)")] Text _whiteTurn;
    [Tooltip("�ǂ����̃^�[����(��)")] Text _blackTurn;
    [Tooltip("�ǂ����̃^�[����(��)")] Image _whiteTurnPanel;
    [Tooltip("�ǂ����̃^�[����(��)")] Image _blackTurnPanel;
    [SerializeField] public List<GameObject> _whitePieces = new List<GameObject>();
    [SerializeField] public List<GameObject> _blackPieces = new List<GameObject>();
    [Tooltip("�T���͈͂ɂ����l�邱�Ƃ��o�����")] public List<GameObject> _getablePieces = new List<GameObject>();
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
        //���^�[���\����Text
        _whiteTurn = GameObject.Find("WhiteText").GetComponent<Text>();
        _blackTurn = GameObject.Find("BlackText").GetComponent<Text>();
        _whiteTurn.color = Color.yellow;
        //���^�[���\����Panel
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
    /// �l������l��Ȃ������ꍇ��List�ɖ߂�
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
