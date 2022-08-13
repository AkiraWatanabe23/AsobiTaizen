using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// マスにある駒の情報を取得する
/// </summary>
public class BoardInfo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //それぞれのマスが上方向(?)にRayを飛ばし、駒に当たったらその駒のtagをマスに代入
        //駒が個別の移動でマス情報を取得する時に使える...と思う
    }
}
