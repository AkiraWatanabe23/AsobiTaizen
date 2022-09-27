using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour
{
    //��̈ړ�����
    public static int moveCount;
    [SerializeField] int _maxMoveCount;
    //�l������̐�
    public static int _getPieceCount;

    [SerializeField] public Text _moveText;
    [SerializeField] public Text _maxText;
    [SerializeField] Image _clearImage;
    [SerializeField] Image _failedImage;
    //�ŏ��̓G��̐�
    int _start;

    // Start is called before the first frame update
    void Start()
    {
        _getPieceCount = 0;
        moveCount = _maxMoveCount;
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
        _moveText.text = moveCount.ToString();
        _maxText.text = _maxMoveCount.ToString();

        //�N���A��
        if (_getPieceCount == _start)
        {
            _clearImage.gameObject.SetActive(true);
        }
        //���s��
        else if (_getPieceCount != _start && moveCount == 0)
        {
            _failedImage.gameObject.SetActive(true);
        }
    }
}
