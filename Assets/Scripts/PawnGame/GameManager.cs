using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Phase
{
    White = 0,
    Black = 1,
}

public class GameManager : MonoBehaviour
{
    [SerializeField] public static int _scoreWhite = 0; //”’”Ô‚Ì“¾“_
    [SerializeField] public static int _scoreBlack = 0; //•”Ô‚Ì“¾“_

    [SerializeField] public static Phase _state = Phase.White;


    // Start is called before the first frame update
    void Start()
    {
        _state = Phase.White;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
