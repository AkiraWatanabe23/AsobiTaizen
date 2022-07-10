using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 
/// ��̓����Ɋւ���N���X...�u�I��ŁA�����v�u�D��ꂽ��A�j���v�̋�S�̂œ��ꂳ�ꂽ����
/// </summary>
public class PieceController : MonoBehaviour
{
    ///<summary>�v���C���[ </summary>
    public int _player;
    /// <summary> ��̎�� </summary>
    public Type _type;
    /// <summary> �ړ���� </summary>
    public List<Status> _status;
    /// <summary> �ړ�������Ƃ邽�߂̃t���O </summary>
    public bool _select;
    /// <summary> �ړ����̃}�X�I�� </summary>
    public RaycastHit _hitTile;

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
        _status = new List<Status>();
    }

    // Update is called once per frame
    void Update()
    {
        //�}�E�X���N���b�N�ŋ��I�сA��x�ڂ̃N���b�N�Ŕz�u�ꏊ���m�肷��
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 _mousePos = Input.mousePosition;
            Vector3 _cameraPos = Camera.main.transform.position;

            if (Physics.Linecast(_cameraPos, _mousePos)) //�����Ă΂�ĂȂ�...
            {
                //Ray���΂��A�^�O���������g�Ɠ����������ꍇ�I����Ԃ̕ύX
                if (_hitTile.collider.gameObject.tag == gameObject.tag)
                {
                    _select = true;
                }
                //���I�𒆂��ARay���q�b�g�����I�u�W�F�N�g���}�X�������ꍇ�ړ�
                if (_select && _hitTile.collider.gameObject.tag == "Tile")
                {
                    Vector3 _newPiecePos = _hitTile.collider.gameObject.transform.position;
                    transform.position = new Vector3(_newPiecePos.x, transform.position.y, _newPiecePos.z);

                    //�ړ���͋�̑I����Ԃ��I���ɂ���
                    _select = false;
                }
            }
            Debug.Log(_mousePos);
            Debug.Log(_cameraPos);
        }
    }

    //�����ݒ�
    public void SetPiece(int player, Type type)
    {
        _player = player;
        _type = type;
    }
}
