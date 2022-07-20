using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] public static int _scoreWhite = 0; //îíî‘ÇÃìæì_
    [SerializeField] public static int _scoreBlack = 0; //çïî‘ÇÃìæì_

    [SerializeField] public Text _whiteTurn;
    [SerializeField] public Text _blackTurn;

    PieceController _current;
    public int _currentPlayer;
    public int _playerOne;
    public int _playerTwo;

    // Start is called before the first frame update
    void Start()
    {
        _whiteTurn = GameObject.Find("WhiteText").GetComponent<Text>();
        _blackTurn = GameObject.Find("BlackText").GetComponent<Text>();
        _current = GetComponent<PieceController>(); //Ç±Ç±Ç™è„éËÇ≠Ç¢Ç¡ÇƒÇ»Ç¢

        _currentPlayer = _current._currentPlayer;
        _playerOne = _current._playerOne;
        _playerTwo = _current._playerTwo;

        //_blackTurn.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeTurn()
    {
        if (_currentPlayer == _playerOne)
        {
            _whiteTurn.gameObject.SetActive(true);
            _blackTurn.gameObject.SetActive(false);
            Debug.Log("WhiteTurn");
        }
        else if (_currentPlayer == _playerTwo)
        {
            _whiteTurn.gameObject.SetActive(false);
            _blackTurn.gameObject.SetActive(true);
            Debug.Log("BlackTurn");
        }
    }
}
