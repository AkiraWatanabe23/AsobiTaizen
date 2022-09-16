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
    Transform _currentPos;
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
                _currentPos = _promWhite.transform;
                Destroy(_promWhite);
                GameObject _promPiece = Instantiate(_promQueenWhite, _currentPos.position, _currentPos.rotation);
                _piece._whitePieces.Remove(_promWhite);
                _piece._whitePieces.Add(_promPiece);
                PieceMove _pieceInfo = _promPiece.GetComponent<PieceMove>();
                _pieceInfo.PromAssign();
                Debug.Log("�N�C�[���Ƀv�����[�V�������܂�");
            }
            else if (gameObject.name == "Rook")
            {
                _currentPos = _promWhite.transform;
                Destroy(_promWhite);
                GameObject _promPiece = Instantiate(_promRookWhite, _currentPos.position, _currentPos.rotation);
                _piece._whitePieces.Remove(_promWhite);
                _piece._whitePieces.Add(_promPiece);
                PieceMove _pieceInfo = _promPiece.GetComponent<PieceMove>();
                _pieceInfo.PromAssign();
                Debug.Log("���[�N�Ƀv�����[�V�������܂�");
            }
            else if (gameObject.name == "Bishop")
            {
                _currentPos = _promWhite.transform;
                Destroy(_promWhite);
                GameObject _promPiece = Instantiate(_promBishopWhite, _currentPos.position, _currentPos.rotation);
                _piece._whitePieces.Remove(_promWhite);
                _piece._whitePieces.Add(_promPiece);
                PieceMove _pieceInfo = _promPiece.GetComponent<PieceMove>();
                _pieceInfo.PromAssign();
                Debug.Log("�r�V���b�v�Ƀv�����[�V�������܂�");
            }
            else if (gameObject.name == "Knight")
            {
                _currentPos = _promWhite.transform;
                Destroy(_promWhite);
                GameObject _promPiece = Instantiate(_promKnightWhite, _currentPos.position, _currentPos.rotation);
                _piece._whitePieces.Remove(_promWhite);
                _piece._whitePieces.Add(_promPiece);
                PieceMove _pieceInfo = _promPiece.GetComponent<PieceMove>();
                _pieceInfo.PromAssign();
                Debug.Log("�i�C�g�Ƀv�����[�V�������܂�");
            }
            _promWhite = null;
            _promBlack = null;
        }
        //����̃v�����[�V����
        else if (_promWhite == null && _promBlack != null)
        {
            if (gameObject.name == "Queen")
            {
                _currentPos = _promBlack.transform;
                Destroy(_promBlack);
                GameObject _promPiece = Instantiate(_promQueenBlack, _currentPos.position, _currentPos.rotation);
                _piece._blackPieces.Remove(_promBlack);
                _piece._blackPieces.Add(_promPiece);
                PieceMove _pieceInfo = _promPiece.GetComponent<PieceMove>();
                _pieceInfo.PromAssign();
                Debug.Log("�N�C�[���Ƀv�����[�V�������܂�");
            }
            else if (gameObject.name == "Rook")
            {
                _currentPos = _promBlack.transform;
                Destroy(_promBlack);
                GameObject _promPiece = Instantiate(_promRookBlack, _currentPos.position, _currentPos.rotation);
                _piece._blackPieces.Remove(_promBlack);
                _piece._blackPieces.Add(_promPiece);
                PieceMove _pieceInfo = _promPiece.GetComponent<PieceMove>();
                _pieceInfo.PromAssign();
                Debug.Log("���[�N�Ƀv�����[�V�������܂�");
            }
            else if (gameObject.name == "Bishop")
            {
                _currentPos = _promBlack.transform;
                Destroy(_promBlack);
                GameObject _promPiece = Instantiate(_promBishopBlack, _currentPos.position, _currentPos.rotation);
                _piece._blackPieces.Remove(_promBlack);
                _piece._blackPieces.Add(_promPiece);
                PieceMove _pieceInfo = _promPiece.GetComponent<PieceMove>();
                _pieceInfo.PromAssign();
                Debug.Log("�r�V���b�v�Ƀv�����[�V�������܂�");
            }
            else if (gameObject.name == "Knight")
            {
                _currentPos = _promBlack.transform;
                Destroy(_promBlack);
                GameObject _promPiece = Instantiate(_promKnightBlack, _currentPos.position, _currentPos.rotation);
                _piece._blackPieces.Remove(_promBlack);
                _piece._blackPieces.Add(_promPiece);
                PieceMove _pieceInfo = _promPiece.GetComponent<PieceMove>();
                _pieceInfo.PromAssign();
                Debug.Log("�i�C�g�Ƀv�����[�V�������܂�");
            }
            _promWhite = null;
            _promBlack = null;
        }
    }
}
