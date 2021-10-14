using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerMovement _playerMovement;

    private bool _isJumping = false;
    void Update() {
        _playerMovement.Move(Math.Sign(Input.GetAxis("Horizontal"))); // On ne convertit qu'au dernier moment pour limiter le nombre de casts

        if (Input.GetButtonDown("Jump")) {
            _isJumping = true;
        }
    }

    void FixedUpdate() {
        if (_isJumping) {
            _playerMovement.Jump();
            _isJumping = false;
        }
    }
}
