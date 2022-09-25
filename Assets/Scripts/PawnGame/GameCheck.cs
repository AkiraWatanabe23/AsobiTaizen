using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCheck : MonoBehaviour
{
    float _checkX;
    float _checkZ;
    /// <summary> �����𖞂��������������ł��邩(�T��������) </summary>
    public int[] _checkCount = new int[8];
    RaycastHit _hit;
    PieceManager _piece;

    void Start()
    {
        for (int i = 0; i < _checkCount.Length; i++)
        {
            _checkCount[i] = 0;
        }
        _piece = GameObject.Find("Piece").GetComponent<PieceManager>();
    }

    /// <summary>
    /// 8������Ray���΂��A�����ł��邩�𔻒肷��
    /// </summary>
    public void Check()
    {
        //�O�㍶�E
        //�O����
        _checkX = 0f;
        _checkZ = 6f;
        for (int i = 0; i < 3; i++)
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(0f, 2f, 0f), new Vector3(_checkX, 0f, _checkZ), out _hit, 10))
            {
                if (_hit.collider.gameObject.tag == gameObject.tag ||
                    _hit.collider.gameObject.GetComponent<PieceMove>()._type == gameObject.GetComponent<PieceMove>()._type)
                {
                    _checkCount[0]++;
                    _checkZ += 6f;
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_checkCount[0]);
                }
            }
            else
            {
                break;
            }
        }
        //������
        _checkX = 0f;
        _checkZ = 6f;
        for (int i = 0; i < 3; i++)
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(0f, 2f, 0f), new Vector3(_checkX, 0f, -_checkZ), out _hit, 10))
            {
                if (_hit.collider.gameObject.tag == gameObject.tag ||
                    _hit.collider.gameObject.GetComponent<PieceMove>()._type == gameObject.GetComponent<PieceMove>()._type)
                {
                    _checkCount[1]++;
                    _checkZ += 6f;
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_checkCount[1]);
                }
            }
            else
            {
                break;
            }
        }
        //������
        _checkX = 6f;
        _checkZ = 0f;
        for (int i = 0; i < 3; i++)
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(0f, 2f, 0f), new Vector3(-_checkX, 0f, _checkZ), out _hit, 10))
            {
                Debug.DrawRay(gameObject.transform.position + new Vector3(0f, 2f, 0f), new Vector3(-_checkX, 0f, _checkZ), Color.yellow, 10f);
                if (_hit.collider.gameObject.tag == gameObject.tag ||
                    _hit.collider.gameObject.GetComponent<PieceMove>()._type == gameObject.GetComponent<PieceMove>()._type)
                {
                    _checkCount[2]++;
                    _checkX += 6f;
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_checkCount[2]);
                }
            }
            else
            {
                break;
            }
        }
        //�E����
        _checkX = 6f;
        _checkZ = 0f;
        for (int i = 0; i < 3; i++)
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(0f, 2f, 0f), new Vector3(_checkX, 0f, _checkZ), out _hit, 10))
            {
                if (_hit.collider.gameObject.tag == gameObject.tag ||
                    _hit.collider.gameObject.GetComponent<PieceMove>()._type == gameObject.GetComponent<PieceMove>()._type)
                {
                    _checkCount[3]++;
                    _checkX += 6f;
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_checkCount[3]);
                }
            }
            else
            {
                break;
            }
        }

        //�΂ߕ���
        //���΂ߑO
        _checkX = 7.5f;
        _checkZ = 7.5f;
        for (int i = 0; i < 3; i++)
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(0f, 2f, 0f), new Vector3(-_checkX, 0f, _checkZ), out _hit, 10))
            {
                if (_hit.collider.gameObject.tag == gameObject.tag ||
                    _hit.collider.gameObject.GetComponent<PieceMove>()._type == gameObject.GetComponent<PieceMove>()._type)
                {
                    _checkCount[4]++;
                    _checkX += 6f;
                    _checkZ += 6f;
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_checkCount[4]);
                }
            }
            else
            {
                break;
            }
        }
        //�E�΂ߑO
        _checkX = 7.5f;
        _checkZ = 7.5f;
        for (int i = 0; i < 3; i++)
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(0f, 2f, 0f), new Vector3(_checkX, 0f, _checkZ), out _hit, 10))
            {
                if (_hit.collider.gameObject.tag == gameObject.tag ||
                    _hit.collider.gameObject.GetComponent<PieceMove>()._type == gameObject.GetComponent<PieceMove>()._type)
                {
                    _checkCount[5]++;
                    _checkX += 6f;
                    _checkZ += 6f;
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_checkCount[5]);
                }
            }
            else
            {
                break;
            }
        }
        //���΂ߌ��
        _checkX = 7.5f;
        _checkZ = 7.5f;
        for (int i = 0; i < 3; i++)
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(0f, 2f, 0f), new Vector3(-_checkX, 0f, -_checkZ), out _hit, 10))
            {
                if (_hit.collider.gameObject.tag == gameObject.tag ||
                    _hit.collider.gameObject.GetComponent<PieceMove>()._type == gameObject.GetComponent<PieceMove>()._type)
                {
                    _checkCount[6]++;
                    _checkX += 6f;
                    _checkZ += 6f;
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_checkCount[6]);
                }
            }
            else
            {
                break;
            }
        }
        //�E�΂ߌ��
        _checkX = 7.5f;
        _checkZ = 7.5f;
        for (int i = 0; i < 3; i++)
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(0f, 2f, 0f), new Vector3(_checkX, 0f, -_checkZ), out _hit, 10))
            {
                if (_hit.collider.gameObject.tag == gameObject.tag ||
                    _hit.collider.gameObject.GetComponent<PieceMove>()._type == gameObject.GetComponent<PieceMove>()._type)
                {
                    _checkCount[7]++;
                    _checkX += 6f;
                    _checkZ += 6f;
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
