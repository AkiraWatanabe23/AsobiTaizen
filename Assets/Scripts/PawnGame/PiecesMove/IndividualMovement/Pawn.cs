using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ポーンの移動処理
/// </summary>
public class Pawn : MonoBehaviour
{
    MasuSearch _search;
    [Tooltip("移動させるポーン")] public GameObject _pieceInfo;
    public RaycastHit _hit;
    float _vecX;
    float _vecY;
    float _vecZ;

    // Start is called before the first frame update
    void Start()
    {
        _search = GameObject.Find("Board,Tile").GetComponent<MasuSearch>();
    }

    public void PawnMovement()
    {
        _pieceInfo = _search._pieceInfo;

        /********************白駒の移動処理********************/
        if (_pieceInfo.tag == "WhitePiece")
        {
            //1,1回目の動き...2マス移動可
            if (_pieceInfo.GetComponent<PieceMove>()._moveCount == 0)
            {
                _vecX = 0f;
                _vecY = 2.55f;
                _vecZ = 2.55f;
                for (int i = 0; i < 2; i++)
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
                            _vecZ += 2.5f;
                            if (_search._movableTile.Contains(_hit.collider))
                            {
                                break;
                            }
                            else
                            {
                                _search._movableTile.Add(_hit.collider);
                            }
                            _search._tile.Remove(_hit.collider);
                            Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                            if (_hit.collider.gameObject.tag == "BlackPiece")
                            {
                                break;
                            }
                        }
                    }
                }
                //移動範囲以外のColliderをoffにする処理を書く
                foreach (Collider col in _search._tile)
                {
                    col.enabled = false;
                }
            }
            //2,2回目以降は1マス移動
            else if (_pieceInfo.GetComponent<PieceMove>()._moveCount != 0)
            {
                _vecX = 0f;
                _vecY = 2.55f;
                _vecZ = 2.55f;
                if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
                {
                    if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                    {
                        _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                        _search._immovablePieces.Add(_hit.collider.gameObject);
                        Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                    }
                    else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                    {
                        if (!_search._movableTile.Contains(_hit.collider))
                        {
                            _search._movableTile.Add(_hit.collider);
                        }
                        _search._tile.Remove(_hit.collider);
                        Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                        if (_hit.collider.gameObject.tag == "BlackPiece")
                        {

                        }
                    }
                }
                //移動範囲以外のColliderをoffにする処理を書く
                foreach (Collider col in _search._tile)
                {
                    col.enabled = false;
                }
            }

            //常に斜め前の探索
            _vecX = 2.55f;
            _vecY = 2.55f;
            _vecZ = 2.55f;
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == "BlackPiece")
                {
                    Debug.Log(_hit.collider.gameObject.name + "をとることが出来ます");
                }
            }
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), Color.yellow, 10f);
                if (_hit.collider.gameObject.tag == "BlackPiece")
                {
                    Debug.Log(_hit.collider.gameObject.name + "をとることが出来ます");
                }
            }
            //En_Passant_White();
        }
        /********************黒駒の移動処理********************/
        else if (_pieceInfo.tag == "BlackPiece")
        {
            //1,1回目の動き...2マス移動可
            if (_pieceInfo.GetComponent<PieceMove>()._moveCount == 0)
            {
                _vecX = 0f;
                _vecY = 2.55f;
                _vecZ = 2.55f;
                for (int i = 0; i < 2; i++)
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
                            _vecZ += 2.5f;
                            if (_search._movableTile.Contains(_hit.collider))
                            {
                                break;
                            }
                            else
                            {
                                _search._movableTile.Add(_hit.collider);
                            }
                            _search._tile.Remove(_hit.collider);
                            Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                            if (_hit.collider.gameObject.tag == "WhitePiece")
                            {
                                break;
                            }
                        }
                    }
                }
                //移動範囲以外のColliderをoffにする処理を書く
                foreach (Collider col in _search._tile)
                {
                    col.enabled = false;
                }
            }
            //2,2回目以降は1マス移動
            else if (_pieceInfo.GetComponent<PieceMove>()._moveCount != 0)
            {
                _vecX = 0f;
                _vecY = 2.55f;
                _vecZ = 2.55f;
                if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), out _hit, 100))
                {
                    if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                    {
                        _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                        _search._immovablePieces.Add(_hit.collider.gameObject);
                        Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                    }
                    else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                    {
                        if (!_search._movableTile.Contains(_hit.collider))
                        {
                            _search._movableTile.Add(_hit.collider);
                        }
                        _search._tile.Remove(_hit.collider);
                        Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                        if (_hit.collider.gameObject.tag == "WhitePiece")
                        {

                        }
                    }
                }
                //移動範囲以外のColliderをoffにする処理を書く
                foreach (Collider col in _search._tile)
                {
                    col.enabled = false;
                }
            }
            //常に斜め前の探索
            _vecX = 2.55f;
            _vecY = 2.55f;
            _vecZ = 2.55f;
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == "WhitePiece")
                {
                    Debug.Log(_hit.collider.gameObject.name + "をとることが出来ます");
                }
            }
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, -_vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == "WhitePiece")
                {
                    Debug.Log(_hit.collider.gameObject.name + "をとることが出来ます");
                }
            }
            //En_Passant_Black();
        }
    }

    /// <summary>
    /// アンパッサン処理(白)
    /// </summary>
    //void En_Passant_White()
    //{
    //    if (_search._tileRank == 5)
    //    {
    //        _vecX = 2.55f;
    //        _vecY = 0f;
    //        _vecZ = 0f;
    //        if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, _vecY, _vecZ), out _hit, 10))
    //        {
    //            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, _vecY, _vecZ), Color.yellow, 10f);
    //            if (_hit.collider.gameObject.GetComponent<PieceMove>()._type == PieceMove.PieceType.Pawn)
    //            {
    //                if (_hit.collider.gameObject.GetComponent<PieceMove>()._moveCount == 1)
    //                {
    //                    _vecX = 2.55f;
    //                    _vecY = 2.55f;
    //                    _vecZ = 2.55f;
    //                    if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
    //                    {
    //                        if (_hit.collider.gameObject.tag == "Tile")
    //                        {
    //                            Debug.Log("アンパッサンが可能です");
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //        else if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, _vecY, _vecZ), out _hit, 10))
    //        {
    //            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, _vecY, _vecZ), Color.yellow, 10f);
    //            if (_hit.collider.gameObject.GetComponent<PieceMove>()._type == PieceMove.PieceType.Pawn)
    //            {
    //                if (_hit.collider.gameObject.GetComponent<PieceMove>()._moveCount == 1)
    //                {
    //                    if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), out _hit, 100))
    //                    {
    //                        if (_hit.collider.gameObject.tag == "Tile")
    //                        {
    //                            Debug.Log("アンパッサンが可能です");
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}

    /// <summary>
    /// アンパッサン処理(黒)
    /// </summary>
    //void En_Passant_Black()
    //{
    //    if (_search._tileRank == 4)
    //    {
    //        _vecX = 2.55f;
    //        _vecY = 0f;
    //        _vecZ = 0f;
    //        if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, _vecY, _vecZ), out _hit, 10))
    //        {
    //            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, _vecY, _vecZ), Color.yellow, 10f);
    //            if (_hit.collider.gameObject.GetComponent<PieceMove>()._type == PieceMove.PieceType.Pawn)
    //            {
    //                if (_hit.collider.gameObject.GetComponent<PieceMove>()._moveCount == 1)
    //                {
    //                    _vecX = 2.55f;
    //                    _vecY = 2.55f;
    //                    _vecZ = 2.55f;
    //                    if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), out _hit, 100))
    //                    {
    //                        if (_hit.collider.gameObject.tag == "Tile")
    //                        {
    //                            Debug.Log("アンパッサンが可能です");
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //        else if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, _vecY, _vecZ), out _hit, 10))
    //        {
    //            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, _vecY, _vecZ), Color.yellow, 10f);
    //            if (_hit.collider.gameObject.GetComponent<PieceMove>()._type == PieceMove.PieceType.Pawn)
    //            {
    //                if (_hit.collider.gameObject.GetComponent<PieceMove>()._moveCount == 1)
    //                {
    //                    if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, -_vecZ), out _hit, 100))
    //                    {
    //                        if (_hit.collider.gameObject.tag == "Tile")
    //                        {
    //                            Debug.Log("アンパッサンが可能です");
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}
}
