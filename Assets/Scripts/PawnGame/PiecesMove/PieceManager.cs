using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    /// <summary> ����܂Ƃ߂Ă���e�I�u�W�F�N�g���擾 </summary>
    GameObject _pieceParent;
    /// <summary> ��(�q�I�u�W�F�N�g�B)���ꎟ���z��Ƃ��Ď擾 </summary>
    Transform[] _pieceChildrens;

    // Start is called before the first frame update
    public void Start()
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
