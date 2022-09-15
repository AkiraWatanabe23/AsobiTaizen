using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ナイトの移動処理
/// </summary>
public class Knight : MonoBehaviour
{
    MasuSearch _search;
    PieceManager _piece;
    [Tooltip("この駒獲れます"), SerializeField] Material _getable;
    [Tooltip("移動させるナイト")] public GameObject _pieceInfo;
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

        //桂馬 前方向
        _vecX = 2.55f;
        _vecY = 2.55f;
        _vecZ = 5f;
        for (int i = 0; i < 2; i++)
        {
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                //探索先にいたのが味方の駒だったら
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    _search._immovablePieces.Add(_hit.collider.gameObject);
                    Debug.Log(_hit.collider.gameObject.name + "にはすすめません");
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
                    Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
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
            else //探索先が盤面の範囲外だった場合
            {
                _vecX -= 5f;
                Debug.Log("Colliderが当たってない");
            }
        }
        //桂馬 後ろ方向
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
                    Debug.Log(_hit.collider.gameObject.name + "にはすすめません");
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
                    Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
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
            else //探索先が盤面の範囲外だった場合
            {
                _vecX -= 5f;
                Debug.Log("Colliderが当たってない");
            }
        }
        //桂馬 左方向
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
                    Debug.Log(_hit.collider.gameObject.name + "にはすすめません");
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
                    Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
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
            else //探索先が盤面の範囲外だった場合
            {
                _vecZ -= 5f;
                Debug.Log("Colliderが当たってない");
            }
        }
        //桂馬 右方向
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
                    Debug.Log(_hit.collider.gameObject.name + "にはすすめません");
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
                    Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
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
            else //探索先が盤面の範囲外だった場合
            {
                _vecZ -= 5f;
                Debug.Log("Colliderが当たってない");
            }
        }
        //移動範囲以外のマスのColliderをoffにする処理を書く
        foreach (Collider col in _search._tile)
        {
            col.enabled = false;
            Debug.Log(col + "のColliderをoffにします");
        }
    }
}
