using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    private Vector3 startPos = new Vector3(60f, 38f, -10f);
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(ballPrefab,startPos,Quaternion.identity,transform.parent);
            this.enabled = false;
        }
    }
}
