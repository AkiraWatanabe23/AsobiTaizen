using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �N�C�[���̈ړ�����
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
        /*==========�O�㍶�E�̓���==========*/
        //�O����
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
                    Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
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
                    Debug.Log(_hit.collider.gameObject.name + "�ɐi�ނ��Ƃ��o���܂�");
                    if (_hit.collider.gameObject.tag == "BlackPiece")
                    {
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Collider���������ĂȂ�");
            }
        }
        //������
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
                    Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
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
                    Debug.Log(_hit.collider.gameObject.name + "�ɐi�ނ��Ƃ��o���܂�");
                    if (_hit.collider.gameObject.tag == "BlackPiece")
                    {
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Collider���������ĂȂ�");
            }
        }
        //������
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
                    Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
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
                    Debug.Log(_hit.collider.gameObject.name + "�ɐi�ނ��Ƃ��o���܂�");
                    if (_hit.collider.gameObject.tag == "BlackPiece")
                    {
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Collider���������ĂȂ�");
            }
        }
        //�E����
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
                    Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
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
                    Debug.Log(_hit.collider.gameObject.name + "�ɐi�ނ��Ƃ��o���܂�");
                    if (_hit.collider.gameObject.tag == "BlackPiece")
                    {
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Collider���������ĂȂ�");
            }
        }
        /*==========�΂ߕ����̓���==========*/
        //���΂ߑO����
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
                    Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
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
                    Debug.Log(_hit.collider.gameObject.name + "�ɐi�ނ��Ƃ��o���܂�");
                    if (_hit.collider.gameObject.tag == "BlackPiece")
                    {
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Collider���������ĂȂ�");
            }
        }
        //�E�΂ߑO����
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
                    Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
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
                    Debug.Log(_hit.collider.gameObject.name + "�ɐi�ނ��Ƃ��o���܂�");
                    if (_hit.collider.gameObject.tag == "BlackPiece")
                    {
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Collider���������ĂȂ�");
            }
        }
        //���΂ߌ�����
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
                    Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
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
                    Debug.Log(_hit.collider.gameObject.name + "�ɐi�ނ��Ƃ��o���܂�");
                    if (_hit.collider.gameObject.tag == "BlackPiece")
                    {
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Collider���������ĂȂ�");
            }
        }
        //�E�΂ߌ�����
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
                    Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
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
                    Debug.Log(_hit.collider.gameObject.name + "�ɐi�ނ��Ƃ��o���܂�");
                    if (_hit.collider.gameObject.tag == "BlackPiece")
                    {
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Collider���������ĂȂ�");
            }
        }
        //�ړ��͈͈ȊO��Collider��off�ɂ��鏈��������
        foreach (Collider col in _search._tile)
        {
            col.enabled = false;
            Debug.Log(col + "��Collider��off�ɂ��܂�");
        }
    }
}
