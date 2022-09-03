using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �|�[���̓��ʂȓ���
/// </summary>
public class Promotion : MonoBehaviour
{
    MasuSearch _search;
    [Tooltip("�v�����[�V�������ɕ\������")] Image _promImage;

    // Start is called before the first frame update
    void Start()
    {
        _search = GameObject.Find("Board,Tile").GetComponent<MasuSearch>();
        _promImage = GameObject.Find("PromotionPanel").GetComponent<Image>();
        _promImage.gameObject.SetActive(false);
    }

    public void OnClick()
    {
        if (gameObject.name == "Queen")
        {
            Debug.Log("�N�C�[���Ƀv�����[�V�������܂�");
        }
        else if (gameObject.name == "Rook")
        {
            Debug.Log("���[�N�Ƀv�����[�V�������܂�");
        }
        else if (gameObject.name == "Bishop")
        {
            Debug.Log("�r�V���b�v�Ƀv�����[�V�������܂�");
        }
        else if (gameObject.name == "Knight")
        {
            Debug.Log("�i�C�g�Ƀv�����[�V�������܂�");
        }
    }

    /// <summary>
    /// �v�����[�V��������(��)
    /// </summary>
    public void Promotion_White()
    {
        if (_search._tileRank == 8)
        {
            _promImage.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// �v�����[�V��������(��)
    /// </summary>
    public void Promotion_Black()
    {
        if (_search._tileRank == 1)
        {
            _promImage.gameObject.SetActive(true);
        }
    }
}
