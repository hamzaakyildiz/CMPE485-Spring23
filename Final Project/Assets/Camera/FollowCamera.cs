using System;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target; // The target object to follow (your car)
    public Vector3 offset = new (0,2,-4); // The offset position from the target
    public float dampingTime = 0.2f; // The time to damp the movement

    private Vector3 velocity = Vector3.zero;
    

    private void FixedUpdate()
    {
        if (target)
        {
            Vector3 targetPosition = target.position + target.TransformDirection(offset);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, dampingTime);

            // Look at the target
            transform.LookAt(target);
        }
    }
}