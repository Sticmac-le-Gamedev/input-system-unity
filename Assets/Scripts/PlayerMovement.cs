using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] SpriteRenderer _renderer;
    [SerializeField] Rigidbody2D _rb2d;
    [SerializeField] Animator _animator;

    [SerializeField] float _speed;
    [SerializeField] float _movementSmoothing = 0.05f;

    private Vector2 _targetVelocity = Vector2.zero;

    void FixedUpdate() {
        Vector2 v = new Vector2();
        _targetVelocity = new Vector2(_targetVelocity.x, _rb2d.velocity.y); // On préserve la vélocité verticale
        _rb2d.velocity = Vector2.SmoothDamp(_rb2d.velocity, _targetVelocity, ref v, _movementSmoothing);
    }

    public void Move(int direction) {
        _targetVelocity = new Vector2(direction * _speed, _rb2d.velocity.y);

        // Animation de course
        _animator.SetBool("Running", direction != 0);

        if ((_renderer.flipX && direction > 0)
            || (!_renderer.flipX && direction < 0)) {
                Flip();
            }
    }

    public void Flip() {
        _renderer.flipX = !_renderer.flipX;
    }
}
