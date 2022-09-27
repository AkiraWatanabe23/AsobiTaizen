using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ポーンの移動処理(少々複雑な動きです)
/// </summary>
public class Pawn : MonoBehaviour
{
    MasuSearch _search;
    PieceManager _piece;
    [Tooltip("この駒獲れます"), SerializeField] Material _getable;
    [Tooltip("移動させるポーン")] GameObject _pieceInfo;
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

    public void PawnMovement()
    {
        _pieceInfo = _search.pieceInfo;

        /********************白駒の移動処理********************/
        if (_pieceInfo.tag == "WhitePiece")
        {
            //1,1回目の動き...2マス移動可
            if (_pieceInfo.GetComponent<PieceMove>().moveCount == 0)
            {
                _vecX = 0f;
                _vecY = 3f;
                _vecZ = 6f;
                for (int i = 0; i < 2; i++)
                {
                    if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.5f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
                    {
                        if (_hit.collider.gameObject.tag == _pieceInfo.tag || _hit.collider.gameObject.tag == "BlackPiece")
                        {
                            _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                            _search.ImmovablePieces.Add(_hit.collider.gameObject);
                            Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                            break;
                        }
                        else if (_hit.collider.gameObject.tag == "Tile")
                        {
                            _vecZ += 5f;
                            //探索中に同じ要素が何度もListに入るのを防ぐ
                            if (!_search.MovableTile.Contains(_hit.collider))
                            {
                                _search.MovableTile.Add(_hit.collider);
                                _hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
                            }
                            _search.Tile.Remove(_hit.collider);
                            Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                            if (_hit.collider.gameObject.tag != "Tile")
                            {
                                break;
                            }
                        }
                    }
                }
                //移動範囲以外のマスのColliderをoffにする処理を書く
                foreach (Collider col in _search.Tile)
                {
                    col.enabled = false;
                }
            }
            //2,2回目以降は1マス移動
            else if (_pieceInfo.GetComponent<PieceMove>().moveCount != 0)
            {
                _vecX = 0f;
                _vecY = 3f;
                _vecZ = 6f;
                if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.5f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
                {
                    if (_hit.collider.gameObject.tag == _pieceInfo.tag || _hit.collider.gameObject.tag == "BlackPiece")
                    {
                        _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                        _search.ImmovablePieces.Add(_hit.collider.gameObject);
                        Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                    }
                    else if (_hit.collider.gameObject.tag == "Tile")
                    {
                        //探索中に同じ要素が何度もListに入るのを防ぐ
                        if (!_search.MovableTile.Contains(_hit.collider))
                        {
                            _search.MovableTile.Add(_hit.collider);
                            _hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
                        }
                        _search.Tile.Remove(_hit.collider);
                        Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                        if (_hit.collider.gameObject.tag != "Tile")
                        {
                            _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                        }
                    }
                }
                //移動範囲以外のマスのColliderをoffにする処理を書く
                foreach (Collider col in _search.Tile)
                {
                    col.enabled = false;
                }
            }

            //常に斜め前の探索
            _vecX = 6f;
            _vecY = 3f;
            _vecZ = 6f;
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.5f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == "BlackPiece")
                {
                    if (!_piece.GetablePieces.Contains(_hit.collider.gameObject))
                    {
                        _piece.GetablePieces.Add(_hit.collider.gameObject);
                        //奪える駒のMaterialを切り替える
                        _hit.collider.gameObject.GetComponent<MeshRenderer>().material = _getable;
                    }
                    _piece.BlackPieces.Remove(_hit.collider.gameObject);
                    Debug.Log(_hit.collider.gameObject.name + "をとることが出来ます");
                }
            }
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.5f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == "BlackPiece")
                {
                    if (!_piece.GetablePieces.Contains(_hit.collider.gameObject))
                    {
                        _piece.GetablePieces.Add(_hit.collider.gameObject);
                        //奪える駒のMaterialを切り替える
                        _hit.collider.gameObject.GetComponent<MeshRenderer>().material = _getable;
                    }
                    _piece.BlackPieces.Remove(_hit.collider.gameObject);
                    Debug.Log(_hit.collider.gameObject.name + "をとることが出来ます");
                }
            }
        }
        /********************黒駒の移動処理********************/
        else if (_pieceInfo.tag == "BlackPiece")
        {
            //1,1回目の動き...2マス移動可
            if (_pieceInfo.GetComponent<PieceMove>().moveCount == 0)
            {
                _vecX = 0f;
                _vecY = 3f;
                _vecZ = 6f;
                for (int i = 0; i < 2; i++)
                {
                    if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.5f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), out _hit, 100))
                    {
                        if (_hit.collider.gameObject.tag == _pieceInfo.tag || _hit.collider.gameObject.tag == "WhitePiece")
                        {
                            _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                            _search.ImmovablePieces.Add(_hit.collider.gameObject);
                            Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                            break;
                        }
                        else if (_hit.collider.gameObject.tag == "Tile")
                        {
                            _vecZ += 5f;
                            //探索中に同じ要素が何度もListに入るのを防ぐ
                            if (!_search.MovableTile.Contains(_hit.collider))
                            {
                                _search.MovableTile.Add(_hit.collider);
                                _hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
                            }
                            _search.Tile.Remove(_hit.collider);
                            Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                            if (_hit.collider.gameObject.tag != "Tile")
                            {
                                break;
                            }
                        }
                    }
                }
                //移動範囲以外のマスのColliderをoffにする処理を書く
                foreach (Collider col in _search.Tile)
                {
                    col.enabled = false;
                }
            }
            //2,2回目以降は1マス移動
            else if (_pieceInfo.GetComponent<PieceMove>().moveCount != 0)
            {
                _vecX = 0f;
                _vecY = 3f;
                _vecZ = 6f;
                if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.5f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), out _hit, 100))
                {
                    if (_hit.collider.gameObject.tag == _pieceInfo.tag || _hit.collider.gameObject.tag == "WhitePiece")
                    {
                        _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                        _search.ImmovablePieces.Add(_hit.collider.gameObject);
                        Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                    }
                    else if (_hit.collider.gameObject.tag == "Tile")
                    {
                        //探索中に同じ要素が何度もListに入るのを防ぐ
                        if (!_search.MovableTile.Contains(_hit.collider))
                        {
                            _search.MovableTile.Add(_hit.collider);
                            _hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
                        }
                        _search.Tile.Remove(_hit.collider);
                        Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                        if (_hit.collider.gameObject.tag != "Tile")
                        {
                            _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                        }
                    }
                }
                //移動範囲以外のマスのColliderをoffにする処理を書く
                foreach (Collider col in _search.Tile)
                {
                    col.enabled = false;
                }
            }
            //常に斜め前の探索
            _vecX = 6f;
            _vecY = 3f;
            _vecZ = 6f;
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.5f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == "WhitePiece")
                {
                    if (!_piece.GetablePieces.Contains(_hit.collider.gameObject))
                    {
                        _piece.GetablePieces.Add(_hit.collider.gameObject);
                        //奪える駒のMaterialを切り替える
                        _hit.collider.gameObject.GetComponent<MeshRenderer>().material = _getable;
                    }
                    _piece.WhitePieces.Remove(_hit.collider.gameObject);
                    Debug.Log(_hit.collider.gameObject.name + "をとることが出来ます");
                }
            }
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.5f, 0f), new Vector3(-_vecX, -_vecY, -_vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == "WhitePiece")
                {
                    if (!_piece.GetablePieces.Contains(_hit.collider.gameObject))
                    {
                        _piece.GetablePieces.Add(_hit.collider.gameObject);
                        //奪える駒のMaterialを切り替える
                        _hit.collider.gameObject.GetComponent<MeshRenderer>().material = _getable;
                    }
                    _piece.WhitePieces.Remove(_hit.collider.gameObject);
                    Debug.Log(_hit.collider.gameObject.name + "をとることが出来ます");
                }
            }
        }
    }
}
