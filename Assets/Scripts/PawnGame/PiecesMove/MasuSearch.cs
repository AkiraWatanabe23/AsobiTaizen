using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasuSearch : MonoBehaviour
{
    [SerializeField] Collider[] _masu = new Collider[64];
    [SerializeField] public PieceMove _piece = default;
    [SerializeField] public GameObject _pieceInfo;
    [Tooltip("駒のいるマス番号")] public int _tileNum = 0;
    RaycastHit _hit;
    float _vecX = 0f;
    float _vecY = 0.3f;
    float _vecZ = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 64; i++)
        {
            _masu[i] = gameObject.transform.GetChild(i).GetComponent<Collider>();
            _tileNum = int.Parse(_masu[i].name[1].ToString());
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
                Debug.Log("マス番号取得");
                _tileNum = int.Parse(_hit.collider.gameObject.name[1].ToString());
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
        for (int i = 0; i <= 8 - _tileNum; i++)
        {
            Debug.DrawRay(_pieceInfo.transform.position, new Vector3(_vecX, -_vecY, _vecZ), Color.yellow, 20f);
        }
        //後ろ方向
        for (int j = 0; j <= 8 - _tileNum; j++)
        {
            Debug.DrawRay(_pieceInfo.transform.position, new Vector3(_vecX, -_vecY, _vecZ), Color.yellow, 20f);
        }
        //左方向
        for (int k = 0; k <= 8 - _tileNum; k++)
        {
            Debug.DrawRay(_pieceInfo.transform.position, new Vector3(_vecX, -_vecY, _vecZ), Color.yellow, 20f);
        }
        //右方向
        for (int l = 0; l <= 8 - _tileNum; l++)
        {
            Debug.DrawRay(_pieceInfo.transform.position, new Vector3(_vecX, -_vecY, _vecZ), Color.yellow, 20f);
        }
    }

    void Queen()
    {

    }
}
