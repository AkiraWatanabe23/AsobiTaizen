using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultSceneManager : MonoBehaviour
{
    [SerializeField] Text _whiteWinText; //どっちが勝ったか表示するText(白)
    [SerializeField] Text _blackWinText; //どっちが勝ったか表示するText(黒)
    public static int Win { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        //シーンに入ったとき、どっちが勝ったか
        if (Win == 1)
        {
            _whiteWinText.gameObject.SetActive(false);
            _blackWinText.gameObject.SetActive(true);
        }
        else if (Win == 2)
        {
            _whiteWinText.gameObject.SetActive(true);
            _blackWinText.gameObject.SetActive(false);
        }
    }
}