using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class FireballMovement : MonoBehaviour
{
    private Vector3 direction;
    
    public void Init(Vector3 direction)
    {
        this.direction = direction;
        transform.DOMove(transform.position + (direction * 20), 4f).SetEase(Ease.InSine).OnComplete(delegate { Destroy(gameObject); });
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            UIManager.instance.Lose();
        }
    }

}
