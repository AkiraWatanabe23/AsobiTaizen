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
    //レイヤーマスク(InspectorのLayerから選択する)
    [SerializeField] LayerMask _tileLayer;
    [SerializeField] LayerMask _whiteLayer;
    [SerializeField] LayerMask _blackLayer;
    /// <summary> 黒番目線のカメラ </summary>
    public static Camera _camera;
    /// <summary> 駒を移動した時にマスのcolliderの上に置く </summary>
    [SerializeField] Vector3 _offset = Vector3.up;
    /// <summary> 通常状態の駒のマテリアル </summary>
    [SerializeField] Material _normalMaterial;
    /// <summary> 移動状態の駒のマテリアル </summary>
    [SerializeField] Material _moveMaterial;
    Renderer _renderer;
    [Tooltip("どっちのターンかの表示(白)")] Text _whiteTurn;
    [Tooltip("どっちのターンかの表示(白)")] Image _whiteTurnPanel;
    [Tooltip("どっちのターンかの表示(黒)")] Text _blackTurn;
    [Tooltip("どっちのターンかの表示(黒)")] Image _blackTurnPanel;
    /// <summary> 白駒か黒駒か </summary>
    public PieceColor _color = PieceColor.White;
    /// <summary> 駒の状態 </summary>
    public Status _status = Status.Normal;
    //駒の得点(Inspectorで設定)
    public int _getScore;

    //extern...UnityやVisualStudioにはない機能(関数)をとってくる
    //上記を訂正 : extern...外部ファイル(dllファイル)で定義されている関数や変数を使用する、という命令
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
        //↑カメラから現在のマウスカーソルの位置にRayを飛ばし、当たったオブジェクトを代入する
        var piece = go.GetComponent<PieceMove>();

        print($"{ name } を選んだ");
        piece.ChangeState();
    }

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _camera = GameObject.Find("Camera(black)").GetComponent<Camera>();
        //↓ターン表示のText
        _whiteTurn = GameObject.Find("WhiteText").GetComponent<Text>();
        _blackTurn = GameObject.Find("BlackText").GetComponent<Text>();
        _whiteTurn.color = Color.yellow;
        //↓ターン表示のPanel
        _whiteTurnPanel = GameObject.Find("WhiteTurnPanel").GetComponent<Image>();
        _blackTurnPanel = GameObject.Find("BlackTurnPanel").GetComponent<Image>();
        _blackTurnPanel.gameObject.GetComponent<Image>().enabled = false;
        /*↑enabled...オブジェクトの指定した[コンポーネント]のアクティブ、非アクティブを変更する
         *  (SetActiveでオブジェクトをとってこれないのを回避する)*/
    }

    // Update is called once per frame
    void Update()
    {
        //左クリックが行われた場合に以下の処理を行う
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
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition); //白番カメラからRayをとばす
        Ray _ray2 = _camera.ScreenPointToRay(Input.mousePosition);    //黒番カメラからRayをとばす
        float _rayDistance = 100;
        /* ↑この関数内[Move()]全体で使う変数 */

        //白番目線の駒の移動
        //白番目線のRayの処理(駒を奪う場合)
        if (Physics.Raycast(_ray, out RaycastHit hit, _rayDistance, _blackLayer))
        {
            GameObject _target = hit.collider.gameObject;

            //Rayが当たったオブジェクトが黒駒だった場合、駒を奪ってそのマスに移動する
            if (_target.tag == "BlackPiece")
            {
                //白のスコアを加算
                GameManager._scoreWhite += _getScore; //←とった駒に設定した_getScoreが加算される
                                                      //(×どの駒がとられたか　〇どの駒がとったか　になってる...逆になってほしい)
                //盤上にある敵駒のカウントを減らす
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
        else if (Physics.Raycast(_ray, out RaycastHit hit2, _rayDistance, _tileLayer))
        {
            GameObject _target = hit2.collider.gameObject;
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
        if (Physics.Raycast(_ray2, out RaycastHit hit3, _rayDistance, _whiteLayer))
        {
            GameObject _target = hit3.collider.gameObject;

            //Rayが当たったオブジェクトが白駒だった場合、駒を奪ってそのマスに移動する
            if (_target.tag == "WhitePiece")
            {
                //黒のスコアを加算
                GameManager._scoreBlack += _getScore;
                //盤上にある駒のカウントを減らす
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
        else if (Physics.Raycast(_ray2, out RaycastHit hit4, _rayDistance, _tileLayer))
        {
            GameObject _target = hit4.collider.gameObject;
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
        }
        //通常状態→移動状態(黒)
        else if (_status == Status.Normal && _color == PieceColor.Black && GameManager._state == Phase.Black)
        {
            _status = Status.Move;
            _renderer.material = _moveMaterial;
        }
        //移動状態→通常状態(駒が移動した後の処理)
        else if (_status == Status.Move)
        {
            _status = Status.Normal;
            _renderer.material = _normalMaterial;
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
            //case int: の下[break;]まで実行される({ }は書かない)
            case 0: //Color.White                                                                                                                       
                _whiteTurn.color = Color.black;
                _blackTurn.color = Color.yellow;
                _whiteTurnPanel.gameObject.GetComponent<Image>().enabled = false;
                _blackTurnPanel.gameObject.GetComponent<Image>().enabled = true;

                if (_target.tag == "WhitePiece")
                {
                    _status = Status.Normal;
                }
                break;

            case (PieceColor)1: //Color.Black
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