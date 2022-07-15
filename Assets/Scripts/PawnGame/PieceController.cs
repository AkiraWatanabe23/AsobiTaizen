using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary> 
/// �S�Ă̋�ɓ��ꂳ�ꂽ����
/// </summary>
public class PieceController : MonoBehaviour, IPointerClickHandler
{
    ///<summary> �v���C���[(����) </summary>
    private const int _playerOne = 1;
    ///<summary> �v���C���[(����) </summary>
    private const int _playerTwo = 2;
    ///<summary> current(���݂�)�v���C���[ </summary>
    private int _currentPlayer = _playerOne;
    /// <summary> ��̎�� </summary>
    public Type _type;
    /// <summary> �ړ�������Ƃ邽�߂̃t���O </summary>
    public bool _select;
    /// <summary> Ray�̒��� </summary>
    [SerializeField] public float _rayDistance = 100;
    /// <summary> ���C���[�}�X�N(Inspector����Layer�̔ԍ�) </summary>
    // �����C���[�}�X�N�̒l��2bit�l(2�i��)�ŊǗ����Ă��邽�߁A10�i���ŕ\���́~
    [SerializeField] LayerMask _tileLayer;
    /// <summary> ����ړ���������collider�̏�ɒu�� </summary>
    [SerializeField] Vector3 _offset = Vector3.up;

    Status _status = Status.Normal;


    public void OnPointerClick(PointerEventData eventData)
    {
        print($"{ name } ���N���b�N����");
        ChangeState();
    }

    void ChangeState()
    {
        if (_status == Status.Normal)
        {
            _status = Status.Move;
        }
        else if (_status == Status.Move)
        {
            _status = Status.Normal;
        }
    }

    bool Move()
    {
        //�}�E�X�̈ʒu���擾���ARay�ɑ��
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(_ray.origin, _ray.direction * 30, Color.green, 30/*���s����(�b)*/, false);
        Debug.Log(_ray);

        if (Physics.Raycast(_ray, out RaycastHit hit, _rayDistance, _tileLayer))
        {
            GameObject _target = hit.collider.gameObject;
            print($"Ray �� {_target.name} �ɓ�������");
            this.transform.position = _target.transform.position + _offset;
            return true;
        }

        return false;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //���ڂ̃}�E�X���N���b�N�ŋ��I�сA��x�ڂ̃N���b�N�Ŕz�u�ꏊ���m��A�ړ�����
        if (Input.GetMouseButtonDown(0))
        {
            if (_status == Status.Move)
            {
                if (Move())
                {
                    ChangeState();
                }
            }
        }
    }

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

    //�ʏ��ԁA�ړ����
    public enum Status
    {
        Normal, //�ʏ���
        Move,   //�ړ����
    }
}