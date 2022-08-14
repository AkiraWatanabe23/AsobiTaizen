using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 個別(Individual)の動き
/// </summary>
public class IndividualMove : PieceMove
{
    //駒を選択した時にマスにRayを飛ばし、移動不可のマスのColliderをoffにする→誤選択で移動しなくなる?
    //↑これだと全部のマスにRayを飛ばすことになる?にRayを飛ばし、それ以外のマスは強制的にoffにする?
    //ターンの切り替わり毎にoff→onにする必要がある
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
        }
        //ナイトの動き
        else if (this.gameObject.GetComponent<PieceMove>()._type == PieceType.Knight)
        {
            Debug.Log("ナイトが選択されました");
        }
        //ビショップの動き
        else if (this.gameObject.GetComponent<PieceMove>()._type == PieceType.Bishop)
        {
            Debug.Log("ビショップが選択されました");
        }
        //ルークの動き
        else if (this.gameObject.GetComponent<PieceMove>()._type == PieceType.Rook)
        {
            Debug.Log("ルークが選択されました");
        }
        //クイーンの動き
        else if (this.gameObject.GetComponent<PieceMove>()._type == PieceType.Queen)
        {
            Debug.Log("クイーンが選択されました");
        }
    }
}
