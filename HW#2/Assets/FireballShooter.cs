using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballShooter : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform shootingPosition;
    private Vector3 dir;
    
    private void Start()
    {
        dir = shootingPosition.position - transform.position;
        StartCoroutine(ShootFireballs());
    }

    private IEnumerator ShootFireballs()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            var fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
            fireball.GetComponent<FireballMovement>().Init(dir);
        }
    }
}
