using System;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private readonly string _jump = "Jump";
    private readonly string _run = "Run";

    private void Start() => _animator = GetComponent<Animator>();

    public void PlayRunAnimation(float speed)
    {
        if (Math.Abs(speed) > 0)
        {
            _animator.SetBool(_run, true);
        }
        else
        {
            _animator.SetBool(_run, false);
        }
    }

    public void PlayJumpAnimation() => _animator.SetTrigger(_jump);
}
