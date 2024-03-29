using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// どっちのターンか
/// </summary>
public enum GamePhase
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
    public const int PLAYER_ONE = 1;
    /// <summary> プレイヤー(黒) </summary>
    public const int PLAYER_TWO = 2;
    /// <summary> 現在の(current)プレイヤー </summary>
    public static int Player;
    int _beFrPlayer; //(BeforeFramePlayer)

    //↓Panel(UI)は、Image(UI)として扱う
    [SerializeField] Image _resultPanel;
    [SerializeField] GamePhase _phase = GamePhase.White;
    [Tooltip("どっちのターンか(白)")] Text _whiteTurn;
    [Tooltip("どっちのターンか(黒)")] Text _blackTurn;
    [SerializeField] Button _whiteSelect;
    [SerializeField] Button _blackSelect;
    PieceManager _piece;
    public GamePhase Phase { get; set; }
    public string GetPiece { get; set; }


    // Start is called before the first frame update
    void Start()
    {
        Player = 1;
        _beFrPlayer = 1;
        Phase = GamePhase.White;
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
        LineCount();
        //SelectButtonの切り替え
        if (Player == 1)
        {
            _whiteSelect.gameObject.SetActive(true);
            _blackSelect.gameObject.SetActive(false);
        }
        else if (Player == 2)
        {
            _whiteSelect.gameObject.SetActive(false);
            _blackSelect.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// ターンが切り替わったときに、駒が条件に沿って並んでいるかの判定をする
    /// </summary>
    public void LineCount()
    {
        //今のフレームが白のターン かつ 前のフレームが黒のターンであれば実行する(白のターンになった瞬間に一度だけ実行する)
        //または、今のフレームが黒のターン かつ 前のフレームが白のターンであれば実行する(黒のターンになった瞬間に一度だけ実行する)
        if (Player != _beFrPlayer)
        {
            foreach (var i in _piece.WhitePieces)
            {
                if (i != null)
                {
                    i.GetComponent<GameCheck>().Check();
                    PlayerWin();
                    for (int j = 0; j < 8; j++)
                    {
                        i.GetComponent<GameCheck>().CheckCount[j] = 0;
                    }
                }
            }
            foreach (var i in _piece.BlackPieces)
            {
                if (i != null)
                {
                    i.GetComponent<GameCheck>().Check();
                    PlayerWin();
                    for (int j = 0; j < 8; j++)
                    {
                        i.GetComponent<GameCheck>().CheckCount[j] = 0;
                    }
                }
            }
            //判定中にColliderOffにした駒を元に戻す
            foreach (var i in _piece.WhitePieces)
            {
                if (i != null)
                {
                    if (i.GetComponent<Collider>().enabled == false)
                    {
                        i.GetComponent<Collider>().enabled = true;
                    }
                }
            }
            foreach (var i in _piece.BlackPieces)
            {
                if (i != null)
                {
                    if (i.GetComponent<Collider>().enabled == false)
                    {
                        i.GetComponent<Collider>().enabled = true;
                    }
                }
            }
        }
        //次フレームの為に現在、どちらのターンかを保存しておく(これをしないと上記の処理がたくさん実行される)
        _beFrPlayer = Player;
    }

    void PlayerWin()
    {
        //勝利時のシーン遷移
        //敵のキングを獲ったとき、または条件を満たして1列揃えたとき
        if (GetPiece != null)
        {
            if (GetPiece.Contains("King"))
            {
                _resultPanel.gameObject.SetActive(true);
                Invoke("SceneSwitch", 2f);
            }
        }
        foreach (var i in _piece.WhitePieces)
        {
            for (int j = 0; j < 8; j++)
            {
                if (i != null)
                {
                    if (i.GetComponent<GameCheck>().CheckCount[j] >= 3)
                    {
                        _resultPanel.gameObject.SetActive(true);
                        //白の勝ち
                        if (_beFrPlayer == 1)
                        {
                            ResultSceneManager.Win = 2;
                        }
                        //黒の勝ち
                        else if (_beFrPlayer == 2)
                        {
                            ResultSceneManager.Win = 1;
                        }
                        Invoke("SceneSwitch", 2f);
                    }
                }
            }
        }
        foreach (var i in _piece.BlackPieces)
        {
            for (int j = 0; j < 8; j++)
            {
                if (i != null)
                {
                    if (i.GetComponent<GameCheck>().CheckCount[j] >= 3)
                    {
                        _resultPanel.gameObject.SetActive(true);
                        //白の勝ち
                        if (_beFrPlayer == 1)
                        {
                            ResultSceneManager.Win = 2;
                        }
                        //黒の勝ち
                        else if (_beFrPlayer == 2)
                        {
                            ResultSceneManager.Win = 1;
                        }
                        Invoke("SceneSwitch", 2f);
                    }
                }
            }
        }
    }

    public void SwitchTurnWhite()
    {
        _whiteTurn.color = Color.white;
        _blackTurn.color = Color.yellow;
    }

    public void SwitchTurnBlack()
    {
        _whiteTurn.color = Color.yellow;
        _blackTurn.color = Color.white;
    }

    /// <summary>
    /// リザルトシーンへの移行
    /// </summary>
    void SceneSwitch()
    {
        if (GetPiece != null)
        {
            //黒が勝ったら
            if (GetPiece.Contains("White"))
            {
                ResultSceneManager.Win = 1;
            }
            //白が勝ったら
            else if (GetPiece.Contains("Black"))
            {
                ResultSceneManager.Win = 2;
            }
        }
        SceneManager.LoadScene("ChessResult");
    }
}
