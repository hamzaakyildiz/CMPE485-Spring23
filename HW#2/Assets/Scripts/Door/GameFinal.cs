using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameFinal : MonoBehaviour
{

    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void WrongKey()
    {
        StartCoroutine(TurnRed());
    }
    
    public IEnumerator TurnRed()
    {
        for (int i = 0; i < 5; i++)
        {
            meshRenderer.material.color = Color.red;
            yield return new WaitForSeconds(.25f);
            meshRenderer.material.color = Color.white;
            yield return new WaitForSeconds(.25f);
        }

        this.enabled = false;
    }
}
