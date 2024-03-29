using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キングの移動処理(全方向に1マスできる)
/// </summary>
public class King : MonoBehaviour
{
    MasuSearch _search;
    PieceManager _piece;
    [Tooltip("この駒獲れます"), SerializeField] Material _getable;
    [Tooltip("移動させるキング")] GameObject _pieceInfo;
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

    public void KingMovement()
    {
        _pieceInfo = _search.PieceInfo;

        //////////前後左右の動き//////////
        //前後方向
        _vecX = 0f;
        _vecY = 15f;
        _vecZ = 6f;
        if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(_vecX, _vecY, _vecZ), Vector3.down, out _hit, 20))
        {
            MovableCheck();
        }
        else
        {
            Debug.Log("none");
        }
        if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(_vecX, _vecY, -_vecZ), Vector3.down, out _hit, 20))
        {
            MovableCheck();
        }
        else
        {
            Debug.Log("none");
        }

        //左右方向
        _vecX = 6f;
        _vecY = 15f;
        _vecZ = 0f;
        if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(-_vecX, _vecY, _vecZ), Vector3.down, out _hit, 20))
        {
            MovableCheck();
        }
        else
        {
            Debug.Log("none");
        }
        if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(_vecX, _vecY, _vecZ), Vector3.down, out _hit, 20))
        {
            MovableCheck();
        }
        else
        {
            Debug.Log("none");
        }

        //////////斜め方向の動き//////////
        //斜め前2方向
        _vecX = 6f;
        _vecY = 15f;
        _vecZ = 6f;
        if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(-_vecX, _vecY, _vecZ), Vector3.down, out _hit, 20))
        {
            MovableCheck();
        }
        else
        {
            Debug.Log("none");
        }
        if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(_vecX, _vecY, _vecZ), Vector3.down, out _hit, 20))
        {
            MovableCheck();
        }
        else
        {
            Debug.Log("none");
        }

        //斜め後ろ2方向
        if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(-_vecX, _vecY, -_vecZ), Vector3.down, out _hit, 20))
        {
            MovableCheck();
        }
        else
        {
            Debug.Log("none");
        }
        if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(_vecX, _vecY, -_vecZ), Vector3.down, out _hit, 20))
        {
            MovableCheck();
        }
        else
        {
            Debug.Log("none");
        }
        //移動範囲以外のマスのColliderをoffにする処理を書く
        foreach (Collider col in _search.Tile)
        {
            col.enabled = false;
            Debug.Log(col + "のColliderをoffにします");
        }
    }

    private void MovableCheck()
    {
        //探索先に味方の駒があった場合
        if (_hit.collider.gameObject.tag == _pieceInfo.tag)
        {
            _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
            _search.ImmovablePieces.Add(_hit.collider.gameObject);
            Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
        }
        //探索先が味方じゃなかった(敵駒orマス)場合
        else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
        {
            if (!_search.MovableTile.Contains(_hit.collider))
            {
                _search.MovableTile.Add(_hit.collider);
                _hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
            }
            _search.Tile.Remove(_hit.collider);
            Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
            /**************************************************/
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
            /**************************************************/
        }
    }
}
