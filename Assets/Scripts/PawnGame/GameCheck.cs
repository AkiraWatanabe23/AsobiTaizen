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

    void Start()
    {
        for (int i = 0; i < _checkCount.Length; i++)
        {
            _checkCount[i] = 0;
        }
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
        for (int i = 0; i < 4; i++)
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(0f, 4f, 0f), new Vector3(_checkX, 0f, _checkZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == gameObject.tag ||
                    _hit.collider.gameObject.GetComponent<PieceMove>()._type == gameObject.GetComponent<PieceMove>()._type)
                {
                    _checkCount[0]++;
                    _checkZ += 6f;
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
        for (int i = 0; i < 4; i++)
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(0f, 4f, 0f), new Vector3(_checkX, 0f, -_checkZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == gameObject.tag ||
                    _hit.collider.gameObject.GetComponent<PieceMove>()._type == gameObject.GetComponent<PieceMove>()._type)
                {
                    _checkCount[1]++;
                    _checkZ += 6f;
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
        for (int i = 0; i < 4; i++)
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(0f, 4f, 0f), new Vector3(-_checkX, 0f, _checkZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == gameObject.tag ||
                    _hit.collider.gameObject.GetComponent<PieceMove>()._type == gameObject.GetComponent<PieceMove>()._type)
                {
                    _checkCount[2]++;
                    _checkX += 6f;
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
        for (int i = 0; i < 4; i++)
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(0f, 4f, 0f), new Vector3(_checkX, 0f, _checkZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == gameObject.tag ||
                    _hit.collider.gameObject.GetComponent<PieceMove>()._type == gameObject.GetComponent<PieceMove>()._type)
                {
                    _checkCount[3]++;
                    _checkX += 6f;
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
        for (int i = 0; i < 4; i++)
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(0f, 4f, 0f), new Vector3(-_checkX, 0f, _checkZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == gameObject.tag ||
                    _hit.collider.gameObject.GetComponent<PieceMove>()._type == gameObject.GetComponent<PieceMove>()._type)
                {
                    _checkCount[4]++;
                    _checkX += 6f;
                    _checkZ += 6f;
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
        for (int i = 0; i < 4; i++)
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(0f, 4f, 0f), new Vector3(_checkX, 0f, _checkZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == gameObject.tag ||
                    _hit.collider.gameObject.GetComponent<PieceMove>()._type == gameObject.GetComponent<PieceMove>()._type)
                {
                    _checkCount[5]++;
                    _checkX += 6f;
                    _checkZ += 6f;
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
        for (int i = 0; i < 4; i++)
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(0f, 4f, 0f), new Vector3(-_checkX, 0f, -_checkZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == gameObject.tag ||
                    _hit.collider.gameObject.GetComponent<PieceMove>()._type == gameObject.GetComponent<PieceMove>()._type)
                {
                    _checkCount[6]++;
                    _checkX += 6f;
                    _checkZ += 6f;
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
        for (int i = 0; i < 4; i++)
        {
            if (Physics.Raycast(gameObject.transform.position + new Vector3(0f, 4f, 0f), new Vector3(_checkX, 0f, -_checkZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == gameObject.tag ||
                    _hit.collider.gameObject.GetComponent<PieceMove>()._type == gameObject.GetComponent<PieceMove>()._type)
                {
                    _checkCount[7]++;
                    _checkX += 6f;
                    _checkZ += 6f;
                }
            }
            else
            {
                break;
            }
        }
    }
}
