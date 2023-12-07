using System;
using UnityEngine;

public static class PlayerAnimatorData
{
    public static class Params
    {
        public static readonly int Run = Animator.StringToHash(nameof(Run));
        public static readonly int Jump = Animator.StringToHash(nameof(Jump));        
        public static readonly int Attack = Animator.StringToHash(nameof(Attack));

    }
}

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    
    public event Action AttackAnimationEnd = delegate { };

    private void Awake() => 
        _animator = GetComponent<Animator>();

    public void PlayRunAnimation(float speed) => 
        _animator.SetBool(PlayerAnimatorData.Params.Run, Math.Abs(speed) > 0);

    public void PlayJumpAnimation() => 
        _animator.SetTrigger(PlayerAnimatorData.Params.Jump);

    public void StartAttack() => 
        _animator.SetBool(PlayerAnimatorData.Params.Attack, true);

    public void StopAttack() => 
        _animator.SetBool(PlayerAnimatorData.Params.Attack, false);
    
    public void AttackAnimationEnded() => 
        AttackAnimationEnd();
}