using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    //‹î‚ÌˆÚ“®§ŒÀ
    public static int _moveCount;
    public static int _maxMoveCount;
    //Šl‚Á‚½‹î‚Ì”
    public int _getPieceCount;

    [SerializeField] public Text _moveText;
    [SerializeField] public Text _maxText;
    [SerializeField] Image _clearImage;
    [SerializeField] Image _failedImage;
    //Å‰‚Ì“G‹î‚Ì”
    int _start;

    // Start is called before the first frame update
    void Start()
    {
        _moveCount = 6;
        _maxMoveCount = 6;
        _clearImage.gameObject.SetActive(false);
        _failedImage.gameObject.SetActive(false);

        foreach (var obj in FindObjectsOfType(typeof(GameObject)))
        {
            if (obj.name.Contains("Black"))
            {
                _start++;
            }
        }
        Debug.Log(_start);
    }

    // Update is called once per frame
    void Update()
    {
        _moveText.text = _moveCount.ToString();
        _maxText.text = _maxMoveCount.ToString();

        //ƒNƒŠƒA
        if (_getPieceCount == _start && _moveCount != 0)
        {
            _clearImage.gameObject.SetActive(true);
        }
        //¸”s
        else if (_moveCount == 0 && _getPieceCount != _start)
        {
            _failedImage.gameObject.SetActive(true);
        }
    }
}
