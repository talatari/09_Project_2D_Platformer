using System;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(UnityEngine.Animator))]
    public class Animator : MonoBehaviour
    {
        private UnityEngine.Animator _animator;
        private SpriteRenderer[] _spriteRenderers;

        public event Action AttackAnimationEnd;

        private void Awake()
        {
            _animator = GetComponent<UnityEngine.Animator>();
            _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        }

        public void Move() => 
            _animator.SetBool(AnimatorData.Params.Move, true);

        public void StopMove() => 
            _animator.SetBool(AnimatorData.Params.Move, false);

        public void Flip()
        {
            foreach (SpriteRenderer spriteRenderer in _spriteRenderers)
                if (spriteRenderer.flipX)
                    spriteRenderer.flipX = false;
                else
                    spriteRenderer.flipX = true;
        }

        public void PlayAttack() => 
            _animator.SetBool(AnimatorData.Params.Attack, true);

        public void StopAttack() => 
            _animator.SetBool(AnimatorData.Params.Attack, false);
    
        public void AttackAnimationEnded() => 
            AttackAnimationEnd?.Invoke();
    }
}