using System;
using Enemies;
using UnityEngine;

namespace Players
{
    [RequireComponent(typeof(Health))]
    public class Player : MonoBehaviour
    {
        private Animator _animator;
        private Detector _detector;
        private Health _health;
        private Enemy _currentTarget;
        private Enemies.Health _enemyHealth;

        public event Action<Enemy> PlayerGiveDamage;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _detector = GetComponentInChildren<Detector>();
            _health = GetComponent<Health>();
        }

        private void OnEnable()
        {
            _animator.AttackAnimationEnd += OnGiveDamage;
            _detector.EnemyClose += OnSelectTarget;
            _detector.EnemyFar += OnIdle;
            _health.PlayerDestroy += OnDestroy;
        }

        private void OnDisable()
        {
            _animator.AttackAnimationEnd -= OnGiveDamage;
            _detector.EnemyClose -= OnSelectTarget;
            _detector.EnemyFar -= OnIdle;
            _health.PlayerDestroy -= OnDestroy;
        }

        private void OnDestroy() => 
            Destroy(gameObject);

        public void CollectedAidKit(int health) =>
            _health.CollectedAidKit(health);

        public void TakeDamage(int damage) =>
            _health.TakeDamage(damage);

        private void OnGiveDamage() => 
            PlayerGiveDamage?.Invoke(_currentTarget);
    
        private void OnIdle() => 
            _animator.StopAttack();
    
        private void OnSelectTarget(Enemy enemy)
        {
            _animator.StartAttack();

            if (_currentTarget is null)
            {
                _currentTarget = enemy;

                if (_currentTarget.TryGetComponent(out Enemies.Health enemyHealth))
                {
                    _enemyHealth = enemyHealth;
                    _enemyHealth.EnemyDestroy += OnClearTarget;
                }
            }
        }

        private void OnClearTarget()
        {
            if (_enemyHealth is not null)
            {
                _enemyHealth.EnemyDestroy -= OnClearTarget;
                _enemyHealth = null;
                _currentTarget = null;
            }
        }
    }
}