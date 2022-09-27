using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 駒の移動に関するもの(パズル版)
/// </summary>
public class PuzzlePiece : MonoBehaviour, IPointerClickHandler
{
    [Tooltip("通常状態のマテリアル"), SerializeField] Material _normalMaterial;
    [Tooltip("移動状態のマテリアル"), SerializeField] Material _moveMaterial;
    Renderer _renderer;
    [Tooltip("白駒か黒駒かのenum")] public PieceColor color = PieceColor.White;
    [Tooltip("駒の状態のenum")] public Status status = Status.Normal;
    [Tooltip("駒の種類のenum")] public PieceType type;
    RaycastHit _hit;
    [Header("移動後の位置調整")]
    [SerializeField] Vector3 _movedOffset = Vector3.up;
    [SerializeField] Vector3 _gotOffset = Vector3.down;
    //レイヤーマスク(InspectorのLayerから選択する)
    [SerializeField] LayerMask _tileLayer;
    [SerializeField] LayerMask _whiteLayer;
    [SerializeField] LayerMask _blackLayer;
    //移動可能範囲の探索
    [SerializeField] MasuSearch _search;
    [SerializeField] PieceManager _piece;
    [Tooltip("駒の移動回数")] public int moveCount = 0;
    public GameObject currentPieceTile;
    public GameObject movedPieceTile;

    /// <summary>
    /// オブジェクトをクリックした時の処理
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        var go = eventData.pointerCurrentRaycast.gameObject;

        print($"{ name } を選んだ");
        if (go.gameObject.tag == "WhitePiece")
        {
            go.GetComponent<PuzzlePiece>().ChangeState();
        }

        //_movedPieceTile = null で駒の再選択を出来るようにする
        if (status == Status.Normal && currentPieceTile == movedPieceTile)
        {
            movedPieceTile = null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();

        if (Physics.Raycast(gameObject.transform.position, Vector3.down, out _hit, 10))
        {
            currentPieceTile = _hit.collider.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //左クリックで選択、移動
        if (Input.GetMouseButtonDown(0))
        {
            //駒が移動状態になっていたら
            if (status == Status.Move)
            {
                //移動処理
                if (Move()) /*←return の値が true だったら*/
                {
                    //移動状態→通常状態
                    ChangeState();
                }
            }
        }
        //右クリックで非選択状態に変更
        else if (Input.GetMouseButtonDown(1) && status == Status.Move)
        {
            Debug.Log("選び直し");

            //マスを元の状態に戻す
            for (int i = 0; i < _search.MovableTile.Count; i++)
            {
                if (_search.MovableTile[i].tag == "Tile")
                {
                    _search.MovableTile[i].GetComponent<MeshRenderer>().enabled = false;
                    _search.Tile.Add(_search.MovableTile[i]);
                }
            }
            foreach (var tiles in _search.Tile)
            {
                tiles.GetComponent<Collider>().enabled = true;
            }

            foreach (var piece in _search.ImmovablePieces)
            {
                piece.GetComponent<Collider>().enabled = true;
            }
            _search.ImmovablePieces.Clear();

            if (gameObject.tag == "WhitePiece")
            {
                foreach (var pieces in _piece.WhitePieces)
                {
                    pieces.GetComponent<Collider>().enabled = true;
                }
                _piece.WhitePieces.Add(gameObject);
            }
            else if (gameObject.tag == "BlackPiece")
            {
                foreach (var pieces in _piece.BlackPieces)
                {
                    pieces.GetComponent<Collider>().enabled = true;
                }
                _piece.BlackPieces.Add(gameObject);
            }

            //獲れる駒を獲らなかった場合にListに戻す
            if (_piece.GetablePieces != null)
            {
                _piece.UnGetPiece();
            }

            //駒の状態をもとに戻す
            status = Status.Normal;
            _renderer.material = _normalMaterial;
            _search.MovableTile.Clear();
            _search.piece = null;
            _search.pieceInfo = null;
        }
    }

    public bool Move()
    {
        //マウスの位置を取得し、Rayに代入
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float rayDistance = 100;

        //黒駒を獲るときの処理
        if (Physics.Raycast(ray, out _hit, rayDistance, _blackLayer))
        {
            GameObject target = _hit.collider.gameObject;

            if (target.tag == "BlackPiece")
            {
                PuzzleManager._getPieceCount++;
                Destroy(target);
            }

            this.transform.position = target.transform.position + _gotOffset;
            GameManager.Player = GameManager.Player_Two;
            GameManager.phase = Phase.Black;

            print($"駒は {target.name} をとった");

            if (Physics.Raycast(ray, out RaycastHit hitTile, rayDistance, _tileLayer))
            {
                GameObject _hitTile = hitTile.collider.gameObject;
                print($"駒は {_hitTile.name} に移動した");
            }

            PuzzleManager.moveCount--;

            return true;
        }
        //駒を移動する処理
        else if (Physics.Raycast(ray, out _hit, rayDistance, _tileLayer))
        {
            foreach (var i in _search.MovableTile)
            {
                GameObject _target = _hit.collider.gameObject;
                if (_target == i.gameObject)
                {
                    this.transform.position = _target.transform.position + _movedOffset;
                    GameManager.Player = GameManager.Player_Two;
                    GameManager.phase = Phase.Black;

                    print($"駒は {_target.name} に移動した");
                }
                else
                {
                    Debug.Log("指定したマスには動けません");
                }
            }
            movedPieceTile = _hit.collider.gameObject;

            PuzzleManager.moveCount--;
            Debug.Log(moveCount);

            return true;
        }
        return false;
    }

    public void ChangeState()
    {
        //通常状態→選択状態(白)
        if (status == Status.Normal && color == PieceColor.White && currentPieceTile != movedPieceTile)
        {
            if (Physics.Raycast(gameObject.transform.position, Vector3.down, out _hit, 10))
            {
                currentPieceTile = movedPieceTile = _hit.collider.gameObject;
            }
            status = Status.Move;
            _renderer.material = _moveMaterial;
            _search.puzzle = this;
            _search.pieceInfo = gameObject;
            _piece.WhitePieces.Remove(gameObject);
        }
        //通常状態→選択状態(黒)
        else if (status == Status.Normal && color == PieceColor.Black && currentPieceTile != movedPieceTile)
        {
            if (Physics.Raycast(gameObject.transform.position, Vector3.down, out _hit, 10))
            {
                currentPieceTile = movedPieceTile = _hit.collider.gameObject;
            }
            status = Status.Move;
            _renderer.material = _moveMaterial;
            _search.puzzle = this;
            _search.pieceInfo = gameObject;
            _piece.BlackPieces.Remove(gameObject);
        }
        //選択状態→通常状態(駒が移動した後の処理)
        else if (status == Status.Move)
        {
            //駒の移動回数を加算する(ポーンの移動制限用)
            if (currentPieceTile != movedPieceTile && movedPieceTile.tag == "Tile")
            {
                moveCount++;
            }

            //マスを元の状態に戻す
            for (int i = 0; i < _search.MovableTile.Count; i++)
            {
                if (_search.MovableTile[i].tag == "Tile")
                {
                    _search.MovableTile[i].GetComponent<MeshRenderer>().enabled = false;
                    _search.Tile.Add(_search.MovableTile[i]);
                }
            }
            foreach (var tiles in _search.Tile)
            {
                tiles.GetComponent<Collider>().enabled = true;
            }

            foreach (var piece in _search.ImmovablePieces)
            {
                piece.GetComponent<Collider>().enabled = true;
            }
            _search.ImmovablePieces.Clear();

            //ColliderOffにした駒をもとに戻す
            foreach (var pieces in _piece.WhitePieces)
            {
                if (pieces != null)
                {
                    pieces.GetComponent<Collider>().enabled = true;
                }
            }
            foreach (var pieces in _piece.BlackPieces)
            {
                if (pieces != null)
                {
                    pieces.GetComponent<Collider>().enabled = true;
                }
            }
            if (gameObject.tag == "WhitePiece")
            {
                _piece.WhitePieces.Add(gameObject);
            }
            else if (gameObject.tag == "BlackPiece")
            {
                _piece.BlackPieces.Add(gameObject);
            }

            //駒を獲った時に移動したマスを_movedPieceTileに代入する
            if (Physics.Raycast(gameObject.transform.position, Vector3.down, out _hit, 10))
            {
                movedPieceTile = _hit.collider.gameObject;
            }
            else
            {
                Debug.Log("none");
            }

            //獲れる駒を獲らなかった場合にListに戻す
            if (_piece.GetablePieces != null)
            {
                _piece.UnGetPiece();
            }

            //駒の状態をもとに戻す
            status = Status.Normal;
            _renderer.material = _normalMaterial;
            _search.MovableTile.Clear();
            _search.piece = null;
            _search.pieceInfo = null;
            _search.puzzle = null;
        }
    }

    /// <summary> 通常状態、選択状態 </summary>
    public enum Status
    {
        Normal,
        Move,
    }

    /// <summary> 白駒or黒駒 </summary>
    public enum PieceColor
    {
        White = 0,
        Black = 1,
    }

    /// <summary> 駒の種類 </summary>
    public enum PieceType
    {
        Pawn = 1,
        Knight,
        Bishop,
        Rook,
        Queen,
        King,
    }
}
