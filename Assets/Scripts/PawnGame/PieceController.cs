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
    /// <summary> �ړ�������Ƃ邽�߂̃t���O </summary>
    public bool _select;
    /// <summary> Ray�̏Փ˂��m���߂� </summary>
    public RaycastHit _hitTile;
    /// <summary> ���C���[�}�X�N </summary>
    private LayerMask _pieceLay = 6;

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

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(_select);
    }

    // Update is called once per frame
    void Update()
    {
        //�}�E�X���N���b�N�ŋ��I�сA��x�ڂ̃N���b�N�Ŕz�u�ꏊ���m��A�ړ�����
        if (Input.GetMouseButtonDown(0))
        {
            //�}�E�X�̈ʒu���擾���ARay�ɑ��
            Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(_ray.origin, _ray.direction * 30, Color.green, 10, false); //Ray�����Ԃ̃J�����̕�����o�Ă���...Tag�ς�����MainCamera�̕��ɕς����
            Debug.Log(_ray);
            /*�������܂ł͌Ă΂�Ă���*/

            //�}�E�X�̃|�W�V��������Ray��L�΂��A�����ɓ���������_hitTile�ɑ������
            //������if�����Ă΂�ĂȂ�...�ǂ̈������_���H
            //if (Physics.Raycast( _ray /*���ˑΏۂ�Ray*/, out _hitTile /*�Փ˂�������I�u�W�F�N�g�̏��*/, 50 /*Ray�̒���(�ȗ������ꍇ�A������)*/, _pieceLay /*�ՓˑΏۂɂȂ郌�C���[(���C���[�}�X�N)*/ ))
            //{
                _select = !_select; //�u!�v...���݂̒l�Ƃ͔��΂̒l�������Ă���(����̏ꍇ�Afalse��true)
                Debug.Log("SelectPosition");

                //���I�𒆁A����Ray���q�b�g�����I�u�W�F�N�g���Ֆʂ̃}�X�Ȃ�Έړ�����
                if (_select/*(== true)*/ && _hitTile.collider.gameObject.tag == "Tile")//...NullReferenceException
                {
                    Vector3 _newPos = Input.mousePosition;
                    transform.position = new Vector3(_newPos.x, _newPos.y, _newPos.z);
                    _select = false;
                }
            //}
        }
    }
}
