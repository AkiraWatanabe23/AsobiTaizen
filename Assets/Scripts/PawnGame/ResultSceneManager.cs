using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultSceneManager : MonoBehaviour
{
    [SerializeField] public Text _whiteWinText; //�ǂ��������������\������Text(��)
    [SerializeField] public Text _blackWinText; //�ǂ��������������\������Text(��)
    [SerializeField] public Text _drawText;     //����������Text

    [Tooltip("�ŏI�I�ȓ��_��\������(��)"), SerializeField] public Text _whiteScoreText;
    [Tooltip("�ŏI�I�ȓ��_��\������(��)"), SerializeField] public Text _blackScoreText;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager._scoreWhite == GameManager._finalScore)
        {
            Debug.Log("WhiteWin!!");
        }
        else if (GameManager._scoreBlack == GameManager._finalScore)
        {
            Debug.Log("BlackWin!!");
        }
        else
        {
            Debug.Log("Draw...");
        }
    }

    // Update is called once per frame
    void Update()
    {
        _whiteScoreText.text = GameManager._scoreWhite.ToString();
        _blackScoreText.text = GameManager._scoreBlack.ToString();

        //���̏���
        if (GameManager._scoreWhite == GameManager._finalScore || GameManager._bPieceCount == 0)
        {
            _whiteWinText.gameObject.SetActive(true); //�u���̏����v��Text ��\��
            _blackWinText.gameObject.SetActive(false);
            _drawText.gameObject.SetActive(false);
        }
        //���̏���
        else if (GameManager._scoreBlack == GameManager._finalScore || GameManager._wPieceCount == 0)
        {
            _whiteWinText.gameObject.SetActive(false);
            _blackWinText.gameObject.SetActive(true); //�u���̏����v��Text ��\��
            _drawText.gameObject.SetActive(false);
        }
        //��������(���̎��A�X�R�A�\������?)
        //����������(��̐������ꂼ��1�Ȃ瓾�_�֌W�Ȃ���������)
        //�@�@�@�@�@(�X�e�C�����C�g...��������Ȃ��ꍇ�́A���_�̍������̏����A���Ȃ�...)
        else
        {
            _whiteWinText.gameObject.SetActive(false);
            _blackWinText.gameObject.SetActive(false);
            _drawText.gameObject.SetActive(true); //�u���������v��Text ��\��
        }
    }
}