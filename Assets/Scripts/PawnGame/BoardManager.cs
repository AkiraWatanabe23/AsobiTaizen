using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �}�X�ɂ���
/// </summary>
public class BoardManager : MonoBehaviour
{
    //�}�X�̓񎟌��z���錾
    GameObject[,] _tiles = new GameObject[8, 8];
    //�}�X�̔ԍ�����]�Ŕ��f����(�̂����肩�Ȃ�...)

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
        //���g�̎q�Ƀ^�C����������������B�������ʂȏ��������Ă��銴������
        //1�����z��Ŏ擾�������̂𖳗����2�����ɕϊ����Ă���
        int count = 0;
        Collider[] itizigenn = transform.GetComponentsInChildren<Collider>();
        for (int i = 0; i < 8; ++i)
        {
            for (int j = 0; j < 8; ++j)
            {
                _tiles[i, j] = itizigenn[count].gameObject;
                count++;
                print($"{i+1} - {j+1} �Ԗڂ̃}�X�� {_tiles[i, j].name}");
            }
        }
    }
}
