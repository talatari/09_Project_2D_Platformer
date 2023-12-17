using System;
using UnityEngine;

namespace Players
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        private Animator _animator;
    
        public event Action AttackAnimationEnded;

        private void Awake() => 
            _animator = GetComponent<Animator>();

        public void PlayRunAnimation(float speed) => 
            _animator.SetBool(PlayerAnimatorParameters.Run, Math.Abs(speed) > 0);

        public void PlayJumpAnimation() => 
            _animator.SetTrigger(PlayerAnimatorParameters.Jump);

        public void StartAttack() => 
            _animator.SetBool(PlayerAnimatorParameters.Attack, true);

        public void StopAttack() => 
            _animator.SetBool(PlayerAnimatorParameters.Attack, false);
    
        public void AttackAnimationEnd() => 
            AttackAnimationEnded?.Invoke();
    }
}