using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private static readonly int _isMoving = Animator.StringToHash("isMoving");

    private Animator _animator;
    private SpriteRenderer[] _spriteRenderers;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    public void Move()
    {
        _animator.SetBool(_isMoving, true);
    }

    public void Idle()
    {
        _animator.SetBool(_isMoving, false);
    }

    public void Flip()
    {
        foreach (SpriteRenderer spriteRenderer in _spriteRenderers)
            if (spriteRenderer.flipX)
                spriteRenderer.flipX = false;
            else
                spriteRenderer.flipX = true;
    }
}