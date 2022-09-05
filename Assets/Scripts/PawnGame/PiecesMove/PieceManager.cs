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

    // Start is called before the first frame update
    public void Start()
    {
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

    public void AfterProm()
    {
        foreach (Promotion i in GetComponentsInChildren<Promotion>())
        {
            i._promWhite = null;
            i._promBlack = null;
        }
    }
}
