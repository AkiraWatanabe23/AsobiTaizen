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
    /// <summary> �z�u������I�񂾂�đI���ł��Ȃ��悤�Ƀ{�^���̏�ɂ���Panel </summary>
    [Tooltip("�z�u�����̍đI��s��")] Image _unselectable;
    SelectPhase _phase = SelectPhase.Piece;
    //���z�u���鎞�̈ʒu�C��(�}�X�̈ʒu�ɋ��u���Ƌ��Collider�ƃ}�X��Collider���Ԃ��邽��)
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

            //����ɂ���}�X�ɂ͒u���Ȃ�
            if (Physics.Raycast(ray, out _hit, rayDistance, _whiteLayer))
            {
                Debug.Log("�����ɂ͒u���Ȃ� White");
                _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
            }
            else if (Physics.Raycast(ray, out _hit, rayDistance, _blackLayer))
            {
                Debug.Log("�����ɂ͒u���Ȃ� Black");
                _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
            }
            else if (Physics.Raycast(ray, out _hit, rayDistance, _tileLayer))
            {
                GameObject target = _hit.collider.gameObject;

                //deploy...�z�u����
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

                print($"{deployPiece.name} �� {target.name} �ɔz�u����");
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
            //gameObject��SetActive��if���Ŕ���(bool��Ԃ�)
            if (WhereText.gameObject.activeSelf)
            {
                _phase = SelectPhase.Tile;
            }
        }
    }

    /// <summary>
    /// �^�[���؂�ւ�
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
