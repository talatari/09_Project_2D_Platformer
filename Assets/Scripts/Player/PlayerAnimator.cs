using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;

    private void Awake() => 
        _animator = GetComponent<Animator>();

    public void PlayRunAnimation(float speed)
    {
        string run = "Run";
        
        _animator.SetBool(run, Math.Abs(speed) > 0);
    }

    public void PlayJumpAnimation()
    {
        string jump = "Jump";

        _animator.SetTrigger(jump);
    }

    public void PlayAttackAnimation()
    {
        string attack = "Attack";
        
        _animator.SetBool(attack, true);
    }

    public void StopAttackAnimation()
    {
        string attack = "Attack";
        
        _animator.SetBool(attack, false);
    }
}