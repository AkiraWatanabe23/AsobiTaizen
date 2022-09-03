using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ポーンの特別な動き
/// </summary>
public class Promotion : MonoBehaviour
{
    MasuSearch _search;
    [Tooltip("プロモーション時に表示する")] Image _promImage;

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
            Debug.Log("クイーンにプロモーションします");
        }
        else if (gameObject.name == "Rook")
        {
            Debug.Log("ルークにプロモーションします");
        }
        else if (gameObject.name == "Bishop")
        {
            Debug.Log("ビショップにプロモーションします");
        }
        else if (gameObject.name == "Knight")
        {
            Debug.Log("ナイトにプロモーションします");
        }
    }

    /// <summary>
    /// プロモーション処理(白)
    /// </summary>
    public void Promotion_White()
    {
        if (_search._tileRank == 8)
        {
            _promImage.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// プロモーション処理(黒)
    /// </summary>
    public void Promotion_Black()
    {
        if (_search._tileRank == 1)
        {
            _promImage.gameObject.SetActive(true);
        }
    }
}
