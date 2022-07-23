using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultSceneManager : MonoBehaviour
{
    [SerializeField] public Text _whiteWinText; //�ǂ��������������\������Text(��)
    [SerializeField] public Text _blackWinText; //�ǂ��������������\������Text(��)

    [SerializeField] public Text _whiteScoreText;
    [SerializeField] public Text _blackScoreText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _whiteScoreText.text = GameManager._scoreWhite.ToString();
        _blackScoreText.text = GameManager._scoreBlack.ToString();

        if (GameManager._scoreWhite == 5)
        {
            _whiteWinText.gameObject.SetActive(true); //�u���̏���!!�v��Text ��\��
            _blackWinText.gameObject.SetActive(false);
            Debug.Log("WhiteWin!!");
        }
        else if (GameManager._scoreBlack == 5)
        {
            _whiteWinText.gameObject.SetActive(false);  //�u���̏���!!�v��Text ��\��
            _blackWinText.gameObject.SetActive(true);
            Debug.Log("BlackWin!!");
        }
    }
}
