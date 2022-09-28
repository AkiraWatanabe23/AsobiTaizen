using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    //****************************************************//
    //public Image Unselectable => _unselectable;
    //public Image Unselectable { get => _unselectable; }

    // public int Test { get; set; }
    private int _testNum;
    public int Get_TestNum()
    {
        return _testNum;
    }
    public void Set_TestNum(int value)
    {
        _testNum = value;
    }
    //****************************************************//

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
