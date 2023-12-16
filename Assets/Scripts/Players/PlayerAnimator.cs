using System;
using UnityEngine;

namespace Players
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        private Animator _animator;
    
        public event Action AttackAnimationEnd;

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
            AttackAnimationEnd?.Invoke();
    }
}