using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

    public int[,] board = new int[8,8]; // 8*8�̓񎟌��z��

    // Start is called before the first frame update
    void Start()
    {
        InitializeArray(); //�錾�����z��̏�����
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
