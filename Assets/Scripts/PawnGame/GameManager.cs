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
    [SerializeField] public static int _scoreWhite; //���Ԃ̓��_
    [SerializeField] public static int _scoreBlack; //���Ԃ̓��_
    [SerializeField] public static int _finalScore; //�ڕW�_
    //�Ղɂ����̐�(���A�����ꂼ��)���擾����
    public static int _wPieceCount = 8;
    public static int _bPieceCount = 8;
    //�Ղɂ����̃|�C���g��(?)�𔒍����ꂼ��Ŏ擾(�v�����[�V���������p)
    public static int _wFieldPiecePoints = 8;
    public static int _bFieldPiecePoints = 8;

    //��Panel(UI)�́AImage(UI)�Ƃ��Ĉ���
    [SerializeField] public Image _resultPanel;
    [Header("���̃X�R�A")]
    [SerializeField] public Text _scoreWhiteText;
    [Header("���̃X�R�A")]
    [SerializeField] public Text _scoreBlackText;
    [SerializeField] public static Phase _state = Phase.White;


    // Start is called before the first frame update
    void Start()
    {
        _scoreWhite = 0; //��������錾
        _scoreBlack = 0;
        _finalScore = 10;

        _state = Phase.White;
        _resultPanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        _scoreWhiteText.text = _scoreWhite.ToString(); //���_���V�[���ɕ\��
        _scoreBlackText.text = _scoreBlack.ToString();

        //�������ꂼ��̋�̐����擾����
        //Start�ł��ƁA��̐����ς�������ɕύX���擾�o���Ȃ����߁AUpdate�ōs��
        _wPieceCount = GameObject.FindGameObjectsWithTag("WhitePiece").Length;
        _bPieceCount = GameObject.FindGameObjectsWithTag("BlackPiece").Length;

        //������(���_�ɂ��)�̃V�[���J��
        //�ǂ��炩���ڕW�_�܂Ŋl��������
        if (_scoreWhite >= _finalScore || _scoreBlack >= _finalScore)
        {
            _resultPanel.gameObject.SetActive(true);
            Invoke("GoResult", 2f); //2�b���GoResult�̏��������s����(������x�点��)
        }
        //�G�̋0�ɂȂ�����
        else if (_wPieceCount == 0 || _bPieceCount == 0)
        {
            _resultPanel.gameObject.SetActive(true);
            Invoke("GoResult", 2f);
        }

        //����������(��̐������݂�1�ɂȂ�����)�̃V�[���J��
        if (_wPieceCount == 1 && _bPieceCount == 1)
        {
            _resultPanel.gameObject.SetActive(true);
            Invoke("GoResult", 2f); //2�b���GoResult()�̏��������s����
        }
    }

    void GoResult()
    {
        SceneManager.LoadScene("ChessResult");
    }
}
