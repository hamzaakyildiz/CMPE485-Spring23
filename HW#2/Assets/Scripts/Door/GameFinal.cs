using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameFinal : MonoBehaviour
{

    public Material doorMat;

    public void WrongKey()
    {
        StartCoroutine(TurnRed());
    }
    
    public IEnumerator TurnRed()
    {
        for (int i = 0; i < 5; i++)
        {
            doorMat.color = Color.red;
            yield return new WaitForSeconds(.25f);
            doorMat.color = Color.white;
            yield return new WaitForSeconds(.25f);
        }

        this.enabled = false;
    }
}
