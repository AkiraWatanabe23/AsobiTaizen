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

    [Header("���̃X�R�A���V�[���ɕ\������"), SerializeField] public Text _scoreWhiteText;
    [Header("���̃X�R�A���V�[���ɕ\������"), SerializeField] public Text _scoreBlackText;

    [SerializeField] public static Phase _state = Phase.White;


    // Start is called before the first frame update
    void Start()
    {
        _scoreWhite = 0;
        _scoreBlack = 0;

        _state = Phase.White;
    }

    // Update is called once per frame
    void Update()
    {
        _scoreWhiteText.text = _scoreWhite.ToString(); //���_���V�[���ɕ\��
        _scoreBlackText.text = _scoreBlack.ToString();

        if (_scoreWhite == 5 || _scoreBlack == 5) //�ǂ��炩�̓��_���ڕW�_�ɓ͂�����
        {
            SceneManager.LoadScene("ResultScene");
        }
    }
}
