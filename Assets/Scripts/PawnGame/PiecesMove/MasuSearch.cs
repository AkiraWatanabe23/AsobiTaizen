using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasuSearch : MonoBehaviour
{
    [SerializeField] Collider[] _masu = new Collider[64];
    [SerializeField] public PieceMove _piece = default;
    [SerializeField] public GameObject _pieceInfo;
    [Tooltip("駒のいるマスのランク(横)")] public int _tileRank = 0;
    [Tooltip("駒のいるマスのファイル(縦)")] public int _tileFile = 0;
    RaycastHit _hit;
    float _vecX = 0f;
    float _vecY = 2.5f;
    float _vecZ = 2.55f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 64; i++)
        {
            _masu[i] = gameObject.transform.GetChild(i).GetComponent<Collider>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_piece != null)
        {
            GetTileNum();
            var _pieceNum = _piece.gameObject.GetComponent<PieceMove>()._type;
            _piece = null;
            Search((int)_pieceNum);
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
                Debug.Log("マス番号取得(列、行それぞれ)");
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
                Debug.Log("なにもない");
            }
        }
    }

    void Pawn()
    {

    }

    void Knight()
    {

    }

    void Bishop()
    {

    }

    void Rook()
    {
        //前方向
        _vecX = 0f;
        _vecY = 2.55f;
        _vecZ = 2.55f;
        for (int i = 0; i < 8 - _tileRank; i++)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                //探索停止
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                    break;
                }
                //探索続行
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecZ += 2.5f;
                    Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                    if (_hit.collider.gameObject.tag == "BlackPiece")
                    {
                        break;
                    }
                }
            }
            else 
            {
                Debug.Log("Colliderが当たってない");
            }
        }

        //後ろ方向
        _vecX = 0f;
        _vecY = 2.55f;
        _vecZ = 2.55f;
        for (int j = _tileRank; j > 1; j--)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), out _hit, 100))
            {
                //探索停止
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                    break;
                }
                //探索続行
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecZ += 2.5f;
                    Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                    if (_hit.collider.gameObject.tag == "BlackPiece")
                    {
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Colliderが当たってない");
            }
        }

        //左方向
        _vecX = 2.55f;
        _vecY = 2.55f;
        _vecZ = 0f;
        for (int k = _tileFile; k > 1; k--)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                //探索停止
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                    break;
                }
                //探索続行
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX += 2.5f;
                    Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                    if (_hit.collider.gameObject.tag == "BlackPiece")
                    {
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Colliderが当たってない");
            }
        }

        //右方向
        _vecX = 2.55f;
        _vecY = 2.55f;
        _vecZ = 0f;
        for (int l = 0; l < 8 - _tileFile; l++)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                //探索停止
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                    break;
                }
                //探索続行
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX += 2.5f;
                    Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                    if (_hit.collider.gameObject.tag == "BlackPiece")
                    {
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Colliderが当たってない");
            }
        }
    }

    void Queen()
    {

    }
}
