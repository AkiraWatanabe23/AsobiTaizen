using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCheck : MonoBehaviour
{
    float _checkX;
    float _checkZ;
    /// <summary> 条件を満たした駒がいくつ並んでいるか(探索方向毎) </summary>
    public int[] _checkCount = new int[8];
    RaycastHit _hit;
    int _rayDist;

    void Start()
    {
        for (int i = 0; i < _checkCount.Length; i++)
        {
            _checkCount[i] = 0;
        }
    }

    /// <summary>
    /// 8方向にRayを飛ばし、駒が並んでいるかを判定する
    /// </summary>
    public void Check()
    {
        //前後左右
        //前方向
        _rayDist = 6;
        for (int i = 0; i < 3; i++)
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(0f, 2f, 0f), Vector3.forward, out _hit, _rayDist))
            {
                if (_hit.collider.gameObject.tag == gameObject.tag ||
                    _hit.collider.gameObject.GetComponent<PieceMove>()._type == gameObject.GetComponent<PieceMove>()._type)
                {
                    _checkCount[0]++;
                    _rayDist += 6;
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_checkCount[0]);
                }
            }
            else
            {
                break;
            }
        }
        //後ろ方向
        _rayDist = 6;
        for (int i = 0; i < 3; i++)
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(0f, 2f, 0f), -Vector3.forward, out _hit, _rayDist))
            {
                if (_hit.collider.gameObject.tag == gameObject.tag ||
                    _hit.collider.gameObject.GetComponent<PieceMove>()._type == gameObject.GetComponent<PieceMove>()._type)
                {
                    _checkCount[1]++;
                    _rayDist += 6;
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_checkCount[1]);
                }
            }
            else
            {
                break;
            }
        }
        //左方向
        _rayDist = 6;
        for (int i = 0; i < 3; i++)
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(0f, 2f, 0f), Vector3.left, out _hit, _rayDist))
            {
                Debug.DrawRay(gameObject.transform.position + new Vector3(0f, 2f, 0f), Vector3.left, Color.yellow, _rayDist);
                if (_hit.collider.gameObject.tag == gameObject.tag ||
                    _hit.collider.gameObject.GetComponent<PieceMove>()._type == gameObject.GetComponent<PieceMove>()._type)
                {
                    _checkCount[2]++;
                    _rayDist += 6;
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_checkCount[2]);
                }
            }
            else
            {
                break;
            }
        }
        //右方向
        _rayDist = 6;
        for (int i = 0; i < 3; i++)
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(0f, 2f, 0f), Vector3.right, out _hit, _rayDist))
            {
                if (_hit.collider.gameObject.tag == gameObject.tag ||
                    _hit.collider.gameObject.GetComponent<PieceMove>()._type == gameObject.GetComponent<PieceMove>()._type)
                {
                    _checkCount[3]++;
                    _rayDist += 6;
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_checkCount[3]);
                }
            }
            else
            {
                break;
            }
        }

        //斜め方向
        _checkX = 1f;
        _checkZ = 1f;
        //左斜め前
        _rayDist = 6;
        for (int i = 0; i < 3; i++)
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(0f, 2f, 0f), new Vector3(-_checkX, 0f, _checkZ), out _hit, _rayDist))
            {
                if (_hit.collider.gameObject.tag == gameObject.tag ||
                    _hit.collider.gameObject.GetComponent<PieceMove>()._type == gameObject.GetComponent<PieceMove>()._type)
                {
                    _checkCount[4]++;
                    _rayDist += 6;
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_checkCount[4]);
                }
            }
            else
            {
                break;
            }
        }
        //右斜め前
        _rayDist = 6;
        for (int i = 0; i < 3; i++)
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(0f, 2f, 0f), new Vector3(_checkX, 0f, _checkZ), out _hit, _rayDist))
            {
                if (_hit.collider.gameObject.tag == gameObject.tag ||
                    _hit.collider.gameObject.GetComponent<PieceMove>()._type == gameObject.GetComponent<PieceMove>()._type)
                {
                    _checkCount[5]++;
                    _rayDist += 6;
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_checkCount[5]);
                }
            }
            else
            {
                break;
            }
        }
        //左斜め後ろ
        _rayDist = 6;
        for (int i = 0; i < 3; i++)
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(0f, 2f, 0f), new Vector3(-_checkX, 0f, -_checkZ), out _hit, _rayDist))
            {
                if (_hit.collider.gameObject.tag == gameObject.tag ||
                    _hit.collider.gameObject.GetComponent<PieceMove>()._type == gameObject.GetComponent<PieceMove>()._type)
                {
                    _checkCount[6]++;
                    _rayDist += 6;
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_checkCount[6]);
                }
            }
            else
            {
                break;
            }
        }
        //右斜め後ろ
        _rayDist = 6;
        for (int i = 0; i < 3; i++)
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(0f, 2f, 0f), new Vector3(_checkX, 0f, -_checkZ), out _hit, _rayDist))
            {
                if (_hit.collider.gameObject.tag == gameObject.tag ||
                    _hit.collider.gameObject.GetComponent<PieceMove>()._type == gameObject.GetComponent<PieceMove>()._type)
                {
                    _checkCount[7]++;
                    _rayDist += 6;
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_checkCount[7]);
                }
            }
            else
            {
                break;
            }
        }
    }
}
