using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasuSearch : MonoBehaviour
{
    [SerializeField] public List<Collider> _tile = new List<Collider>();
    [SerializeField] public List<Collider> _movableTile = new List<Collider>();
    [SerializeField] public PieceMove _piece = default;
    [SerializeField] public GameObject _pieceInfo;
    [Tooltip("駒のいるマスのランク(横)")] public int _tileRank = 0;
    [Tooltip("駒のいるマスのファイル(縦)")] public int _tileFile = 0;
    public RaycastHit _hit;
    public float _vecX = 0f;
    public float _vecY = 2.5f;
    public float _vecZ = 2.55f;
    [SerializeField] public Pawn _pawn;
    [SerializeField] public Knight _knight;
    [SerializeField] public Bishop _bishop;
    [SerializeField] public Rook _rook;
    [SerializeField] public Queen _queen;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 64; i++)
        {
            _tile.Add(this.gameObject.transform.GetChild(i).GetComponent<Collider>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_piece != null)
        {
            GetTileNum();
            var _pieceNum = _piece.gameObject.GetComponent<PieceMove>()._type;
            Search((int)_pieceNum);
            //_piece = null;
        }
    }

    public void Search(int pieceType)
    {
        switch (pieceType)
        {
            case 1:
                Debug.Log("ポーンの移動処理を実行します");
                _pawn.PawnMovement();
                //Pawn();
                break;
            case 2:
                Debug.Log("ナイトの移動処理を実行します");
                _knight.KnightMovement();
                //Knight();
                break;
            case 3:
                Debug.Log("ビショップの移動処理を実行します");
                _bishop.BishopMovement();
                //Bishop();
                break;
            case 4:
                Debug.Log("ルークの移動処理を実行します");
                _rook.RookMovement();
                Rook();
                break;
            case 5:
                Debug.Log("クイーンの移動処理を実行します");
                _queen.QueenMovement();
                Queen();
                break;
        }   
    }

    void GetTileNum()
    {
        if (_pieceInfo != null)
        {
            if (Physics.Raycast(_pieceInfo.transform.position, Vector3.down, out _hit, 5))
            {
                //マス番号取得(列、行それぞれ)
                _tileRank = int.Parse(_hit.collider.gameObject.name[1].ToString());
                if (_hit.collider.gameObject.name[0] == 'a')
                {
                    _tileFile = 1;
                }
                else if (_hit.collider.gameObject.name[0] == 'b')
                {
                    _tileFile = 2;
                }
                else if (_hit.collider.gameObject.name[0] == 'c')
                {
                    _tileFile = 3;
                }
                else if (_hit.collider.gameObject.name[0] == 'd')
                {
                    _tileFile = 4;
                }
                else if (_hit.collider.gameObject.name[0] == 'e')
                {
                    _tileFile = 5;
                }
                else if (_hit.collider.gameObject.name[0] == 'f')
                {
                    _tileFile = 6;
                }
                else if (_hit.collider.gameObject.name[0] == 'g')
                {
                    _tileFile = 7;
                }
                else if (_hit.collider.gameObject.name[0] == 'h')
                {
                    _tileFile = 8;
                }
            }
        }
    }

    /***************ポーンの移動処理***************/
    void Pawn()
    {
        if (_pieceInfo.tag == "WhitePiece")
        {
            //1,1回目の動き...2マス移動可
            if (_pieceInfo.GetComponent<PieceMove>()._moveCount == 0)
            {
                _vecX = 0f;
                _vecY = 2.55f;
                _vecZ = 2.55f;
                for (int i = 0; i < 2; i++)
                {
                    Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), Color.yellow, 10f);
                    if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
                    {
                        if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                        {
                            _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                            Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                            break;
                        }
                        else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                        {
                            _vecZ += 2.5f;
                            if (_movableTile.Contains(_hit.collider))
                            {
                                break;
                            }
                            else
                            {
                                _movableTile.Add(_hit.collider);
                            }
                            _tile.Remove(_hit.collider);
                            Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                            if (_hit.collider.gameObject.tag == "BlackPiece")
                            {
                                break;
                            }
                        }
                    }
                }
                //移動範囲以外のColliderをoffにする処理を書く
                foreach (Collider col in _tile)
                {
                    col.enabled = false;
                    Debug.Log(col + "のColliderをoffにします");
                }
            }
            //2,2回目以降は1マス移動
            else if (_pieceInfo.GetComponent<PieceMove>()._moveCount != 0)
            {
                _vecX = 0f;
                _vecY = 2.55f;
                _vecZ = 2.55f;
                Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), Color.yellow, 10f);
                if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
                {
                    if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                    {
                        _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                        Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                    }
                    else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                    {
                        if (_movableTile.Contains(_hit.collider))
                        {

                        }
                        else
                        {
                            _movableTile.Add(_hit.collider);
                        }
                        _tile.Remove(_hit.collider);
                        Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                        if (_hit.collider.gameObject.tag == "BlackPiece")
                        {

                        }
                    }
                }
                //移動範囲以外のColliderをoffにする処理を書く
                foreach (Collider col in _tile)
                {
                    col.enabled = false;
                    Debug.Log(col + "のColliderをoffにします");
                }
            }
            //　常に斜め前の探索(アンパッサンに使える?)
            _vecX = 2.55f;
            _vecY = 2.55f;
            _vecZ = 2.55f;
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == "BlackPiece")
                {
                    Debug.Log(_hit.collider.gameObject.name + "をとることが出来ます");
                }
            }
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == "BlackPiece")
                {
                    Debug.Log(_hit.collider.gameObject.name + "をとることが出来ます");
                }
            }
            //3,アンパッサン...真隣のマス探索
        }
        else if (_pieceInfo.tag == "BlackPiece")
        {
            //1,1回目の動き...2マス移動可
            if (_pieceInfo.GetComponent<PieceMove>()._moveCount == 0)
            {
                _vecX = 0f;
                _vecY = 2.55f;
                _vecZ = 2.55f;
                for (int i = 0; i < 2; i++)
                {
                    Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), Color.yellow, 10f);
                    if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), out _hit, 100))
                    {
                        if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                        {
                            _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                            Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                            break;
                        }
                        else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                        {
                            _vecZ += 2.5f;
                            if (_movableTile.Contains(_hit.collider))
                            {
                                break;
                            }
                            else
                            {
                                _movableTile.Add(_hit.collider);
                            }
                            _tile.Remove(_hit.collider);
                            Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                            if (_hit.collider.gameObject.tag == "WhitePiece")
                            {
                                break;
                            }
                        }
                    }
                }
                //移動範囲以外のColliderをoffにする処理を書く
                foreach (Collider col in _tile)
                {
                    col.enabled = false;
                    Debug.Log(col + "のColliderをoffにします");
                }
            }
            //2,2回目以降は1マス移動
            else if (_pieceInfo.GetComponent<PieceMove>()._moveCount != 0)
            {
                _vecX = 0f;
                _vecY = 2.55f;
                _vecZ = 2.55f;
                Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), Color.yellow, 10f);
                if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
                {
                    if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                    {
                        _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                        Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                    }
                    else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                    {
                        if (_movableTile.Contains(_hit.collider))
                        {

                        }
                        else
                        {
                            _movableTile.Add(_hit.collider);
                        }
                        _tile.Remove(_hit.collider);
                        Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                        if (_hit.collider.gameObject.tag == "WhitePiece")
                        {

                        }
                    }
                }
                //移動範囲以外のColliderをoffにする処理を書く
                foreach (Collider col in _tile)
                {
                    col.enabled = false;
                    Debug.Log(col + "のColliderをoffにします");
                }
            }
            //　常に斜め前の探索(アンパッサンに使える?)
            _vecX = 2.55f;
            _vecY = 2.55f;
            _vecZ = 2.55f;
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == "WhitePiece")
                {
                    Debug.Log(_hit.collider.gameObject.name + "をとることが出来ます");
                }
            }
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, -_vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, -_vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == "WhitePiece")
                {
                    Debug.Log(_hit.collider.gameObject.name + "をとることが出来ます");
                }
            }
            //3,アンパッサン...真隣のマス探索
        }

    }

    /***************ナイトの移動処理***************/
    void Knight()
    {
        //桂馬 前方向
        _vecX = 2.55f;
        _vecY = 2.55f;
        _vecZ = 5f;
        for (int i = 0; i < 2; i++)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "にはすすめません");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX -= 5f;
                    if (_movableTile.Contains(_hit.collider))
                    {
                        break;
                    }
                    else
                    {
                        _movableTile.Add(_hit.collider);
                    }
                    _tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                    if (_hit.collider.gameObject.tag == "BlackPiece")
                    {
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Colliderが当たってない");
            }
        }
        //桂馬 後ろ方向
        _vecX = 2.55f;
        _vecY = 2.55f;
        _vecZ = 5f;
        for (int j = 0; j < 2; j++)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "にはすすめません");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX -= 5f;
                    if (_movableTile.Contains(_hit.collider))
                    {
                        break;
                    }
                    else
                    {
                        _movableTile.Add(_hit.collider);
                    }
                    _tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                    if (_hit.collider.gameObject.tag == "BlackPiece")
                    {
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Colliderが当たってない");
            }
        }
        //桂馬 左方向
        _vecX = 5f;
        _vecY = 2.55f;
        _vecZ = 2.55f;
        for (int k = 0; k < 2; k++)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "にはすすめません");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecZ -= 5f;
                    if (_movableTile.Contains(_hit.collider))
                    {
                        break;
                    }
                    else
                    {
                        _movableTile.Add(_hit.collider);
                    }
                    _tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                    if (_hit.collider.gameObject.tag == "BlackPiece")
                    {
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Colliderが当たってない");
            }
        }
        //桂馬 右方向
        _vecX = 5f;
        _vecY = 2.55f;
        _vecZ = 2.55f;
        for (int l = 0; l < 2; l++)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "にはすすめません");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecZ -= 5f;
                    if (_movableTile.Contains(_hit.collider))
                    {
                        break;
                    }
                    else
                    {
                        _movableTile.Add(_hit.collider);
                    }
                    _tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                    if (_hit.collider.gameObject.tag == "BlackPiece")
                    {
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Colliderが当たってない");
            }
        }
        //移動範囲以外のColliderをoffにする処理を書く
        foreach (Collider col in _tile)
        {
            col.enabled = false;
            Debug.Log(col + "のColliderをoffにします");
        }
    }

    /***************ビショップの移動処理***************/
    void Bishop()
    {
        //左斜め前方向
        _vecX = 2.55f;
        _vecY = 2.55f;
        _vecZ = 2.55f;
        for (int i = 0; i < 8 - _tileRank; i++)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX += 2.5f;
                    _vecZ += 2.5f;
                    if (_movableTile.Contains(_hit.collider))
                    {
                        break;
                    }
                    else
                    {
                        _movableTile.Add(_hit.collider);
                    }
                    _tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                    if (_hit.collider.gameObject.tag == "BlackPiece")
                    {
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Colliderが当たってない");
            }
        }
        //右斜め前方向
        _vecX = 2.55f;
        _vecY = 2.55f;
        _vecZ = 2.55f;
        for (int j = _tileRank; j > 1; j--)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX += 2.5f;
                    _vecZ += 2.5f;
                    if (_movableTile.Contains(_hit.collider))
                    {
                        break;
                    }
                    else
                    {
                        _movableTile.Add(_hit.collider);
                    }
                    _tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                    if (_hit.collider.gameObject.tag == "BlackPiece")
                    {
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Colliderが当たってない");
            }
        }
        //左斜め後ろ方向
        _vecX = 2.55f;
        _vecY = 2.55f;
        _vecZ = 2.55f;
        for (int k = _tileFile; k > 1; k--)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, -_vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, -_vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX += 2.5f;
                    _vecZ += 2.5f;
                    if (_movableTile.Contains(_hit.collider))
                    {
                        break;
                    }
                    else
                    {
                        _movableTile.Add(_hit.collider);
                    }
                    _tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                    if (_hit.collider.gameObject.tag == "BlackPiece")
                    {
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Colliderが当たってない");
            }
        }
        //右斜め後ろ方向
        _vecX = 2.55f;
        _vecY = 2.55f;
        _vecZ = 2.55f;
        for (int l = 0; l < 8 - _tileFile; l++)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX += 2.5f;
                    _vecZ += 2.5f;
                    if (_movableTile.Contains(_hit.collider))
                    {
                        break;
                    }
                    else
                    {
                        _movableTile.Add(_hit.collider);
                    }
                    _tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                    if (_hit.collider.gameObject.tag == "BlackPiece")
                    {
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Colliderが当たってない");
            }
        }
        //移動範囲以外のColliderをoffにする処理を書く
        foreach (Collider col in _tile)
        {
            col.enabled = false;
            Debug.Log(col + "のColliderをoffにします");
        }
    }

    /***************ルークの移動処理***************/
    void Rook()
    {
        //前方向
        _vecX = 0f;
        _vecY = 2.55f;
        _vecZ = 2.55f;
        for (int i = 0; i < 8 - _tileRank; i++)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecZ += 2.5f;
                    if (_movableTile.Contains(_hit.collider))
                    {
                        break;
                    }
                    else
                    {
                        _movableTile.Add(_hit.collider);
                    }
                    _tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                    if (_hit.collider.gameObject.tag == "BlackPiece")
                    {
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Colliderが当たってない");
            }
        }
        //後ろ方向
        _vecX = 0f;
        _vecY = 2.55f;
        _vecZ = 2.55f;
        for (int j = _tileRank; j > 1; j--)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecZ += 2.5f;
                    if (_movableTile.Contains(_hit.collider))
                    {
                        break;
                    }
                    else
                    {
                        _movableTile.Add(_hit.collider);
                    }
                    _tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                    if (_hit.collider.gameObject.tag == "BlackPiece")
                    {
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Colliderが当たってない");
            }
        }
        //左方向
        _vecX = 2.55f;
        _vecY = 2.55f;
        _vecZ = 0f;
        for (int k = _tileFile; k > 1; k--)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX += 2.5f;
                    if (_movableTile.Contains(_hit.collider))
                    {
                        break;
                    }
                    else
                    {
                        _movableTile.Add(_hit.collider);
                    }
                    _tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                    if (_hit.collider.gameObject.tag == "BlackPiece")
                    {
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Colliderが当たってない");
            }
        }
        //右方向
        _vecX = 2.55f;
        _vecY = 2.55f;
        _vecZ = 0f;
        for (int l = 0; l < 8 - _tileFile; l++)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX += 2.5f;
                    if (_movableTile.Contains(_hit.collider))
                    {
                        break;
                    }
                    else
                    {
                        _movableTile.Add(_hit.collider);
                    }
                    _tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                    if (_hit.collider.gameObject.tag == "BlackPiece")
                    {
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Colliderが当たってない");
            }
        }
        //移動範囲以外のColliderをoffにする処理を書く
        foreach (Collider col in _tile)
        {
            col.enabled = false;
            Debug.Log(col + "のColliderをoffにします");
        }
    }

    /***************クイーンの移動処理***************/
    void Queen()
    {
        /*==========前後左右の動き==========*/
        //前方向
        _vecX = 0f;
        _vecY = 2.55f;
        _vecZ = 2.55f;
        for (int i = 0; i < 8 - _tileRank; i++)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecZ += 2.5f;
                    if (_movableTile.Contains(_hit.collider))
                    {
                        break;
                    }
                    else
                    {
                        _movableTile.Add(_hit.collider);
                    }
                    _tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                    if (_hit.collider.gameObject.tag == "BlackPiece")
                    {
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Colliderが当たってない");
            }
        }
        //後ろ方向
        _vecX = 0f;
        _vecY = 2.55f;
        _vecZ = 2.55f;
        for (int j = _tileRank; j > 1; j--)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecZ += 2.5f;
                    if (_movableTile.Contains(_hit.collider))
                    {
                        break;
                    }
                    else
                    {
                        _movableTile.Add(_hit.collider);
                    }
                    _tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                    if (_hit.collider.gameObject.tag == "BlackPiece")
                    {
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Colliderが当たってない");
            }
        }
        //左方向
        _vecX = 2.55f;
        _vecY = 2.55f;
        _vecZ = 0f;
        for (int k = _tileFile; k > 1; k--)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX += 2.5f;
                    if (_movableTile.Contains(_hit.collider))
                    {
                        break;
                    }
                    else
                    {
                        _movableTile.Add(_hit.collider);
                    }
                    _tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                    if (_hit.collider.gameObject.tag == "BlackPiece")
                    {
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Colliderが当たってない");
            }
        }
        //右方向
        _vecX = 2.55f;
        _vecY = 2.55f;
        _vecZ = 0f;
        for (int l = 0; l < 8 - _tileFile; l++)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX += 2.5f;
                    if (_movableTile.Contains(_hit.collider))
                    {
                        break;
                    }
                    else
                    {
                        _movableTile.Add(_hit.collider);
                    }
                    _tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                    if (_hit.collider.gameObject.tag == "BlackPiece")
                    {
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Colliderが当たってない");
            }
        }
        /*==========斜め方向の動き==========*/
        //左斜め前方向
        _vecX = 2.55f;
        _vecY = 2.55f;
        _vecZ = 2.55f;
        for (int i = 0; i < 8 - _tileRank; i++)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX += 2.5f;
                    _vecZ += 2.5f;
                    if (_movableTile.Contains(_hit.collider))
                    {
                        break;
                    }
                    else
                    {
                        _movableTile.Add(_hit.collider);
                    }
                    _tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                    if (_hit.collider.gameObject.tag == "BlackPiece")
                    {
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Colliderが当たってない");
            }
        }
        //右斜め前方向
        _vecX = 2.55f;
        _vecY = 2.55f;
        _vecZ = 2.55f;
        for (int j = _tileRank; j > 1; j--)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, _vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX += 2.5f;
                    _vecZ += 2.5f;
                    if (_movableTile.Contains(_hit.collider))
                    {
                        break;
                    }
                    else
                    {
                        _movableTile.Add(_hit.collider);
                    }
                    _tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                    if (_hit.collider.gameObject.tag == "BlackPiece")
                    {
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Colliderが当たってない");
            }
        }
        //左斜め後ろ方向
        _vecX = 2.55f;
        _vecY = 2.55f;
        _vecZ = 2.55f;
        for (int k = _tileFile; k > 1; k--)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, -_vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(-_vecX, -_vecY, -_vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX += 2.5f;
                    _vecZ += 2.5f;
                    if (_movableTile.Contains(_hit.collider))
                    {
                        break;
                    }
                    else
                    {
                        _movableTile.Add(_hit.collider);
                    }
                    _tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                    if (_hit.collider.gameObject.tag == "BlackPiece")
                    {
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Colliderが当たってない");
            }
        }
        //右斜め後ろ方向
        _vecX = 2.55f;
        _vecY = 2.55f;
        _vecZ = 2.55f;
        for (int l = 0; l < 8 - _tileFile; l++)
        {
            Debug.DrawRay(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), Color.yellow, 10f);
            if (Physics.Raycast(_pieceInfo.transform.position + new Vector3(0f, 2.6f, 0f), new Vector3(_vecX, -_vecY, -_vecZ), out _hit, 100))
            {
                if (_hit.collider.gameObject.tag == _pieceInfo.tag)
                {
                    _hit.collider.gameObject.GetComponent<Collider>().enabled = false;
                    Debug.Log(_hit.collider.gameObject.name + "より先にはすすめません");
                    break;
                }
                else if (_hit.collider.gameObject.tag != _pieceInfo.tag)
                {
                    _vecX += 2.5f;
                    _vecZ += 2.5f;
                    if (_movableTile.Contains(_hit.collider))
                    {
                        break;
                    }
                    else
                    {
                        _movableTile.Add(_hit.collider);
                    }
                    _tile.Remove(_hit.collider);
                    Debug.Log(_hit.collider.gameObject.name + "に進むことが出来ます");
                    if (_hit.collider.gameObject.tag == "BlackPiece")
                    {
                        break;
                    }
                }
            }
            else
            {
                Debug.Log("Colliderが当たってない");
            }
        }
        //移動範囲以外のColliderをoffにする処理を書く
        foreach (Collider col in _tile)
        {
            col.enabled = false;
            Debug.Log(col + "のColliderをoffにします");
        }
    }
}
