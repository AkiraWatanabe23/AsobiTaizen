using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectTile : MonoBehaviour
{
    GameManager _manager;
    PieceManager _piece;
    public GameObject setPiece;
    RaycastHit _hit;
    public int[] SelectPieceCount = new int[6];
    [SerializeField] Text[] _countText = new Text[6];
    [SerializeField] LayerMask _tileLayer;
    [SerializeField] LayerMask _whiteLayer;
    [SerializeField] LayerMask _blackLayer;
    [SerializeField] public Text _whereText;
    public SelectPhase phase = SelectPhase.Piece;
    //駒を配置する時の位置修正
    [SerializeField] Vector3 _offset = Vector3.up;

    // Start is called before the first frame update
    void Start()
    {
        _manager = GetComponent<GameManager>();
        _piece = GameObject.Find("Piece").GetComponent<PieceManager>();
        for (int i = 0; i < SelectPieceCount.Length; i++)
        {
            SelectPieceCount[i] = 4;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetPiece();
        }

        for (int i = 0; i < 6; i++)
        {
            _countText[i].text = SelectPieceCount[i].ToString();
        }
    }

    public void SetPiece()
    {
        if (phase == SelectPhase.Tile)
        {
            Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float _rayDistance = 100;

            //駒を選んだらそこには置けない
            if (Physics.Raycast(_ray, out _hit, _rayDistance, _whiteLayer))
            {
                Debug.Log("ここには置けない White");
                _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
            }
            else if (Physics.Raycast(_ray, out _hit, _rayDistance, _blackLayer))
            {
                Debug.Log("ここには置けない Black");
                _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
            }
            else if (Physics.Raycast(_ray, out _hit, _rayDistance, _tileLayer))
            {
                GameObject target = _hit.collider.gameObject;

                GameObject _setPiece = Instantiate(setPiece, target.transform.position + _offset, setPiece.transform.rotation);
                if (_setPiece.tag == "WhitePiece")
                {
                    _piece.WhitePieces.Add(_setPiece);
                }
                else if (_setPiece.tag == "BlackPiece")
                {
                    _piece.BlackPieces.Add(_setPiece);
                }
                PieceMove _pieceInfo = _setPiece.GetComponent<PieceMove>();
                _pieceInfo.SelectAssign();

                print($"{_setPiece.name} を {target.name} に配置した");
                setPiece = null;
                _whereText.gameObject.SetActive(false);
                phase = SelectPhase.Piece;
                _manager.LineCount();
                SwitchTurn();
            }
        }
        else if (phase == SelectPhase.Piece)
        {
            //ObjectのSetActiveをif文で判定(boolを返す)
            if (_whereText.gameObject.activeSelf)
            {
                phase = SelectPhase.Tile;
            }
        }
    }

    /// <summary>
    /// ターン切り替え
    /// </summary>
    public void SwitchTurn()
    {
        if (GameManager.Player == 1)
        {
            GameManager.Player = 2;
            GameManager.phase = Phase.Black;
            _manager.SwitchTurnWhite();
        }
        else if (GameManager.Player == 2)
        {
            GameManager.Player = 1;
            GameManager.phase = Phase.White;
            _manager.SwitchTurnBlack();
        }
    }
}
