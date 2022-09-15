using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �i�C�g�̈ړ�����
/// </summary>
public class Knight : MonoBehaviour
{
    MasuSearch _search;
    PieceManager _piece;
    [Tooltip("���̋�l��܂�"), SerializeField] Material _getable;
    [Tooltip("�ړ�������i�C�g")] public GameObject _pieceInfo;
    public RaycastHit _hit;
    float _vecX;
    float _vecY;
    float _vecZ;

    // Start is called before the first frame update
    void Start()
    {
        _search = GameObject.Find("Board,Tile").GetComponent<MasuSearch>();
        _piece = GameObject.Find("Piece").GetComponent<PieceManager>();
    }

    public void KnightMovement ()
    {
        _pieceInfo = _search._pieceInfo;

        //�j�n �O����
        _vecX = 2.55f;
        _vecY = 2.55f;
        _vecZ = 5f;
        for (int i = 0; i < 2; i++)
        {
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                //�T����ɂ����̂������̋������
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    _search._immovablePieces.Add(_hit.collider.gameObject);
                    Debug.Log(_hit.collider.gameObject.name + "�ɂ͂����߂܂���");
                    //break;
                }
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX -= 5f;
                    if (!_search._movableTile.Contains(_hit.collider))
                    {
                        _search._movableTile.Add(_hit.collider);
                        _hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    }
                    _search._tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "�ɐi�ނ��Ƃ��o���܂�");
                    if (_pieceInfo.tag == "WhitePiece")
                    {
                        if (_hit.collider.gameObject.tag == "BlackPiece")
                        {
                            if (!_piece._getablePieces.Contains(_hit.collider.gameObject))
                            {
                                _piece._getablePieces.Add(_hit.collider.gameObject);
                            }
                            _piece._blackPieces.Remove(_hit.collider.gameObject);
                            //break;
                        }
                    }
                    else if (_pieceInfo.tag == "BlackPiece")
                    {
                        if (_hit.collider.gameObject.tag == "WhitePiece")
                        {
                            if (!_piece._getablePieces.Contains(_hit.collider.gameObject))
                            {
                                _piece._getablePieces.Add(_hit.collider.gameObject);
                            }
                            _piece._whitePieces.Remove(_hit.collider.gameObject);
                            //break;
                        }
                    }
                }
            }
            else //�T���悪�Ֆʂ͈̔͊O�������ꍇ
            {
                _vecX -= 5f;
                Debug.Log("Collider���������ĂȂ�");
            }
        }
        //�j�n ������
        _vecX = 2.55f;
        _vecY = 2.55f;
        _vecZ = 5f;
        for (int j = 0; j < 2; j++)
        {
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    _search._immovablePieces.Add(_hit.collider.gameObject);
                    Debug.Log(_hit.collider.gameObject.name + "�ɂ͂����߂܂���");
                    //break;
                }
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX -= 5f;
                    if (!_search._movableTile.Contains(_hit.collider))
                    {
                        _search._movableTile.Add(_hit.collider);
                        _hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    }
                    _search._tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "�ɐi�ނ��Ƃ��o���܂�");
                    if (_pieceInfo.tag == "WhitePiece")
                    {
                        if (_hit.collider.gameObject.tag == "BlackPiece")
                        {
                            if (!_piece._getablePieces.Contains(_hit.collider.gameObject))
                            {
                                _piece._getablePieces.Add(_hit.collider.gameObject);
                            }
                            _piece._blackPieces.Remove(_hit.collider.gameObject);
                            //break;
                        }
                    }
                    else if (_pieceInfo.tag == "BlackPiece")
                    {
                        if (_hit.collider.gameObject.tag == "WhitePiece")
                        {
                            if (!_piece._getablePieces.Contains(_hit.collider.gameObject))
                            {
                                _piece._getablePieces.Add(_hit.collider.gameObject);
                            }
                            _piece._whitePieces.Remove(_hit.collider.gameObject);
                            //break;
                        }
                    }
                }
            }
            else //�T���悪�Ֆʂ͈̔͊O�������ꍇ
            {
                _vecX -= 5f;
                Debug.Log("Collider���������ĂȂ�");
            }
        }
        //�j�n ������
        _vecX = 5f;
        _vecY = 2.55f;
        _vecZ = 2.55f;
        for (int k = 0; k < 2; k++)
        {
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    _search._immovablePieces.Add(_hit.collider.gameObject);
                    Debug.Log(_hit.collider.gameObject.name + "�ɂ͂����߂܂���");
                    //break;
                }
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecZ -= 5f;
                    if (!_search._movableTile.Contains(_hit.collider))
                    {
                        _search._movableTile.Add(_hit.collider);
                        _hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    }
                    _search._tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "�ɐi�ނ��Ƃ��o���܂�");
                    if (_pieceInfo.tag == "WhitePiece")
                    {
                        if (_hit.collider.gameObject.tag == "BlackPiece")
                        {
                            if (!_piece._getablePieces.Contains(_hit.collider.gameObject))
                            {
                                _piece._getablePieces.Add(_hit.collider.gameObject);
                            }
                            _piece._blackPieces.Remove(_hit.collider.gameObject);
                            //break;
                        }
                    }
                    else if (_pieceInfo.tag == "BlackPiece")
                    {
                        if (_hit.collider.gameObject.tag == "WhitePiece")
                        {
                            if (!_piece._getablePieces.Contains(_hit.collider.gameObject))
                            {
                                _piece._getablePieces.Add(_hit.collider.gameObject);
                            }
                            _piece._whitePieces.Remove(_hit.collider.gameObject);
                            //break;
                        }
                    }
                }
            }
            else //�T���悪�Ֆʂ͈̔͊O�������ꍇ
            {
                _vecZ -= 5f;
                Debug.Log("Collider���������ĂȂ�");
            }
        }
        //�j�n �E����
        _vecX = 5f;
        _vecY = 2.55f;
        _vecZ = 2.55f;
        for (int l = 0; l < 2; l++)
        {
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    _search._immovablePieces.Add(_hit.collider.gameObject);
                    Debug.Log(_hit.collider.gameObject.name + "�ɂ͂����߂܂���");
                    //break;
                }
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecZ -= 5f;
                    if (!_search._movableTile.Contains(_hit.collider))
                    {
                        _search._movableTile.Add(_hit.collider);
                        _hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    }
                    _search._tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "�ɐi�ނ��Ƃ��o���܂�");
                    if (_pieceInfo.tag == "WhitePiece")
                    {
                        if (_hit.collider.gameObject.tag == "BlackPiece")
                        {
                            if (!_piece._getablePieces.Contains(_hit.collider.gameObject))
                            {
                                _piece._getablePieces.Add(_hit.collider.gameObject);
                            }
                            _piece._blackPieces.Remove(_hit.collider.gameObject);
                            //break;
                        }
                    }
                    else if (_pieceInfo.tag == "BlackPiece")
                    {
                        if (_hit.collider.gameObject.tag == "WhitePiece")
                        {
                            if (!_piece._getablePieces.Contains(_hit.collider.gameObject))
                            {
                                _piece._getablePieces.Add(_hit.collider.gameObject);
                            }
                            _piece._whitePieces.Remove(_hit.collider.gameObject);
                            //break;
                        }
                    }
                }
            }
            else //�T���悪�Ֆʂ͈̔͊O�������ꍇ
            {
                _vecZ -= 5f;
                Debug.Log("Collider���������ĂȂ�");
            }
        }
        //�ړ��͈͈ȊO�̃}�X��Collider��off�ɂ��鏈��������
        foreach (Collider col in _search._tile)
        {
            col.enabled = false;
            Debug.Log(col + "��Collider��off�ɂ��܂�");
        }
    }
}
