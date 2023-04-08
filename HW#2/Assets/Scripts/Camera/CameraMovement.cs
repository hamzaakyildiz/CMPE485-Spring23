using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public Vector3 followOffset = new Vector3(0,-2,5.5f);

    private void Start()
    {
        transform.rotation = Quaternion.Euler(6f, 0, 0);
    }

    void LateUpdate()
    {
        transform.position = player.position - followOffset;
    }
}