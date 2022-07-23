using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PieceManager : MonoBehaviour, IPointerClickHandler
{
    /// <summary> ����܂Ƃ߂Ă���e�I�u�W�F�N�g���擾 </summary>
    GameObject _pieceParent;
    /// <summary> ��(�q�I�u�W�F�N�g�B)��z��Ƃ��Ď擾 </summary>
    Transform[] _pieceChildrens;

    /// <summary>
    /// �}�E�X�N���b�N���s��ꂽ(�ǂ̃}�E�X�N���b�N�ł����s�����)���̏���
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        var go = eventData.pointerCurrentRaycast.gameObject;
        //���J�������猻�݂̃}�E�X�J�[�\���̈ʒu��Ray���΂��A���������I�u�W�F�N�g��������
        var piece = go.GetComponent<PieceController>();
        //�����������I�u�W�F�N�g�́uPieceController�v�������Ă���

        print($"{ name } ��I��");
        piece.ChangeState();
    }

    // Start is called before the first frame update
    void Start()
    {
        ///<summary> ����܂Ƃ߂Ă���e�I�u�W�F�N�g���������� </summary>
        _pieceParent = GameObject.Find("Piece");
        ///<summary> �q�I�u�W�F�N�g�B�̔z��������� </summary>
        _pieceChildrens = new Transform[_pieceParent.transform.childCount];
        ///<summary> �q�I�u�W�F�N�g���擾 </summary>
        for (int i = 0; i < _pieceParent.transform.childCount; i++)
        {
            _pieceChildrens[i] = _pieceParent.transform.GetChild(i);
            Debug.Log(i + "�Ԗڂ̋��" + _pieceChildrens[i].name + "�ł�");
        }
    }
}
