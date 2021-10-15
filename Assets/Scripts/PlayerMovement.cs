using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] SpriteRenderer _renderer;
    [SerializeField] Rigidbody2D _rb2d;
    [SerializeField] Animator _animator;

    [Header("Ground Check")]
    [SerializeField] Transform _groundCheck;
    [SerializeField] LayerMask _groundLayer;

    [Header("Parameters")]
    [SerializeField] float _speed;
    [SerializeField] float _jumpForce = 200f;
    [SerializeField] float _movementSmoothing = 0.05f;

    private Vector2 _targetVelocity = Vector2.zero;
    private bool _grounded = true;

    void FixedUpdate() {
        Vector2 v = new Vector2();
        _targetVelocity = new Vector2(_targetVelocity.x, _rb2d.velocity.y); // On préserve la vélocité verticale
        _rb2d.velocity = Vector2.SmoothDamp(_rb2d.velocity, _targetVelocity, ref v, _movementSmoothing);

        // Vérifie que l'on touche le sol en tombant
        if (!_grounded && _rb2d.velocity.y < -0.001f && Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _groundLayer)) {
            _animator.SetBool("Jumping", false);
            _grounded = true;
        }
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

    public void Jump() {
        if (_grounded) { // Saut autorisé uniquement si on se trouve au sol
            _grounded = false;
            _animator.SetBool("Jumping", true);
            _rb2d.AddForce(new Vector2(0f, _jumpForce)); // Force vers le haut pour le saut
        }
    }
}
