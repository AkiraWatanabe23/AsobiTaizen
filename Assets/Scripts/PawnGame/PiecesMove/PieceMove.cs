using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//↓マウスカーソルポジションの強制移動させる処理をするのに宣言する必要があるらしい
using System.Runtime.InteropServices;

/// <summary> 
/// 駒の移動に関するスクリプト
/// </summary>
public class PieceMove : MonoBehaviour, IPointerClickHandler
{
    [Tooltip("通常状態のマテリアル"), SerializeField] Material _normalMaterial;
    [Tooltip("移動状態のマテリアル"), SerializeField] Material _moveMaterial;
    Renderer _renderer;
    [Tooltip("白駒か黒駒かのenum"), SerializeField] PieceColor _color = PieceColor.White;
    [Tooltip("駒の種類のenum"), SerializeField] PieceType _type;
    [Tooltip("駒の状態のenum")] PieceState _state = PieceState.Normal;
    [SerializeField] Vector3 _offset = Vector3.up;
    //レイヤーマスク(InspectorのLayerから選択する)
    [SerializeField] LayerMask _tileLayer;
    [SerializeField] LayerMask _whiteLayer;
    [SerializeField] LayerMask _blackLayer;
    RaycastHit _hit;
    //移動可能範囲の探索
    MasuSearch _search;
    PieceManager _piece;
    GameManager _manager;
    GameObject _currentPieceTile;
    GameObject _movedPieceTile;
    /// <summary> 駒の移動回数 </summary>
    public int MoveCount { get; set; }
    public PieceType Type { get => _type; set => _type = value; }

    //extern...UnityやVisualStudioにはない機能(関数)をとってくる{訂正:外部ファイル(dllファイル)で定義されている関数や変数を使用する、という命令}
    //[DllImport("user32.dll")]...外のどのファイル(今回は[user32.dll])からとってくるのか
    //SetCursorPos(関数)...指定したファイル内のどの機能(関数)を使うのか
    //以下2行はセットで書かないとコンパイルエラー発生
    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int x, int y);

    /// <summary> 
    /// マウスクリックが行われた(どのマウスクリックでも実行される)時の処理
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        var go = eventData.pointerCurrentRaycast.gameObject;
        print($"{ name } を選んだ");
        go.GetComponent<PieceMove>().ChangeState();

        //_movedPieceTile = null で駒の再選択を出来るようにする
        if (_state == PieceState.Normal && _currentPieceTile == _movedPieceTile)
        {
            _movedPieceTile = null;
        }
    }

    void Start()
    {
        _manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _search = GameObject.Find("Board,Tile").GetComponent<MasuSearch>();
        _piece = GameObject.Find("Piece").GetComponent<PieceManager>();
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
            if (_state == PieceState.Move)
            {
                //移動処理
                if (Move()) /*←return の値が true だったら[Move()はbool型の関数]*/
                {
                    //移動状態→通常状態
                    ChangeState();
                }
            }
        }
        //右クリックで非選択状態に変更
        else if (Input.GetMouseButtonDown(1) && _state == PieceState.Move)
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
            _state = PieceState.Normal;
            _renderer.material = _normalMaterial;
            _search.MovableTile.Clear();
            _search.Piece = null;
            _search.PieceInfo = null;
        }
    }

    /// <summary> 移動するマス(または奪う駒)をマウスクリックで選び、その位置に移動する </summary>
    public bool Move()
    {
        //マウスの位置を取得し、Rayに代入
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float rayDistance = 100;

        //白が黒の駒を奪う処理
        if (Physics.Raycast(ray, out _hit, rayDistance, _blackLayer))
        {
            GameObject target = _hit.collider.gameObject;

            if (gameObject.tag == "WhitePiece")
            {
                Destroy(target);
                transform.position = target.transform.position;

                PhaseChange(target);
                SetCursorPos(Screen.width / 2, Screen.height / 2);
                GameManager.Player = GameManager.PLAYER_TWO;
                _manager.Phase = GamePhase.Black;

                print($"駒は {target.name} をとった");

                if (Physics.Raycast(ray, out RaycastHit hitTile, rayDistance, _tileLayer))
                {
                    GameObject _hitTile = hitTile.collider.gameObject;
                    print($"駒は {_hitTile.name} に移動した");
                }
                _manager.GetPiece = target.name;
                return true;
            }
        }
        //黒が白の駒を奪う処理
        else if (Physics.Raycast(ray, out _hit, rayDistance, _whiteLayer))
        {
            GameObject target = _hit.collider.gameObject;

            if (gameObject.tag == "BlackPiece")
            {
                Destroy(target);
                transform.position = target.transform.position;

                PhaseChange(target);
                SetCursorPos(Screen.width / 2, Screen.height / 2);
                GameManager.Player = GameManager.PLAYER_ONE;
                _manager.Phase = GamePhase.White;

                print($"駒は {target.name} をとった");

                if (Physics.Raycast(ray, out RaycastHit hitTile, rayDistance, _tileLayer))
                {
                    GameObject _hitTile = hitTile.collider.gameObject;
                    print($"駒は {_hitTile.name} に移動した");
                }

                if (Physics.Raycast(gameObject.transform.position, Vector3.down, out _hit, 10))
                {
                    _movedPieceTile = _hit.collider.gameObject;
                }
                _manager.GetPiece = target.name;
                return true;
            }
        }
        //白黒共通の移動処理
        else if (Physics.Raycast(ray, out _hit, rayDistance, _tileLayer))
        {
            foreach (var i in _search.MovableTile)
            {
                GameObject target = _hit.collider.gameObject;
                if (target == i.gameObject)
                {
                    transform.position = target.transform.position + _offset;

                    PhaseChange(target);
                    SetCursorPos(Screen.width / 2, Screen.height / 2);
                    //ターン切り替え
                    if (gameObject.tag == "WhitePiece")
                    {
                        GameManager.Player = GameManager.PLAYER_TWO;
                        _manager.Phase = GamePhase.Black;
                    }
                    else if (gameObject.tag == "BlackPiece")
                    {
                        GameManager.Player = GameManager.PLAYER_ONE;
                        _manager.Phase = GamePhase.White;
                    }

                    print($"駒は {target.name} に移動した");
                }
                else
                {
                    Debug.Log("指定したマスには動けません");
                }
            }
            _movedPieceTile = _hit.collider.gameObject;
            return true;
        }
        return false;
    }

    /// <summary> 
    /// マウスクリックをした(駒を選んだ、動かした)時に実行される処理
    /// </summary>
    public void ChangeState() //駒を右クリックをすると選択状態→通常状態にできる
    {
        //通常状態→選択状態(白)
        if (_state == PieceState.Normal && _color == PieceColor.White && _manager.Phase == GamePhase.White && _currentPieceTile != _movedPieceTile)
        {
            if (Physics.Raycast(gameObject.transform.position, Vector3.down, out _hit, 10))
            {
                _currentPieceTile = _movedPieceTile = _hit.collider.gameObject;
            }
            _state = PieceState.Move;
            _renderer.material = _moveMaterial;
            _search.Piece = this;
            _search.PieceInfo = gameObject;
            _piece.WhitePieces.Remove(gameObject);
        }
        //通常状態→選択状態(黒)
        else if (_state == PieceState.Normal && _color == PieceColor.Black && _manager.Phase == GamePhase.Black && _currentPieceTile != _movedPieceTile)
        {
            if (Physics.Raycast(gameObject.transform.position, Vector3.down, out _hit, 10))
            {
                _currentPieceTile = _movedPieceTile = _hit.collider.gameObject;
            }
            _state = PieceState.Move;
            _renderer.material = _moveMaterial;
            _search.Piece = this;
            _search.PieceInfo = gameObject;
            _piece.BlackPieces.Remove(gameObject);
        }
        //選択状態→通常状態(駒が移動した後の処理)
        else if (_state == PieceState.Move)
        {
            //駒の移動回数を加算する(ポーンの移動制限用)
            if (_currentPieceTile != _movedPieceTile && _movedPieceTile.tag == "Tile")
            {
                MoveCount++;
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
                _movedPieceTile = _hit.collider.gameObject;
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
            _state = PieceState.Normal;
            _renderer.material = _normalMaterial;
            _search.MovableTile.Clear();
            _search.Piece = null;
            _search.PieceInfo = null;
        }
    }

    /// <summary>
    /// ターン表示の切り替え、駒の選択状態→通常状態
    /// </summary>
    /// <param name="_target"></param>
    public void PhaseChange(GameObject _target)
    {
        switch (_color)
        {
            case PieceColor.White:
                _manager.SwitchTurnWhite();
                if (_target.tag == "WhitePiece")
                {
                    _state = PieceState.Normal;
                }
                break;

            case PieceColor.Black:
                _manager.SwitchTurnBlack();
                if (_target.tag == "BlackPiece")
                {
                    _state = PieceState.Normal;
                }
                break;
        }
    }

    /// <summary>
    /// 駒を配置した時に動かせる状態にする(変数にスクリプトを直接代入する)
    /// </summary>
    public void SelectAssign()
    {
        _search = GameObject.Find("Board,Tile").GetComponent<MasuSearch>();
        _piece = GameObject.Find("Piece").GetComponent<PieceManager>();
    }

    /// <summary> 通常状態、選択状態 </summary>
    public enum PieceState
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