using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// どっちのターンか
/// </summary>
public enum Phase
{
    White = 0,
    Black = 1,
}

/// <summary>
/// 選択状態(駒を選ぶか、マスを選ぶか)
/// </summary>
public enum SelectPhase
{
    Piece,
    Tile,
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
    int _beFrPlayer;

    //↓Panel(UI)は、Image(UI)として扱う
    [SerializeField] public Image _resultPanel;
    [SerializeField] public static Phase _phase = Phase.White;
    [Tooltip("どっちのターンか(白)")] Text _whiteTurn;
    [Tooltip("どっちのターンか(黒)")] Text _blackTurn;
    [SerializeField] Button _whiteSelect;
    [SerializeField] Button _blackSelect;
    public string _getPiece;
    public static bool isClear = false;
    PieceManager _piece;


    // Start is called before the first frame update
    void Start()
    {
        _player = 1;
        _beFrPlayer = 1;
        _phase = Phase.White;
        _resultPanel.gameObject.SetActive(false);
        //↓ターン表示のText
        _whiteTurn = GameObject.Find("WhiteText").GetComponent<Text>();
        _blackTurn = GameObject.Find("BlackText").GetComponent<Text>();
        _whiteTurn.color = Color.yellow;
        _piece = GameObject.Find("Piece").GetComponent<PieceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //今のフレームが白のターン かつ 前のフレームが黒のターンであれば実行する(白のターンになった瞬間に一度だけ実行する)
        //または、今のフレームが黒のターン かつ 前のフレームが白のターンであれば実行する(黒のターンになった瞬間に一度だけ実行する)
        if (GameManager._player == 1 && _beFrPlayer == 2
            || GameManager._player == 2 && _beFrPlayer == 1)
        {
            foreach (var i in _piece._whitePieces)
            {
                print($"{i.name} aaa");
                i.GetComponent<GameCheck>().Check();
                PlayerWin();
                if (i.GetComponent<Collider>().enabled == false)
                {
                    i.GetComponent<Collider>().enabled = true;
                }
                for (int j = 0; j < 8; j++)
                {
                    i.GetComponent<GameCheck>()._checkCount[j] = 0;
                }
            }
            foreach (var i in _piece._blackPieces)
            {
                print($"{i.name} aaa");
                i.GetComponent<GameCheck>().Check();
                PlayerWin();
                if (i.GetComponent<Collider>().enabled == false)
                {
                    i.GetComponent<Collider>().enabled = true;
                }
                for (int j = 0; j < 8; j++)
                {
                    i.GetComponent<GameCheck>()._checkCount[j] = 0;
                }
            }
        }
        //次フレームの為に現在、どちらのターンかを保存しておく。
        _beFrPlayer = _player;

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
        //引き分け時のシーン遷移
    }

    void PlayerWin()
    {
        //勝利時のシーン遷移
        //敵のキングを獲ったとき、または条件を満たして1列揃えたとき
        if (_getPiece != null)
        {
            if (_getPiece.Contains("King"))
            {
                _resultPanel.gameObject.SetActive(true);
                Invoke("SceneSwitch", 2f);
            }
        }
        for (int i = 0; i < _piece._whitePieces.Count; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (_piece._whitePieces[i].GetComponent<GameCheck>()._checkCount[j] >= 3)
                {
                    isClear = true;
                    _resultPanel.gameObject.SetActive(true);
                    Invoke("SceneSwitch", 2f);
                }
            }
        }
        for (int i = 0; i < _piece._blackPieces.Count; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (_piece._blackPieces[i].GetComponent<GameCheck>()._checkCount[j] >= 3)
                {
                    isClear = true;
                    _resultPanel.gameObject.SetActive(true);
                    Invoke("SceneSwitch", 2f);
                }
            }
        }
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

    /// <summary>
    /// リザルトシーンへの移行
    /// </summary>
    void SceneSwitch()
    {
        //黒が勝ったら
        if (_getPiece.Contains("White"))
        {
            ResultSceneManager._win = 1;
        }
        //白が勝ったら
        else if (_getPiece.Contains("Black"))
        {
            ResultSceneManager._win = 2;
        }
        SceneManager.LoadScene("ChessResult");
    }
}
