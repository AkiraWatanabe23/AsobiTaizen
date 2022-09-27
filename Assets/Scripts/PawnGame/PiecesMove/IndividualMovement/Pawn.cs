using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �|�[���̈ړ�����(���X���G�ȓ����ł�)
/// </summary>
public class Pawn : MonoBehaviour
{
    MasuSearch _search;
    PieceManager _piece;
    [Tooltip("���̋�l��܂�"), SerializeField] Material _getable;
    [Tooltip("�ړ�������|�[��")] GameObject _pieceInfo;
    RaycastHit _hit;
    float _vecX;
    float _vecY;
    float _vecZ;

    // Start is called before the first frame update
    void Start()
    {
        _search = GameObject.Find("Board,Tile").GetComponent<MasuSearch>();
        _piece = GameObject.Find("Piece").GetComponent<PieceManager>();
    }

    public void PawnMovement()
    {
        _pieceInfo = _search.pieceInfo;

        /********************����̈ړ�����********************/
        if (_pieceInfo.tag == "WhitePiece")
        {
            //1,1��ڂ̓���...2�}�X�ړ���
            if (_pieceInfo.GetComponent<PieceMove>().moveCount == 0)
            {
                _vecX = 0f;
                _vecY = 3f;
                _vecZ = 6f;
                for (int i = 0; i < 2; i++)
                {
                    if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.5f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
                    {
                        if (_hit.collider.gameObject.tag == _pieceInfo.tag || _hit.collider.gameObject.tag == "BlackPiece")
                        {
                            _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                            _search.ImmovablePieces.Add(_hit.collider.gameObject);
                            Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
                            break;
                        }
                        else if (_hit.collider.gameObject.tag == "Tile")
                        {
                            _vecZ += 5f;
                            //�T�����ɓ����v�f�����x��List�ɓ���̂�h��
                            if (!_search.MovableTile.Contains(_hit.collider))
                            {
                                _search.MovableTile.Add(_hit.collider);
                                _hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
                            }
                            _search.Tile.Remove(_hit.collider);
                            Debug.Log(_hit.collider.gameObject.name + "�ɐi�ނ��Ƃ��o���܂�");
                            if (_hit.collider.gameObject.tag != "Tile")
                            {
                                break;
                            }
                        }
                    }
                }
                //�ړ��͈͈ȊO�̃}�X��Collider��off�ɂ��鏈��������
                foreach (Collider col in _search.Tile)
                {
                    col.enabled = false;
                }
            }
            //2,2��ڈȍ~��1�}�X�ړ�
            else if (_pieceInfo.GetComponent<PieceMove>().moveCount != 0)
            {
                _vecX = 0f;
                _vecY = 3f;
                _vecZ = 6f;
                if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.5f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
                {
                    if (_hit.collider.gameObject.tag == _pieceInfo.tag || _hit.collider.gameObject.tag == "BlackPiece")
                    {
                        _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                        _search.ImmovablePieces.Add(_hit.collider.gameObject);
                        Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
                    }
                    else if (_hit.collider.gameObject.tag == "Tile")
                    {
                        //�T�����ɓ����v�f�����x��List�ɓ���̂�h��
                        if (!_search.MovableTile.Contains(_hit.collider))
                        {
                            _search.MovableTile.Add(_hit.collider);
                            _hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
                        }
                        _search.Tile.Remove(_hit.collider);
                        Debug.Log(_hit.collider.gameObject.name + "�ɐi�ނ��Ƃ��o���܂�");
                        if (_hit.collider.gameObject.tag != "Tile")
                        {
                            _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                        }
                    }
                }
                //�ړ��͈͈ȊO�̃}�X��Collider��off�ɂ��鏈��������
                foreach (Collider col in _search.Tile)
                {
                    col.enabled = false;
                }
            }

            //��Ɏ΂ߑO�̒T��
            _vecX = 6f;
            _vecY = 3f;
            _vecZ = 6f;
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.5f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == "BlackPiece")
                {
                    if (!_piece.GetablePieces.Contains(_hit.collider.gameObject))
                    {
                        _piece.GetablePieces.Add(_hit.collider.gameObject);
                        //�D������Material��؂�ւ���
                        _hit.collider.gameObject.GetComponent<MeshRenderer>().material = _getable;
                    }
                    _piece.BlackPieces.Remove(_hit.collider.gameObject);
                    Debug.Log(_hit.collider.gameObject.name + "���Ƃ邱�Ƃ��o���܂�");
                }
            }
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.5f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == "BlackPiece")
                {
                    if (!_piece.GetablePieces.Contains(_hit.collider.gameObject))
                    {
                        _piece.GetablePieces.Add(_hit.collider.gameObject);
                        //�D������Material��؂�ւ���
                        _hit.collider.gameObject.GetComponent<MeshRenderer>().material = _getable;
                    }
                    _piece.BlackPieces.Remove(_hit.collider.gameObject);
                    Debug.Log(_hit.collider.gameObject.name + "���Ƃ邱�Ƃ��o���܂�");
                }
            }
        }
        /********************����̈ړ�����********************/
        else if (_pieceInfo.tag == "BlackPiece")
        {
            //1,1��ڂ̓���...2�}�X�ړ���
            if (_pieceInfo.GetComponent<PieceMove>().moveCount == 0)
            {
                _vecX = 0f;
                _vecY = 3f;
                _vecZ = 6f;
                for (int i = 0; i < 2; i++)
                {
                    if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.5f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), out _hit, 100))
                    {
                        if (_hit.collider.gameObject.tag == _pieceInfo.tag || _hit.collider.gameObject.tag == "WhitePiece")
                        {
                            _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                            _search.ImmovablePieces.Add(_hit.collider.gameObject);
                            Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
                            break;
                        }
                        else if (_hit.collider.gameObject.tag == "Tile")
                        {
                            _vecZ += 5f;
                            //�T�����ɓ����v�f�����x��List�ɓ���̂�h��
                            if (!_search.MovableTile.Contains(_hit.collider))
                            {
                                _search.MovableTile.Add(_hit.collider);
                                _hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
                            }
                            _search.Tile.Remove(_hit.collider);
                            Debug.Log(_hit.collider.gameObject.name + "�ɐi�ނ��Ƃ��o���܂�");
                            if (_hit.collider.gameObject.tag != "Tile")
                            {
                                break;
                            }
                        }
                    }
                }
                //�ړ��͈͈ȊO�̃}�X��Collider��off�ɂ��鏈��������
                foreach (Collider col in _search.Tile)
                {
                    col.enabled = false;
                }
            }
            //2,2��ڈȍ~��1�}�X�ړ�
            else if (_pieceInfo.GetComponent<PieceMove>().moveCount != 0)
            {
                _vecX = 0f;
                _vecY = 3f;
                _vecZ = 6f;
                if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.5f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), out _hit, 100))
                {
                    if (_hit.collider.gameObject.tag == _pieceInfo.tag || _hit.collider.gameObject.tag == "WhitePiece")
                    {
                        _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                        _search.ImmovablePieces.Add(_hit.collider.gameObject);
                        Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
                    }
                    else if (_hit.collider.gameObject.tag == "Tile")
                    {
                        //�T�����ɓ����v�f�����x��List�ɓ���̂�h��
                        if (!_search.MovableTile.Contains(_hit.collider))
                        {
                            _search.MovableTile.Add(_hit.collider);
                            _hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
                        }
                        _search.Tile.Remove(_hit.collider);
                        Debug.Log(_hit.collider.gameObject.name + "�ɐi�ނ��Ƃ��o���܂�");
                        if (_hit.collider.gameObject.tag != "Tile")
                        {
                            _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                        }
                    }
                }
                //�ړ��͈͈ȊO�̃}�X��Collider��off�ɂ��鏈��������
                foreach (Collider col in _search.Tile)
                {
                    col.enabled = false;
                }
            }
            //��Ɏ΂ߑO�̒T��
            _vecX = 6f;
            _vecY = 3f;
            _vecZ = 6f;
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.5f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == "WhitePiece")
                {
                    if (!_piece.GetablePieces.Contains(_hit.collider.gameObject))
                    {
                        _piece.GetablePieces.Add(_hit.collider.gameObject);
                        //�D������Material��؂�ւ���
                        _hit.collider.gameObject.GetComponent<MeshRenderer>().material = _getable;
                    }
                    _piece.WhitePieces.Remove(_hit.collider.gameObject);
                    Debug.Log(_hit.collider.gameObject.name + "���Ƃ邱�Ƃ��o���܂�");
                }
            }
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.5f, 0f), new Vector3(-_vecX, -_vecY, -_vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == "WhitePiece")
                {
                    if (!_piece.GetablePieces.Contains(_hit.collider.gameObject))
                    {
                        _piece.GetablePieces.Add(_hit.collider.gameObject);
                        //�D������Material��؂�ւ���
                        _hit.collider.gameObject.GetComponent<MeshRenderer>().material = _getable;
                    }
                    _piece.WhitePieces.Remove(_hit.collider.gameObject);
                    Debug.Log(_hit.collider.gameObject.name + "���Ƃ邱�Ƃ��o���܂�");
                }
            }
        }
    }
}
