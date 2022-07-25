using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultSceneManager : MonoBehaviour
{
    [SerializeField] public Text _whiteWinText; //どっちが勝ったか表示するText(白)
    [SerializeField] public Text _blackWinText; //どっちが勝ったか表示するText(黒)
    [SerializeField] public Text _drawText;     //引き分けのText

    [Tooltip("最終的な得点を表示する(白)"), SerializeField] public Text _whiteScoreText;
    [Tooltip("最終的な得点を表示する(黒)"), SerializeField] public Text _blackScoreText;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager._scoreWhite == 5)
        {
            Debug.Log("WhiteWin!!");
        }
        else if (GameManager._scoreBlack == 5)
        {
            Debug.Log("BlackWin!!");
        }
        else
        {
            Debug.Log("Draw...");
        }
    }

    // Update is called once per frame
    void Update()
    {
        _whiteScoreText.text = GameManager._scoreWhite.ToString();
        _blackScoreText.text = GameManager._scoreBlack.ToString();

        if (GameManager._scoreWhite == 5)
        {
            _whiteWinText.gameObject.SetActive(true); //「白の勝ち」のText を表示
            _blackWinText.gameObject.SetActive(false);
            _drawText.gameObject.SetActive(false);
        }
        else if (GameManager._scoreBlack == 5)
        {
            _whiteWinText.gameObject.SetActive(false); //「黒の勝ち」のText を表示
            _blackWinText.gameObject.SetActive(true);
            _drawText.gameObject.SetActive(false);
        }
        else
        {
            _whiteWinText.gameObject.SetActive(false);
            _blackWinText.gameObject.SetActive(false);
            _drawText.gameObject.SetActive(true);
        }
    }
}
