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
    private int _currentPlayer;
    /// <summary> ��̎�� </summary>
    public Type _type;
    /// <summary> Ray�̒��� </summary>
    [SerializeField] public float _rayDistance = 100;
    /// <summary> ���C���[�}�X�N </summary>
    [SerializeField] LayerMask _tileLayer;
    /// <summary> ����ړ���������collider�̏�ɒu�� </summary>
    [SerializeField] Vector3 _offset = Vector3.up;
    /// <summary> �ʏ��ԁA�ړ���Ԃ̋�̃}�e���A�� </summary>
    [SerializeField] Material _normalMaterial;
    [SerializeField] Material _moveMaterial;
    Renderer _renderer;
    /// <summary> ���Ԗڐ��̃J���� </summary>
    Camera _camera;
    /// <summary> ��̏�� </summary>
    public Status _status = Status.Normal;

    /// <summary>
    /// �}�E�X�N���b�N���s��ꂽ���̏���
    /// </summary>
    /// <param name="eventData"></param>
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
            _renderer.material = _moveMaterial;
        }
        else if (_status == Status.Move)
        {
            _status = Status.Normal;
            _renderer.material = _normalMaterial;
        }
    }

    bool Move()
    {
        //�}�E�X�̈ʒu���擾���ARay�ɑ��
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition); //���C���J����(���Ԗڐ�)����Ray���Ƃ΂�
        Ray _ray2 = _camera.ScreenPointToRay(Input.mousePosition);    //second�J����(���Ԗڐ�)����Ray���Ƃ΂�

        //���Ԗڐ�
        if (Physics.Raycast(_ray, out RaycastHit hit, _rayDistance, _tileLayer))
        {
            GameObject _target = hit.collider.gameObject;
            print($"Ray �� {_target.name} �ɓ�������");                     //�u print($"..."); �v= Debug.Log("..."); �Ɠ���
            this.transform.position = _target.transform.position + _offset;
            return true;
        }
        //���Ԗڐ�
        else if (Physics.Raycast(_ray2, out RaycastHit hit2, _rayDistance, _tileLayer))
        {
            GameObject _target = hit2.collider.gameObject;
            print($"Ray �� {_target.name} �ɓ�������");
            this.transform.position = _target.transform.position + _offset;
            return true;
        }

        return false;
    }

    void Start()
    {
        _currentPlayer = _playerOne;                                       //�ŏ��͔��Ԃ���
        _camera = GameObject.Find("Camera(black)").GetComponent<Camera>(); //���Ԗڐ��̃J�����������Ă���
        _renderer = GetComponent<Renderer>();                              //���Renderer(�R���|�[�l���g)���Ƃ��Ă���
    }

    // Update is called once per frame
    void Update()
    {
        //���ڂ̃}�E�X�N���b�N�ŋ��I�сA��x�ڂ̃N���b�N�Ŕz�u�ꏊ���m��A�ړ�����
        //�}�E�X�N���b�N�̒��ł��A���N���b�N���s��ꂽ�ꍇ�Ɉȉ��̏������s��
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