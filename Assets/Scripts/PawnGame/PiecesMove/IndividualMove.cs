using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 駒の個別(Individual)の動き
/// </summary>
public class IndividualMove : PieceMove, IPointerClickHandler
{
    [Tooltip("ポーンの移動回数")] int _moveCount = 0;
    [Tooltip("マスの間隔")] float _masuSpace = 2.5f;
    GameObject go;

    //必要なマスにRayを飛ばし(駒からではなく、カメラ視点からのRayでやってみる)、それ以外のマスは強制的にoffにする?→誤選択で移動しなくなる(?)
    //ターンの切り替わり毎にoff→onにする必要がある
    //※駒があるマスはCollider off(これはBoardInfo()で書いてる)
    //移動可能範囲のマスだけ
    //選択したマスのtagでマスの色を変化させる(駒がとれるなら黄色、"Tile"tagなら青色など)

    /// <summary>
    /// マウスクリックが行われた(どのマウスクリックでも実行される)時の処理
    /// </summary>
    /// <param name="eventData"></param>
    public new void OnPointerClick(PointerEventData eventData)
    {
        go = eventData.pointerCurrentRaycast.gameObject;
        //↑カメラから現在のマウスカーソルの位置にRayを飛ばし、当たったオブジェクトを代入する
        var piece = go.GetComponent<PieceMove>();

        Debug.DrawRay(go.transform.position, new Vector3(0f, -0.3f, _masuSpace), Color.yellow, 20f);

        print($"{ name } を選んだ");
        piece.ChangeState();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    /// <summary>
    /// 駒の個別の移動(PieceMoveのMoveを実行している部分で実行する)
    /// もっと簡単なやり方を探す...
    /// </summary>
    public void MovableSpace()
    {
        //ポーンの動き
        if (this.gameObject.GetComponent<PieceMove>()._type == PieceType.Pawn)
        {
            Debug.Log("ポーンが選択されました");
            //1,1回目の動きか、そうでないか
            //　1回目の場合→2マス移動可
            if (_moveCount == 0)
            {
                Debug.DrawRay(go.transform.position + new Vector3(0, 2, 0), new Vector3(0f, -2.5f, 2.5f), Color.yellow, 20f);
                Debug.DrawRay(go.transform.position + new Vector3(0, 2, 0), new Vector3(0f, -2.5f, 5f), Color.yellow, 20f);
                _moveCount++;
            }
            //2,2回目以降は1マス移動
            else if (_moveCount != 0)
            {
                Debug.DrawRay(go.transform.position + new Vector3(0, 2, 0), new Vector3(0f, -2.5f, 2.5f), Color.yellow, 20f);
            }
            //　常に斜め1コ前は探索(アンパッサンに使える?)
            //3,アンパッサン...真隣のマス探索
        }
        //ナイトの動き
        else if (this.gameObject.GetComponent<PieceMove>()._type == PieceType.Knight)
        {
            Debug.Log("ナイトが選択されました");
            //移動可能なマス(桂馬4方向)に駒があるか、ないかの判定(あったら白or黒、なければ移動可)
            //味方駒があれば移動不可
        }
        //ビショップの動き
        else if (this.gameObject.GetComponent<PieceMove>()._type == PieceType.Bishop)
        {
            Debug.Log("ビショップが選択されました");
            //斜め方向を探索(駒があればそこでstop)
            //駒が味方→そのマスは×、敵→〇
            //左斜め前方向
            for (int i = 0; i < 8; ++i)
            {
                Debug.DrawRay(go.transform.position, new Vector3(0f, -0.3f, 2.5f), Color.yellow, 20f);
            }
            //右斜め前方向
            for (int j = 0; j < 8; ++j)
            {
                Debug.DrawRay(go.transform.position, new Vector3(0f, -0.3f, 2.5f), Color.yellow, 20f);
            }
            //左斜め後ろ方向
            for (int k = 0; k < 8; ++k)
            {
                Debug.DrawRay(go.transform.position, new Vector3(0f, -0.3f, 2.5f), Color.yellow, 20f);
            }
            //右斜め後ろ方向
            for (int l = 0; l < 8; ++l)
            {
                Debug.DrawRay(go.transform.position, new Vector3(0f, -0.3f, 2.5f), Color.yellow, 20f);
            }
        }
        //ルークの動き
        else if (this.gameObject.GetComponent<PieceMove>()._type == PieceType.Rook)
        {
            Debug.Log("ルークが選択されました");
            //上下左右を探索(駒があればそこでstop)
            //駒が味方→そのマスは×、敵→〇
            //前方向
            for (int i = 0; i < 8; ++i)
            {
                Debug.DrawRay(go.transform.position, new Vector3(0f, -0.3f, 2.5f), Color.yellow, 20f);
            }
            //後ろ方向
            for (int j = 0; j < 8; ++j)
            {
                Debug.DrawRay(go.transform.position, new Vector3(0f, -0.3f, 2.5f), Color.yellow, 20f);
            }
            //左方向
            for (int k = 0; k < 8; ++k)
            {
                Debug.DrawRay(go.transform.position, new Vector3(0f, -0.3f, 2.5f), Color.yellow, 20f);
            }
            //右方向
            for (int l = 0; l < 8; ++l)
            {
                Debug.DrawRay(go.transform.position, new Vector3(0f, -0.3f, 2.5f), Color.yellow, 20f);
            }
        }
        //クイーンの動き
        else if (this.gameObject.GetComponent<PieceMove>()._type == PieceType.Queen)
        {
            Debug.Log("クイーンが選択されました");
            //斜め方向 + 上下左右を探索(駒があればそこでstop)
            //駒が味方→そのマスは×、敵→〇
            //前方向
            for (int i = 0; i < 8; ++i)
            {
                Debug.DrawRay(go.transform.position, new Vector3(0f, -0.3f, 2.5f), Color.yellow, 20f);
            }
            //後ろ方向
            for (int j = 0; j < 8; ++j)
            {
                Debug.DrawRay(go.transform.position, new Vector3(0f, -0.3f, 2.5f), Color.yellow, 20f);
            }
            //左方向
            for (int k = 0; k < 8; ++k)
            {
                Debug.DrawRay(go.transform.position, new Vector3(0f, -0.3f, 2.5f), Color.yellow, 20f);
            }
            //右方向
            for (int l = 0; l < 8; ++l)
            {
                Debug.DrawRay(go.transform.position, new Vector3(0f, -0.3f, 2.5f), Color.yellow, 20f);
            }
            //左斜め前方向
            for (int m = 0; m < 8; ++m)
            {
                Debug.DrawRay(go.transform.position, new Vector3(0f, -0.3f, 2.5f), Color.yellow, 20f);
            }
            //右斜め前方向
            for (int n = 0; n < 8; ++n)
            {
                Debug.DrawRay(go.transform.position, new Vector3(0f, -0.3f, 2.5f), Color.yellow, 20f);
            }
            //左斜め後ろ方向
            for (int o = 0; o < 8; ++o)
            {
                Debug.DrawRay(go.transform.position, new Vector3(0f, -0.3f, 2.5f), Color.yellow, 20f);
            }
            //右斜め後ろ方向
            for (int p = 0; p < 8; ++p)
            {
                Debug.DrawRay(go.transform.position, new Vector3(0f, -0.3f, 2.5f), Color.yellow, 20f);
            }
        }
    }
}
