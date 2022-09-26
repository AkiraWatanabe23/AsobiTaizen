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
    int _beFrPlayer;

    //��Panel(UI)�́AImage(UI)�Ƃ��Ĉ���
    [SerializeField] public Image _resultPanel;
    [SerializeField] public static Phase _phase = Phase.White;
    [Tooltip("�ǂ����̃^�[����(��)")] Text _whiteTurn;
    [Tooltip("�ǂ����̃^�[����(��)")] Text _blackTurn;
    [SerializeField] Button _whiteSelect;
    [SerializeField] Button _blackSelect;
    public string _getPiece;
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
        LineCount();
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
    }

    /// <summary>
    /// �^�[�����؂�ւ�����Ƃ��ɁA������ɉ����ĕ���ł��邩�̔��������
    /// </summary>
    public void LineCount()
    {
        //���̃t���[�������̃^�[�� ���� �O�̃t���[�������̃^�[���ł���Ύ��s����(���̃^�[���ɂȂ����u�ԂɈ�x�������s����)
        //�܂��́A���̃t���[�������̃^�[�� ���� �O�̃t���[�������̃^�[���ł���Ύ��s����(���̃^�[���ɂȂ����u�ԂɈ�x�������s����)
        if (_player != _beFrPlayer)
        {
            foreach (var i in _piece._whitePieces)
            {
                i.GetComponent<GameCheck>().Check();
                PlayerWin();
                for (int j = 0; j < 8; j++)
                {
                    i.GetComponent<GameCheck>()._checkCount[j] = 0;
                }
            }
            foreach (var i in _piece._blackPieces)
            {
                i.GetComponent<GameCheck>().Check();
                PlayerWin();
                for (int j = 0; j < 8; j++)
                {
                    i.GetComponent<GameCheck>()._checkCount[j] = 0;
                }
            }
            //���蒆��ColliderOff�ɂ���������ɖ߂�
            foreach (var i in _piece._whitePieces)
            {
                if (i.GetComponent<Collider>().enabled == false)
                {
                    i.GetComponent<Collider>().enabled = true;
                }
            }
            foreach (var i in _piece._blackPieces)
            {
                if (i.GetComponent<Collider>().enabled == false)
                {
                    i.GetComponent<Collider>().enabled = true;
                }
            }
        }
        //���t���[���ׂ̈Ɍ��݁A�ǂ���̃^�[������ۑ����Ă���(��������Ȃ��Ə�L�̏���������������s�����)
        _beFrPlayer = _player;
    }

    void PlayerWin()
    {
        //�������̃V�[���J��
        //�G�̃L���O���l�����Ƃ��A�܂��͏����𖞂�����1�񑵂����Ƃ�
        if (_getPiece != null)
        {
            if (_getPiece.Contains("King"))
            {
                _resultPanel.gameObject.SetActive(true);
                Invoke("SceneSwitch", 2f);
            }
        }
        foreach (var i in _piece._whitePieces)
        {
            for (int j = 0; j < 8; j++)
            {
                if (i.GetComponent<GameCheck>()._checkCount[j] >= 3)
                {
                    _resultPanel.gameObject.SetActive(true);
                    //���̏���
                    if (_beFrPlayer == 1)
                    {
                        ResultSceneManager._win = 2;
                    }
                    //���̏���
                    else if (_beFrPlayer == 2)
                    {
                        ResultSceneManager._win = 1;
                    }
                    Invoke("SceneSwitch", 2f);
                }
            }
        }
        foreach (var i in _piece._blackPieces)
        {
            for (int j = 0; j < 8; j++)
            {
                if (i.GetComponent<GameCheck>()._checkCount[j] >= 3)
                {
                    _resultPanel.gameObject.SetActive(true);
                    //���̏���
                    if (_beFrPlayer == 1)
                    {
                        ResultSceneManager._win = 2;
                    }
                    //���̏���
                    else if (_beFrPlayer == 2)
                    {
                        ResultSceneManager._win = 1;
                    }
                    Invoke("SceneSwitch", 2f);
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
