using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCheck : MonoBehaviour
{
    float _checkX;
    float _checkZ;
    /// <summary> �����𖞂��������������ł��邩(�T��������) </summary>
    public int[] CheckCount = new int[8];
    RaycastHit _hit;
    float _rayDist;

    void Start()
    {
        for (int i = 0; i < CheckCount.Length; i++)
        {
            CheckCount[i] = 0;
        }
    }

    /// <summary>
    /// 8������Ray���΂��A�����ł��邩�𔻒肷��
    /// </summary>
    public void Check()
    {
        //�O�㍶�E
        //�O����
        _rayDist = 7f;
        for (int i = 0; i < 3; i++)
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(0f, 2f, 0f), Vector3.forward, out _hit, _rayDist))
            {
                if (_hit.collider.gameObject.tag == gameObject.tag ||
                    _hit.collider.gameObject.GetComponent<PieceMove>().type == gameObject.GetComponent<PieceMove>().type)
                {
                    CheckCount[0]++;
                    _rayDist += 7f;
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(CheckCount[0]);
                }
            }
            else
            {
                break;
            }
        }
        //������
        _rayDist = 7f;
        for (int i = 0; i < 3; i++)
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(0f, 2f, 0f), -Vector3.forward, out _hit, _rayDist))
            {
                if (_hit.collider.gameObject.tag == gameObject.tag ||
                    _hit.collider.gameObject.GetComponent<PieceMove>().type == gameObject.GetComponent<PieceMove>().type)
                {
                    CheckCount[1]++;
                    _rayDist += 7f;
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(CheckCount[1]);
                }
            }
            else
            {
                break;
            }
        }
        //������
        _rayDist = 7f;
        for (int i = 0; i < 3; i++)
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(0f, 2f, 0f), Vector3.left, out _hit, _rayDist))
            {
                Debug.DrawRay(gameObject.transform.position + new Vector3(0f, 2f, 0f), Vector3.left, Color.yellow, _rayDist);
                if (_hit.collider.gameObject.tag == gameObject.tag ||
                    _hit.collider.gameObject.GetComponent<PieceMove>().type == gameObject.GetComponent<PieceMove>().type)
                {
                    CheckCount[2]++;
                    _rayDist += 7f;
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(CheckCount[2]);
                }
            }
            else
            {
                break;
            }
        }
        //�E����
        _rayDist = 7f;
        for (int i = 0; i < 3; i++)
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(0f, 2f, 0f), Vector3.right, out _hit, _rayDist))
            {
                if (_hit.collider.gameObject.tag == gameObject.tag ||
                    _hit.collider.gameObject.GetComponent<PieceMove>().type == gameObject.GetComponent<PieceMove>().type)
                {
                    CheckCount[3]++;
                    _rayDist += 7f;
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(CheckCount[3]);
                }
            }
            else
            {
                break;
            }
        }

        //�΂ߕ���
        _checkX = 1f;
        _checkZ = 1f;
        //���΂ߑO
        _rayDist = 9f;
        for (int i = 0; i < 3; i++)
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(0f, 2f, 0f), new Vector3(-_checkX, 0f, _checkZ), out _hit, _rayDist))
            {
                if (_hit.collider.gameObject.tag == gameObject.tag ||
                    _hit.collider.gameObject.GetComponent<PieceMove>().type == gameObject.GetComponent<PieceMove>().type)
                {
                    CheckCount[4]++;
                    _rayDist += 9f;
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(CheckCount[4]);
                }
            }
            else
            {
                break;
            }
        }
        //�E�΂ߑO
        _rayDist = 9f;
        for (int i = 0; i < 3; i++)
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(0f, 2f, 0f), new Vector3(_checkX, 0f, _checkZ), out _hit, _rayDist))
            {
                if (_hit.collider.gameObject.tag == gameObject.tag ||
                    _hit.collider.gameObject.GetComponent<PieceMove>().type == gameObject.GetComponent<PieceMove>().type)
                {
                    CheckCount[5]++;
                    _rayDist += 9f;
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(CheckCount[5]);
                }
            }
            else
            {
                break;
            }
        }
        //���΂ߌ��
        _rayDist = 9f;
        for (int i = 0; i < 3; i++)
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(0f, 2f, 0f), new Vector3(-_checkX, 0f, -_checkZ), out _hit, _rayDist))
            {
                if (_hit.collider.gameObject.tag == gameObject.tag ||
                    _hit.collider.gameObject.GetComponent<PieceMove>().type == gameObject.GetComponent<PieceMove>().type)
                {
                    CheckCount[6]++;
                    _rayDist += 9f;
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(CheckCount[6]);
                }
            }
            else
            {
                break;
            }
        }
        //�E�΂ߌ��
        _rayDist = 9f;
        for (int i = 0; i < 3; i++)
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(0f, 2f, 0f), new Vector3(_checkX, 0f, -_checkZ), out _hit, _rayDist))
            {
                if (_hit.collider.gameObject.tag == gameObject.tag ||
                    _hit.collider.gameObject.GetComponent<PieceMove>().type == gameObject.GetComponent<PieceMove>().type)
                {
                    CheckCount[7]++;
                    _rayDist += 9f;
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
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
