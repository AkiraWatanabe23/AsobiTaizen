using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCheck : MonoBehaviour
{
    float _checkX;
    float _checkZ;
    int[] _checkCount = new int[8];
    RaycastHit _hit;
    /// <summary> �����𖞂��������������ł��邩(�T��������) </summary>
    public int[] CheckCount { get => _checkCount; set => _checkCount = value; }

    void Start()
    {
        for (int i = 0; i < CheckCount.Length; i++)
        {
            CheckCount[i] = 0;
        }
    }

    /// <summary>
    /// �����ɉ����ċ����ł��邩�𔻒肷��
    /// </summary>
    public void Check()
    {
        //�O�㍶�E
        //�O����
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
        //������
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
        //������
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
        //�E����
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

        //�΂ߕ���
        //���΂ߑO
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
        //�E�΂ߑO
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
        //���΂ߌ��
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
        //�E�΂ߌ��
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
