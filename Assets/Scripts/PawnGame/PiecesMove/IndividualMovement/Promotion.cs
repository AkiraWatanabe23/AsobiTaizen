using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �|�[���̓��ʂȓ���
/// </summary>
public class Promotion : MonoBehaviour
{
    //�v�����[�V�����̋�
    [SerializeField] GameObject[] _promPieces = new GameObject[8];
    GameObject _promWhite;
    GameObject _promBlack;
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
                PromPos();
                GameObject _promPiece = Instantiate(_promPieces[0], _currentPos.position, _currentPos.rotation);
                _piece.WhitePieces.Remove(_promWhite);
                _piece.WhitePieces.Add(_promPiece);
                Debug.Log("�N�C�[���Ƀv�����[�V�������܂�");
            }
            else if (gameObject.name == "Rook")
            {
                PromPos();
                GameObject _promPiece = Instantiate(_promPieces[1], _currentPos.position, _currentPos.rotation);
                _piece.WhitePieces.Remove(_promWhite);
                _piece.WhitePieces.Add(_promPiece);
                Debug.Log("���[�N�Ƀv�����[�V�������܂�");
            }
            else if (gameObject.name == "Bishop")
            {
                PromPos();
                GameObject _promPiece = Instantiate(_promPieces[2], _currentPos.position, _currentPos.rotation);
                _piece.WhitePieces.Remove(_promWhite);
                _piece.WhitePieces.Add(_promPiece);
                Debug.Log("�r�V���b�v�Ƀv�����[�V�������܂�");
            }
            else if (gameObject.name == "Knight")
            {
                PromPos();
                GameObject _promPiece = Instantiate(_promPieces[3], _currentPos.position, _currentPos.rotation);
                _piece.WhitePieces.Remove(_promWhite);
                _piece.WhitePieces.Add(_promPiece);
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
                PromPos();
                GameObject _promPiece = Instantiate(_promPieces[4], _currentPos.position, _currentPos.rotation);
                _piece.BlackPieces.Remove(_promBlack);
                _piece.BlackPieces.Add(_promPiece);
                Debug.Log("�N�C�[���Ƀv�����[�V�������܂�");
            }
            else if (gameObject.name == "Rook")
            {
                PromPos();
                GameObject _promPiece = Instantiate(_promPieces[5], _currentPos.position, _currentPos.rotation);
                _piece.BlackPieces.Remove(_promBlack);
                _piece.BlackPieces.Add(_promPiece);
                Debug.Log("���[�N�Ƀv�����[�V�������܂�");
            }
            else if (gameObject.name == "Bishop")
            {
                PromPos();
                GameObject _promPiece = Instantiate(_promPieces[6], _currentPos.position, _currentPos.rotation);
                _piece.BlackPieces.Remove(_promBlack);
                _piece.BlackPieces.Add(_promPiece);
                Debug.Log("�r�V���b�v�Ƀv�����[�V�������܂�");
            }
            else if (gameObject.name == "Knight")
            {
                PromPos();
                GameObject _promPiece = Instantiate(_promPieces[7], _currentPos.position, _currentPos.rotation);
                _piece.BlackPieces.Remove(_promBlack);
                _piece.BlackPieces.Add(_promPiece);
                Debug.Log("�i�C�g�Ƀv�����[�V�������܂�");
            }
            _promWhite = null;
            _promBlack = null;
        }
    }

    /// <summary>
    /// �v�����[�V�������̈ʒu���
    /// </summary>
    private void PromPos()
    {
        if (_promWhite != null && _promBlack == null)
        {
            _currentPos = _promWhite.transform;
            Destroy(_promWhite);
        }
        else if (_promWhite == null && _promBlack != null)
        {
            _currentPos = _promBlack.transform;
            Destroy(_promBlack);
        }

        Vector3 _pos = _currentPos.position;
        _pos.y = 1f;
        _currentPos.position = _pos;
    }
}
