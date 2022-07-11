using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 
/// �S�Ă̋�ɓ��ꂳ�ꂽ����
/// </summary>
public class PieceController : MonoBehaviour
{
    ///<summary> �v���C���[1(����) </summary>
    private const int _playerOne = 1;
    ///<summary> �v���C���[2(����) </summary>
    private const int _playerTwo = 2;
    ///<summary> ���݂̃v���C���[ </summary>
    private int _currentPlayer = _playerOne; //current...����(��)
    /// <summary> ��̎�� </summary>
    public Type _type;
    /// <summary> �ړ���� </summary>
    public List<Status> _status;
    /// <summary> �ړ�������Ƃ邽�߂̃t���O </summary>
    public bool _select;

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
        Check,         //�`�F�b�N(�N�C�[���ɑ΂���)
    }

    // Start is called before the first frame update
    void Start()
    {
        _status = new List<Status>();
        Debug.Log(_select);
    }

    // Update is called once per frame
    void Update()
    {
        //�}�E�X���N���b�N�ŋ��I�сA��x�ڂ̃N���b�N�Ŕz�u�ꏊ���m�肷��
        if (Input.GetMouseButtonDown(0))
        {
            //�}�E�X�̈ʒu���擾���ARay�ɑ��
            Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit _hitTile;
            Debug.DrawRay(_ray.origin, _ray.direction * 10, Color.green, 10, false); //Ray�����Ԃ̃J�����̕�����o�Ă���...Tag�ς�����MainCamera�̕��ɕς����
            _select = true;
            Debug.Log(_select);
            /*�������܂ł͌Ă΂�Ă���*/
            /*���������炪�Ă΂�Ă��Ȃ�*/

            //�}�E�X�̃|�W�V��������Ray��L�΂��A�����ɓ���������_hitTile�ɑ������
            if (Physics.Raycast(_ray, out _hitTile)) //�������������_��...
            {
                Debug.Log("SelectPosition");

                //���I�𒆁A����Ray���q�b�g�����I�u�W�F�N�g���Ֆʂ̃}�X�Ȃ�Έړ�����
                if (_select && _hitTile.collider.gameObject.tag == "Tile")
                {
                    Vector3 _newPos = _hitTile.collider.gameObject.transform.position;
                    transform.position = new Vector3(_newPos.x, _newPos.y, _newPos.z);

                    _select = false;
                }
            }
        }
    }
}
