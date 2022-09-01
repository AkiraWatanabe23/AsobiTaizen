using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// start���ɋ���擾
/// </summary>
public class PieceManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> _whitePieces = new List<GameObject>();
    [SerializeField] public List<GameObject> _blackPieces = new List<GameObject>();

    // Start is called before the first frame update
    public void Start()
    {
        for (int i = 0; i < 16; i++)
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
