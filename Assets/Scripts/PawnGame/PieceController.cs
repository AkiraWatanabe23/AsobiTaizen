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
    ///<summary> current(���݂�)�v���C���[ </summary>
    private int _currentPlayer = _playerOne;
    /// <summary> ��̎�� </summary>
    public Type _type;
    /// <summary> �ړ�������Ƃ邽�߂̃t���O </summary>
    public bool _select;
    /// <summary> ���C���[�}�X�N(Inspector����Layer�̔ԍ�) </summary>
    // �����C���[�}�X�N�̒l��2bit�l(2�i��)�ŊǗ����Ă��邽�߁A10�i���ŕ\���́~
    private LayerMask _pieceLay = 1 << 8; //2�i���Łu1000�v
                              //= LayerMask.NameToLayer(Layer��(string)); �ł�OK

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

    // Update is called once per frame
    void Update()
    {
        //���ڂ̃}�E�X���N���b�N�ŋ��I�сA��x�ڂ̃N���b�N�Ŕz�u�ꏊ���m��A�ړ�����(�\��)
        //�����s���ŕʂ̏����ɂł���?
        if (Input.GetMouseButtonDown(0))
        {
            //�}�E�X�̈ʒu���擾���ARay�ɑ��
            Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Ray�̏Փ˂��m���߂�
            RaycastHit _hit;
            //���I����Ԃɂ���
            _select = !_select; //false �� true

            Debug.DrawRay(_ray.origin, _ray.direction * 30, Color.green, 30/*���s����(�b)*/, false);
            Debug.Log(_ray);

            if (Physics.Raycast(_ray.origin, _ray.direction * 30, out _hit, Mathf.Infinity, _pieceLay))
            {
                //  ���I�𒆁@�����ԁ@�@�@�@�@�@�@�@�@�@�@�@�@��Ray��"WhitePiece"�^�O�̃I�u�W�F�N�g�ɓ���������
                if (_select && _currentPlayer == _playerOne && _hit.collider.gameObject.tag == "WhitePiece")
                {
                    Debug.Log("select");
                    //���̏��������ׂĂ̋�s���Ă��邽�߁A�ꃖ���ɋ�W�܂��ďՓ˂��Ă��܂�
                    Vector3 _newPos = _hit.collider.gameObject.transform.position;
                    transform.position = new Vector3(_newPos.x, _newPos.y, _newPos.z);
                    _currentPlayer = _playerTwo;
                    _select = false;
                }
                //       ���I�𒆁@�����ԁ@�@�@�@�@�@�@�@�@�@�@�@�@��Ray��"BlackPiece"�^�O�̃I�u�W�F�N�g�ɓ���������
                else if (_select && _currentPlayer == _playerTwo && _hit.collider.gameObject.tag == "BlackPiece")
                {
                    //���̏��������ׂĂ̋�s���Ă��邽�߁A�ꃖ���ɋ�W�܂��ďՓ˂��Ă��܂�
                    Vector3 _newPos = _hit.collider.gameObject.transform.position;
                    transform.position = new Vector3(_newPos.x, _newPos.y, _newPos.z);
                    _currentPlayer = _playerOne;
                    _select = false;
                }
            }
        }
    }
}
