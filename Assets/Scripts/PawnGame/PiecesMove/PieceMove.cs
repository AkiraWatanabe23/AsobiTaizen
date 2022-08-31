using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    [Tooltip("どっちのターンか(白)")] Text _whiteTurn;
    [Tooltip("どっちのターンか(白)")] Image _whiteTurnPanel;
    [Tooltip("どっちのターンか(黒)")] Text _blackTurn;
    [Tooltip("どっちのターンか(黒)")] Image _blackTurnPanel;
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
    Pawn _pawn;
    Knight _knight;
    Bishop _bishop;
    Rook _rook;
    Queen _queen;
    public int _moveCount = 0;

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
        //↓ターン表示のText
        _whiteTurn = GameObject.Find("WhiteText").GetComponent<Text>();
        _blackTurn = GameObject.Find("BlackText").GetComponent<Text>();
        _whiteTurn.color = Color.yellow;
        //↓ターン表示のPanel
        _whiteTurnPanel = GameObject.Find("WhiteTurnPanel").GetComponent<Image>();
        _blackTurnPanel = GameObject.Find("BlackTurnPanel").GetComponent<Image>();
        _blackTurnPanel.gameObject.GetComponent<Image>().enabled = false;
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
            SetCursorPos(Screen.width/2, Screen.height/2);
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
            GameObject _target = _hit.collider.gameObject;
            this.transform.position = _target.transform.position + _offset;
            GameManager._player = GameManager.Player_Two;

            PhaseChange(_target);
            SetCursorPos(Screen.width / 2, Screen.height / 2);
            GameManager._state = Phase.Black;

            print($"駒は {_target.name} に移動した");
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
            GameObject _target = _hit.collider.gameObject;
            this.transform.position = _target.transform.position + _offset;
            GameManager._player = GameManager.Player_One;

            PhaseChange(_target);
            SetCursorPos(Screen.width / 2, Screen.height / 2);
            GameManager._state = Phase.White;

            print($"駒は {_target.name} に移動した");
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
            _status = Status.Move;
            _renderer.material = _moveMaterial;
            _search.gameObject.GetComponent<MasuSearch>()._piece = this;
            _search.gameObject.GetComponent<MasuSearch>()._pieceInfo = gameObject;
            _pawn.gameObject.GetComponent<Pawn>()._pieceInfo = gameObject;
            _knight.gameObject.GetComponent<Knight>()._pieceInfo = gameObject;
            _bishop.gameObject.GetComponent<Bishop>()._pieceInfo = gameObject;
            _rook.gameObject.GetComponent<Rook>()._pieceInfo = gameObject;
            _queen.gameObject.GetComponent<Queen>()._pieceInfo = gameObject;
        }
        //通常状態→移動状態(黒)
        else if (_status == Status.Normal && _color == PieceColor.Black && GameManager._state == Phase.Black)
        {
            _status = Status.Move;
            _renderer.material = _moveMaterial;
            _search.gameObject.GetComponent<MasuSearch>()._piece = this;
            _search.gameObject.GetComponent<MasuSearch>()._pieceInfo = gameObject;
            _pawn.gameObject.GetComponent<Pawn>()._pieceInfo = gameObject;
            _knight.gameObject.GetComponent<Knight>()._pieceInfo = gameObject;
            _bishop.gameObject.GetComponent<Bishop>()._pieceInfo = gameObject;
            _rook.gameObject.GetComponent<Rook>()._pieceInfo = gameObject;
            _queen.gameObject.GetComponent<Queen>()._pieceInfo = gameObject;
        }
        //移動状態→通常状態(駒が移動した後の共通処理)
        else if (_status == Status.Move)
        {
            _moveCount++;
            _status = Status.Normal;
            _renderer.material = _normalMaterial;
            for (int i = 0; i < _search.gameObject.GetComponent<MasuSearch>()._movableTile.Count; i++)
            {
                if (_search.gameObject.GetComponent<MasuSearch>()._movableTile[i].tag == "Tile")
                {
                    _search.gameObject.GetComponent<MasuSearch>()._tile.Add(_search.gameObject.GetComponent<MasuSearch>()._movableTile[i]);
                }
            }
            _search.gameObject.GetComponent<MasuSearch>()._movableTile.Clear();
            _search.gameObject.GetComponent<MasuSearch>()._piece = null;
            _search.gameObject.GetComponent<MasuSearch>()._pieceInfo = null;
            _pawn.gameObject.GetComponent<Pawn>()._pieceInfo = null;
            _knight.gameObject.GetComponent<Knight>()._pieceInfo = null;
            _bishop.gameObject.GetComponent<Bishop>()._pieceInfo = null;
            _rook.gameObject.GetComponent<Rook>()._pieceInfo = null;
            _queen.gameObject.GetComponent<Queen>()._pieceInfo = null;
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
                _whiteTurn.color = Color.black;
                _blackTurn.color = Color.yellow;
                _whiteTurnPanel.gameObject.GetComponent<Image>().enabled = false;
                _blackTurnPanel.gameObject.GetComponent<Image>().enabled = true;
                if (_target.tag == "WhitePiece")
                {
                    _status = Status.Normal;
                }
                break;

            case PieceColor.Black:
                _whiteTurn.color = Color.yellow;
                _blackTurn.color = Color.black;
                _whiteTurnPanel.gameObject.GetComponent<Image>().enabled = true;
                _blackTurnPanel.gameObject.GetComponent<Image>().enabled = false;
                if (_target.tag == "BlackPiece")
                {
                    _status = Status.Normal;
                }
                break;
        }
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