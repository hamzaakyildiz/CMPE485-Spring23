using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Vector3 rbForce = new Vector3(0.1f,0f,0f);
    
    void Start()
    {
        FollowBall.instance.SetTarget(transform);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(rbForce,ForceMode.Force);
    }
}
