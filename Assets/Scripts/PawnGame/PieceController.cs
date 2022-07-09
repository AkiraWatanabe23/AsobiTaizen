using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 
/// ��̓����Ɋւ���N���X...�u�I��ŁA�����v�u�D��ꂽ��A�j���v�̋�S�̂œ��ꂳ�ꂽ����
/// </summary>
public class PieceController : MonoBehaviour
{
    ///<summary>�v���C���[ </summary>
    public int Player;
    /// <summary> ��̎�� </summary>
    public Type _type;
    /// <summary> ����o�߂����� </summary>
    public int _turnCount;
    /// <summary> ��̔z�u����Ă���ʒu </summary>
    public Vector2Int Pos, _oldPos;
    /// <summary> �ړ���� </summary>
    public List<Status> _status;
    /// <summary> �ړ�������Ƃ邽�߂̃t���O </summary>
    public bool _isSelect;
    /// <summary> �ړ����̃}�X�I�� </summary>
    RaycastHit _hitTile;

    //Queen = 5, Rook = 4, Bishop = 3, Knight = 2, Pawn = 1 �Ɛ�����U��
    public enum Type
    {
        None = -1,
        Pawn = 1,
        Knight,
        Bishop,
        Rook,
        Queen,
    }

    //�ړ����
    public enum Status
    {
        None = -1,
        EnPassant = 1, //�A���p�b�T��
        Check, //�`�F�b�N(�N�C�[���ɑ΂���)
    }

    // Start is called before the first frame update
    void Start()
    {
        _turnCount = -1;
        _status = new List<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        //�}�E�X���N���b�N�ŋ��I�сA��x�ڂ̃N���b�N�Ŕz�u�ꏊ���m�肷��
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 _mousePos = Input.mousePosition;
            Ray _ray = Camera.main.ScreenPointToRay(_mousePos);
            Debug.Log(_isSelect);

            if (Physics.Raycast(_ray, out _hitTile, 50))
              //Physics.Raycast(Ray�̊J�n�n�_(���[���h���W), Ray�̕���, Ray���Փ˂����m����ő�̋���)
            {
                //Ray���΂��A�^�O���������g�Ɠ����������ꍇ�I����Ԃ̕ύX
                if (_hitTile.collider.gameObject.tag == gameObject.tag)
                {
                    _isSelect = !_isSelect;
                }
                //���I�𒆂��ARay���q�b�g�����I�u�W�F�N�g���}�X�������ꍇ�ړ�
                if (_isSelect && _hitTile.collider.gameObject.tag == "Tile")
                {
                    Vector3 _newPiecePos = _hitTile.collider.gameObject.transform.position;
                    transform.position = new Vector3(_newPiecePos.x, transform.position.y, _newPiecePos.z);

                    //�ړ���͋�̑I����Ԃ��I���ɂ���
                    _isSelect = false;
                }
            }
            Debug.Log(_mousePos);
        }
    }

    //�����ݒ�
    public void SetPiece(int player, Type type)
    {
        Player = player;
        _type = type;
        _turnCount = -1; //�����ɖ߂�
    }
}
