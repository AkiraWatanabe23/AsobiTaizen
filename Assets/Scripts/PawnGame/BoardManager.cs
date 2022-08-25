using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// マスについて
/// </summary>
public class BoardManager : MonoBehaviour
{
    //マスの二次元配列を宣言
    GameObject[,] _tiles = new GameObject[8, 8];
    //マスの番号を剰余で判断する(のがありかなぁ...)

    // Start is called before the first frame update
    void Start()
    {
        Get_Tile();
    }

    void Update()
    {
        
    }

    void Get_Tile()
    {
        //自身の子にタイルを持たせるやり方。少し無駄な処理をしている感がある
        //1次元配列で取得したものを無理やり2次元に変換している
        int count = 0;
        Collider[] itizigenn = transform.GetComponentsInChildren<Collider>();
        for (int i = 0; i < 8; ++i)
        {
            for (int j = 0; j < 8; ++j)
            {
                _tiles[i, j] = itizigenn[count].gameObject;
                count++;
                print($"{i+1} - {j+1} 番目のマスは {_tiles[i, j].name}");
            }
        }
    }
}
