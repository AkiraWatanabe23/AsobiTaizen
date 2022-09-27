using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ルークの移動処理(前後左右に何マスでも移動できる)
/// </summary>
public class Rook : MonoBehaviour
{
    MasuSearch _search;
    PieceManager _piece;
    [Tooltip("この駒獲れます"), SerializeField] Material _getable;
    [Tooltip("移動させるルーク")] public GameObject _pieceInfo;
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

    public void RookMovement()
    {
        _pieceInfo = _search.pieceInfo;

        //前方向
        _vecX = 0f;
        _vecY = 10f;
        _vecZ = 6f;
        for (int i = 0; i < 8 - _search._tileRank; i++)
        {
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(_vecX, _vecY, _vecZ), Vector3.down, out _hit, 20))
            {
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    _search.ImmovablePieces.Add(_hit.collider.gameObject);
                    Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
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
                    Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                    //探索先に獲れる駒があった場合
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
                Debug.Log("Colliderが当たってない");
            }
        }
        //後ろ方向
        _vecX = 0f;
        _vecY = 10f;
        _vecZ = 6f;
        for (int j = _search._tileRank; j > 1; j--)
        {
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(_vecX, _vecY, -_vecZ), Vector3.down, out _hit, 20))
            {
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    _search.ImmovablePieces.Add(_hit.collider.gameObject);
                    Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
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
                    Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                    //探索先に獲れる駒があった場合
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
                Debug.Log("Colliderが当たってない");
            }
        }
        //左方向
        _vecX = 6f;
        _vecY = 10f;
        _vecZ = 0f;
        for (int k = _search._tileFile; k > 1; k--)
        {
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(-_vecX, _vecY, _vecZ), Vector3.down, out _hit, 20))
            {
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    _search.ImmovablePieces.Add(_hit.collider.gameObject);
                    Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
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
                    Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                    //探索先に獲れる駒があった場合
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
                Debug.Log("Colliderが当たってない");
            }
        }
        //右方向
        _vecX = 6f;
        _vecY = 10f;
        _vecZ = 0f;
        for (int l = 0; l < 8 - _search._tileFile; l++)
        {
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(_vecX, _vecY, _vecZ), Vector3.down, out _hit, 20))
            {
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    _search.ImmovablePieces.Add(_hit.collider.gameObject);
                    Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
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
                    Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                    //探索先に獲れる駒があった場合
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
                Debug.Log("Colliderが当たってない");
            }
        }
        //移動範囲以外のマスのColliderをoffにする処理を書く
        foreach (Collider col in _search.Tile)
        {
            col.enabled = false;
            Debug.Log(col + "のColliderをoffにします");
        }
    }
}
