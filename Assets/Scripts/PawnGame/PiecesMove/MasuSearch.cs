using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasuSearch : MonoBehaviour
{
    [SerializeField] public List<Collider> _tile = new List<Collider>();
    [SerializeField] public List<Collider> _movableTile = new List<Collider>();
    [SerializeField] public PieceMove _piece = default;
    [SerializeField] public GameObject _pieceInfo;
    [Tooltip("��̂���}�X�̃����N(��)")] public int _tileRank = 0;
    [Tooltip("��̂���}�X�̃t�@�C��(�c)")] public int _tileFile = 0;
    RaycastHit _hit;
    float _vecX = 0f;
    float _vecY = 2.5f;
    float _vecZ = 2.55f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 64; i++)
        {
            _tile[i] = gameObject.transform.GetChild(i).GetComponent<BoxCollider>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_piece != null)
        {
            GetTileNum();
            var _pieceNum = _piece.gameObject.GetComponent<PieceMove>()._type;
            Search((int)_pieceNum);
            _piece = null;
        }
    }

    public void Search(int pieceType)
    {
        switch (pieceType)
        {
            case 1:
                Pawn();
                break;
            case 2:
                Knight();
                break;
            case 3:
                Bishop();
                break;
            case 4:
                Rook();
                break;
            case 5:
                Queen();
                break;
        }   
    }

    void GetTileNum()
    {
        if (_pieceInfo != null)
        {
            if (Physics.Raycast(_pieceInfo.transform.position, Vector3.down, out _hit, 5))
            {
                //�}�X�ԍ��擾(��A�s���ꂼ��)
                _tileRank = int.Parse(_hit.collider.gameObject.name[1].ToString());
                if (_hit.collider.gameObject.name[0] == 'a')
                {
                    _tileFile = 1;
                }
                else if (_hit.collider.gameObject.name[0] == 'b')
                {
                    _tileFile = 2;
                }
                else if (_hit.collider.gameObject.name[0] == 'c')
                {
                    _tileFile = 3;
                }
                else if (_hit.collider.gameObject.name[0] == 'd')
                {
                    _tileFile = 4;
                }
                else if (_hit.collider.gameObject.name[0] == 'e')
                {
                    _tileFile = 5;
                }
                else if (_hit.collider.gameObject.name[0] == 'f')
                {
                    _tileFile = 6;
                }
                else if (_hit.collider.gameObject.name[0] == 'g')
                {
                    _tileFile = 7;
                }
                else if (_hit.collider.gameObject.name[0] == 'h')
                {
                    _tileFile = 8;
                }
            }
            else if (_hit.collider == null)
            {
                Debug.Log("�Ȃɂ��Ȃ�");
            }
        }
    }

    void Pawn()
    {
        if (_pieceInfo.tag == "WhitePiece") /*���|�[��*/
        {
            //1,1��ڂ̓������A�����łȂ���
            //�@1��ڂ̏ꍇ��2�}�X�ړ���
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
                        //�T����~
                        if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                        {
                            _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                            Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
                            break;
                        }
                        //�T�����s
                        else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                        {
                            _vecZ += 2.5f;
                            _movableTile.Add(_hit.collider);
                            _tile.Remove(_hit.collider);
                            Debug.Log(_hit.collider.gameObject.name + "�ɐi�ނ��Ƃ��o���܂�");
                            if (_hit.collider.gameObject.tag == "BlackPiece")
                            {
                                break;
                            }
                        }
                    }
                }

                foreach (Collider col in _tile)
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
                    //�T����~
                    if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                    {
                        _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                        Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
                    }
                    //�T�����s
                    else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                    {
                        _movableTile.Add(_hit.collider);
                        _tile.Remove(_hit.collider);
                        Debug.Log(_hit.collider.gameObject.name + "�ɐi�ނ��Ƃ��o���܂�");
                        if (_hit.collider.gameObject.tag == "BlackPiece")
                        {

                        }
                    }
                }

                foreach (Collider col in _tile)
                {
                    col.enabled = false;
                    Debug.Log(col + "��Collider��off�ɂ��܂�");
                }
            }
            //�@��Ɏ΂�1�R�O�͒T��(�A���p�b�T���Ɏg����?)
            //3,�A���p�b�T��...�^�ׂ̃}�X�T��
        }
        else if (_pieceInfo.tag == "BlackPiece") /*���|�[��*/
        {
            //1,1��ڂ̓������A�����łȂ���
            //�@1��ڂ̏ꍇ��2�}�X�ړ���
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
                        //�T����~
                        if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                        {
                            _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                            Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
                            break;
                        }
                        //�T�����s
                        else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                        {
                            _vecZ += 2.5f;
                            _movableTile.Add(_hit.collider);
                            _tile.Remove(_hit.collider);
                            Debug.Log(_hit.collider.gameObject.name + "�ɐi�ނ��Ƃ��o���܂�");
                            if (_hit.collider.gameObject.tag == "WhitePiece")
                            {
                                break;
                            }
                        }
                    }
                }

                foreach (Collider col in _tile)
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
                    //�T����~
                    if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                    {
                        _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                        Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
                    }
                    //�T�����s
                    else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                    {
                        _movableTile.Add(_hit.collider);
                        _tile.Remove(_hit.collider);
                        Debug.Log(_hit.collider.gameObject.name + "�ɐi�ނ��Ƃ��o���܂�");
                        if (_hit.collider.gameObject.tag == "WhitePiece")
                        {

                        }
                    }
                }

                foreach (Collider col in _tile)
                {
                    col.enabled = false;
                    Debug.Log(col + "��Collider��off�ɂ��܂�");
                }
            }
            //�@��Ɏ΂�1�R�O�͒T��(�A���p�b�T���Ɏg����?)
            //3,�A���p�b�T��...�^�ׂ̃}�X�T��
        }

    }

    void Knight()
    {
        //�ړ��\�ȃ}�X(�j�n4����)�ɋ���邩�A�Ȃ����̔���(�������甒or���A�Ȃ���Έړ���)
        //���������Έړ��s��
        //�j�n �O����
        _vecX = 2.55f;
        _vecY = 2.55f;
        _vecZ = 5f;
        for (int i = 0; i < 2; i++)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                //�T����~
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "�ɂ͂����߂܂���");
                    break;
                }
                //�T�����s
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX -= 5f;
                    _movableTile.Add(_hit.collider);
                    _tile.Remove(_hit.collider);
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
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), out _hit, 100))
            {
                //�T����~
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "�ɂ͂����߂܂���");
                    break;
                }
                //�T�����s
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX -= 5f;
                    _movableTile.Add(_hit.collider);
                    _tile.Remove(_hit.collider);
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
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                //�T����~
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "�ɂ͂����߂܂���");
                    break;
                }
                //�T�����s
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecZ -= 5f;
                    _movableTile.Add(_hit.collider);
                    _tile.Remove(_hit.collider);
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
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                //�T����~
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "�ɂ͂����߂܂���");
                    break;
                }
                //�T�����s
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecZ -= 5f;
                    _movableTile.Add(_hit.collider);
                    _tile.Remove(_hit.collider);
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
        foreach (Collider col in _tile)
        {
            col.enabled = false;
            Debug.Log(col + "��Collider��off�ɂ��܂�");
        }
    }

    void Bishop()
    {
        //���΂ߑO����
        _vecX = 2.55f;
        _vecY = 2.55f;
        _vecZ = 2.55f;
        for (int i = 0; i < 8 - _tileRank; i++)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                //�T����~
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
                    break;
                }
                //�T�����s
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX += 2.5f;
                    _vecZ += 2.5f;
                    _movableTile.Add(_hit.collider);
                    _tile.Remove(_hit.collider);
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
        for (int j = _tileRank; j > 1; j--)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                //�T����~
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
                    break;
                }
                //�T�����s
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX += 2.5f;
                    _vecZ += 2.5f;
                    _movableTile.Add(_hit.collider);
                    _tile.Remove(_hit.collider);
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
        for (int k = _tileFile; k > 1; k--)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, -_vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, -_vecZ), out _hit, 100))
            {
                //�T����~
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
                    break;
                }
                //�T�����s
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX += 2.5f;
                    _vecZ += 2.5f;
                    _movableTile.Add(_hit.collider);
                    _tile.Remove(_hit.collider);
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
        for (int l = 0; l < 8 - _tileFile; l++)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), out _hit, 100))
            {
                //�T����~
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
                    break;
                }
                //�T�����s
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX += 2.5f;
                    _vecZ += 2.5f;
                    _movableTile.Add(_hit.collider);
                    _tile.Remove(_hit.collider);
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
        foreach (Collider col in _tile)
        {
            col.enabled = false;
            Debug.Log(col + "��Collider��off�ɂ��܂�");
        }
    }

    void Rook()
    {
        //�O����
        _vecX = 0f;
        _vecY = 2.55f;
        _vecZ = 2.55f;
        for (int i = 0; i < 8 - _tileRank; i++)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                //�T����~
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
                    break;
                }
                //�T�����s
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecZ += 2.5f;
                    _movableTile.Add(_hit.collider);
                    _tile.Remove(_hit.collider);
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
        for (int j = _tileRank; j > 1; j--)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), out _hit, 100))
            {
                //�T����~
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
                    break;
                }
                //�T�����s
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecZ += 2.5f;
                    _movableTile.Add(_hit.collider);
                    _tile.Remove(_hit.collider);
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
        for (int k = _tileFile; k > 1; k--)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                //�T����~
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
                    break;
                }
                //�T�����s
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX += 2.5f;
                    _movableTile.Add(_hit.collider);
                    _tile.Remove(_hit.collider);
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
        for (int l = 0; l < 8 - _tileFile; l++)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                //�T����~
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
                    break;
                }
                //�T�����s
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX += 2.5f;
                    _movableTile.Add(_hit.collider);
                    _tile.Remove(_hit.collider);
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
        foreach (Collider col in _tile)
        {
            col.enabled = false;
            Debug.Log(col + "��Collider��off�ɂ��܂�");
        }
    }

    void Queen()
    {
        /*==========�O�㍶�E�̓���==========*/
        //�O����
        _vecX = 0f;
        _vecY = 2.55f;
        _vecZ = 2.55f;
        for (int i = 0; i < 8 - _tileRank; i++)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                //�T����~
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
                    break;
                }
                //�T�����s
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecZ += 2.5f;
                    _movableTile.Add(_hit.collider);
                    _tile.Remove(_hit.collider);
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
        for (int j = _tileRank; j > 1; j--)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), out _hit, 100))
            {
                //�T����~
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
                    break;
                }
                //�T�����s
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecZ += 2.5f;
                    _movableTile.Add(_hit.collider);
                    _tile.Remove(_hit.collider);
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
        for (int k = _tileFile; k > 1; k--)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                //�T����~
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
                    break;
                }
                //�T�����s
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX += 2.5f;
                    _movableTile.Add(_hit.collider);
                    _tile.Remove(_hit.collider);
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
        for (int l = 0; l < 8 - _tileFile; l++)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                //�T����~
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
                    break;
                }
                //�T�����s
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX += 2.5f;
                    _movableTile.Add(_hit.collider);
                    _tile.Remove(_hit.collider);
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
        for (int i = 0; i < 8 - _tileRank; i++)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                //�T����~
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
                    break;
                }
                //�T�����s
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX += 2.5f;
                    _vecZ += 2.5f;
                    _movableTile.Add(_hit.collider);
                    _tile.Remove(_hit.collider);
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
        for (int j = _tileRank; j > 1; j--)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                //�T����~
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
                    break;
                }
                //�T�����s
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX += 2.5f;
                    _vecZ += 2.5f;
                    _movableTile.Add(_hit.collider);
                    _tile.Remove(_hit.collider);
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
        for (int k = _tileFile; k > 1; k--)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, -_vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, -_vecZ), out _hit, 100))
            {
                //�T����~
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
                    break;
                }
                //�T�����s
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX += 2.5f;
                    _vecZ += 2.5f;
                    _movableTile.Add(_hit.collider);
                    _tile.Remove(_hit.collider);
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
        for (int l = 0; l < 8 - _tileFile; l++)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), out _hit, 100))
            {
                //�T����~
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "����ɂ͂����߂܂���");
                    break;
                }
                //�T�����s
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX += 2.5f;
                    _vecZ += 2.5f;
                    _movableTile.Add(_hit.collider);
                    _tile.Remove(_hit.collider);
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
        foreach (Collider col in _tile)
        {
            col.enabled = false;
            Debug.Log(col + "��Collider��off�ɂ��܂�");
        }
    }
}
