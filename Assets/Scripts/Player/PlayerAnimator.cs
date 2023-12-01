using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private static readonly int _runNameParameter = Animator.StringToHash("Run");
    private static readonly int _jumpNameParameter = Animator.StringToHash("Jump");
    private static readonly int _attackNameParameter = Animator.StringToHash("Attack");

    private Animator _animator;
    
    public event Action AttackAnimationEnd;

    private void Awake() => 
        _animator = GetComponent<Animator>();

    public void PlayRunAnimation(float speed) => 
        _animator.SetBool(_runNameParameter, Math.Abs(speed) > 0);

    public void PlayJumpAnimation() => 
        _animator.SetTrigger(_jumpNameParameter);

    public void StartAttack() => 
        _animator.SetBool(_attackNameParameter, true);

    public void StopAttack() => 
        _animator.SetBool(_attackNameParameter, false);
    
    public void AttackAnimationEnded() => 
        AttackAnimationEnd?.Invoke();
}