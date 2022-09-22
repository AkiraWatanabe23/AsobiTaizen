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

    //↓Panel(UI)は、Image(UI)として扱う
    [SerializeField] public Image _resultPanel;
    [SerializeField] public static Phase _phase = Phase.White;
    [Tooltip("どっちのターンか(白)")] Text _whiteTurn;
    [Tooltip("どっちのターンか(黒)")] Text _blackTurn;


    // Start is called before the first frame update
    void Start()
    {
        _phase = Phase.White;
        _resultPanel.gameObject.SetActive(false);
        //↓ターン表示のText
        _whiteTurn = GameObject.Find("WhiteText").GetComponent<Text>();
        _blackTurn = GameObject.Find("BlackText").GetComponent<Text>();
        _whiteTurn.color = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {
        //勝利時(得点による)のシーン遷移
        //引き分け時(駒の数がお互い1つになった時)のシーン遷移
    }

    public void SwitchTurnWhite()
    {
        _whiteTurn.color = Color.black;
        _blackTurn.color = Color.yellow;
    }

    public void SwitchTurnBlack()
    {
        _whiteTurn.color = Color.yellow;
        _blackTurn.color = Color.black;
    }

    void SceneTransition()
    {
        SceneManager.LoadScene("ChessResult");
    }
}
