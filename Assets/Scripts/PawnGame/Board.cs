using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

    public int[,] board = new int[8,8]; // 8*8の二次元配列

    // Start is called before the first frame update
    void Start()
    {
        InitializeArray(); //宣言した配列の初期化
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitializeArray()
    {
        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                board[i, j] = 0;
            }
        }
    }
}
