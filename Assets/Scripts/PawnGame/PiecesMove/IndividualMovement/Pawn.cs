using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �|�[���̈ړ�����
/// </summary>
public class Pawn : MonoBehaviour
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

    public void PawnMovement()
    {
        if (_search._pieceInfo.tag == "WhitePiece")
        {
            //1,1��ڂ̓���...2�}�X�ړ���
            if (_search._pieceInfo.GetComponent<PieceMove>()._moveCount == 0)
            {
                _search._vecX = 0f;
                _search._vecY = 2.55f;
                _search._vecZ = 2.55f;
                for (int i = 0; i < 2; i++)
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
                }
                //�ړ��͈͈ȊO��Collider��off�ɂ��鏈��������
                foreach (Collider col in _search._tile)
                {
                    col.enabled = false;
                    Debug.Log(col + "��Collider��off�ɂ��܂�");
                }
            }
            //2,2��ڈȍ~��1�}�X�ړ�
            else if (_search._pieceInfo.GetComponent<PieceMove>()._moveCount != 0)
            {
                _vecX = 0f;
                _vecY = 2.55f;
                _vecZ = 2.55f;
                Debug.DrawRay(_search._pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), Color.yellow, 10f);
                if (Physics.Raycast(_search._pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
                {
                    if (_hit.collider.gameObject.tag == _search._pieceInfo.tag)
                    {
                        _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                        Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
                    }
                    else if (_hit.collider.gameObject.tag != _search._pieceInfo.tag)
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
            //�@��Ɏ΂ߑO�̒T��(�A���p�b�T���Ɏg����?)
            _vecX = 2.55f;
            _vecY = 2.55f;
            _vecZ = 2.55f;
            Debug.DrawRay(_search._pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_search._pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == "BlackPiece")
                {
                    Debug.Log(_hit.collider.gameObject.name + "���Ƃ邱�Ƃ��o���܂�");
                }
            }
            Debug.DrawRay(_search._pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_search._pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == "BlackPiece")
                {
                    Debug.Log(_search._hit.collider.gameObject.name + "���Ƃ邱�Ƃ��o���܂�");
                }
            }
            //3,�A���p�b�T��...�^�ׂ̃}�X�T��
        }
        else if (_search._pieceInfo.tag == "BlackPiece")
        {
            //1,1��ڂ̓���...2�}�X�ړ���
            if (_search._pieceInfo.GetComponent<PieceMove>()._moveCount == 0)
            {
                _vecX = 0f;
                _vecY = 2.55f;
                _vecZ = 2.55f;
                for (int i = 0; i < 2; i++)
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
            else if (_search._pieceInfo.GetComponent<PieceMove>()._moveCount != 0)
            {
                _vecX = 0f;
                _vecY = 2.55f;
                _vecZ = 2.55f;
                Debug.DrawRay(_search._pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), Color.yellow, 10f);
                if (Physics.Raycast(_search._pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
                {
                    if (_hit.collider.gameObject.tag == _search._pieceInfo.tag)
                    {
                        _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                        Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
                    }
                    else if (_hit.collider.gameObject.tag != _search._pieceInfo.tag)
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
            //�@��Ɏ΂ߑO�̒T��(�A���p�b�T���Ɏg����?)
            _vecX = 2.55f;
            _vecY = 2.55f;
            _vecZ = 2.55f;
            Debug.DrawRay(_search._pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_search._pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == "WhitePiece")
                {
                    Debug.Log(_hit.collider.gameObject.name + "���Ƃ邱�Ƃ��o���܂�");
                }
            }
            Debug.DrawRay(_search._pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, -_vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_search._pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, -_vecZ), out _hit, 100))
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
