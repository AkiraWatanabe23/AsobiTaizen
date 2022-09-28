using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// �ǂ����̃^�[����
/// </summary>
public enum GamePhase
{
    White = 0,
    Black = 1,
}

/// <summary>
/// �I�����(���I�Ԃ��A�}�X��I�Ԃ�)
/// </summary>
public enum SelectPhase
{
    Piece,
    Tile,
}

/// <summary>
/// �Q�[���S�̂̊Ǘ�
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary> �v���C���[(��) </summary>
    public const int PLAYER_ONE = 1;
    /// <summary> �v���C���[(��) </summary>
    public const int PLAYER_TWO = 2;
    /// <summary> ���݂�(current)�v���C���[ </summary>
    public static int Player;
    int _beFrPlayer; //(BeforeFramePlayer)

    //��Panel(UI)�́AImage(UI)�Ƃ��Ĉ���
    [SerializeField] Image _resultPanel;
    [SerializeField] GamePhase _phase = GamePhase.White;
    [Tooltip("�ǂ����̃^�[����(��)")] Text _whiteTurn;
    [Tooltip("�ǂ����̃^�[����(��)")] Text _blackTurn;
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
        //���^�[���\����Text
        _whiteTurn = GameObject.Find("WhiteText").GetComponent<Text>();
        _blackTurn = GameObject.Find("BlackText").GetComponent<Text>();
        _whiteTurn.color = Color.yellow;
        _piece = GameObject.Find("Piece").GetComponent<PieceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        LineCount();
        //SelectButton�̐؂�ւ�
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
    /// �^�[�����؂�ւ�����Ƃ��ɁA������ɉ����ĕ���ł��邩�̔��������
    /// </summary>
    public void LineCount()
    {
        //���̃t���[�������̃^�[�� ���� �O�̃t���[�������̃^�[���ł���Ύ��s����(���̃^�[���ɂȂ����u�ԂɈ�x�������s����)
        //�܂��́A���̃t���[�������̃^�[�� ���� �O�̃t���[�������̃^�[���ł���Ύ��s����(���̃^�[���ɂȂ����u�ԂɈ�x�������s����)
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
            //���蒆��ColliderOff�ɂ���������ɖ߂�
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
        //���t���[���ׂ̈Ɍ��݁A�ǂ���̃^�[������ۑ����Ă���(��������Ȃ��Ə�L�̏���������������s�����)
        _beFrPlayer = Player;
    }

    void PlayerWin()
    {
        //�������̃V�[���J��
        //�G�̃L���O���l�����Ƃ��A�܂��͏����𖞂�����1�񑵂����Ƃ�
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
                        //���̏���
                        if (_beFrPlayer == 1)
                        {
                            ResultSceneManager.Win = 2;
                        }
                        //���̏���
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
                        //���̏���
                        if (_beFrPlayer == 1)
                        {
                            ResultSceneManager.Win = 2;
                        }
                        //���̏���
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
    /// ���U���g�V�[���ւ̈ڍs
    /// </summary>
    void SceneSwitch()
    {
        //������������
        if (GetPiece.Contains("White"))
        {
            ResultSceneManager.Win = 1;
        }
        //������������
        else if (GetPiece.Contains("Black"))
        {
            ResultSceneManager.Win = 2;
        }
        SceneManager.LoadScene("ChessResult");
    }
}
