using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// クイーンの移動処理
/// </summary>
public class Queen : MonoBehaviour
{
    MasuSearch _search;
    public GameObject _pieceInfo;
    RaycastHit _hit;
    float _vecX;
    float _vecY;
    float _vecZ;

    // Start is called before the first frame update
    void Start()
    {
        _search = GetComponent<MasuSearch>();
    }

    public void QueenMovement()
    {
        /*==========前後左右の動き==========*/
        //前方向
        _vecX = 0f;
        _vecY = 2.55f;
        _vecZ = 2.55f;
        for (int i = 0; i < 8 - _search._tileRank; i++)
        {
            Debug.DrawRay(_search._pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_search._pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == _search._pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _search._pieceInfo.tag)
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
            else
            {
                Debug.Log("Colliderが当たってない");
            }
        }
        //後ろ方向
        _vecX = 0f;
        _vecY = 2.55f;
        _vecZ = 2.55f;
        for (int j = _search._tileRank; j > 1; j--)
        {
            Debug.DrawRay(_search._pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_search._pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == _search._pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _search._pieceInfo.tag)
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
            else
            {
                Debug.Log("Colliderが当たってない");
            }
        }
        //左方向
        _vecX = 2.55f;
        _vecY = 2.55f;
        _vecZ = 0f;
        for (int k = _search._tileFile; k > 1; k--)
        {
            Debug.DrawRay(_search._pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_search._pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == _search._pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _search._pieceInfo.tag)
                {
                    _vecX += 2.5f;
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
            else
            {
                Debug.Log("Colliderが当たってない");
            }
        }
        //右方向
        _vecX = 2.55f;
        _vecY = 2.55f;
        _vecZ = 0f;
        for (int l = 0; l < 8 - _search._tileFile; l++)
        {
            Debug.DrawRay(_search._pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_search._pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == _search._pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _search._pieceInfo.tag)
                {
                    _vecX += 2.5f;
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
            else
            {
                Debug.Log("Colliderが当たってない");
            }
        }
        /*==========斜め方向の動き==========*/
        //左斜め前方向
        _vecX = 2.55f;
        _vecY = 2.55f;
        _vecZ = 2.55f;
        for (int i = 0; i < 8 - _search._tileRank; i++)
        {
            Debug.DrawRay(_search._pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_search._pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == _search._pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _search._pieceInfo.tag)
                {
                    _vecX += 2.5f;
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
            Debug.DrawRay(_search._pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_search._pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == _search._pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _search._pieceInfo.tag)
                {
                    _vecX += 2.5f;
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
            Debug.DrawRay(_search._pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, -_vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_search._pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, -_vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == _search._pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _search._pieceInfo.tag)
                {
                    _vecX += 2.5f;
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
            Debug.DrawRay(_search._pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_search._pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == _search._pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _search._pieceInfo.tag)
                {
                    _vecX += 2.5f;
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
