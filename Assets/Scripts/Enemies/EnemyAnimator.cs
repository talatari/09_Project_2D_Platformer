using System;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimator : MonoBehaviour
    {
        private Animator _animator;
        private SpriteRenderer[] _spriteRenderers;

        public event Action AttackAnimationEnded;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        }

        public void Move() => 
            _animator.SetBool(EnemyAnimatorParameters.Move, true);

        public void StopMove() => 
            _animator.SetBool(EnemyAnimatorParameters.Move, false);

        public void Flip()
        {
            foreach (SpriteRenderer spriteRenderer in _spriteRenderers)
                if (spriteRenderer.flipX)
                    spriteRenderer.flipX = false;
                else
                    spriteRenderer.flipX = true;
        }

        public void PlayAttack() => 
            _animator.SetBool(EnemyAnimatorParameters.Attack, true);

        public void StopAttack() => 
            _animator.SetBool(EnemyAnimatorParameters.Attack, false);
    
        public void AttackAnimationEnd() => 
            AttackAnimationEnded?.Invoke();
    }
}