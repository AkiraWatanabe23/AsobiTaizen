using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Runtime.InteropServices;

public class PieceManager : MonoBehaviour, IPointerClickHandler
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
    Camera _camera;
    /// <summary> 駒を移動した時にcolliderの上に置く </summary>
    [SerializeField] Vector3 _offset = Vector3.up;

    PieceController _phase;

    //extern...UnityやVisualStudioにはない機能(関数)をとってくる(C++でいうと「::」と同じらしい)
    //[DllImport("user32.dll")]...外のどのファイル(今回は「user32.dll」)からとってくるのか
    //SetCursorPos(関数)...指定したファイル内のどの機能(関数)を使うのか
    [DllImport("user32.dll")]
    public static extern bool SetCursorPos(int x, int y);

    /// <summary>
    /// マウスクリックが行われた(どのマウスクリックでも実行される)時の処理
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        var go = eventData.pointerCurrentRaycast.gameObject;
        var piece = go.GetComponent<PieceController>();

        print($"{ name } を選んだ");
        piece.ChangeState();
    }

    // Start is called before the first frame update
    void Start()
    {
        _currentPlayer = _playerOne;                                       //白番から始める
        _camera = GameObject.Find("Camera(black)").GetComponent<Camera>(); //黒番目線のカメラを見つけてくる

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Move()
    {
        //マウスの位置を取得し、Rayに代入
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition); //メインカメラ(白番目線)からRayをとばす
        Ray _ray2 = _camera.ScreenPointToRay(Input.mousePosition);    //secondカメラ(黒番目線)からRayをとばす
        //Rayの長さ
        float _rayDistance = 100;

        //白番目線の駒の移動
        //白番目線のRayの処理(駒を奪う場合)
        if (Physics.Raycast(_ray, out RaycastHit hit, _rayDistance, _blackLayer))
        {
            GameObject _target = hit.collider.gameObject;

            //Rayが当たったオブジェクトが敵の駒だった場合、駒を奪ってそのマスに移動する
            if (_target.tag == "BlackPiece")
            {
                _target.SetActive(false);
            }

            this.transform.position = _target.transform.position + _offset;
            _currentPlayer = _playerTwo;

            _phase.PhaseChange(_target);
            SetCursorPos(950, 400); //駒を移動させた後、マウスカーソルをゲーム画面の中央辺りに強制移動させる

            print($"Ray は {_target.name} に移動した");
            Debug.Log(_currentPlayer);
            return true;
        }
        //白番目線のRayの処理(移動のみ)
        else if (Physics.Raycast(_ray, out RaycastHit hit2, _rayDistance, _tileLayer))
        {
            GameObject _target = hit2.collider.gameObject;
            this.transform.position = _target.transform.position + _offset;
            _currentPlayer = _playerTwo;

            _phase.PhaseChange(_target);
            SetCursorPos(950, 400); //駒を移動させた後、マウスカーソルをゲーム画面の中央辺りに強制移動させる

            print($"Ray は {_target.name} に移動した"); // print($"..."); ←→ Debug.Log("..."); と同じ
            Debug.Log(_currentPlayer);
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
                _target.SetActive(false);
            }

            this.transform.position = _target.transform.position + _offset;
            _currentPlayer = _playerOne;

            _phase.PhaseChange(_target);
            SetCursorPos(950, 400); //駒を移動させた後、マウスカーソルをゲーム画面の中央辺りに強制移動させる

            print($"Ray は {_target.name} に移動した");
            Debug.Log(_currentPlayer);
            return true;
        }
        //黒番目線のRayの処理(移動のみ)
        else if (Physics.Raycast(_ray2, out RaycastHit hit4, _rayDistance, _tileLayer))
        {
            GameObject _target = hit4.collider.gameObject;
            this.transform.position = _target.transform.position + _offset;
            _currentPlayer = _playerOne;

            _phase.PhaseChange(_target);
            SetCursorPos(950, 400); //駒を移動させた後、マウスカーソルをゲーム画面の中央辺りに強制移動させる

            print($"Ray は {_target.name} に移動した");
            Debug.Log(_currentPlayer);
            return true;
        }

        return false;
    }
}
