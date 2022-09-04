using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �|�[���̓��ʂȓ���
/// </summary>
public class Promotion : MonoBehaviour
{
    MasuSearch _search;
    [SerializeField] GameObject _promQueenWhite;
    [SerializeField] GameObject _promRookWhite;
    [SerializeField] GameObject _promBishopWhite;
    [SerializeField] GameObject _promKnightWhite;

    [SerializeField] GameObject _promQueenBlack;
    [SerializeField] GameObject _promRookBlack;
    [SerializeField] GameObject _promBishopBlack;
    [SerializeField] GameObject _promKnightBlack;
    public GameObject _promWhite;
    public GameObject _promBlack;

    // Start is called before the first frame update
    void Start()
    {
        _search = GameObject.Find("Board,Tile").GetComponent<MasuSearch>();
    }

    public void OnClick()
    {
        if (_promWhite != null && _promBlack == null)
        {
            if (_promWhite.tag == "WhitePiece")
            {
                if (gameObject.name == "Queen")
                {
                    _promWhite.gameObject.SetActive(false);
                    Instantiate(_promQueenWhite, _promWhite.transform.position, Quaternion.identity);
                    Debug.Log("�N�C�[���Ƀv�����[�V�������܂�");
                }
                else if (gameObject.name == "Rook")
                {
                    _promWhite.gameObject.SetActive(false);
                    Instantiate(_promRookWhite, _promWhite.transform.position, Quaternion.identity);
                    Debug.Log("���[�N�Ƀv�����[�V�������܂�");
                }
                else if (gameObject.name == "Bishop")
                {
                    _promWhite.gameObject.SetActive(false);
                    Instantiate(_promBishopWhite, _promWhite.transform.position, Quaternion.identity);
                    Debug.Log("�r�V���b�v�Ƀv�����[�V�������܂�");
                }
                else if (gameObject.name == "Knight")
                {
                    _promWhite.gameObject.SetActive(false);
                    Instantiate(_promKnightWhite, _promWhite.transform.position, Quaternion.identity);
                    Debug.Log("�i�C�g�Ƀv�����[�V�������܂�");
                }
                _promWhite = null;
            }
        }

        if (_promBlack != null && _promWhite == null)
        {
            if (_promBlack.tag == "BlackPiece")
            {
                if (gameObject.name == "Queen")
                {
                    _promBlack.gameObject.SetActive(false);
                    Instantiate(_promQueenBlack, _promBlack.transform.position, Quaternion.identity);
                    Debug.Log("�N�C�[���Ƀv�����[�V�������܂�");
                }
                else if (gameObject.name == "Rook")
                {
                    _promBlack.gameObject.SetActive(false);
                    Instantiate(_promRookBlack, _promBlack.transform.position, Quaternion.identity);
                    Debug.Log("���[�N�Ƀv�����[�V�������܂�");
                }
                else if (gameObject.name == "Bishop")
                {
                    _promBlack.gameObject.SetActive(false);
                    Instantiate(_promBishopBlack, _promBlack.transform.position, Quaternion.identity);
                    Debug.Log("�r�V���b�v�Ƀv�����[�V�������܂�");
                }
                else if (gameObject.name == "Knight")
                {
                    _promBlack.gameObject.SetActive(false);
                    Instantiate(_promKnightBlack, _promBlack.transform.position, Quaternion.identity);
                    Debug.Log("�i�C�g�Ƀv�����[�V�������܂�");
                }
            }
            _promBlack = null;
        }
    }
}
