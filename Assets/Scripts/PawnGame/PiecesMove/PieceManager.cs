using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��𓮂������Ƃ��ׂ̍��ȏ���
/// </summary>
public class PieceManager : MonoBehaviour
{
    [SerializeField] Material _white;
    [SerializeField] Material _black;
    [SerializeField] public List<GameObject> WhitePieces = new List<GameObject>();
    [SerializeField] public List<GameObject> BlackPieces = new List<GameObject>();
    [Tooltip("�T���͈͂ɂ���l�邱�Ƃ��o�����")] public List<GameObject> GetablePieces = new List<GameObject>();
    [Tooltip("���ݑI��ł����")] public GameObject CurrentPiece;
    MasuSearch _search;

    // Start is called before the first frame update
    private void Start()
    {
        _search = GameObject.Find("Board,Tile").GetComponent<MasuSearch>();

        for (int i = 0; i < 2; i++)
        {
            if (transform.GetChild(i).gameObject != null)
            {
                if (transform.GetChild(i).gameObject.tag == "WhitePiece")
                {
                    WhitePieces.Add(transform.GetChild(i).gameObject);
                }
                else if (transform.GetChild(i).gameObject.tag == "BlackPiece")
                {
                    BlackPieces.Add(transform.GetChild(i).gameObject);
                }
            }
        }
    }

    /// <summary>
    /// �l������l��Ȃ������ꍇ��List�ɖ߂�
    /// </summary>
    public void UnGetPiece()
    {
        foreach (var i in GetablePieces)
        {
            if (_search.pieceInfo.tag == "BlackPiece")
            {
                if (i.gameObject.tag == "WhitePiece")
                {
                    i.gameObject.GetComponent<MeshRenderer>().material = _white;
                }
                WhitePieces.Add(i);
            }
            else if (_search.pieceInfo.gameObject.tag == "WhitePiece")
            {
                if (i.gameObject.tag == "BlackPiece")
                {
                    i.gameObject.GetComponent<MeshRenderer>().material = _black;
                }
                BlackPieces.Add(i);
            }
            i.gameObject.GetComponent<Collider>().enabled = true;
        }
        GetablePieces.Clear();
    }
}
