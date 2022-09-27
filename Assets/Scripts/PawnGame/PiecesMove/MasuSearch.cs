using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �}�X�̏����擾�A��̌ʂ̈ړ������Ɉڍs
/// </summary>
public class MasuSearch : MonoBehaviour
{
    [Tooltip("�Ֆʂ̃}�X"), SerializeField] public List<Collider> Tile = new List<Collider>();
    [Tooltip("�ړ��\�}�X"), SerializeField] public List<Collider> MovableTile = new List<Collider>();
    [Tooltip("�T����ɂ���������"), SerializeField] public List<GameObject> ImmovablePieces = new List<GameObject>();
    [SerializeField] public PieceMove piece = default;
    [SerializeField] public PuzzlePiece puzzle = default;
    [SerializeField] public GameObject pieceInfo;
    [Tooltip("��̂���}�X�̃t�@�C��(�c) a�`h")] public int tileFile = 0;
    [Tooltip("��̂���}�X�̃����N(��) 1�`8")] public int tileRank = 0;
    RaycastHit _hit;
    [SerializeField] PieceManager manager;
    [SerializeField] Pawn _pawn;
    [SerializeField] Knight _knight;
    [SerializeField] Bishop _bishop;
    [SerializeField] Rook _rook;
    [SerializeField] Queen _queen;
    [SerializeField] King _king;

    // Start is called before the first frame update
    void Start()
    {
        //�}�X���擾���A�F���\���ɂ���
        for (int i = 0; i < 64; i++)
        {
            Tile.Add(gameObject.transform.GetChild(i).GetComponent<Collider>());
            Tile[i].GetComponent<MeshRenderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (piece != null)
        {
            GetTileNum();
            var _pieceNum = piece.gameObject.GetComponent<PieceMove>().type;
            Search((int)_pieceNum);
        }

        if (puzzle != null)
        {
            GetTileNum(); 
            var _pieceNum = puzzle.gameObject.GetComponent<PuzzlePiece>().type;
            Search((int)_pieceNum);
        }
    }

    /// <summary>
    /// ��̌ʂ̈ړ�����
    /// </summary>
    /// <param name="pieceType"> �I�񂾋�̎�� </param>
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
            case 6:
                _king.KingMovement();
                break;
        }
        //�ړ��͈͊O�̋��Collider��off�ɂ��鏈��
        foreach (var pieces in manager.WhitePieces)
        {
            //List�̗v�f��Missing�������ꍇ�A��������
            if (pieces != null)
            {
                pieces.GetComponent<Collider>().enabled = false;
            }
        }
        foreach (var pieces in manager.BlackPieces)
        {
            //List�̗v�f��Missing�������ꍇ�A��������
            if (pieces != null)
            {
                pieces.GetComponent<Collider>().enabled = false;
            }
        }
    }

    /// <summary>
    /// �}�X�̔ԍ����擾����
    /// </summary>
    void GetTileNum()
    {
        if (pieceInfo != null)
        {
            if (Physics.Raycast(pieceInfo.transform.position, Vector3.down, out _hit, 5))
            {
                //�}�X�ԍ��擾(�����N)
                tileRank = int.Parse(_hit.collider.gameObject.name[1].ToString());
                //�}�X�ԍ��擾(�t�@�C��)
                if (_hit.collider.gameObject.name[0] == 'a')
                {
                    tileFile = 1;
                }
                else if (_hit.collider.gameObject.name[0] == 'b')
                {
                    tileFile = 2;
                }
                else if (_hit.collider.gameObject.name[0] == 'c')
                {
                    tileFile = 3;
                }
                else if (_hit.collider.gameObject.name[0] == 'd')
                {
                    tileFile = 4;
                }
                else if (_hit.collider.gameObject.name[0] == 'e')
                {
                    tileFile = 5;
                }
                else if (_hit.collider.gameObject.name[0] == 'f')
                {
                    tileFile = 6;
                }
                else if (_hit.collider.gameObject.name[0] == 'g')
                {
                    tileFile = 7;
                }
                else if (_hit.collider.gameObject.name[0] == 'h')
                {
                    tileFile = 8;
                }
            }
        }
    }
}
