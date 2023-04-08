using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

namespace Player
{
    public class Animations : MonoBehaviour
    {
        private Animator _playerAnimations;

        private void Awake()
        {
            _playerAnimations = GetComponent<Animator>();
        }

        public void ChangeAnimation(string state)
        {
            switch (state)
            {
                case "Grounded":
                    _playerAnimations.SetBool("isMoving", false);
                    break;
                case "Moving":
                    _playerAnimations.SetBool("isMoving", true);
                    break;
                case "Jumping":
                    _playerAnimations.SetTrigger("isJumping");
                    break;
            }
        }
    }
}