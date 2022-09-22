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
        //シーンに入ったとき、どっちが勝ったか
    }

    // Update is called once per frame
    void Update()
    {
        //白の勝ち
        //黒の勝ち
        //引き分け(この時、スコア表示する?)
    }
}