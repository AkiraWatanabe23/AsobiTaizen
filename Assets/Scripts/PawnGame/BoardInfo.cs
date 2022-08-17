using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// マスにある駒の情報を取得する
/// </summary>
public class BoardInfo : MonoBehaviour
{
    RaycastHit _hit;

    // Update is called once per frame
    void Update()
    {
        GetInfo();
    }

    /// <summary>
    /// それぞれのマスが上方向にRayを飛ばし、駒に当たったらその駒のtag("WhitePiece" or "BlackPiece")をマスに代入
    /// なにもなければ"Tile"tagにする
    /// 駒があるマスはColliderをoffにする
    /// </summary>
    void GetInfo()
    {
        //Rayを指定の方向に飛ばす処理(オブジェクトに当たるのが前提の書き方)
        if (Physics.Raycast(gameObject.transform.position, Vector3.up, out _hit, 5))
        {
            //マスに白駒がある場合
            if (_hit.collider.gameObject.tag == "WhitePiece")
            {
                this.gameObject.tag = "WhitePiece";
                this.gameObject.GetComponent<BoxCollider>().enabled = false;
            }
            //マスに黒駒がある場合
            else if (_hit.collider.gameObject.tag == "BlackPiece")
            {
                this.gameObject.tag = "BlackPiece";
                this.gameObject.GetComponent<BoxCollider>().enabled = false;
            }
        }
        //マスに駒がない場合
        else if (_hit.collider == null)
        {
            this.gameObject.tag = "Tile";
            this.gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
