using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �L���O�̈ړ�����(�S������1�}�X�ł���)
/// </summary>
public class King : MonoBehaviour
{
    MasuSearch _search;
    PieceManager _piece;
    [Tooltip("���̋�l��܂�"), SerializeField] Material _getable;
    [Tooltip("�ړ�������N�C�[��")] public GameObject _pieceInfo;
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

    public void KingMovement()
    {
        _pieceInfo = _search._pieceInfo;

        //////////�O�㍶�E�̓���//////////
        //�O�����
        _vecX = 0f;
        _vecY = 3f;
        _vecZ = 4.5f;
        if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 4f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
        {
            MovableCheck();
        }
        else
        {
            Debug.Log("none");
        }
        if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 4f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), out _hit, 100))
        {
            MovableCheck();
        }
        else
        {
            Debug.Log("none");
        }

        //���E����
        _vecX = 4.5f;
        _vecY = 3f;
        _vecZ = 0f;
        if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 4f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
        {
            MovableCheck();
        }
        else
        {
            Debug.Log("none");
        }
        if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 4f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), out _hit, 100))
        {
            MovableCheck();
        }
        else
        {
            Debug.Log("none");
        }

        //////////�΂ߕ����̓���//////////
        //�΂ߑO2����
        _vecX = 5f;
        _vecY = 3f;
        _vecZ = 5f;
        if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 4f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
        {
            MovableCheck();
        }
        else
        {
            Debug.Log("none");
        }
        if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 4f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), out _hit, 100))
        {
            MovableCheck();
        }
        else
        {
            Debug.Log("none");
        }

        //�΂ߌ��2����
        if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 4f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), out _hit, 100))
        {
            MovableCheck();
        }
        else
        {
            Debug.Log("none");
        }
        if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 4f, 0f), new Vector3(-_vecX, -_vecY, -_vecZ), out _hit, 100))
        {
            MovableCheck();
        }
        else
        {
            Debug.Log("none");
        }
        //�ړ��͈͈ȊO�̃}�X��Collider��off�ɂ��鏈��������
        foreach (Collider col in _search._tile)
        {
            col.enabled = false;
            Debug.Log(col + "��Collider��off�ɂ��܂�");
        }
    }

    private void MovableCheck()
    {
        if (_hit.collider.gameObject.tag == _pieceInfo.tag)
        {
            _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
            _search._immovablePieces.Add(_hit.collider.gameObject);
            Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
        }
        else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
        {
            if (!_search._movableTile.Contains(_hit.collider))
            {
                _search._movableTile.Add(_hit.collider);
                _hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
            }
            _search._tile.Remove(_hit.collider);
            Debug.Log(_hit.collider.gameObject.name + "�ɐi�ނ��Ƃ��o���܂�");
            //�T����Ɋl����������ꍇ
            if (_pieceInfo.tag == "WhitePiece")
            {
                if (_hit.collider.gameObject.tag == "BlackPiece")
                {
                    if (!_piece._getablePieces.Contains(_hit.collider.gameObject))
                    {
                        _piece._getablePieces.Add(_hit.collider.gameObject);
                        _hit.collider.gameObject.GetComponent<MeshRenderer>().material = _getable;
                    }
                    _piece._blackPieces.Remove(_hit.collider.gameObject);
                }
            }
            else if (_pieceInfo.tag == "BlackPiece")
            {
                if (_hit.collider.gameObject.tag == "WhitePiece")
                {
                    if (!_piece._getablePieces.Contains(_hit.collider.gameObject))
                    {
                        _piece._getablePieces.Add(_hit.collider.gameObject);
                        _hit.collider.gameObject.GetComponent<MeshRenderer>().material = _getable;
                    }
                    _piece._whitePieces.Remove(_hit.collider.gameObject);
                }
            }
        }
    }
}
