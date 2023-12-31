using System;
using Others;
using UnityEngine;

namespace Players
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _jumpForce = 1250f;
    
        private Rigidbody2D _rigidbody2D;
        private SpriteRenderer _spriteRenderer;
        private PlayerAnimator _playerAnimator;
        private float _horizontalMove;
        private bool _isGrounded;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _playerAnimator = GetComponentInChildren<PlayerAnimator>();
        }

        private void Update()
        {
            Moving();

            Fliping();

            TryJump();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.TryGetComponent(out Ground ground))
                _isGrounded = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Ground ground))
                _isGrounded = false;
        }

        private void Moving()
        {
            _horizontalMove = Input.GetAxis("Horizontal") * _speed;
            _rigidbody2D.velocity = new Vector2(_horizontalMove, _rigidbody2D.velocity.y);

            _playerAnimator.PlayRunAnimation(_horizontalMove);
        }

        private void Fliping()
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                _spriteRenderer.flipX = true;
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) 
                _spriteRenderer.flipX = false;
        }
        
        private void TryJump()
        {
            if (_isGrounded)
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
                {
                    _playerAnimator.PlayJumpAnimation();
                    _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                }
        }
    }
}