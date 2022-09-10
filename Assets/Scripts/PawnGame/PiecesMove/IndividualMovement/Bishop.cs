using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ビショップの移動処理
/// </summary>
public class Bishop : MonoBehaviour
{
    MasuSearch _search;
    PieceManager _piece;
    [Tooltip("移動させるビショップ")] public GameObject _pieceInfo;
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

    public void BishopMovement()
    {
        _pieceInfo = _search._pieceInfo;

        //左斜め前方向
        _vecX = 2.55f;
        _vecY = 2.55f;
        _vecZ = 2.55f;
        for (int i = 0; i < 8 - _search._tileRank; i++)
        {
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    _search._immovablePieces.Add(_hit.collider.gameObject);
                    Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX += 2.5f;
                    _vecZ += 2.5f;
                    if (!_search._movableTile.Contains(_hit.collider))
                    {
                        _search._movableTile.Add(_hit.collider);
                        _hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    }
                    _search._tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                    if (_pieceInfo.tag == "WhitePiece")
                    {
                        if (_hit.collider.gameObject.tag == "BlackPiece")
                        {
                            break;
                        }
                    }
                    else if (_pieceInfo.tag == "BlackPiece")
                    {
                        if (_hit.collider.gameObject.tag == "WhitePiece")
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                Debug.Log("Colliderが当たってない");
            }
        }
        //右斜め前方向
        _vecX = 2.55f;
        _vecY = 2.55f;
        _vecZ = 2.55f;
        for (int j = _search._tileRank; j > 1; j--)
        {
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    _search._immovablePieces.Add(_hit.collider.gameObject);
                    Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX += 2.5f;
                    _vecZ += 2.5f;
                    if (!_search._movableTile.Contains(_hit.collider))
                    {
                        _search._movableTile.Add(_hit.collider);
                        _hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    }
                    _search._tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                    if (_pieceInfo.tag == "WhitePiece")
                    {
                        if (_hit.collider.gameObject.tag == "BlackPiece")
                        {
                            break;
                        }
                    }
                    else if (_pieceInfo.tag == "BlackPiece")
                    {
                        if (_hit.collider.gameObject.tag == "WhitePiece")
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                Debug.Log("Colliderが当たってない");
            }
        }
        //左斜め後ろ方向
        _vecX = 2.55f;
        _vecY = 2.55f;
        _vecZ = 2.55f;
        for (int k = _search._tileFile; k > 1; k--)
        {
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, -_vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    _search._immovablePieces.Add(_hit.collider.gameObject);
                    Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX += 2.5f;
                    _vecZ += 2.5f;
                    if (!_search._movableTile.Contains(_hit.collider))
                    {
                        _search._movableTile.Add(_hit.collider);
                        _hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    }
                    _search._tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                    if (_pieceInfo.tag == "WhitePiece")
                    {
                        if (_hit.collider.gameObject.tag == "BlackPiece")
                        {
                            break;
                        }
                    }
                    else if (_pieceInfo.tag == "BlackPiece")
                    {
                        if (_hit.collider.gameObject.tag == "WhitePiece")
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                Debug.Log("Colliderが当たってない");
            }
        }
        //右斜め後ろ方向
        _vecX = 2.55f;
        _vecY = 2.55f;
        _vecZ = 2.55f;
        for (int l = 0; l < 8 - _search._tileFile; l++)
        {
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    _search._immovablePieces.Add(_hit.collider.gameObject);
                    Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX += 2.5f;
                    _vecZ += 2.5f;
                    if (!_search._movableTile.Contains(_hit.collider))
                    {
                        _search._movableTile.Add(_hit.collider);
                        _hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    }
                    _search._tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                    if (_pieceInfo.tag == "WhitePiece")
                    {
                        if (_hit.collider.gameObject.tag == "BlackPiece")
                        {
                            break;
                        }
                    }
                    else if (_pieceInfo.tag == "BlackPiece")
                    {
                        if (_hit.collider.gameObject.tag == "WhitePiece")
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                Debug.Log("Colliderが当たってない");
            }
        }
        //移動範囲以外のColliderをoffにする処理を書く
        foreach (Collider col in _search._tile)
        {
            col.enabled = false;
            Debug.Log(col + "のColliderをoffにします");
        }
    }
}
