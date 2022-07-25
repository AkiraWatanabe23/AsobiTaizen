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

public class GameManager : MonoBehaviour
{
    [SerializeField] public static int _scoreWhite; //���Ԃ̓��_
    [SerializeField] public static int _scoreBlack; //���Ԃ̓��_
    //��̐�(���A�����ꂼ��)���擾����
    public static int _wPieceCount = 0;
    public static int _bPieceCount = 0;

    [Header("���̃X�R�A���V�[���ɕ\������")]
    [SerializeField] public Text _scoreWhiteText;
    [Header("���̃X�R�A���V�[���ɕ\������")]
    [SerializeField] public Text _scoreBlackText;
    [SerializeField] public GameObject _resultPanel; //Panel(UI)�́AUI�ł͂Ȃ��AGameObject�Ƃ��Ĉ���

    [SerializeField] public static Phase _state = Phase.White;


    // Start is called before the first frame update
    void Start()
    {
        _scoreWhite = 0;
        _scoreBlack = 0;

        _state = Phase.White;
        _resultPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        _scoreWhiteText.text = _scoreWhite.ToString(); //���_���V�[���ɕ\��
        _scoreBlackText.text = _scoreBlack.ToString();

        //�Q�[���J�n���ɁA�������ꂼ��̋�̐����擾����
        //���Q�[�����i��ŋ�̐����ω�����̂ɁAStart�Ŏ��s���đ��v?
        _wPieceCount = GameObject.FindGameObjectsWithTag("WhitePiece").Length;
        _bPieceCount = GameObject.FindGameObjectsWithTag("BlackPiece").Length;

        //�������̃V�[���J��
        if (_scoreWhite == 5 || _scoreBlack == 5) //�ǂ��炩�̓��_���ڕW�_�ɓ͂�����
        {
            _resultPanel.SetActive(true);
            Invoke("ChangeResult", 2f); //2�b���ChangeResult�̏��������s����
        }
        //�����������̃V�[���J��
        if (_wPieceCount == 1 && _bPieceCount == 1)
        {
            _resultPanel.SetActive(true);
            Invoke("ChangeResult", 2f); //2�b���ChangeResult�̏��������s����
        }
    }

    void ChangeResult()
    {
        SceneManager.LoadScene("ResultScene");
    }
}
