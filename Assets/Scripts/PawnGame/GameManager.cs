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
    [SerializeField] Button _whiteSelect;
    [SerializeField] Button _blackSelect;
    public GameObject _getPiece;


    // Start is called before the first frame update
    void Start()
    {
        _player = 1;
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
        //SelectButtonの切り替え
        if (_player == 1)
        {
            _whiteSelect.gameObject.SetActive(true);
            _blackSelect.gameObject.SetActive(false);
        }
        else if (_player == 2)
        {
            _whiteSelect.gameObject.SetActive(false);
            _blackSelect.gameObject.SetActive(true);
        }
        //勝利時(得点による)のシーン遷移
        //敵のキングを獲ったとき、または条件を満たして1列揃えたら
        if (_getPiece != null && _getPiece.name == "King")
        {
            _resultPanel.gameObject.SetActive(true);
            Invoke("SceneTransition", 2f);
        }
        //引き分け時のシーン遷移
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
