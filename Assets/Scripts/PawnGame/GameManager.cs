using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum Phase
{
    White = 0,
    Black = 1,
}

public class GameManager : MonoBehaviour
{
    [SerializeField] public static int _scoreWhite; //白番の得点
    [SerializeField] public static int _scoreBlack; //黒番の得点

    [Header("白のスコアをシーンに表示する"), SerializeField] public Text _scoreWhiteText;
    [Header("黒のスコアをシーンに表示する"), SerializeField] public Text _scoreBlackText;

    [SerializeField] public static Phase _state = Phase.White;


    // Start is called before the first frame update
    void Start()
    {
        _scoreWhite = 0;
        _scoreBlack = 0;

        _state = Phase.White;
    }

    // Update is called once per frame
    void Update()
    {
        _scoreWhiteText.text = _scoreWhite.ToString(); //得点をシーンに表示
        _scoreBlackText.text = _scoreBlack.ToString();

        if (_scoreWhite == 5 || _scoreBlack == 5) //どちらかの得点が目標点に届いたら
        {
            SceneManager.LoadScene("ResultScene");
        }
    }
}
