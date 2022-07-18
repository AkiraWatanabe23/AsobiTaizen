using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PawnMove : PieceController, IPointerClickHandler
{
    /// <summary> 駒の移動可能範囲のマスを表示 </summary>
    [SerializeField] public GameObject _movePosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
