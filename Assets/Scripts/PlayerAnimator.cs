using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private readonly string _jump = "Jump";
    private readonly string _run = "Run";

    private void OnValidate() => _animator ??= GetComponent<Animator>();

    public void PlayRunAnimation(float speed) => _animator.SetBool(_run, Math.Abs(speed) > 0);

    public void PlayJumpAnimation() => _animator.SetTrigger(_jump);
    
    
}