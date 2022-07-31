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
        if (GameManager._scoreWhite == GameManager._finalScore)
        {
            Debug.Log("WhiteWin!!");
        }
        else if (GameManager._scoreBlack == GameManager._finalScore)
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

        //白の勝ち
        if (GameManager._scoreWhite == GameManager._finalScore || GameManager._bPieceCount == 0)
        {
            _whiteWinText.gameObject.SetActive(true); //「白の勝ち」のText を表示
            _blackWinText.gameObject.SetActive(false);
            _drawText.gameObject.SetActive(false);
        }
        //黒の勝ち
        else if (GameManager._scoreBlack == GameManager._finalScore || GameManager._wPieceCount == 0)
        {
            _whiteWinText.gameObject.SetActive(false);
            _blackWinText.gameObject.SetActive(true); //「黒の勝ち」のText を表示
            _drawText.gameObject.SetActive(false);
        }
        //引き分け(この時、スコア表示する?)
        //↑条件次第(駒の数がそれぞれ1つなら得点関係なく引き分け)
        //　　　　　(ステイルメイト...駒が動かせない場合は、得点の高い方の勝ち、かなぁ...)
        else
        {
            _whiteWinText.gameObject.SetActive(false);
            _blackWinText.gameObject.SetActive(false);
            _drawText.gameObject.SetActive(true); //「引き分け」のText を表示
        }
    }
}