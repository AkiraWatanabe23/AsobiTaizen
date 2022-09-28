using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectTile : MonoBehaviour
{
    GameManager _manager;
    PieceManager _piece;
    RaycastHit _hit;
    int[] _selectPieceCount = new int[6];
    [SerializeField] Text[] _countText = new Text[6];
    [SerializeField] LayerMask _tileLayer;
    [SerializeField] LayerMask _whiteLayer;
    [SerializeField] LayerMask _blackLayer;
    [SerializeField] Text _whereText;
    /// <summary> 配置する駒を選んだら再選択できないようにボタンの上にあるPanel </summary>
    [Tooltip("配置する駒の再選択不可")] Image _unselectable;
    SelectPhase _phase = SelectPhase.Piece;
    //駒を配置する時の位置修正(マスの位置に駒を置くと駒のColliderとマスのColliderがぶつかるため)
    [SerializeField] Vector3 _offset = Vector3.up;
    public GameObject SetPiece { get; set; }
    public int[] SelectPieceCount { get => _selectPieceCount; set => _selectPieceCount = value; }
    public Text WhereText { get => _whereText; set => _whereText = value; }

    // Start is called before the first frame update
    void Start()
    {
        _manager = GetComponent<GameManager>();
        _piece = GameObject.Find("Piece").GetComponent<PieceManager>();

        _unselectable = GameObject.Find("UnselectablePanel").GetComponent<Image>();
        for (int i = 0; i < SelectPieceCount.Length; i++)
        {
            SelectPieceCount[i] = 4;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SettingPiece();
        }

        for (int i = 0; i < 6; i++)
        {
            _countText[i].text = SelectPieceCount[i].ToString();
        }
    }

    public void SettingPiece()
    {
        if (_phase == SelectPhase.Tile)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float rayDistance = 100;

            //駒が既にあるマスには置けない
            if (Physics.Raycast(ray, out _hit, rayDistance, _whiteLayer))
            {
                Debug.Log("ここには置けない White");
                _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
            }
            else if (Physics.Raycast(ray, out _hit, rayDistance, _blackLayer))
            {
                Debug.Log("ここには置けない Black");
                _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
            }
            else if (Physics.Raycast(ray, out _hit, rayDistance, _tileLayer))
            {
                GameObject target = _hit.collider.gameObject;

                //deploy...配置する
                GameObject deployPiece = Instantiate(SetPiece, target.transform.position + _offset, SetPiece.transform.rotation);
                if (deployPiece.tag == "WhitePiece")
                {
                    _piece.WhitePieces.Add(deployPiece);
                }
                else if (deployPiece.tag == "BlackPiece")
                {
                    _piece.BlackPieces.Add(deployPiece);
                }
                PieceMove pieceInfo = deployPiece.GetComponent<PieceMove>();
                pieceInfo.SelectAssign();

                print($"{deployPiece.name} を {target.name} に配置した");
                SetPiece = null;
                WhereText.gameObject.SetActive(false);
                _unselectable.raycastTarget = false;
                _phase = SelectPhase.Piece;
                _manager.LineCount();
                SwitchTurn();
            }
        }
        else if (_phase == SelectPhase.Piece)
        {
            //gameObjectのSetActiveをif文で判定(boolを返す)
            if (WhereText.gameObject.activeSelf)
            {
                _phase = SelectPhase.Tile;
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
            _manager.Phase = GamePhase.Black;
            _manager.SwitchTurnWhite();
        }
        else if (GameManager.Player == 2)
        {
            GameManager.Player = 1;
            _manager.Phase = GamePhase.White;
            _manager.SwitchTurnBlack();
        }
    }
}
