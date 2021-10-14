using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerMovement _playerMovement;

    void Update() {
        _playerMovement.Move(Math.Sign(Input.GetAxis("Horizontal"))); // On ne convertit qu'au dernier moment pour limiter le nombre de casts
    }
}
