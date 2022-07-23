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

    [Header("Canvasから白のスコアを表示しているTextObjectをアタッチ")]
    [SerializeField] public Text _scoreWhiteText;
    [Header("Canvasから黒のスコアを表示しているTextObjectをアタッチ")]
    [SerializeField] public Text _scoreBlackText;

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
        _scoreWhiteText.text = _scoreWhite.ToString();
        _scoreBlackText.text = _scoreBlack.ToString();

        if (_scoreWhite == 5 || _scoreBlack == 5)
        {
            SceneManager.LoadScene("ResultScene");
        }
    }
}
