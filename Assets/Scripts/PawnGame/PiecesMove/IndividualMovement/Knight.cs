using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �i�C�g�̈ړ�����
/// </summary>
public class Knight : MonoBehaviour
{
    MasuSearch _search;
    public RaycastHit _hit;
    public float _vecX = 0f;
    public float _vecY = 2.5f;
    public float _vecZ = 2.55f;

    // Start is called before the first frame update
    void Start()
    {
        _search = GetComponent<MasuSearch>();

        _hit = _search._hit;
        _vecX = _search._vecX;
        _vecY = _search._vecY;
        _vecZ = _search._vecZ;
    }

    public void KnightMovement ()
    {
        //�j�n �O����
        _vecX = 2.55f;
        _vecY = 2.55f;
        _vecZ = 5f;
        for (int i = 0; i < 2; i++)
        {
            Debug.DrawRay(_search._pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_search._pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == _search._pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "�ɂ͂����߂܂���");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _search._pieceInfo.tag)
                {
                    _vecX -= 5f;
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
        //�j�n ������
        _vecX = 2.55f;
        _vecY = 2.55f;
        _vecZ = 5f;
        for (int j = 0; j < 2; j++)
        {
            Debug.DrawRay(_search._pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_search._pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == _search._pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "�ɂ͂����߂܂���");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _search._pieceInfo.tag)
                {
                    _vecX -= 5f;
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
        //�j�n ������
        _vecX = 5f;
        _vecY = 2.55f;
        _vecZ = 2.55f;
        for (int k = 0; k < 2; k++)
        {
            Debug.DrawRay(_search._pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_search._pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == _search._pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "�ɂ͂����߂܂���");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _search._pieceInfo.tag)
                {
                    _vecZ -= 5f;
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
        //�j�n �E����
        _vecX = 5f;
        _vecY = 2.55f;
        _vecZ = 2.55f;
        for (int l = 0; l < 2; l++)
        {
            Debug.DrawRay(_search._pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_search._pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == _search._pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "�ɂ͂����߂܂���");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _search._pieceInfo.tag)
                {
                    _vecZ -= 5f;
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
