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

    // Start is called before the first frame update
    void Start()
    {
        _search = GameObject.Find("Board,Tile").GetComponent<MasuSearch>();
    }

    public void OnClick()
    {
        if (_search._pieceInfo.tag == "WhitePiece")
        {
            if (gameObject.name == "Queen")
            {
                Instantiate(_promQueenWhite, _search._pieceInfo.transform.position, Quaternion.identity);
                Debug.Log("�N�C�[���Ƀv�����[�V�������܂�");
            }
            else if (gameObject.name == "Rook")
            {
                Instantiate(_promRookWhite, _search._pieceInfo.transform.position, Quaternion.identity);
                Debug.Log("���[�N�Ƀv�����[�V�������܂�");
            }
            else if (gameObject.name == "Bishop")
            {
                Instantiate(_promBishopWhite, _search._pieceInfo.transform.position, Quaternion.identity);
                Debug.Log("�r�V���b�v�Ƀv�����[�V�������܂�");
            }
            else if (gameObject.name == "Knight")
            {
                Instantiate(_promKnightWhite, _search._pieceInfo.transform.position, Quaternion.identity);
                Debug.Log("�i�C�g�Ƀv�����[�V�������܂�");
            }
        }
        else if (_search._pieceInfo.tag == "BlackPiece")
        {
            if (gameObject.name == "Queen")
            {
                Instantiate(_promQueenBlack, _search._pieceInfo.transform.position, Quaternion.identity);
                Debug.Log("�N�C�[���Ƀv�����[�V�������܂�");
            }
            else if (gameObject.name == "Rook")
            {
                Instantiate(_promRookBlack, _search._pieceInfo.transform.position, Quaternion.identity);
                Debug.Log("���[�N�Ƀv�����[�V�������܂�");
            }
            else if (gameObject.name == "Bishop")
            {
                Instantiate(_promBishopBlack, _search._pieceInfo.transform.position, Quaternion.identity);
                Debug.Log("�r�V���b�v�Ƀv�����[�V�������܂�");
            }
            else if (gameObject.name == "Knight")
            {
                Instantiate(_promKnightBlack, _search._pieceInfo.transform.position, Quaternion.identity);
                Debug.Log("�i�C�g�Ƀv�����[�V�������܂�");
            }
        }
    }
}
