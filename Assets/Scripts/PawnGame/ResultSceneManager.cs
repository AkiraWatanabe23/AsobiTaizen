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
        //�V�[���ɓ������Ƃ��A�ǂ�������������
    }

    // Update is called once per frame
    void Update()
    {
        //���̏���
        //���̏���
        //��������(���̎��A�X�R�A�\������?)
    }
}