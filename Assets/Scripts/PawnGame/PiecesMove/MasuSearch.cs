using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �}�X�̏����擾�A��̌ʂ̈ړ������Ɉڍs
/// </summary>
public class MasuSearch : MonoBehaviour
{
    [Tooltip("�Ֆʂ̃}�X"), SerializeField] public List<Collider> _tile = new List<Collider>();
    [Tooltip("�ړ��\�}�X"), SerializeField] public List<Collider> _movableTile = new List<Collider>();
    [Tooltip("�T����ɂ���������"), SerializeField] public List<GameObject> _immovablePieces = new List<GameObject>();
    [SerializeField] public PieceMove _piece = default;
    [SerializeField] public GameObject _pieceInfo;
    [Tooltip("��̂���}�X�̃t�@�C��(�c) a�`h")] public int _tileFile = 0;
    [Tooltip("��̂���}�X�̃����N(��) 1�`8")] public int _tileRank = 0;
    RaycastHit _hit;
    [SerializeField] public PieceManager _manager;
    [SerializeField] public Pawn _pawn;
    [SerializeField] public Knight _knight;
    [SerializeField] public Bishop _bishop;
    [SerializeField] public Rook _rook;
    [SerializeField] public Queen _queen;

    // Start is called before the first frame update
    void Start()
    {
        //�}�X���擾���A�F���\���ɂ���
        for (int i = 0; i < 64; i++)
        {
            _tile.Add(gameObject.transform.GetChild(i).GetComponent<Collider>());
            _tile[i].GetComponent<MeshRenderer>().enabled = false;
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

    /// <summary>
    /// ��̌ʂ̈ړ�����
    /// </summary>
    /// <param name="pieceType"></param>
    public void Search(int pieceType)
    {
        switch (pieceType)
        {
            case 1:
                _pawn.PawnMovement();
                break;
            case 2:
                _knight.KnightMovement();
                break;
            case 3:
                _bishop.BishopMovement();
                break;
            case 4:
                _rook.RookMovement();
                break;
            case 5:
                _queen.QueenMovement();
                break;
        }
        //�ړ��͈͊O�̋�A�}�X��Collider��off�ɂ��鏈��
        foreach (var pieces in _manager._whitePieces)
        {
            pieces.GetComponent<Collider>().enabled = false;
        }
        foreach (var pieces in _manager._blackPieces)
        {
            pieces.GetComponent<Collider>().enabled = false;
        }
    }

    /// <summary>
    /// �}�X�̔ԍ����擾����
    /// </summary>
    void GetTileNum()
    {
        if (_pieceInfo != null)
        {
            if (Physics.Raycast(_pieceInfo.transform.position, Vector3.down, out _hit, 5))
            {
                //�}�X�ԍ��擾(�����N)
                _tileRank = int.Parse(_hit.collider.gameObject.name[1].ToString());
                //�}�X�ԍ��擾(�t�@�C��)
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
        }
    }
}
