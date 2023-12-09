using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer[] _spriteRenderers;

    public event Action AttackAnimationEnd;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    public void Move() => 
        _animator.SetBool(EnemyAnimatorData.Params.Move, true);

    public void StopMove() => 
        _animator.SetBool(EnemyAnimatorData.Params.Move, false);

    public void Flip()
    {
        foreach (SpriteRenderer spriteRenderer in _spriteRenderers)
            if (spriteRenderer.flipX)
                spriteRenderer.flipX = false;
            else
                spriteRenderer.flipX = true;
    }

    public void PlayAttack() => 
        _animator.SetBool(EnemyAnimatorData.Params.Attack, true);

    public void StopAttack() => 
        _animator.SetBool(EnemyAnimatorData.Params.Attack, false);
    
    public void AttackAnimationEnded() => 
        AttackAnimationEnd?.Invoke();
}