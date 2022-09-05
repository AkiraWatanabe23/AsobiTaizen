using System;
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
    [Tooltip("黒番目線のカメラ")] public static Camera _camera;
    [Tooltip("通常状態のマテリアル"), SerializeField] Material _normalMaterial;
    [Tooltip("移動状態のマテリアル"), SerializeField] Material _moveMaterial;
    Renderer _renderer;
    [Tooltip("白駒か黒駒かのenum")] public PieceColor _color = PieceColor.White;
    [Tooltip("駒の状態のenum")] public Status _status = Status.Normal;
    [Tooltip("駒の種類のenum")] public PieceType _type;
    RaycastHit _hit;
    [SerializeField] Vector3 _offset = Vector3.up;
    //レイヤーマスク(InspectorのLayerから選択する)
    [SerializeField] LayerMask _tileLayer;
    [SerializeField] LayerMask _whiteLayer;
    [SerializeField] LayerMask _blackLayer;
    //駒の得点(Inspectorで設定)
    [SerializeField] public int _getScore;
    //移動可能範囲の探索
    [SerializeField] MasuSearch _search;
    [SerializeField] PieceManager _piece;
    public int _moveCount = 0;
    GameObject _currentPieceTile;
    public GameObject _movedPieceTile;
    [SerializeField] Promotion _promQ;
    [SerializeField] Promotion _promR;
    [SerializeField] Promotion _promB;
    [SerializeField] Promotion _promK;

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
        var piece = go.GetComponent<PieceMove>();

        print($"{ name } を選んだ");
        piece.ChangeState();
    }

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _camera = GameObject.Find("Camera(black)").GetComponent<Camera>();
        /*↑enabled...オブジェクトの指定した[コンポーネント(今回はImage)]のアクティブ、非アクティブを変更する
         *  SetActive(false)でオブジェクトをとってこれないのを回避する*/
    }

    // Update is called once per frame
    void Update()
    {
        //左クリックが行われた場合の処理
        if (Input.GetMouseButtonDown(0))
        {
            //駒が移動状態になっていたら
            if (_status == Status.Move)
            {
                //移動処理
                if (Move())
                {
                    //移動状態→通常状態
                    ChangeState();
                }
            }
        }
    }

    public bool Move()
    {
        //マウスの位置を取得し、Rayに代入
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Ray _ray2 = _camera.ScreenPointToRay(Input.mousePosition);
        float _rayDistance = 100;

        //白番目線の駒の移動
        //白番目線のRayの処理(駒を奪う場合)
        if (Physics.Raycast(_ray, out _hit, _rayDistance, _blackLayer))
        {
            GameObject _target = _hit.collider.gameObject;
            int _targetScore = _target.GetComponent<PieceMove>()._getScore; //とった駒が持っている_getScoreを取得

            if (_target.tag == "BlackPiece")
            {
                //白のスコアを加算
                GameManager._scoreWhite += _targetScore;
                //盤上にある敵駒のカウントを減らして、駒を破壊する
                GameManager._bPieceCount--;
                Destroy(_target);
            }

            this.transform.position = _target.transform.position + _offset;
            GameManager._player = GameManager.Player_Two;

            PhaseChange(_target);
            SetCursorPos(Screen.width / 2, Screen.height / 2);
            GameManager._state = Phase.Black;

            print($"駒は {_target.name} をとった");

            if (Physics.Raycast(_ray, out RaycastHit hitTile, _rayDistance, _tileLayer))
            {
                GameObject _hitTile = hitTile.collider.gameObject;
                print($"駒は {_hitTile.name} に移動した");
            }
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
                    this.transform.position = _target.transform.position + _offset;
                    GameManager._player = GameManager.Player_Two;

                    PhaseChange(_target);
                    SetCursorPos(Screen.width / 2, Screen.height / 2);
                    GameManager._state = Phase.Black;

                    print($"駒は {_target.name} に移動した");
                }
                else
                {
                    Debug.Log("指定したマスには動けません");
                }
            }
            _movedPieceTile = _hit.collider.gameObject;
            return true;
        }
        //黒番目線の駒の移動
        //黒番目線のRayの処理(駒を奪う場合)
        if (Physics.Raycast(_ray2, out _hit, _rayDistance, _whiteLayer))
        {
                GameObject _target = _hit.collider.gameObject;
                int _targetScore = _target.GetComponent<PieceMove>()._getScore; //とった駒が持っている_getScoreを取得

                if (_target.tag == "WhitePiece")
                {
                    //黒のスコアを加算
                    GameManager._scoreBlack += _targetScore;
                    //盤上にある駒のカウントを減らして、駒を破壊する
                    GameManager._wPieceCount--;
                    Destroy(_target);
                }

                this.transform.position = _target.transform.position + _offset;
                GameManager._player = GameManager.Player_One;

                PhaseChange(_target);
                SetCursorPos(Screen.width / 2, Screen.height / 2);
                GameManager._state = Phase.White;

                print($"駒は {_target.name} をとった");

                if (Physics.Raycast(_ray, out RaycastHit hitTile, _rayDistance, _tileLayer))
                {
                    GameObject _hitTile = hitTile.collider.gameObject;
                    print($"駒は {_hitTile.name} に移動した");
                }
            return true;
        }
        //黒番目線のRayの処理(移動処理)
        else if (Physics.Raycast(_ray2, out _hit, _rayDistance, _tileLayer))
        {
            foreach (var i in _search._movableTile)
            {
                GameObject _target = _hit.collider.gameObject;
                if (_target == i.gameObject)
                {
                    this.transform.position = _target.transform.position + _offset;
                    GameManager._player = GameManager.Player_One;

                    PhaseChange(_target);
                    SetCursorPos(Screen.width / 2, Screen.height / 2);
                    GameManager._state = Phase.White;

                    print($"駒は {_target.name} に移動した");
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
    public void ChangeState() //駒を右クリックをすると移動状態→通常状態にできる
    {
        //通常状態→移動状態(白)
        if (_status == Status.Normal && _color == PieceColor.White && GameManager._state == Phase.White)
        {
            _currentPieceTile = gameObject;
            _status = Status.Move;
            _renderer.material = _moveMaterial;
            _search._piece = this;
            _search._pieceInfo = gameObject;
            _piece._whitePieces.Remove(gameObject);
            foreach (var pieces in _piece._whitePieces)
            {
                pieces.GetComponent<Collider>().enabled = false;
            }
        }
        //通常状態→移動状態(黒)
        else if (_status == Status.Normal && _color == PieceColor.Black && GameManager._state == Phase.Black)
        {
            _currentPieceTile = gameObject;
            _status = Status.Move;
            _renderer.material = _moveMaterial;
            _search._piece = this;
            _search._pieceInfo = gameObject;
            _piece._blackPieces.Remove(gameObject);
            foreach (var pieces in _piece._blackPieces)
            {
                pieces.GetComponent<Collider>().enabled = false;
            }
        }
        //移動状態→通常状態(駒が移動した後の共通処理)
        else if (_status == Status.Move)
        {
            if (_currentPieceTile != _movedPieceTile && _movedPieceTile.tag == "Tile")
            {
                _moveCount++;
            }
            _status = Status.Normal;
            _renderer.material = _normalMaterial;

            if (gameObject.tag == "WhitePiece" && int.Parse(_movedPieceTile.name[1].ToString()) == 8)
            {
                _promQ._promWhite = _promR._promWhite = _promB._promWhite = _promK._promWhite = gameObject;
                _piece.ActivePanel();
            }
            else if (gameObject.tag == "BlackPiece" && int.Parse(_movedPieceTile.name[1].ToString()) == 1)
            {
                _promQ._promBlack = _promR._promBlack = _promB._promBlack = _promK._promBlack = gameObject;
                _piece.ActivePanel();
            }

            for (int i = 0; i < _search._movableTile.Count; i++)
            {
                if (_search._movableTile[i].tag == "Tile")
                {
                    _search._tile.Add(_search._movableTile[i]);
                }
            }
            foreach (var tiles in _search._tile)
            {
                tiles.GetComponent<Collider>().enabled = true;
            }
            _search._movableTile.Clear();
            _search._piece = null;
            _search._pieceInfo = null;

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
        }
    }

    /// <summary>
    /// ターン表示の切り替え、駒の移動状態→通常状態
    /// </summary>
    /// <param name="_target"></param>
    public void PhaseChange(GameObject _target)
    {
        switch (_color)
        {
            case PieceColor.White:
                _piece.SwitchTurnWhite();
                if (_target.tag == "WhitePiece")
                {
                    _status = Status.Normal;
                }
                break;

            case PieceColor.Black:
                _piece.SwitchTurnBlack();
                if (_target.tag == "BlackPiece")
                {
                    _status = Status.Normal;
                }
                break;
        }
    }

    public void TestAsign() 
    {
        _search = GameObject.Find("Board,Tile").GetComponent<MasuSearch>();
        _piece = GameObject.Find("Piece").GetComponent<PieceManager>();
        _promQ = GameObject.Find("Queen").GetComponent<Promotion>();
        _promR = GameObject.Find("Rook").GetComponent<Promotion>();
        _promB = GameObject.Find("Bishop").GetComponent<Promotion>();
        _promK = GameObject.Find("Knight").GetComponent<Promotion>();
    }

    /// <summary>
    /// 通常状態、移動状態
    /// </summary>
    public enum Status
    {
        Normal,
        Move,
    }

    /// <summary>
    /// 白駒or黒駒
    /// </summary>
    public enum PieceColor
    {
        White = 0,
        Black = 1,
    }

    /// <summary>
    /// 駒の種類
    /// </summary>
    public enum PieceType
    {
        Pawn = 1,
        Knight,
        Bishop,
        Rook,
        Queen,
    }
}