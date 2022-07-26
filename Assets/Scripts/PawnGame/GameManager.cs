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
    [SerializeField] public static int _finalScore; //目標点
    //駒の数(白、黒それぞれ)を取得する
    public static int _wPieceCount = 0;
    public static int _bPieceCount = 0;

    [Header("白のスコアをシーンに表示する")]
    [SerializeField] public Text _scoreWhiteText;
    [Header("黒のスコアをシーンに表示する")]
    [SerializeField] public Text _scoreBlackText;
    [SerializeField] public GameObject _resultPanel; //Panel(UI)は、UIではなく、GameObjectとして扱う

    [SerializeField] public static Phase _state = Phase.White;


    // Start is called before the first frame update
    void Start()
    {
        _scoreWhite = 0;
        _scoreBlack = 0;
        _finalScore = 5;

        _state = Phase.White;
        _resultPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        _scoreWhiteText.text = _scoreWhite.ToString(); //得点をシーンに表示
        _scoreBlackText.text = _scoreBlack.ToString();

        //ゲーム開始時に、白黒それぞれの駒の数を取得する
        //Startでやると、駒の数が変わった時に変更を取得出来ない...
        _wPieceCount = GameObject.FindGameObjectsWithTag("WhitePiece").Length;
        _bPieceCount = GameObject.FindGameObjectsWithTag("BlackPiece").Length;

        //勝利時(得点による)のシーン遷移
        //一定点獲得したら
        if (_scoreWhite == _finalScore || _scoreBlack == _finalScore)
        {
            _resultPanel.SetActive(true);
            Invoke("GoResult", 2f); //2秒後にChangeResultの処理を実行する
        }
        //敵の駒が0になったら
        else if (_wPieceCount == 0 || _bPieceCount == 0)
        {
            _resultPanel.SetActive(true);
            Invoke("GoResult", 2f);
        }

        //引き分け時のシーン遷移
        if (_wPieceCount == 1 && _bPieceCount == 1)
        {
            _resultPanel.SetActive(true);
            Invoke("GoResult", 2f); //2秒後にChangeResultの処理を実行する
        }
    }

    void GoResult()
    {
        SceneManager.LoadScene("ChessResult");
    }
}
