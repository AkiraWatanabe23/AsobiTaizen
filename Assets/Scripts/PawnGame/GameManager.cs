using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// �ǂ����̃^�[����
/// </summary>
public enum Phase
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
    public const int Player_One = 1;
    /// <summary> �v���C���[(��) </summary>
    public const int Player_Two = 2;
    /// <summary> ���݂�(current)�v���C���[ </summary>
    public static int _player;
    public static int _beFrPlayer;

    //��Panel(UI)�́AImage(UI)�Ƃ��Ĉ���
    [SerializeField] public Image _resultPanel;
    [SerializeField] public static Phase _phase = Phase.White;
    [Tooltip("�ǂ����̃^�[����(��)")] Text _whiteTurn;
    [Tooltip("�ǂ����̃^�[����(��)")] Text _blackTurn;
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
        //���^�[���\����Text
        _whiteTurn = GameObject.Find("WhiteText").GetComponent<Text>();
        _blackTurn = GameObject.Find("BlackText").GetComponent<Text>();
        _whiteTurn.color = Color.yellow;
        _piece = GameObject.Find("Piece").GetComponent<PieceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //SelectButton�̐؂�ւ�
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
        //������(���_�ɂ��)�̃V�[���J��
        //�G�̃L���O���l�����Ƃ��A�܂��͏����𖞂�����1�񑵂�����
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
                if (_piece._whitePieces[i].GetComponent<GameCheck>()._checkCount[j] >= 4)
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
                if (_piece._blackPieces[i].GetComponent<GameCheck>()._checkCount[j] >= 4)
                {
                    isClear = true;
                    _resultPanel.gameObject.SetActive(true);
                    Invoke("SceneSwitch", 2f);
                }
            }
        }
        //�����������̃V�[���J��
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
    /// ���U���g�V�[���ւ̈ڍs
    /// </summary>
    void SceneSwitch()
    {
        //������������
        if (_getPiece.Contains("White"))
        {
            ResultSceneManager._win = 1;
        }
        //������������
        else if (_getPiece.Contains("Black"))
        {
            ResultSceneManager._win = 2;
        }
        SceneManager.LoadScene("ChessResult");
    }
}
