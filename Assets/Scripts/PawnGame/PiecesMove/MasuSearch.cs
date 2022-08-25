using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasuSearch : MonoBehaviour
{
    [SerializeField] Collider[] _masu = new Collider[64];
    [SerializeField] public PieceMove _piece = default;
    [SerializeField] public GameObject _pieceInfo;
    [Tooltip("��̂���}�X�ԍ�")] public int _tileNum = 0;
    RaycastHit _hit;

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
            var _pieceNum = _piece.gameObject.GetComponent<PieceMove>()._type;
            Search((int)_pieceNum);
        }
    }

    public void Search(int pieceType)
    {
        Debug.Log("bbb");
        if (Physics.Raycast(_pieceInfo.transform.position, Vector3.down, out _hit, 10))
        {
            if (_hit.collider.gameObject.tag == "Tile")
            {
                Debug.Log("aaa");
                //�}�X�̔ԍ����擾
                _tileNum = int.Parse(_hit.collider.gameObject.name[1].ToString());
            }
        }

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

    void Pawn()
    {
        Debug.Log(_tileNum);
    }

    void Knight()
    {

    }

    void Bishop()
    {
        //�΂ߑO�����̒T��
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {

            }
        }
        //�΂ߌ������̒T��
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {

            }
        }
    }

    void Rook()
    {
        //�O�A�E�����̒T��
        for (int i = 0; i <= 8; i++)
        {
            for (int j = 0; j <= 8; j++)
            {

            }
        }
        //���A�������̒T��
        for (int i = 0; i <= 8; i++)
        {
            for (int j = 0; j <= 8; j++)
            {

            }
        }
    }

    void Queen()
    {

    }
}
