using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectTile : MonoBehaviour
{
    GameManager _manager;
    PieceManager _piece;
    public GameObject _set;
    RaycastHit _hit;
    [SerializeField] LayerMask _tileLayer;

    // Start is called before the first frame update
    void Start()
    {
        _manager = GetComponent<GameManager>();
        _piece = GameObject.Find("Piece").GetComponent<PieceManager>();
    }

    public void SetPiece()
    {
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float _rayDistance = 100;

        if (Physics.Raycast(_ray, out _hit, _rayDistance, _tileLayer))
        {
            GameObject _target = _hit.collider.gameObject;

            GameObject _setPiece = Instantiate(_set, _target.transform.position + new Vector3(0, 0.5f), _target.transform.rotation);
            if (_setPiece.tag == "WhitePiece")
            {
                _piece._whitePieces.Add(_setPiece);
            }
            else if (_setPiece.tag == "BlackPiece")
            {
                _piece._blackPieces.Add(_setPiece);
            }
            PieceMove _pieceInfo = _setPiece.GetComponent<PieceMove>();
            _pieceInfo.SelectAssign();

            print($"{_setPiece.name} を {_target.name} に配置した");
        }
        
        _set = null;

        SwitchTurn();
    }

    /// <summary>
    /// ターン切り替え
    /// </summary>
    public void SwitchTurn()
    {
        if (GameManager._player == 1)
        {
            GameManager._player = 2;
            GameManager._phase = Phase.Black;
            _manager.SwitchTurnWhite();
        }
        else if (GameManager._player == 2)
        {
            GameManager._player = 1;
            GameManager._phase = Phase.White;
            _manager.SwitchTurnBlack();
        }
    }
}
