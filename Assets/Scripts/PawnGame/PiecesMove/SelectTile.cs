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
    public int[] _selectPieceCount = new int[6];
    [SerializeField] Text[] _countText = new Text[6];
    [SerializeField] LayerMask _tileLayer;
    [SerializeField] LayerMask _whiteLayer;
    [SerializeField] LayerMask _blackLayer;
    [SerializeField] public Text _whereText;
    public SelectPhase _phase = SelectPhase.Piece;
    //���z�u���鎞�̈ʒu�C��
    [SerializeField] Vector3 _offset = Vector3.up;

    // Start is called before the first frame update
    void Start()
    {
        _manager = GetComponent<GameManager>();
        _piece = GameObject.Find("Piece").GetComponent<PieceManager>();
        for (int i = 0; i < _selectPieceCount.Length; i++)
        {
            _selectPieceCount[i] = 4;
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
            _countText[i].text = _selectPieceCount[i].ToString();
        }
    }

    public void SetPiece()
    {
        if (_phase == SelectPhase.Tile)
        {
            Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float _rayDistance = 100;

            //���I�񂾂炻���ɂ͒u���Ȃ�
            if (Physics.Raycast(_ray, out _hit, _rayDistance, _whiteLayer))
            {
                Debug.Log("�����ɂ͒u���Ȃ� White");
                _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
            }
            else if (Physics.Raycast(_ray, out _hit, _rayDistance, _blackLayer))
            {
                Debug.Log("�����ɂ͒u���Ȃ� Black");
                _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
            }
            else if (Physics.Raycast(_ray, out _hit, _rayDistance, _tileLayer))
            {
                GameObject target = _hit.collider.gameObject;

                GameObject _setPiece = Instantiate(_set, target.transform.position + _offset, _set.transform.rotation);
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

                print($"{_setPiece.name} �� {target.name} �ɔz�u����");
                _set = null;
                _whereText.gameObject.SetActive(false);
                _phase = SelectPhase.Piece;
                _manager.LineCount();
                SwitchTurn();
            }
        }
        else if (_phase == SelectPhase.Piece)
        {
            //Object��SetActive��if���Ŕ���(bool��Ԃ�)
            if (_whereText.gameObject.activeSelf)
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
