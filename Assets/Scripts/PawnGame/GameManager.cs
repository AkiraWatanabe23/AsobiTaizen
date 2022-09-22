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
    //�Ղɂ����̐�(���A�����ꂼ��)���擾����
    public static int _wPieceCount = 8;
    public static int _bPieceCount = 8;

    //��Panel(UI)�́AImage(UI)�Ƃ��Ĉ���
    [SerializeField] public Image _resultPanel;
    [SerializeField] public static Phase _phase = Phase.White;
    [Tooltip("�ǂ����̃^�[����(��)")] Text _whiteTurn;
    [Tooltip("�ǂ����̃^�[����(��)")] Text _blackTurn;


    // Start is called before the first frame update
    void Start()
    {
        _phase = Phase.White;
        _resultPanel.gameObject.SetActive(false);
        //���^�[���\����Text
        _whiteTurn = GameObject.Find("WhiteText").GetComponent<Text>();
        _blackTurn = GameObject.Find("BlackText").GetComponent<Text>();
        _whiteTurn.color = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {
        //�������ꂼ��̋�̐����擾����
        //Start�ł��ƁA��̐����ς�������ɕύX���擾�o���Ȃ����߁AUpdate�ōs��
        _wPieceCount = GameObject.FindGameObjectsWithTag("WhitePiece").Length;
        _bPieceCount = GameObject.FindGameObjectsWithTag("BlackPiece").Length;

        //������(���_�ɂ��)�̃V�[���J��
        //����������(��̐������݂�1�ɂȂ�����)�̃V�[���J��
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
