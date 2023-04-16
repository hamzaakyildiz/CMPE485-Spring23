using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public bool isTrueKey;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Door") && isTrueKey)
        {
            UIManager.instance.Win();
        }
        else if (other.CompareTag("Door") && !isTrueKey)
        {
            other.GetComponent<GameFinal>().WrongKey();
            Destroy(gameObject);
        }
    }
}
