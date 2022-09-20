using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ‹î‚ğ“®‚©‚µ‚½‚Æ‚«‚Ì×‚©‚Èˆ—
/// </summary>
public class PieceManager : MonoBehaviour
{
    [SerializeField] Material _white;
    [SerializeField] Material _black;
    [SerializeField] public List<GameObject> _whitePieces = new List<GameObject>();
    [SerializeField] public List<GameObject> _blackPieces = new List<GameObject>();
    [Tooltip("’Tõ”ÍˆÍ‚É‚¢‚éŠl‚é‚±‚Æ‚ªo—ˆ‚é‹î")] public List<GameObject> _getablePieces = new List<GameObject>();
    MasuSearch _search;

    // Start is called before the first frame update
    public void Start()
    {
        _search = GameObject.Find("Board,Tile").GetComponent<MasuSearch>();

        for (int i = 0; i < 5; i++)
        {
            if (transform.GetChild(i).gameObject != null)
            {
                if (transform.GetChild(i).gameObject.tag == "WhitePiece")
                {
                    _whitePieces.Add(transform.GetChild(i).gameObject);
                }
                else if (transform.GetChild(i).gameObject.tag == "BlackPiece")
                {
                    _blackPieces.Add(transform.GetChild(i).gameObject);
                }
            }
        }
    }

    /// <summary>
    /// Šl‚ê‚é‹î‚ğŠl‚ç‚È‚©‚Á‚½ê‡‚ÉList‚É–ß‚·
    /// </summary>
    public void UnGetPiece()
    {
        foreach (var i in _getablePieces)
        {
            if (_search._pieceInfo.tag == "BlackPiece")
            {
                if (i.gameObject.tag == "WhitePiece")
                {
                    i.gameObject.GetComponent<MeshRenderer>().material = _white;
                }
                _whitePieces.Add(i);
            }
            else if (_search._pieceInfo.gameObject.tag == "WhitePiece")
            {
                if (i.gameObject.tag == "BlackPiece")
                {
                    i.gameObject.GetComponent<MeshRenderer>().material = _black;
                }
                _blackPieces.Add(i);
            }
        }
        _getablePieces.Clear();
    }
}
