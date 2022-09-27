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
        _pieceInfo = _search.pieceInfo;

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
        foreach (Collider col in _search.Tile)
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
            _search.ImmovablePieces.Add(_hit.collider.gameObject);
            Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
        }
        else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
        {
            if (!_search.MovableTile.Contains(_hit.collider))
            {
                _search.MovableTile.Add(_hit.collider);
                _hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
            }
            _search.Tile.Remove(_hit.collider);
            Debug.Log(_hit.collider.gameObject.name + "�ɐi�ނ��Ƃ��o���܂�");
            //�T����Ɋl����������ꍇ
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
}
