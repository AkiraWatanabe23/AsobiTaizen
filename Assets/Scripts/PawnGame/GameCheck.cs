using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCheck : MonoBehaviour
{
    float _checkX;
    float _checkZ;
    int[] _checkCount = new int[8];
    RaycastHit _hit;
    /// <summary> 条件を満たした駒がいくつ並んでいるか(探索方向毎) </summary>
    public int[] CheckCount { get => _checkCount; set => _checkCount = value; }

    void Start()
    {
        for (int i = 0; i < CheckCount.Length; i++)
        {
            CheckCount[i] = 0;
        }
    }

    /// <summary>
    /// 条件に沿って駒が並んでいるかを判定する
    /// </summary>
    public void Check()
    {
        //前後左右
        //前方向
        _checkX = 0f;
        _checkZ = 6f;
        for (int i = 0; i < 3; i++)
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(_checkX, 10f, _checkZ), Vector3.down, out _hit, 20))
            {
                if (_hit.collider.gameObject.tag == "Tile")
                {
                    break;
                }
                else if (_hit.collider.gameObject.tag == gameObject.tag ||
                    _hit.collider.gameObject.GetComponent<PieceMove>().Type == gameObject.GetComponent<PieceMove>().Type)
                {
                    CheckCount[0]++;
                    _checkZ += 6f;
                    Debug.Log(CheckCount[0]);
                }
            }
            else
            {
                break;
            }
        }
        //後ろ方向
        _checkX = 0f;
        _checkZ = 6f;
        for (int i = 0; i < 3; i++)
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(_checkX, 10f, -_checkZ), Vector3.down, out _hit, 20))
            {
                if (_hit.collider.gameObject.tag == "Tile")
                {
                    break;
                }
                else if (_hit.collider.gameObject.tag == gameObject.tag ||
                    _hit.collider.gameObject.GetComponent<PieceMove>().Type == gameObject.GetComponent<PieceMove>().Type)
                {
                    CheckCount[1]++;
                    _checkZ += 6f;
                    Debug.Log(CheckCount[1]);
                }
            }
            else
            {
                break;
            }
        }
        //左方向
        _checkX = 6f;
        _checkZ = 0f;
        for (int i = 0; i < 3; i++)
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(-_checkX, 10f, _checkZ), Vector3.down, out _hit, 20))
            {
                if (_hit.collider.gameObject.tag == "Tile")
                {
                    break;
                }
                else if (_hit.collider.gameObject.tag == gameObject.tag ||
                    _hit.collider.gameObject.GetComponent<PieceMove>().Type == gameObject.GetComponent<PieceMove>().Type)
                {
                    CheckCount[2]++;
                    _checkX += 6f;
                    Debug.Log(CheckCount[2]);
                }
            }
            else
            {
                break;
            }
        }
        //右方向
        _checkX = 6f;
        _checkZ = 0f;
        for (int i = 0; i < 3; i++)
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(_checkX, 10f, _checkZ), Vector3.down, out _hit, 20))
            {
                if (_hit.collider.gameObject.tag == "Tile")
                {
                    break;
                }
                else if (_hit.collider.gameObject.tag == gameObject.tag ||
                    _hit.collider.gameObject.GetComponent<PieceMove>().Type == gameObject.GetComponent<PieceMove>().Type)
                {
                    CheckCount[3]++;
                    _checkX += 6f;
                    Debug.Log(CheckCount[3]);
                }
            }
            else
            {
                break;
            }
        }

        //斜め方向
        //左斜め前
        _checkX = 6f;
        _checkZ = 6f;
        for (int i = 0; i < 3; i++)
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(-_checkX, 10f, _checkZ), Vector3.down, out _hit, 20))
            {
                if (_hit.collider.gameObject.tag == "Tile")
                {
                    break;
                }
                else if (_hit.collider.gameObject.tag == gameObject.tag ||
                    _hit.collider.gameObject.GetComponent<PieceMove>().Type == gameObject.GetComponent<PieceMove>().Type)
                {
                    CheckCount[4]++;
                    _checkX += 6f;
                    _checkZ += 6f;
                    Debug.Log(CheckCount[4]);
                }
            }
            else
            {
                break;
            }
        }
        //右斜め前
        _checkX = 6f;
        _checkZ = 6f;
        for (int i = 0; i < 3; i++)
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(_checkX, 10f, _checkZ), Vector3.down, out _hit, 20))
            {
                if (_hit.collider.gameObject.tag == "Tile")
                {
                    break;
                }
                else if (_hit.collider.gameObject.tag == gameObject.tag ||
                    _hit.collider.gameObject.GetComponent<PieceMove>().Type == gameObject.GetComponent<PieceMove>().Type)
                {
                    CheckCount[5]++;
                    _checkX += 6f;
                    _checkZ += 6f;
                    Debug.Log(CheckCount[5]);
                }
            }
            else
            {
                break;
            }
        }
        //左斜め後ろ
        _checkX = 6f;
        _checkZ = 6f;
        for (int i = 0; i < 3; i++)
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(-_checkX, 10f, -_checkZ), Vector3.down, out _hit, 20))
            {
                if (_hit.collider.gameObject.tag == "Tile")
                {
                    break;
                }
                else if (_hit.collider.gameObject.tag == gameObject.tag ||
                    _hit.collider.gameObject.GetComponent<PieceMove>().Type == gameObject.GetComponent<PieceMove>().Type)
                {
                    CheckCount[6]++;
                    _checkX += 6f;
                    _checkZ += 6f;
                    Debug.Log(CheckCount[6]);
                }
            }
            else
            {
                break;
            }
        }
        //右斜め後ろ
        _checkX = 6f;
        _checkZ = 6f;
        for (int i = 0; i < 3; i++)
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(_checkX, 10f, -_checkZ), Vector3.down, out _hit, 20))
            {
                if (_hit.collider.gameObject.tag == "Tile")
                {
                    break;
                }
                else if (_hit.collider.gameObject.tag == gameObject.tag ||
                    _hit.collider.gameObject.GetComponent<PieceMove>().Type == gameObject.GetComponent<PieceMove>().Type)
                {
                    CheckCount[7]++;
                    _checkX += 6f;
                    _checkZ += 6f;
                    Debug.Log(CheckCount[7]);
                }
            }
            else
            {
                break;
            }
        }
    }
}
