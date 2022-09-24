using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultSceneManager : MonoBehaviour
{
    [SerializeField] public Text _whiteWinText; //どっちが勝ったか表示するText(白)
    [SerializeField] public Text _blackWinText; //どっちが勝ったか表示するText(黒)
    [SerializeField] public Text _drawText;     //引き分けのText
    public static int _win;

    // Start is called before the first frame update
    void Start()
    {
        //シーンに入ったとき、どっちが勝ったか
        if (_win == 1)
        {
            _whiteWinText.gameObject.SetActive(false);
            _blackWinText.gameObject.SetActive(true);
            _drawText.gameObject.SetActive(false);
        }
        else if (_win == 2)
        {
            _whiteWinText.gameObject.SetActive(true);
            _blackWinText.gameObject.SetActive(false);
            _drawText.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //白の勝ち
        //黒の勝ち
        //引き分け(この時、スコア表示する?)
    }
}