using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// マスにある駒の情報を取得する
/// </summary>
public class BoardInfo : MonoBehaviour
{
    RaycastHit _hit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInfo();
    }

    void GetInfo()
    {
        //それぞれのマスが上方向にRayを飛ばし、駒に当たったらその駒のtagをマスに代入
        //駒があった時の代入はOK←→動かした後に[Tile]tagに戻らない
        if (Physics.Raycast(gameObject.transform.position, Vector3.up, out _hit, 5))
        {
            if (_hit.collider != null)
            {
                if (_hit.collider.gameObject.tag == "WhitePiece")
                {
                    this.gameObject.tag = "WhitePiece";
                    Debug.Log("aaa");
                }
                else if (_hit.collider.gameObject.tag == "BlackPiece")
                {
                    this.gameObject.tag = "BlackPiece";
                    Debug.Log("bbb");
                }
            }
            else if (_hit.collider == null)
            {
                this.gameObject.tag = "Tile";
                Debug.Log("ccc");
            }
        }
    }
}
