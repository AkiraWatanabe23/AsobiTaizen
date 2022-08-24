using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasuSearch : MonoBehaviour
{
   [SerializeField] Collider[] _masu = new Collider[64];
   [SerializeField] public PieceMove _piece = default;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 64; i++)
        {
            _masu[i] = gameObject.transform.GetChild(i).GetComponent<Collider>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_piece != null)
        {
            var _x = _piece.gameObject.GetComponent<PieceMove>()._type;
            Search((int)_x);
        }
    }

    public void Search(int pieceType)
    {
        switch (pieceType)
        {
            case 1:
                Pawn();
                break;
            case 2:
                Knight();
                break;
            case 3:
                Bishop();
                break;
            case 4:
                Rook();
                break;
            case 5:
                Queen();
                break;
        }    }

    void Pawn()
    {

    }

    void Knight()
    {

    }

    void Bishop()
    {

    }

    void Rook()
    {

    }

    void Queen()
    {

    }
}
