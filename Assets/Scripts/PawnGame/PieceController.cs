using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//↓マウスカーソルポジションの強制移動させるのに宣言する必要がある(らしい)
using System.Runtime.InteropServices;

/// <summary> 
/// 全ての駒に統一された動き(駒の選択、移動)
/// </summary>
public class PieceController : MonoBehaviour, IPointerClickHandler
{
    /// <summary> プレイヤー(白) </summary>
    public const int _playerOne = 1;
    /// <summary> プレイヤー(黒) </summary>
    public const int _playerTwo = 2;
    /// <summary> current(現在の)プレイヤー </summary>
    public int _currentPlayer;
    /// <summary> レイヤーマスク(InspectorのLayerから選択する) </summary>
    [SerializeField] LayerMask _tileLayer;
    [SerializeField] LayerMask _whiteLayer;
    [SerializeField] LayerMask _blackLayer;
    /// <summary> 黒番目線のカメラ </summary>
    public static Camera _camera;
    /// <summary> 駒を移動した時にcolliderの上に置く </summary>
    [SerializeField] Vector3 _offset = Vector3.up;
    /// <summary> 通常状態、移動状態の駒のマテリアル </summary>
    [SerializeField] Material _normalMaterial;
    [SerializeField] Material _moveMaterial;
    Renderer _renderer;
    /// <summary> どっちのターンかの表示(白) </summary>
    Text _whiteTurn;
    /// <summary> どっちのターンかの表示(黒) </summary>
    Text _blackTurn;
    /// <summary> 白駒か黒駒か </summary>
    public PieceColor _state = PieceColor.White;
    /// <summary> 駒の種類 </summary>
    public Type _type;
    /// <summary> 駒の状態 </summary>
    public Status _status = Status.Normal;
    bool isMove = false;

    //extern...UnityやVisualStudioにはない機能(関数)をとってくる(C++でいうと「::」と同じらしい)
    //上記を訂正 : extern...外部ファイル(dllファイル)で定義されている関数や変数を使用する、という命令
    //[DllImport("user32.dll")]...外のどのファイル(今回は「user32.dll」)からとってくるのか
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
        //↑カメラから現在のマウスカーソルの位置にRayを飛ばし、当たったオブジェクトを代入する
        var piece = go.GetComponent<PieceController>();
        //↑当たったオブジェクトの「PieceController」を見つけてくる

        print($"{ name } を選んだ");
        piece.ChangeState();
    }

    void Start()
    {
        _renderer = GetComponent<Renderer>(); //駒のRenderer(コンポーネント)をとってくる
        _camera = GameObject.Find("Camera(black)").GetComponent<Camera>(); //黒番目線のカメラを取得
        //↓ターン表示のText
        _whiteTurn = GameObject.Find("WhiteText").GetComponent<Text>();
        _blackTurn = GameObject.Find("BlackText").GetComponent<Text>();
        _whiteTurn.color = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {
        //一回目のマウスクリックで駒を選び、二回目のクリックで配置場所を確定、移動する
        //左クリックが行われた場合に以下の処理を行う
        if (Input.GetMouseButtonDown(0))
        {
            if (_status == Status.Move)
            {
                if (Move())
                {
                    ChangeState();
                }
            }
        }
    }
    public bool Move()
    {
        //マウスの位置を取得し、Rayに代入
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition); //白番カメラからRayをとばす
        Ray _ray2 = _camera.ScreenPointToRay(Input.mousePosition);    //黒番カメラからRayをとばす
        float _rayDistance = 100; //Rayの長さ

        //白番目線の駒の移動
        //白番目線のRayの処理(駒を奪う場合)
        if (Physics.Raycast(_ray, out RaycastHit hit, _rayDistance, _blackLayer))
        {
            GameObject _target = hit.collider.gameObject;

            //Rayが当たったオブジェクトが敵の駒だった場合、駒を奪ってそのマスに移動する
            if (_target.tag == "BlackPiece")
            {
                //白のスコアを加算
                //とった駒の種類によって獲得する点数が異なる
                //word.Contains(string)...wordの中に、string(文字列)が含まれているか
                //とった駒がポーンなら
                if (_target.name.Contains("pawn"))
                {
                    GameManager._scoreWhite += 1;
                    Debug.Log("今は " + GameManager._scoreWhite + " 点");
                }
                //ナイトなら
                else if (_target.name.Contains("knight"))
                {
                    GameManager._scoreWhite += 2;
                    Debug.Log("今は " + GameManager._scoreWhite + " 点");
                }
                //ビショップなら
                else if (_target.name.Contains("bishop"))
                {
                    GameManager._scoreWhite += 3;
                    Debug.Log("今は " + GameManager._scoreWhite + " 点");
                }
                //ルークなら
                else if (_target.name.Contains("rook"))
                {
                    GameManager._scoreWhite += 4;
                    Debug.Log("今は " + GameManager._scoreWhite + " 点");
                }
                //クイーンなら
                else if (_target.name.Contains("queen"))
                {
                    GameManager._scoreWhite += 5;
                    Debug.Log("今は " + GameManager._scoreWhite + " 点");
                }
                //盤上にある敵駒のカウントを減らす
                GameManager._bPieceCount--;
                Destroy(_target);
            }

            this.transform.position = _target.transform.position + _offset;
            _currentPlayer = _playerTwo;

            PhaseChange(_target);
            SetCursorPos(950, 400); //駒を移動させた後、マウスカーソルをゲーム画面の中央辺りに強制移動させる

            //_state = Color.Black;
            GameManager._state = Phase.Black;
            print($"駒は {_target.name} をとった");

            if (Physics.Raycast(_ray, out RaycastHit hitTile, _rayDistance, _tileLayer))
            {
                GameObject _hitTile = hitTile.collider.gameObject;
                print($"駒は {_hitTile.name} に移動した");
            }
            return true;
        }
        //白番目線のRayの処理(移動のみ)
        else if (Physics.Raycast(_ray, out RaycastHit hit2, _rayDistance, _tileLayer))
        {
            GameObject _target = hit2.collider.gameObject;
            this.transform.position = _target.transform.position + _offset;
            _currentPlayer = _playerTwo;

            PhaseChange(_target);
            SetCursorPos(950, 400); //駒を移動させた後、マウスカーソルをゲーム画面の中央辺りに強制移動させる

            //_state = Color.Black;
            GameManager._state = Phase.Black;

            print($"駒は {_target.name} に移動した"); // print($"..."); ←→ Debug.Log("..."); と同じ
            return true;
        }

        //黒番目線の駒の移動
        //黒番目線のRayの処理(駒を奪う場合)
        if (Physics.Raycast(_ray2, out RaycastHit hit3, _rayDistance, _whiteLayer))
        {
            GameObject _target = hit3.collider.gameObject;

            //Rayが当たったオブジェクトが敵の駒だった場合、駒を奪ってそのマスに移動する
            if (_target.tag == "WhitePiece")
            {
                //黒のスコアを加算
                //とった駒の種類によって獲得する点数が異なる
                //とった駒がポーンなら
                if (_target.name.Contains("pawn"))
                {
                    GameManager._scoreBlack += 1;
                    Debug.Log("今は " + GameManager._scoreBlack + " 点");
                }
                //ナイトなら
                else if (_target.name.Contains("knight"))
                {
                    GameManager._scoreBlack += 2;
                    Debug.Log("今は " + GameManager._scoreBlack + " 点");
                }
                //ビショップなら
                else if (_target.name.Contains("bishop"))
                {
                    GameManager._scoreBlack += 3;
                    Debug.Log("今は " + GameManager._scoreBlack + " 点");
                }
                //ルークなら
                else if (_target.name.Contains("rook"))
                {
                    GameManager._scoreBlack += 4;
                    Debug.Log("今は " + GameManager._scoreBlack + " 点");
                }
                //クイーンなら
                else if (_target.name.Contains("queen"))
                {
                    GameManager._scoreBlack += 5;
                    Debug.Log("今は " + GameManager._scoreBlack + " 点");
                }

                //盤上にある駒のカウントを減らす
                GameManager._wPieceCount--;
                Destroy(_target);
            }

            this.transform.position = _target.transform.position + _offset;
            _currentPlayer = _playerOne;

            PhaseChange(_target);
            SetCursorPos(950, 400); //駒を移動させた後、マウスカーソルをゲーム画面の中央辺りに強制移動させる

            //_state = Color.White;
            GameManager._state = Phase.White;
            print($"駒は {_target.name} をとった");

            if (Physics.Raycast(_ray, out RaycastHit hitTile, _rayDistance, _tileLayer))
            {
                GameObject _hitTile = hitTile.collider.gameObject;
                print($"駒は {_hitTile.name} に移動した");
            }
            return true;
        }
        //黒番目線のRayの処理(移動のみ)
        else if (Physics.Raycast(_ray2, out RaycastHit hit4, _rayDistance, _tileLayer))
        {
            GameObject _target = hit4.collider.gameObject;
            this.transform.position = _target.transform.position + _offset;
            _currentPlayer = _playerOne;

            PhaseChange(_target);
            SetCursorPos(950, 400); //駒を移動させた後、マウスカーソルをゲーム画面の中央辺りに強制移動させる

            //_state = Color.White;
            GameManager._state = Phase.White;

            print($"駒は {_target.name} に移動した");
            return true;
        }
        return false;
    }
    /// <summary>
    /// マウスクリックをした(駒を選んだ、動かした)時に実行される処理
    /// </summary>
    public void ChangeState() //右クリックをすると移動状態→通常状態にできる
    {
        //白駒の切り替え
        if (_status == Status.Normal && _state == PieceColor.White && GameManager._state == Phase.White)
        {
            _status = Status.Move;
            _renderer.material = _moveMaterial;
            isMove = true;
        }
        //黒駒の切り替え
        else if (_status == Status.Normal && _state == PieceColor.Black && GameManager._state == Phase.Black)
        {
            _status = Status.Move;
            _renderer.material = _moveMaterial;
            isMove = true;
        }
        //移動状態→通常状態(駒が移動した後の処理)
        else if (_status == Status.Move && isMove == true)
        {
            _status = Status.Normal;
            _renderer.material = _normalMaterial;
        }
    }

    /// <summary>
    /// フェーズ毎の移動制御
    /// </summary>
    /// <param name="_target"></param>
    public void PhaseChange(GameObject _target)
    {
        switch (_state)
        {
            case 0: //Color.White
                _whiteTurn.color = Color.white;
                _blackTurn.color = Color.yellow;
                if (_target.tag == "WhitePiece")
                {
                    _status = Status.Normal;
                    isMove = false;
                }
                else
                {
                    isMove = true;
                }
                break;

            case (PieceColor)1: //Color.Black
                _whiteTurn.color = Color.yellow;
                _blackTurn.color = Color.white;
                if (_target.tag == "BlackPiece")
                {
                    _status = Status.Normal;
                    isMove = false;
                }
                else
                {
                    isMove = true;
                }
                break;
        }
    }

    /// <summary>
    /// Queen = 5, Rook = 4, Bishop = 3, Knight = 2, Pawn = 1 と数字を振る
    /// </summary>
    public enum Type
    {
        Pawn = 1,
        Knight,
        Bishop,
        Rook,
        Queen,
    }

    /// <summary>
    /// 通常状態、移動状態
    /// </summary>
    public enum Status
    {
        Normal, //通常状態
        Move,   //移動状態
    }

    /// <summary>
    /// 白駒or黒駒
    /// </summary>
    public enum PieceColor
    {
        White = 0,
        Black = 1,
    }
}