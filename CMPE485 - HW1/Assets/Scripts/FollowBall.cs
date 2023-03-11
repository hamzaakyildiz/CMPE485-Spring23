using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBall : MonoBehaviour
{
    public static FollowBall instance;
    [SerializeField] private Vector3 offSet;
    private Transform ball;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void LateUpdate()
    {
        transform.position = ball.position - offSet;
    }

    public void SetTarget(Transform target)
    {
        ball = target;
    }
}
