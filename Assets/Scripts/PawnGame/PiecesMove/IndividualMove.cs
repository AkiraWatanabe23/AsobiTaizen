using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// ��̌�(Individual)�̓���
/// </summary>
public class IndividualMove : PieceMove, IPointerClickHandler
{
    [Tooltip("�|�[���̈ړ���")] int _moveCount = 0;
    [Tooltip("�}�X�̊Ԋu")] float _masuSpace = 2.5f;
    GameObject go;

    //�K�v�ȃ}�X��Ray���΂�(���ł͂Ȃ��A�J�������_�����Ray�ł���Ă݂�)�A����ȊO�̃}�X�͋����I��off�ɂ���?����I���ňړ����Ȃ��Ȃ�(?)
    //�^�[���̐؂�ւ�薈��off��on�ɂ���K�v������
    //�������}�X��Collider off(�����BoardInfo()�ŏ����Ă�)
    //�ړ��\�͈͂̃}�X����
    //�I�������}�X��tag�Ń}�X�̐F��ω�������(��Ƃ��Ȃ物�F�A"Tile"tag�Ȃ�F�Ȃ�)

    /// <summary>
    /// �}�E�X�N���b�N���s��ꂽ(�ǂ̃}�E�X�N���b�N�ł����s�����)���̏���
    /// </summary>
    /// <param name="eventData"></param>
    public new void OnPointerClick(PointerEventData eventData)
    {
        go = eventData.pointerCurrentRaycast.gameObject;
        //���J�������猻�݂̃}�E�X�J�[�\���̈ʒu��Ray���΂��A���������I�u�W�F�N�g��������
        var piece = go.GetComponent<PieceMove>();

        Debug.DrawRay(go.transform.position, new Vector3(0f, -0.3f, _masuSpace), Color.yellow, 20f);

        print($"{ name } ��I��");
        piece.ChangeState();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    /// <summary>
    /// ��̌ʂ̈ړ�(PieceMove��Move�����s���Ă��镔���Ŏ��s����)
    /// �����ƊȒP�Ȃ�����T��...
    /// </summary>
    public void MovableSpace()
    {
        //�|�[���̓���
        if (this.gameObject.GetComponent<PieceMove>()._type == PieceType.Pawn)
        {
            Debug.Log("�|�[�����I������܂���");
            //1,1��ڂ̓������A�����łȂ���
            //�@1��ڂ̏ꍇ��2�}�X�ړ���
            if (_moveCount == 0)
            {
                Debug.DrawRay(go.transform.position + new Vector3(0, 2, 0), new Vector3(0f, -2.5f, 2.5f), Color.yellow, 20f);
                Debug.DrawRay(go.transform.position + new Vector3(0, 2, 0), new Vector3(0f, -2.5f, 5f), Color.yellow, 20f);
                _moveCount++;
            }
            //2,2��ڈȍ~��1�}�X�ړ�
            else if (_moveCount != 0)
            {
                Debug.DrawRay(go.transform.position + new Vector3(0, 2, 0), new Vector3(0f, -2.5f, 2.5f), Color.yellow, 20f);
            }
            //�@��Ɏ΂�1�R�O�͒T��(�A���p�b�T���Ɏg����?)
            //3,�A���p�b�T��...�^�ׂ̃}�X�T��
        }
        //�i�C�g�̓���
        else if (this.gameObject.GetComponent<PieceMove>()._type == PieceType.Knight)
        {
            Debug.Log("�i�C�g���I������܂���");
            //�ړ��\�ȃ}�X(�j�n4����)�ɋ���邩�A�Ȃ����̔���(�������甒or���A�Ȃ���Έړ���)
            //���������Έړ��s��
        }
        //�r�V���b�v�̓���
        else if (this.gameObject.GetComponent<PieceMove>()._type == PieceType.Bishop)
        {
            Debug.Log("�r�V���b�v���I������܂���");
            //�΂ߕ�����T��(�����΂�����stop)
            //����������̃}�X�́~�A�G���Z
            //���΂ߑO����
            for (int i = 0; i < 8; ++i)
            {
                Debug.DrawRay(go.transform.position, new Vector3(0f, -0.3f, 2.5f), Color.yellow, 20f);
            }
            //�E�΂ߑO����
            for (int j = 0; j < 8; ++j)
            {
                Debug.DrawRay(go.transform.position, new Vector3(0f, -0.3f, 2.5f), Color.yellow, 20f);
            }
            //���΂ߌ�����
            for (int k = 0; k < 8; ++k)
            {
                Debug.DrawRay(go.transform.position, new Vector3(0f, -0.3f, 2.5f), Color.yellow, 20f);
            }
            //�E�΂ߌ�����
            for (int l = 0; l < 8; ++l)
            {
                Debug.DrawRay(go.transform.position, new Vector3(0f, -0.3f, 2.5f), Color.yellow, 20f);
            }
        }
        //���[�N�̓���
        else if (this.gameObject.GetComponent<PieceMove>()._type == PieceType.Rook)
        {
            Debug.Log("���[�N���I������܂���");
            //�㉺���E��T��(�����΂�����stop)
            //����������̃}�X�́~�A�G���Z
            //�O����
            for (int i = 0; i < 8; ++i)
            {
                Debug.DrawRay(go.transform.position, new Vector3(0f, -0.3f, 2.5f), Color.yellow, 20f);
            }
            //������
            for (int j = 0; j < 8; ++j)
            {
                Debug.DrawRay(go.transform.position, new Vector3(0f, -0.3f, 2.5f), Color.yellow, 20f);
            }
            //������
            for (int k = 0; k < 8; ++k)
            {
                Debug.DrawRay(go.transform.position, new Vector3(0f, -0.3f, 2.5f), Color.yellow, 20f);
            }
            //�E����
            for (int l = 0; l < 8; ++l)
            {
                Debug.DrawRay(go.transform.position, new Vector3(0f, -0.3f, 2.5f), Color.yellow, 20f);
            }
        }
        //�N�C�[���̓���
        else if (this.gameObject.GetComponent<PieceMove>()._type == PieceType.Queen)
        {
            Debug.Log("�N�C�[�����I������܂���");
            //�΂ߕ��� + �㉺���E��T��(�����΂�����stop)
            //����������̃}�X�́~�A�G���Z
            //�O����
            for (int i = 0; i < 8; ++i)
            {
                Debug.DrawRay(go.transform.position, new Vector3(0f, -0.3f, 2.5f), Color.yellow, 20f);
            }
            //������
            for (int j = 0; j < 8; ++j)
            {
                Debug.DrawRay(go.transform.position, new Vector3(0f, -0.3f, 2.5f), Color.yellow, 20f);
            }
            //������
            for (int k = 0; k < 8; ++k)
            {
                Debug.DrawRay(go.transform.position, new Vector3(0f, -0.3f, 2.5f), Color.yellow, 20f);
            }
            //�E����
            for (int l = 0; l < 8; ++l)
            {
                Debug.DrawRay(go.transform.position, new Vector3(0f, -0.3f, 2.5f), Color.yellow, 20f);
            }
            //���΂ߑO����
            for (int m = 0; m < 8; ++m)
            {
                Debug.DrawRay(go.transform.position, new Vector3(0f, -0.3f, 2.5f), Color.yellow, 20f);
            }
            //�E�΂ߑO����
            for (int n = 0; n < 8; ++n)
            {
                Debug.DrawRay(go.transform.position, new Vector3(0f, -0.3f, 2.5f), Color.yellow, 20f);
            }
            //���΂ߌ�����
            for (int o = 0; o < 8; ++o)
            {
                Debug.DrawRay(go.transform.position, new Vector3(0f, -0.3f, 2.5f), Color.yellow, 20f);
            }
            //�E�΂ߌ�����
            for (int p = 0; p < 8; ++p)
            {
                Debug.DrawRay(go.transform.position, new Vector3(0f, -0.3f, 2.5f), Color.yellow, 20f);
            }
        }
    }
}
