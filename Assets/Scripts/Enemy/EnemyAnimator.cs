using System;
using UnityEngine;

public static class PlayerAnimatorData
{
    public static class Params
    {
        public static readonly int Move = Animator.StringToHash(nameof(Move));
        public static readonly int Attack = Animator.StringToHash(nameof(Attack));
    }
}

[RequireComponent(typeof(Animator))]
public class EnemyAnimator : MonoBehaviour
{
    private Animator _animator;
    private SpriteRenderer[] _spriteRenderers;

    public event Action AttackAnimationEnd = delegate { };

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    public void Move() => 
        _animator.SetBool(PlayerAnimatorData.Params.Move, true);

    public void StopMove() => 
        _animator.SetBool(PlayerAnimatorData.Params.Move, false);

    public void Flip()
    {
        foreach (SpriteRenderer spriteRenderer in _spriteRenderers)
            if (spriteRenderer.flipX)
                spriteRenderer.flipX = false;
            else
                spriteRenderer.flipX = true;
    }

    public void PlayAttack() => 
        _animator.SetBool(PlayerAnimatorData.Params.Attack, true);

    public void StopAttack() => 
        _animator.SetBool(PlayerAnimatorData.Params.Attack, false);
    
    public void AttackAnimationEnded() => 
        AttackAnimationEnd();
}