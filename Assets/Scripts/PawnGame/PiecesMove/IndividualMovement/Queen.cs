using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �N�C�[���̈ړ�����(�S�����ɉ��}�X�ł��ړ��ł���)
/// </summary>
public class Queen : MonoBehaviour
{
    MasuSearch _search;
    PieceManager _piece;
    [Tooltip("���̋�l��܂�"), SerializeField] Material _getable;
    [Tooltip("�ړ�������N�C�[��")] GameObject _pieceInfo;
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

    public void QueenMovement()
    {
        _pieceInfo = _search.PieceInfo;

        /*==========�O�㍶�E�̓���==========*/
        //�O����
        _vecX = 0f;
        _vecY = 15f;
        _vecZ = 6f;
        for (int i = 0; i < 7; i++)
        {
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(_vecX, _vecY, _vecZ), Vector3.down, out _hit, 30))
            {
                //�T����ɂ����̂������̋������
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    _search.ImmovablePieces.Add(_hit.collider.gameObject);
                    Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecZ += 6f;
                    if (!_search.MovableTile.Contains(_hit.collider))
                    {
                        _search.MovableTile.Add(_hit.collider);
                        _hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    }
                    _search.Tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "�ɐi�ނ��Ƃ��o���܂�");
                    if (_pieceInfo.tag == "WhitePiece")
                    {
                        if (_hit.collider.gameObject.tag == "BlackPiece")
                        {
                            if (!_piece.GetablePieces.Contains(_hit.collider.gameObject))
                            {
                                _piece.GetablePieces.Add(_hit.collider.gameObject);
                                _hit.collider.gameObject.GetComponent<MeshRenderer>().material = _getable;
                            }
                            _piece.BlackPieces.Remove(_hit.collider.gameObject);
                            break;
                        }
                    }
                    else if (_pieceInfo.tag == "BlackPiece")
                    {
                        if (_hit.collider.gameObject.tag == "WhitePiece")
                        {
                            if (!_piece.GetablePieces.Contains(_hit.collider.gameObject))
                            {
                                _piece.GetablePieces.Add(_hit.collider.gameObject);
                                _hit.collider.gameObject.GetComponent<MeshRenderer>().material = _getable;
                            }
                            _piece.WhitePieces.Remove(_hit.collider.gameObject);
                            break;
                        }
                    }
                }
            }
            else
            {
                Debug.Log("Collider���������ĂȂ�");
            }
        }
        //������
        _vecX = 0f;
        _vecY = 15f;
        _vecZ = 6f;
        for (int j = 0; j < 7; j++)
        {
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(_vecX, _vecY, -_vecZ), Vector3.down, out _hit, 30))
            {
                //�T����ɂ����̂������̋������
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    _search.ImmovablePieces.Add(_hit.collider.gameObject);
                    Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecZ += 6f;
                    if (!_search.MovableTile.Contains(_hit.collider))
                    {
                        _search.MovableTile.Add(_hit.collider);
                        _hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    }
                    _search.Tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "�ɐi�ނ��Ƃ��o���܂�");
                    if (_pieceInfo.tag == "WhitePiece")
                    {
                        if (_hit.collider.gameObject.tag == "BlackPiece")
                        {
                            if (!_piece.GetablePieces.Contains(_hit.collider.gameObject))
                            {
                                _piece.GetablePieces.Add(_hit.collider.gameObject);
                                _hit.collider.gameObject.GetComponent<MeshRenderer>().material = _getable;
                            }
                            _piece.BlackPieces.Remove(_hit.collider.gameObject);
                            break;
                        }
                    }
                    else if (_pieceInfo.tag == "BlackPiece")
                    {
                        if (_hit.collider.gameObject.tag == "WhitePiece")
                        {
                            if (!_piece.GetablePieces.Contains(_hit.collider.gameObject))
                            {
                                _piece.GetablePieces.Add(_hit.collider.gameObject);
                                _hit.collider.gameObject.GetComponent<MeshRenderer>().material = _getable;
                            }
                            _piece.WhitePieces.Remove(_hit.collider.gameObject);
                            break;
                        }
                    }
                }
            }
            else
            {
                Debug.Log("Collider���������ĂȂ�");
            }
        }
        //������
        _vecX = 6f;
        _vecY = 15f;
        _vecZ = 0f;
        for (int k = 0; k < 7; k++)
        {
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(-_vecX, _vecY, _vecZ), Vector3.down, out _hit, 30))
            {
                //�T����ɂ����̂������̋������
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    _search.ImmovablePieces.Add(_hit.collider.gameObject);
                    Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX += 6f;
                    if (!_search.MovableTile.Contains(_hit.collider))
                    {
                        _search.MovableTile.Add(_hit.collider);
                        _hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    }
                    _search.Tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "�ɐi�ނ��Ƃ��o���܂�");
                    if (_pieceInfo.tag == "WhitePiece")
                    {
                        if (_hit.collider.gameObject.tag == "BlackPiece")
                        {
                            if (!_piece.GetablePieces.Contains(_hit.collider.gameObject))
                            {
                                _piece.GetablePieces.Add(_hit.collider.gameObject);
                                _hit.collider.gameObject.GetComponent<MeshRenderer>().material = _getable;
                            }
                            _piece.BlackPieces.Remove(_hit.collider.gameObject);
                            break;
                        }
                    }
                    else if (_pieceInfo.tag == "BlackPiece")
                    {
                        if (_hit.collider.gameObject.tag == "WhitePiece")
                        {
                            if (!_piece.GetablePieces.Contains(_hit.collider.gameObject))
                            {
                                _piece.GetablePieces.Add(_hit.collider.gameObject);
                                _hit.collider.gameObject.GetComponent<MeshRenderer>().material = _getable;
                            }
                            _piece.WhitePieces.Remove(_hit.collider.gameObject);
                            break;
                        }
                    }
                }
            }
            else
            {
                Debug.Log("Collider���������ĂȂ�");
            }
        }
        //�E����
        _vecX = 6f;
        _vecY = 15f;
        _vecZ = 0f;
        for (int l = 0; l < 7; l++)
        {
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(_vecX, _vecY, _vecZ), Vector3.down, out _hit, 30))
            {
                //�T����ɂ����̂������̋������
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    _search.ImmovablePieces.Add(_hit.collider.gameObject);
                    Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX += 6f;
                    if (!_search.MovableTile.Contains(_hit.collider))
                    {
                        _search.MovableTile.Add(_hit.collider);
                        _hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    }
                    _search.Tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "�ɐi�ނ��Ƃ��o���܂�");
                    if (_pieceInfo.tag == "WhitePiece")
                    {
                        if (_hit.collider.gameObject.tag == "BlackPiece")
                        {
                            if (!_piece.GetablePieces.Contains(_hit.collider.gameObject))
                            {
                                _piece.GetablePieces.Add(_hit.collider.gameObject);
                                _hit.collider.gameObject.GetComponent<MeshRenderer>().material = _getable;
                            }
                            _piece.BlackPieces.Remove(_hit.collider.gameObject);
                            break;
                        }
                    }
                    else if (_pieceInfo.tag == "BlackPiece")
                    {
                        if (_hit.collider.gameObject.tag == "WhitePiece")
                        {
                            if (!_piece.GetablePieces.Contains(_hit.collider.gameObject))
                            {
                                _piece.GetablePieces.Add(_hit.collider.gameObject);
                                _hit.collider.gameObject.GetComponent<MeshRenderer>().material = _getable;
                            }
                            _piece.WhitePieces.Remove(_hit.collider.gameObject);
                            break;
                        }
                    }
                }
            }
            else
            {
                Debug.Log("Collider���������ĂȂ�");
            }
        }
        /*==========�΂ߕ����̓���==========*/
        //���΂ߑO����
        _vecX = 6f;
        _vecY = 15f;
        _vecZ = 6f;
        for (int i = 0; i < 7; i++)
        {
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(-_vecX, _vecY, _vecZ), Vector3.down, out _hit, 30))
            {
                //�T����ɂ����̂������̋������
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    _search.ImmovablePieces.Add(_hit.collider.gameObject);
                    Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX += 6f;
                    _vecZ += 6f;
                    if (!_search.MovableTile.Contains(_hit.collider))
                    {
                        _search.MovableTile.Add(_hit.collider);
                        _hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    }
                    _search.Tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "�ɐi�ނ��Ƃ��o���܂�");
                    if (_pieceInfo.tag == "WhitePiece")
                    {
                        if (_hit.collider.gameObject.tag == "BlackPiece")
                        {
                            if (!_piece.GetablePieces.Contains(_hit.collider.gameObject))
                            {
                                _piece.GetablePieces.Add(_hit.collider.gameObject);
                                _hit.collider.gameObject.GetComponent<MeshRenderer>().material = _getable;
                            }
                            _piece.BlackPieces.Remove(_hit.collider.gameObject);
                            break;
                        }
                    }
                    else if (_pieceInfo.tag == "BlackPiece")
                    {
                        if (_hit.collider.gameObject.tag == "WhitePiece")
                        {
                            if (!_piece.GetablePieces.Contains(_hit.collider.gameObject))
                            {
                                _piece.GetablePieces.Add(_hit.collider.gameObject);
                                _hit.collider.gameObject.GetComponent<MeshRenderer>().material = _getable;
                            }
                            _piece.WhitePieces.Remove(_hit.collider.gameObject);
                            break;
                        }
                    }
                }
            }
            else
            {
                Debug.Log("Collider���������ĂȂ�");
            }
        }
        //�E�΂ߑO����
        _vecX = 6f;
        _vecY = 15f;
        _vecZ = 6f;
        for (int j = 0; j < 7; j++)
        {
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(_vecX, _vecY, _vecZ), Vector3.down, out _hit, 30))
            {
                //�T����ɂ����̂������̋������
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    _search.ImmovablePieces.Add(_hit.collider.gameObject);
                    Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX += 6f;
                    _vecZ += 6f;
                    if (!_search.MovableTile.Contains(_hit.collider))
                    {
                        _search.MovableTile.Add(_hit.collider);
                        _hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    }
                    _search.Tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "�ɐi�ނ��Ƃ��o���܂�");
                    if (_pieceInfo.tag == "WhitePiece")
                    {
                        if (_hit.collider.gameObject.tag == "BlackPiece")
                        {
                            if (!_piece.GetablePieces.Contains(_hit.collider.gameObject))
                            {
                                _piece.GetablePieces.Add(_hit.collider.gameObject);
                                _hit.collider.gameObject.GetComponent<MeshRenderer>().material = _getable;
                            }
                            _piece.BlackPieces.Remove(_hit.collider.gameObject);
                            break;
                        }
                    }
                    else if (_pieceInfo.tag == "BlackPiece")
                    {
                        if (_hit.collider.gameObject.tag == "WhitePiece")
                        {
                            if (!_piece.GetablePieces.Contains(_hit.collider.gameObject))
                            {
                                _piece.GetablePieces.Add(_hit.collider.gameObject);
                                _hit.collider.gameObject.GetComponent<MeshRenderer>().material = _getable;
                            }
                            _piece.WhitePieces.Remove(_hit.collider.gameObject);
                            break;
                        }
                    }
                }
            }
            else
            {
                Debug.Log("Collider���������ĂȂ�");
            }
        }
        //���΂ߌ�����
        _vecX = 6f;
        _vecY = 15f;
        _vecZ = 6f;
        for (int k = 0; k < 7; k++)
        {
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(-_vecX, _vecY, -_vecZ), Vector3.down, out _hit, 30))
            {
                //�T����ɂ����̂������̋������
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    _search.ImmovablePieces.Add(_hit.collider.gameObject);
                    Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX += 6f;
                    _vecZ += 6f;
                    if (!_search.MovableTile.Contains(_hit.collider))
                    {
                        _search.MovableTile.Add(_hit.collider);
                        _hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    }
                    _search.Tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "�ɐi�ނ��Ƃ��o���܂�");
                    if (_pieceInfo.tag == "WhitePiece")
                    {
                        if (_hit.collider.gameObject.tag == "BlackPiece")
                        {
                            if (!_piece.GetablePieces.Contains(_hit.collider.gameObject))
                            {
                                _piece.GetablePieces.Add(_hit.collider.gameObject);
                                _hit.collider.gameObject.GetComponent<MeshRenderer>().material = _getable;
                            }
                            _piece.BlackPieces.Remove(_hit.collider.gameObject);
                            break;
                        }
                    }
                    else if (_pieceInfo.tag == "BlackPiece")
                    {
                        if (_hit.collider.gameObject.tag == "WhitePiece")
                        {
                            if (!_piece.GetablePieces.Contains(_hit.collider.gameObject))
                            {
                                _piece.GetablePieces.Add(_hit.collider.gameObject);
                                _hit.collider.gameObject.GetComponent<MeshRenderer>().material = _getable;
                            }
                            _piece.WhitePieces.Remove(_hit.collider.gameObject);
                            break;
                        }
                    }
                }
            }
            else
            {
                Debug.Log("Collider���������ĂȂ�");
            }
        }
        //�E�΂ߌ�����
        _vecX = 6f;
        _vecY = 15f;
        _vecZ = 6f;
        for (int l = 0; l < 7; l++)
        {
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(_vecX, _vecY, -_vecZ), Vector3.down, out _hit, 30))
            {
                //�T����ɂ����̂������̋������
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    _search.ImmovablePieces.Add(_hit.collider.gameObject);
                    Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX += 6f;
                    _vecZ += 6f;
                    if (!_search.MovableTile.Contains(_hit.collider))
                    {
                        _search.MovableTile.Add(_hit.collider);
                        _hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    }
                    _search.Tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "�ɐi�ނ��Ƃ��o���܂�");
                    if (_pieceInfo.tag == "WhitePiece")
                    {
                        if (_hit.collider.gameObject.tag == "BlackPiece")
                        {
                            if (!_piece.GetablePieces.Contains(_hit.collider.gameObject))
                            {
                                _piece.GetablePieces.Add(_hit.collider.gameObject);
                                _hit.collider.gameObject.GetComponent<MeshRenderer>().material = _getable;
                            }
                            _piece.BlackPieces.Remove(_hit.collider.gameObject);
                            break;
                        }
                    }
                    else if (_pieceInfo.tag == "BlackPiece")
                    {
                        if (_hit.collider.gameObject.tag == "WhitePiece")
                        {
                            if (!_piece.GetablePieces.Contains(_hit.collider.gameObject))
                            {
                                _piece.GetablePieces.Add(_hit.collider.gameObject);
                                _hit.collider.gameObject.GetComponent<MeshRenderer>().material = _getable;
                            }
                            _piece.WhitePieces.Remove(_hit.collider.gameObject);
                            break;
                        }
                    }
                }
            }
            else
            {
                Debug.Log("Collider���������ĂȂ�");
            }
        }
        //�ړ��͈͈ȊO�̃}�X��Collider��off�ɂ��鏈��������
        foreach (Collider col in _search.Tile)
        {
            col.enabled = false;
            Debug.Log(col + "��Collider��off�ɂ��܂�");
        }
    }
}
