using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �i�C�g�̈ړ�����(�j�n�̓�����4�����ɂł���)
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
        _pieceInfo = _search.pieceInfo;

        //�j�n �O����
        _vecX = 6f;
        _vecY = 10f;
        _vecZ = 12f;
        for (int i = 0; i < 2; i++)
        {
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(_vecX, _vecY, _vecZ), Vector3.down, out _hit, 30))
            {
                //�T����ɂ����̂������̋������
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    _search.ImmovablePieces.Add(_hit.collider.gameObject);
                    Debug.Log(_hit.collider.gameObject.name + "�ɂ͂����߂܂���");
                    //break;
                }
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX -= 12f;
                    if (!_search.MovableTile.Contains(_hit.collider))
                    {
                        _search.MovableTile.Add(_hit.collider);
                        _hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    }
                    _search.Tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "�ɐi�ނ��Ƃ��o���܂�");
                    GetablePiece();
                }
            }
            else //�T���悪�Ֆʂ͈̔͊O�������ꍇ
            {
                _vecX -= 12f;
                Debug.Log("Collider���������ĂȂ�");
            }
        }
        //�j�n ������
        _vecX = 6f;
        _vecY = 10f;
        _vecZ = 12f;
        for (int j = 0; j < 2; j++)
        {
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(_vecX, _vecY, -_vecZ), Vector3.down, out _hit, 30))
            {
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    _search.ImmovablePieces.Add(_hit.collider.gameObject);
                    Debug.Log(_hit.collider.gameObject.name + "�ɂ͂����߂܂���");
                    //break;
                }
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX -= 12f;
                    if (!_search.MovableTile.Contains(_hit.collider))
                    {
                        _search.MovableTile.Add(_hit.collider);
                        _hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    }
                    _search.Tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "�ɐi�ނ��Ƃ��o���܂�");
                    GetablePiece();
                }
            }
            else //�T���悪�Ֆʂ͈̔͊O�������ꍇ
            {
                _vecX -= 12f;
                Debug.Log("Collider���������ĂȂ�");
            }
        }
        //�j�n ������
        _vecX = 12f;
        _vecY = 10f;
        _vecZ = 6f;
        for (int k = 0; k < 2; k++)
        {
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(-_vecX, _vecY, _vecZ), Vector3.down, out _hit, 30))
            {
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    _search.ImmovablePieces.Add(_hit.collider.gameObject);
                    Debug.Log(_hit.collider.gameObject.name + "�ɂ͂����߂܂���");
                    //break;
                }
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecZ -= 12f;
                    if (!_search.MovableTile.Contains(_hit.collider))
                    {
                        _search.MovableTile.Add(_hit.collider);
                        _hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    }
                    _search.Tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "�ɐi�ނ��Ƃ��o���܂�");
                    GetablePiece();
                }
            }
            else //�T���悪�Ֆʂ͈̔͊O�������ꍇ
            {
                _vecZ -= 12f;
                Debug.Log("Collider���������ĂȂ�");
            }
        }
        //�j�n �E����
        _vecX = 12f;
        _vecY = 10f;
        _vecZ = 6f;
        for (int l = 0; l < 2; l++)
        {
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(_vecX, _vecY, _vecZ), Vector3.down, out _hit, 30))
            {
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    _search.ImmovablePieces.Add(_hit.collider.gameObject);
                    Debug.Log(_hit.collider.gameObject.name + "�ɂ͂����߂܂���");
                    //break;
                }
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecZ -= 12f;
                    if (!_search.MovableTile.Contains(_hit.collider))
                    {
                        _search.MovableTile.Add(_hit.collider);
                        _hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    }
                    _search.Tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "�ɐi�ނ��Ƃ��o���܂�");
                    GetablePiece();
                }
            }
            else //�T���悪�Ֆʂ͈̔͊O�������ꍇ
            {
                _vecZ -= 12f;
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

    /// <summary>
    /// ���D����ꍇ�̏���
    /// </summary>
    public void GetablePiece()
    {
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
            }
        }
    }
}
