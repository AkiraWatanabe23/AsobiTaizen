using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PawnMove : PieceController
{
    /// <summary> ��̈ړ��\�͈͂̃}�X��\�� </summary>
    [SerializeField] public GameObject _movePosition;
    //�v�����[�V�����I��
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
