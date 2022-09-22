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
    [Tooltip("白駒か黒駒かのenum")] public PieceColor _color = PieceColor.White;
    [Tooltip("駒の状態のenum")] public Status _status = Status.Normal;
    [Tooltip("駒の種類のenum")] public PieceType _type;
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
    [Tooltip("駒の移動回数")] public int _moveCount = 0;
    public GameObject _currentPieceTile;
    public GameObject _movedPieceTile;

    /// <summary>
    /// オブジェクトをクリックした時の処理
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        var go = eventData.pointerCurrentRaycast.gameObject;

        print($"{ name } を選んだ");
        go.GetComponent<PuzzlePiece>().ChangeState();

        //_movedPieceTile = null で駒の再選択を出来るようにする
        if (_status == Status.Normal && _currentPieceTile == _movedPieceTile)
        {
            _movedPieceTile = null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();

        if (Physics.Raycast(gameObject.transform.position, Vector3.down, out _hit, 10))
        {
            _currentPieceTile = _hit.collider.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //左クリックで選択、移動
        if (Input.GetMouseButtonDown(0))
        {
            //駒が移動状態になっていたら
            if (_status == Status.Move)
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
        else if (Input.GetMouseButtonDown(1) && _status == Status.Move)
        {
            Debug.Log("選び直し");

            //マスを元の状態に戻す
            for (int i = 0; i < _search._movableTile.Count; i++)
            {
                if (_search._movableTile[i].tag == "Tile")
                {
                    _search._movableTile[i].GetComponent<MeshRenderer>().enabled = false;
                    _search._tile.Add(_search._movableTile[i]);
                }
            }
            foreach (var tiles in _search._tile)
            {
                tiles.GetComponent<Collider>().enabled = true;
            }

            foreach (var piece in _search._immovablePieces)
            {
                piece.GetComponent<Collider>().enabled = true;
            }
            _search._immovablePieces.Clear();

            if (gameObject.tag == "WhitePiece")
            {
                foreach (var pieces in _piece._whitePieces)
                {
                    pieces.GetComponent<Collider>().enabled = true;
                }
                _piece._whitePieces.Add(gameObject);
            }
            else if (gameObject.tag == "BlackPiece")
            {
                foreach (var pieces in _piece._blackPieces)
                {
                    pieces.GetComponent<Collider>().enabled = true;
                }
                _piece._blackPieces.Add(gameObject);
            }

            //獲れる駒を獲らなかった場合にListに戻す
            if (_piece._getablePieces != null)
            {
                _piece.UnGetPiece();
            }

            //駒の状態をもとに戻す
            _status = Status.Normal;
            _renderer.material = _normalMaterial;
            _search._movableTile.Clear();
            _search._piece = null;
            _search._pieceInfo = null;
        }
    }

    public bool Move()
    {
        //マウスの位置を取得し、Rayに代入
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float _rayDistance = 100;

        //白番目線の駒の移動
        //白番目線のRayの処理(駒を奪う場合)
        if (Physics.Raycast(_ray, out _hit, _rayDistance, _blackLayer))
        {
            GameObject _target = _hit.collider.gameObject;
            //int _targetScore = _target.GetComponent<PieceMove>()._getScore; //とった駒が持っている_getScoreを取得

            if (_target.tag == "BlackPiece")
            {
                //白のスコアを加算
                //GameManager._scoreWhite += _targetScore;
                PuzzleManager._getPieceCount++;
                Destroy(_target);
            }

            this.transform.position = _target.transform.position + _gotOffset;
            GameManager._player = GameManager.Player_Two;
            GameManager._phase = Phase.Black;

            print($"駒は {_target.name} をとった");

            if (Physics.Raycast(_ray, out RaycastHit hitTile, _rayDistance, _tileLayer))
            {
                GameObject _hitTile = hitTile.collider.gameObject;
                print($"駒は {_hitTile.name} に移動した");
            }

            PuzzleManager._moveCount--;

            return true;
        }
        //白番目線のRayの処理(移動処理)
        else if (Physics.Raycast(_ray, out _hit, _rayDistance, _tileLayer))
        {
            foreach (var i in _search._movableTile)
            {
                GameObject _target = _hit.collider.gameObject;
                if (_target == i.gameObject)
                {
                    this.transform.position = _target.transform.position + _movedOffset;
                    GameManager._player = GameManager.Player_Two;
                    GameManager._phase = Phase.Black;

                    print($"駒は {_target.name} に移動した");
                }
                else
                {
                    Debug.Log("指定したマスには動けません");
                }
            }
            _movedPieceTile = _hit.collider.gameObject;

            PuzzleManager._moveCount--;
            Debug.Log(_moveCount);

            return true;
        }
        //黒番目線の駒の移動
        //黒番目線のRayの処理(駒を奪う場合)
        //if (Physics.Raycast(_ray2, out _hit, _rayDistance, _whiteLayer))
        //{
        //    GameObject _target = _hit.collider.gameObject;
        //    int _targetScore = _target.GetComponent<PieceMove>()._getScore; //とった駒が持っている_getScoreを取得

        //    if (_target.tag == "WhitePiece")
        //    {
        //        //黒のスコアを加算
        //        GameManager._scoreBlack += _targetScore;
        //        //盤上にある駒のカウントを減らして、駒を破壊する
        //        GameManager._wPieceCount--;
        //        Destroy(_target);
        //    }

        //    this.transform.position = _target.transform.position;
        //    GameManager._player = GameManager.Player_One;
        //    GameManager._phase = Phase.White;

        //    print($"駒は {_target.name} をとった");

        //    if (Physics.Raycast(_ray, out RaycastHit hitTile, _rayDistance, _tileLayer))
        //    {
        //        GameObject _hitTile = hitTile.collider.gameObject;
        //        print($"駒は {_hitTile.name} に移動した");
        //    }

        //    if (Physics.Raycast(gameObject.transform.position, Vector3.down, out _hit, 10))
        //    {
        //        _movedPieceTile = _hit.collider.gameObject;
        //    }
        //    return true;
        //}
        //黒番目線のRayの処理(移動処理)
        //else if (Physics.Raycast(_ray2, out _hit, _rayDistance, _tileLayer))
        //{
        //    foreach (var i in _search._movableTile)
        //    {
        //        GameObject _target = _hit.collider.gameObject;
        //        if (_target == i.gameObject)
        //        {
        //            this.transform.position = _target.transform.position + _offset;
        //            GameManager._player = GameManager.Player_One;
        //            GameManager._phase = Phase.White;

        //            print($"駒は {_target.name} に移動した");
        //        }
        //        else
        //        {
        //            Debug.Log("指定したマスには動けません");
        //        }
        //    }
        //    _movedPieceTile = _hit.collider.gameObject;
        //    return true;
        //}
        return false;
    }

    public void ChangeState()
    {
        //通常状態→選択状態(白)
        if (_status == Status.Normal && _color == PieceColor.White && _currentPieceTile != _movedPieceTile)
        {
            if (Physics.Raycast(gameObject.transform.position, Vector3.down, out _hit, 10))
            {
                _currentPieceTile = _movedPieceTile = _hit.collider.gameObject;
            }
            _status = Status.Move;
            _renderer.material = _moveMaterial;
            _search._puzzle = this;
            _search._pieceInfo = gameObject;
            _piece._whitePieces.Remove(gameObject);
        }
        //通常状態→選択状態(黒)
        else if (_status == Status.Normal && _color == PieceColor.Black && _currentPieceTile != _movedPieceTile)
        {
            if (Physics.Raycast(gameObject.transform.position, Vector3.down, out _hit, 10))
            {
                _currentPieceTile = _movedPieceTile = _hit.collider.gameObject;
            }
            _status = Status.Move;
            _renderer.material = _moveMaterial;
            _search._puzzle = this;
            _search._pieceInfo = gameObject;
            _piece._blackPieces.Remove(gameObject);
        }
        //選択状態→通常状態(駒が移動した後の処理)
        else if (_status == Status.Move)
        {
            //駒の移動回数を加算する(ポーンの移動制限用)
            if (_currentPieceTile != _movedPieceTile && _movedPieceTile.tag == "Tile")
            {
                _moveCount++;
            }

            //マスを元の状態に戻す
            for (int i = 0; i < _search._movableTile.Count; i++)
            {
                if (_search._movableTile[i].tag == "Tile")
                {
                    _search._movableTile[i].GetComponent<MeshRenderer>().enabled = false;
                    _search._tile.Add(_search._movableTile[i]);
                }
            }
            foreach (var tiles in _search._tile)
            {
                tiles.GetComponent<Collider>().enabled = true;
            }

            foreach (var piece in _search._immovablePieces)
            {
                piece.GetComponent<Collider>().enabled = true;
            }
            _search._immovablePieces.Clear();

            //ColliderOffにした駒をもとに戻す
            foreach (var pieces in _piece._whitePieces)
            {
                if (pieces != null)
                {
                    pieces.GetComponent<Collider>().enabled = true;
                }
            }
            foreach (var pieces in _piece._blackPieces)
            {
                if (pieces != null)
                {
                    pieces.GetComponent<Collider>().enabled = true;
                }
            }
            if (gameObject.tag == "WhitePiece")
            {
                _piece._whitePieces.Add(gameObject);
            }
            else if (gameObject.tag == "BlackPiece")
            {
                _piece._blackPieces.Add(gameObject);
            }

            //駒を獲った時に移動したマスを_movedPieceTileに代入する
            if (Physics.Raycast(gameObject.transform.position, Vector3.down, out _hit, 10))
            {
                _movedPieceTile = _hit.collider.gameObject;
            }
            else
            {
                Debug.Log("none");
            }

            //獲れる駒を獲らなかった場合にListに戻す
            if (_piece._getablePieces != null)
            {
                _piece.UnGetPiece();
            }

            //駒の状態をもとに戻す
            _status = Status.Normal;
            _renderer.material = _normalMaterial;
            _search._movableTile.Clear();
            _search._piece = null;
            _search._pieceInfo = null;
            _search._puzzle = null;
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
