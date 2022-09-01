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

/// <summary>
/// ゲーム全体の管理
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary> プレイヤー(白) </summary>
    public const int Player_One = 1;
    /// <summary> プレイヤー(黒) </summary>
    public const int Player_Two = 2;
    /// <summary> 現在の(current)プレイヤー </summary>
    public static int _player;
    [SerializeField] public static int _scoreWhite; //白番の得点
    [SerializeField] public static int _scoreBlack; //黒番の得点
    [SerializeField] public static int _finalScore; //目標点
    //盤にある駒の数(白、黒それぞれ)を取得する
    public static int _wPieceCount = 8;
    public static int _bPieceCount = 8;
    //盤にある駒のポイント数(?)を白黒それぞれで取得(プロモーション制限用)
    public static int _wFieldPiecePoints = 8;
    public static int _bFieldPiecePoints = 8;

    //↓Panel(UI)は、Image(UI)として扱う
    [SerializeField] public Image _resultPanel;
    [Header("白のスコア")]
    [SerializeField] public Text _scoreWhiteText;
    [Header("黒のスコア")]
    [SerializeField] public Text _scoreBlackText;
    [SerializeField] public static Phase _state = Phase.White;


    // Start is called before the first frame update
    void Start()
    {
        _scoreWhite = 0; //初期情報を宣言
        _scoreBlack = 0;
        _finalScore = 10;

        _state = Phase.White;
        _resultPanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        _scoreWhiteText.text = _scoreWhite.ToString(); //得点をシーンに表示
        _scoreBlackText.text = _scoreBlack.ToString();

        //白黒それぞれの駒の数を取得する
        //Startでやると、駒の数が変わった時に変更を取得出来ないため、Updateで行う
        _wPieceCount = GameObject.FindGameObjectsWithTag("WhitePiece").Length;
        _bPieceCount = GameObject.FindGameObjectsWithTag("BlackPiece").Length;

        //勝利時(得点による)のシーン遷移
        //どちらかが目標点まで獲得したら
        if (_scoreWhite >= _finalScore || _scoreBlack >= _finalScore)
        {
            _resultPanel.gameObject.SetActive(true);
            Invoke("GoResult", 2f); //2秒後にGoResultの処理を実行する(処理を遅らせる)
        }
        //敵の駒が0になったら
        else if (_wPieceCount == 0 || _bPieceCount == 0)
        {
            _resultPanel.gameObject.SetActive(true);
            Invoke("GoResult", 2f);
        }

        //引き分け時(駒の数がお互い1つになった時)のシーン遷移
        if (_wPieceCount == 1 && _bPieceCount == 1)
        {
            _resultPanel.gameObject.SetActive(true);
            Invoke("GoResult", 2f); //2秒後にGoResult()の処理を実行する
        }
    }

    void GoResult()
    {
        SceneManager.LoadScene("ChessResult");
    }
}
