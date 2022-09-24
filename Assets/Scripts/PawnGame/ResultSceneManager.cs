using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultSceneManager : MonoBehaviour
{
    [SerializeField] public Text _whiteWinText; //�ǂ��������������\������Text(��)
    [SerializeField] public Text _blackWinText; //�ǂ��������������\������Text(��)
    [SerializeField] public Text _drawText;     //����������Text
    public static int _win;

    // Start is called before the first frame update
    void Start()
    {
        //�V�[���ɓ������Ƃ��A�ǂ�������������
        if (_win == 1)
        {
            _whiteWinText.gameObject.SetActive(false);
            _blackWinText.gameObject.SetActive(true);
            _drawText.gameObject.SetActive(false);
        }
        else if (_win == 2)
        {
            _whiteWinText.gameObject.SetActive(true);
            _blackWinText.gameObject.SetActive(false);
            _drawText.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //���̏���
        //���̏���
        //��������(���̎��A�X�R�A�\������?)
    }
}