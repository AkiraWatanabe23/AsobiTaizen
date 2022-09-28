using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ƒ‹[ƒN‚ÌˆÚ“®ˆ—(‘OŒã¶‰E‚É‰½ƒ}ƒX‚Å‚àˆÚ“®‚Å‚«‚é)
/// </summary>
public class Rook : MonoBehaviour
{
    MasuSearch _search;
    PieceManager _piece;
    [Tooltip("‚±‚Ì‹îŠl‚ê‚Ü‚·"), SerializeField] Material _getable;
    [Tooltip("ˆÚ“®‚³‚¹‚éƒ‹[ƒN")] GameObject _pieceInfo;
    RaycastHit _hit;
    float _vecX;
    float _vecY;
    float _vecZ;

    // Start is called before the first frame update
    void Start()
    {
        _search = GameObject.Find("Board,Tile").GetComponent<MasuSearch>();
        _piece = GameObject.Find("Piece").GetComponent<PieceManager>();
    }

    public void RookMovement()
    {
        _pieceInfo = _search.PieceInfo;

        //‘O•ûŒü
        _vecX = 0f;
        _vecY = 15f;
        _vecZ = 6f;
        for (int i = 0; i < 7; i++)
        {
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(_vecX, _vecY, _vecZ), Vector3.down, out _hit, 20))
            {
                //’Tõæ‚É–¡•û‚Ì‹î‚ª‚ ‚Á‚½ê‡
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    _search.ImmovablePieces.Add(_hit.collider.gameObject);
                    Debug.Log(_hit.collider.gameObject.name + "‚æ‚èæ‚É‚Í‚·‚·‚ß‚Ü‚¹‚ñ");
                    break;
                }
                //’Tõæ‚ª–¡•û‚¶‚á‚È‚©‚Á‚½(“G‹îorƒ}ƒX)ê‡
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecZ += 6f;
                    if (!_search.MovableTile.Contains(_hit.collider))
                    {
                        _search.MovableTile.Add(_hit.collider);
                        _hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    }
                    _search.Tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "‚Éi‚Ş‚±‚Æ‚ªo—ˆ‚Ü‚·");
                    /**************************************************/
                    //’Tõæ‚ÉŠl‚ê‚é‹î‚ª‚ ‚Á‚½ê‡
                    if (_pieceInfo.tag == "WhitePiece")
                    {
                        if (_hit.collider.gameObject.tag == "BlackPiece")
                        {
                            if (!_piece.GetablePieces.Contains(_hit.collider.gameObject))
                            {
                                _piece.GetablePieces.Add(_hit.collider.gameObject);
                                _hit.collider.gameObject.GetComponent<MeshRenderer>().material = _getable;
                            }
                            _piece.BlackPieces.Remove(_hit.collider.gameObject);
                            break;
                        }
                    }
                    else if (_pieceInfo.tag == "BlackPiece")
                    {
                        if (_hit.collider.gameObject.tag == "WhitePiece")
                        {
                            if (!_piece.GetablePieces.Contains(_hit.collider.gameObject))
                            {
                                _piece.GetablePieces.Add(_hit.collider.gameObject);
                                _hit.collider.gameObject.GetComponent<MeshRenderer>().material = _getable;
                            }
                            _piece.WhitePieces.Remove(_hit.collider.gameObject);
                            break;
                        }
                    }
                    /**************************************************/
                }
            }
            else
            {
                Debug.Log("Collider‚ª“–‚½‚Á‚Ä‚È‚¢");
            }
        }
        //Œã‚ë•ûŒü
        _vecX = 0f;
        _vecY = 15f;
        _vecZ = 6f;
        for (int j = 0; j < 7; j++)
        {
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(_vecX, _vecY, -_vecZ), Vector3.down, out _hit, 20))
            {
                //’Tõæ‚É–¡•û‚Ì‹î‚ª‚ ‚Á‚½ê‡
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    _search.ImmovablePieces.Add(_hit.collider.gameObject);
                    Debug.Log(_hit.collider.gameObject.name + "‚æ‚èæ‚É‚Í‚·‚·‚ß‚Ü‚¹‚ñ");
                    break;
                }
                //’Tõæ‚ª–¡•û‚¶‚á‚È‚©‚Á‚½(“G‹îorƒ}ƒX)ê‡
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecZ += 6f;
                    if (!_search.MovableTile.Contains(_hit.collider))
                    {
                        _search.MovableTile.Add(_hit.collider);
                        _hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    }
                    _search.Tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "‚Éi‚Ş‚±‚Æ‚ªo—ˆ‚Ü‚·");
                    /**************************************************/
                    //’Tõæ‚ÉŠl‚ê‚é‹î‚ª‚ ‚Á‚½ê‡
                    if (_pieceInfo.tag == "WhitePiece")
                    {
                        if (_hit.collider.gameObject.tag == "BlackPiece")
                        {
                            if (!_piece.GetablePieces.Contains(_hit.collider.gameObject))
                            {
                                _piece.GetablePieces.Add(_hit.collider.gameObject);
                                _hit.collider.gameObject.GetComponent<MeshRenderer>().material = _getable;
                            }
                            _piece.BlackPieces.Remove(_hit.collider.gameObject);
                            break;
                        }
                    }
                    else if (_pieceInfo.tag == "BlackPiece")
                    {
                        if (_hit.collider.gameObject.tag == "WhitePiece")
                        {
                            if (!_piece.GetablePieces.Contains(_hit.collider.gameObject))
                            {
                                _piece.GetablePieces.Add(_hit.collider.gameObject);
                                _hit.collider.gameObject.GetComponent<MeshRenderer>().material = _getable;
                            }
                            _piece.WhitePieces.Remove(_hit.collider.gameObject);
                            break;
                        }
                    }
                    /**************************************************/
                }
            }
            else
            {
                Debug.Log("Collider‚ª“–‚½‚Á‚Ä‚È‚¢");
            }
        }
        //¶•ûŒü
        _vecX = 6f;
        _vecY = 15f;
        _vecZ = 0f;
        for (int k = 0; k < 7; k++)
        {
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(-_vecX, _vecY, _vecZ), Vector3.down, out _hit, 20))
            {
                //’Tõæ‚É–¡•û‚Ì‹î‚ª‚ ‚Á‚½ê‡
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    _search.ImmovablePieces.Add(_hit.collider.gameObject);
                    Debug.Log(_hit.collider.gameObject.name + "‚æ‚èæ‚É‚Í‚·‚·‚ß‚Ü‚¹‚ñ");
                    break;
                }
                //’Tõæ‚ª–¡•û‚¶‚á‚È‚©‚Á‚½(“G‹îorƒ}ƒX)ê‡
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX += 6f;
                    if (!_search.MovableTile.Contains(_hit.collider))
                    {
                        _search.MovableTile.Add(_hit.collider);
                        _hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    }
                    _search.Tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "‚Éi‚Ş‚±‚Æ‚ªo—ˆ‚Ü‚·");
                    /**************************************************/
                    //’Tõæ‚ÉŠl‚ê‚é‹î‚ª‚ ‚Á‚½ê‡
                    if (_pieceInfo.tag == "WhitePiece")
                    {
                        if (_hit.collider.gameObject.tag == "BlackPiece")
                        {
                            if (!_piece.GetablePieces.Contains(_hit.collider.gameObject))
                            {
                                _piece.GetablePieces.Add(_hit.collider.gameObject);
                                _hit.collider.gameObject.GetComponent<MeshRenderer>().material = _getable;
                            }
                            _piece.BlackPieces.Remove(_hit.collider.gameObject);
                            break;
                        }
                    }
                    else if (_pieceInfo.tag == "BlackPiece")
                    {
                        if (_hit.collider.gameObject.tag == "WhitePiece")
                        {
                            if (!_piece.GetablePieces.Contains(_hit.collider.gameObject))
                            {
                                _piece.GetablePieces.Add(_hit.collider.gameObject);
                                _hit.collider.gameObject.GetComponent<MeshRenderer>().material = _getable;
                            }
                            _piece.WhitePieces.Remove(_hit.collider.gameObject);
                            break;
                        }
                    }
                    /**************************************************/
                }
            }
            else
            {
                Debug.Log("Collider‚ª“–‚½‚Á‚Ä‚È‚¢");
            }
        }
        //‰E•ûŒü
        _vecX = 6f;
        _vecY = 15f;
        _vecZ = 0f;
        for (int l = 0; l < 7; l++)
        {
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(_vecX, _vecY, _vecZ), Vector3.down, out _hit, 20))
            {
                //’Tõæ‚É–¡•û‚Ì‹î‚ª‚ ‚Á‚½ê‡
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    _search.ImmovablePieces.Add(_hit.collider.gameObject);
                    Debug.Log(_hit.collider.gameObject.name + "‚æ‚èæ‚É‚Í‚·‚·‚ß‚Ü‚¹‚ñ");
                    break;
                }
                //’Tõæ‚ª–¡•û‚¶‚á‚È‚©‚Á‚½(“G‹îorƒ}ƒX)ê‡
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX += 6f;
                    if (!_search.MovableTile.Contains(_hit.collider))
                    {
                        _search.MovableTile.Add(_hit.collider);
                        _hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
                    }
                    _search.Tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "‚Éi‚Ş‚±‚Æ‚ªo—ˆ‚Ü‚·");
                    /**************************************************/
                    //’Tõæ‚ÉŠl‚ê‚é‹î‚ª‚ ‚Á‚½ê‡
                    if (_pieceInfo.tag == "WhitePiece")
                    {
                        if (_hit.collider.gameObject.tag == "BlackPiece")
                        {
                            if (!_piece.GetablePieces.Contains(_hit.collider.gameObject))
                            {
                                _piece.GetablePieces.Add(_hit.collider.gameObject);
                                _hit.collider.gameObject.GetComponent<MeshRenderer>().material = _getable;
                            }
                            _piece.BlackPieces.Remove(_hit.collider.gameObject);
                            break;
                        }
                    }
                    else if (_pieceInfo.tag == "BlackPiece")
                    {
                        if (_hit.collider.gameObject.tag == "WhitePiece")
                        {
                            if (!_piece.GetablePieces.Contains(_hit.collider.gameObject))
                            {
                                _piece.GetablePieces.Add(_hit.collider.gameObject);
                                _hit.collider.gameObject.GetComponent<MeshRenderer>().material = _getable;
                            }
                            _piece.WhitePieces.Remove(_hit.collider.gameObject);
                            break;
                        }
                    }
                    /**************************************************/
                }
            }
            else
            {
                Debug.Log("Collider‚ª“–‚½‚Á‚Ä‚È‚¢");
            }
        }
        //ˆÚ“®”ÍˆÍˆÈŠO‚Ìƒ}ƒX‚ÌCollider‚ğoff‚É‚·‚éˆ—‚ğ‘‚­
        foreach (Collider col in _search.Tile)
        {
            col.enabled = false;
            Debug.Log(col + "‚ÌCollider‚ğoff‚É‚µ‚Ü‚·");
        }
    }
}
