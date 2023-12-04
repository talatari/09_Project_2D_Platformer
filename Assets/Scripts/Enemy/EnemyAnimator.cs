using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    private static readonly int _moveNameParameter = Animator.StringToHash("Move");
    private static readonly int _attackNameParameter = Animator.StringToHash("Attack");

    private Animator _animator;
    private SpriteRenderer[] _spriteRenderers;
    
    public event Action AttackAnimationEnd = delegate { };

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    public void Move() => 
        _animator.SetBool(_moveNameParameter, true);

    public void StopMove() => 
        _animator.SetBool(_moveNameParameter, false);

    public void Flip()
    {
        foreach (SpriteRenderer spriteRenderer in _spriteRenderers)
            if (spriteRenderer.flipX)
                spriteRenderer.flipX = false;
            else
                spriteRenderer.flipX = true;
    }

    public void PlayAttack() => 
        _animator.SetBool(_attackNameParameter, true);

    public void StopAttack() => 
        _animator.SetBool(_attackNameParameter, false);
    
    public void AttackAnimationEnded() => 
        AttackAnimationEnd();
}