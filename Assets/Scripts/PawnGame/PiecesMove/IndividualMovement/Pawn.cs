using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �|�[���̈ړ�����
/// </summary>
public class Pawn : MonoBehaviour
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
        _search = GameObject.Find("Board,Tile").GetComponent<MasuSearch>();
    }

    public void PawnMovement()
    {
        _pieceInfo = _search._pieceInfo;
        if (_pieceInfo.tag == "WhitePiece")
        {
            //1,1��ڂ̓���...2�}�X�ړ���
            if (_pieceInfo.GetComponent<PieceMove>()._moveCount == 0)
            {
                _vecX = 0f;
                _vecY = 2.55f;
                _vecZ = 2.55f;
                for (int i = 0; i < 2; i++)
                {
                    Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), Color.yellow, 10f);
                    if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
                    {
                        if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                        {
                            _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                            Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
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
                            Debug.Log(_hit.collider.gameObject.name + "�ɐi�ނ��Ƃ��o���܂�");
                            if (_hit.collider.gameObject.tag == "BlackPiece")
                            {
                                break;
                            }
                        }
                    }
                }
                //�ړ��͈͈ȊO��Collider��off�ɂ��鏈��������
                foreach (Collider col in _search._tile)
                {
                    col.enabled = false;
                    Debug.Log(col + "��Collider��off�ɂ��܂�");
                }
            }
            //2,2��ڈȍ~��1�}�X�ړ�
            else if (_pieceInfo.GetComponent<PieceMove>()._moveCount != 0)
            {
                _vecX = 0f;
                _vecY = 2.55f;
                _vecZ = 2.55f;
                Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), Color.yellow, 10f);
                if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
                {
                    if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                    {
                        _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                        Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
                    }
                    else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                    {
                        if (_search._movableTile.Contains(_hit.collider))
                        {

                        }
                        else
                        {
                            _search._movableTile.Add(_hit.collider);
                        }
                        _search._tile.Remove(_hit.collider);
                        Debug.Log(_hit.collider.gameObject.name + "�ɐi�ނ��Ƃ��o���܂�");
                        if (_hit.collider.gameObject.tag == "BlackPiece")
                        {

                        }
                    }
                }
                //�ړ��͈͈ȊO��Collider��off�ɂ��鏈��������
                foreach (Collider col in _search._tile)
                {
                    col.enabled = false;
                    Debug.Log(col + "��Collider��off�ɂ��܂�");
                }
            }
            //��Ɏ΂ߑO�̒T��
            _vecX = 2.55f;
            _vecY = 2.55f;
            _vecZ = 2.55f;
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == "BlackPiece")
                {
                    Debug.Log(_hit.collider.gameObject.name + "���Ƃ邱�Ƃ��o���܂�");
                }
            }
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == "BlackPiece")
                {
                    Debug.Log(_hit.collider.gameObject.name + "���Ƃ邱�Ƃ��o���܂�");
                }
            }
            //3,�A���p�b�T��...�^�ׂ̃}�X�T��
        }
        else if (_pieceInfo.tag == "BlackPiece")
        {
            //1,1��ڂ̓���...2�}�X�ړ���
            if (_pieceInfo.GetComponent<PieceMove>()._moveCount == 0)
            {
                _vecX = 0f;
                _vecY = 2.55f;
                _vecZ = 2.55f;
                for (int i = 0; i < 2; i++)
                {
                    Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), Color.yellow, 10f);
                    if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), out _hit, 100))
                    {
                        if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                        {
                            _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                            Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
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
                            Debug.Log(_hit.collider.gameObject.name + "�ɐi�ނ��Ƃ��o���܂�");
                            if (_hit.collider.gameObject.tag == "WhitePiece")
                            {
                                break;
                            }
                        }
                    }
                }
                //�ړ��͈͈ȊO��Collider��off�ɂ��鏈��������
                foreach (Collider col in _search._tile)
                {
                    col.enabled = false;
                    Debug.Log(col + "��Collider��off�ɂ��܂�");
                }
            }
            //2,2��ڈȍ~��1�}�X�ړ�
            else if (_pieceInfo.GetComponent<PieceMove>()._moveCount != 0)
            {
                _vecX = 0f;
                _vecY = 2.55f;
                _vecZ = 2.55f;
                Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), Color.yellow, 10f);
                if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
                {
                    if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                    {
                        _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                        Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
                    }
                    else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                    {
                        if (_search._movableTile.Contains(_hit.collider))
                        {

                        }
                        else
                        {
                            _search._movableTile.Add(_hit.collider);
                        }
                        _search._tile.Remove(_hit.collider);
                        Debug.Log(_hit.collider.gameObject.name + "�ɐi�ނ��Ƃ��o���܂�");
                        if (_hit.collider.gameObject.tag == "WhitePiece")
                        {

                        }
                    }
                }
                //�ړ��͈͈ȊO��Collider��off�ɂ��鏈��������
                foreach (Collider col in _search._tile)
                {
                    col.enabled = false;
                    Debug.Log(col + "��Collider��off�ɂ��܂�");
                }
            }
            //��Ɏ΂ߑO�̒T��
            _vecX = 2.55f;
            _vecY = 2.55f;
            _vecZ = 2.55f;
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == "WhitePiece")
                {
                    Debug.Log(_hit.collider.gameObject.name + "���Ƃ邱�Ƃ��o���܂�");
                }
            }
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, -_vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, -_vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == "WhitePiece")
                {
                    Debug.Log(_hit.collider.gameObject.name + "���Ƃ邱�Ƃ��o���܂�");
                }
            }
            //3,�A���p�b�T��...�^�ׂ̃}�X�T��
        }
    }
}
