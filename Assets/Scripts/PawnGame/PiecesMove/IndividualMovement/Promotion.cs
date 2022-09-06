using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �|�[���̓��ʂȓ���
/// </summary>
public class Promotion : MonoBehaviour
{
    //�v�����[�V�����̋�(��)
    [SerializeField] GameObject _promQueenWhite;
    [SerializeField] GameObject _promRookWhite;
    [SerializeField] GameObject _promBishopWhite;
    [SerializeField] GameObject _promKnightWhite;

    //�v�����[�V�����̋�(��)
    [SerializeField] GameObject _promQueenBlack;
    [SerializeField] GameObject _promRookBlack;
    [SerializeField] GameObject _promBishopBlack;
    [SerializeField] GameObject _promKnightBlack;
    public GameObject _promWhite;
    public GameObject _promBlack;
    PieceManager _piece;

    private void Start()
    {
        _piece = GameObject.Find("Piece").GetComponent<PieceManager>();
    }


    /// <summary>
    /// �p�l���̃{�^���N���b�N�Ńv�����[�V����
    /// </summary>
    public void OnClick()
    {
        //����̃v�����[�V����
        if (_promWhite != null && _promBlack == null)
        {
            //��(this.)gameObject...�N���b�N����{�^���̂���
            if (gameObject.name == "Queen")
            {
                _promWhite.SetActive(false);
                GameObject _promPiece = Instantiate(_promQueenWhite, _promWhite.transform.position, Quaternion.identity);
                PieceMove _piece = _promPiece.GetComponent<PieceMove>();
                _piece.PromAssign();
                Debug.Log("�N�C�[���Ƀv�����[�V�������܂�");
            }
            else if (gameObject.name == "Rook")
            {
                _promWhite.SetActive(false);
                GameObject _promPiece = Instantiate(_promRookWhite, _promWhite.transform.position, Quaternion.identity);
                PieceMove _piece = _promPiece.GetComponent<PieceMove>();
                _piece.PromAssign();
                Debug.Log("���[�N�Ƀv�����[�V�������܂�");
            }
            else if (gameObject.name == "Bishop")
            {
                _promWhite.SetActive(false);
                GameObject _promPiece = Instantiate(_promBishopWhite, _promWhite.transform.position, Quaternion.identity);
                PieceMove _piece = _promPiece.GetComponent<PieceMove>();
                _piece.PromAssign();
                Debug.Log("�r�V���b�v�Ƀv�����[�V�������܂�");
            }
            else if (gameObject.name == "Knight")
            {
                _promWhite.SetActive(false);
                GameObject _promPiece = Instantiate(_promKnightWhite, _promWhite.transform.position, Quaternion.identity);
                PieceMove _piece = _promPiece.GetComponent<PieceMove>();
                _piece.PromAssign();
                Debug.Log("�i�C�g�Ƀv�����[�V�������܂�");
            }
            _piece.AfterProm();
        }
        //����̃v�����[�V����
        else if (_promWhite == null && _promBlack != null)
        {
            if (gameObject.name == "Queen")
            {
                _promBlack.SetActive(false);
                GameObject _promPiece = Instantiate(_promQueenBlack, _promBlack.transform.position, Quaternion.identity);
                PieceMove _piece = _promPiece.GetComponent<PieceMove>();
                _piece.PromAssign();
                Debug.Log("�N�C�[���Ƀv�����[�V�������܂�");
            }
            else if (gameObject.name == "Rook")
            {
                _promBlack.SetActive(false);
                GameObject _promPiece = Instantiate(_promRookBlack, _promBlack.transform.position, Quaternion.identity);
                PieceMove _piece = _promPiece.GetComponent<PieceMove>();
                _piece.PromAssign();
                Debug.Log("���[�N�Ƀv�����[�V�������܂�");
            }
            else if (gameObject.name == "Bishop")
            {
                _promBlack.SetActive(false);
                GameObject _promPiece = Instantiate(_promBishopBlack, _promBlack.transform.position, Quaternion.identity);
                PieceMove _piece = _promPiece.GetComponent<PieceMove>();
                _piece.PromAssign();
                Debug.Log("�r�V���b�v�Ƀv�����[�V�������܂�");
            }
            else if (gameObject.name == "Knight")
            {
                _promBlack.SetActive(false);
                GameObject _promPiece = Instantiate(_promKnightBlack, _promBlack.transform.position, Quaternion.identity);
                PieceMove _piece = _promPiece.GetComponent<PieceMove>();
                _piece.PromAssign();
                Debug.Log("�i�C�g�Ƀv�����[�V�������܂�");
            }
            _piece.AfterProm();
        }
    }
}
