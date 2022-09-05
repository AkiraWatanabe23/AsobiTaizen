using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterPromotion : MonoBehaviour
{

    public void PromReturn()
    {
        foreach (Promotion i in GetComponentsInChildren<Promotion>())
        {
            i._promWhite = null;
            i._promBlack = null;
        }
    }
}
