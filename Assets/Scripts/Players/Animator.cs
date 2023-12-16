using System;
using UnityEngine;

namespace Players
{
    [RequireComponent(typeof(UnityEngine.Animator))]
    public class Animator : MonoBehaviour
    {
        private UnityEngine.Animator _animator;
    
        public event Action AttackAnimationEnd;

        private void Awake() => 
            _animator = GetComponent<UnityEngine.Animator>();

        public void PlayRunAnimation(float speed) => 
            _animator.SetBool(AnimatorData.Params.Run, Math.Abs(speed) > 0);

        public void PlayJumpAnimation() => 
            _animator.SetTrigger(AnimatorData.Params.Jump);

        public void StartAttack() => 
            _animator.SetBool(AnimatorData.Params.Attack, true);

        public void StopAttack() => 
            _animator.SetBool(AnimatorData.Params.Attack, false);
    
        public void AttackAnimationEnded() => 
            AttackAnimationEnd?.Invoke();
    }
}