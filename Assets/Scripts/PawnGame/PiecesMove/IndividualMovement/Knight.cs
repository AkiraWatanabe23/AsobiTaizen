using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ナイトの移動処理(桂馬の動きを4方向にできる)
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
        _pieceInfo = _search.pieceInfo;

        //桂馬 前方向
        _vecX = 6f;
        _vecY = 10f;
        _vecZ = 12f;
        for (int i = 0; i < 2; i++)
        {
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(_vecX, _vecY, _vecZ), Vector3.down, out _hit, 30))
            {
                //探索先にいたのが味方の駒だったら
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    _search.ImmovablePieces.Add(_hit.collider.gameObject);
                    Debug.Log(_hit.collider.gameObject.name + "にはすすめません");
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
                    Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                    GetablePiece();
                }
            }
            else //探索先が盤面の範囲外だった場合
            {
                _vecX -= 12f;
                Debug.Log("Colliderが当たってない");
            }
        }
        //桂馬 後ろ方向
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
                    Debug.Log(_hit.collider.gameObject.name + "にはすすめません");
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
                    Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                    GetablePiece();
                }
            }
            else //探索先が盤面の範囲外だった場合
            {
                _vecX -= 12f;
                Debug.Log("Colliderが当たってない");
            }
        }
        //桂馬 左方向
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
                    Debug.Log(_hit.collider.gameObject.name + "にはすすめません");
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
                    Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                    GetablePiece();
                }
            }
            else //探索先が盤面の範囲外だった場合
            {
                _vecZ -= 12f;
                Debug.Log("Colliderが当たってない");
            }
        }
        //桂馬 右方向
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
                    Debug.Log(_hit.collider.gameObject.name + "にはすすめません");
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
                    Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                    GetablePiece();
                }
            }
            else //探索先が盤面の範囲外だった場合
            {
                _vecZ -= 12f;
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

    /// <summary>
    /// 駒を奪える場合の処理
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
