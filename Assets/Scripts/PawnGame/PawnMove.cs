using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PawnMove : PieceController
{
    /// <summary> 駒の移動可能範囲のマスを表示 </summary>
    [SerializeField] public GameObject _movePosition;
    //プロモーション選択
    [SerializeField] public GameObject _wPromotionPanel;

    // Start is called before the first frame update
    void Start()
    {
        _wPromotionPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
