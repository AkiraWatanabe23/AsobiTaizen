using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 個別(Individual)の動き
/// </summary>
public class IndividualMove : PieceMove
{
    //必要なマスにRayを飛ばし、それ以外のマスは強制的にoffにする?→誤選択で移動しなくなる(?)
    //ターンの切り替わり毎にoff→onにする必要がある
    //※駒があるマスはCollider off(これはBoardInfo()で書いてる)
    //移動可能範囲のマスだけ
    //選択したマスのtagでマスの色を変化させる(駒がとれるなら黄色、"Tile"tagなら青色など)

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    /// <summary>
    /// 駒の個別の移動
    /// </summary>
    public void MovableSpace()
    {
        //ポーンの動き
        if (this.gameObject.GetComponent<PieceMove>()._type == PieceType.Pawn)
        {
            Debug.Log("ポーンが選択されました");
            //1,1回目の動きか、そうでないか
            //　1回目の場合→2マス移動可
            //2,2回目以降は1マス移動
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
        }
        //ルークの動き
        else if (this.gameObject.GetComponent<PieceMove>()._type == PieceType.Rook)
        {
            Debug.Log("ルークが選択されました");
            //上下左右を探索(駒があればそこでstop)
            //駒が味方→そのマスは×、敵→〇
        }
        //クイーンの動き
        else if (this.gameObject.GetComponent<PieceMove>()._type == PieceType.Queen)
        {
            Debug.Log("クイーンが選択されました");
            //斜め方向 + 上下左右を探索(駒があればそこでstop)
            //駒が味方→そのマスは×、敵→〇
        }
    }
}
