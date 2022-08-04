using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// マスについて
/// </summary>
public class BoardManager : MonoBehaviour
{/// <summary> 他のスクリプトで関数を呼び出す用 </summary>
    public static BoardManager _managerB;
    //マスの二次元配列を宣言
    GameObject[,] _tiles = new GameObject[8, 8];
    //マスの番号を剰余で判断する(のがありかなぁ...)

    public void Awake()
    {
        if (_managerB == null)
        {
            _managerB = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Get_Masu();
    }

    void Get_Masu()
    {
        //自身の子にタイルを持たせるやり方。少し無駄な処理をしている感がある
        //1次元配列で取得したものを無理やり2次元に変換している
        int count = 0;
        Collider[] itizigenn = transform.GetComponentsInChildren<Collider>();
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                _tiles[i, j] = itizigenn[count].gameObject;
                count++;
                print($"{i+1} - {j+1} 番目のマスは {_tiles[i, j].name}");
            }
        }
    }

    //void Get_Masu2()
    //{
    //    各Lineの子にタイルを持たせるやり方: テストしてないので動くかは知らない
    //    int count = 0;
    //    for (int i = 0; i < 8; i++)
    //    {
    //        int count2 = 0;
    //        Transform child = transform.GetChild(count);
    //        foreach (var j in child.transform.GetComponentsInChildren<Collider>())
    //        {
    //            _tiles[i, count2] = j.gameObject;
    //        }
    //    }
    //}

}
