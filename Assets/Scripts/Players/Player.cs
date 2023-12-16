using System;
using Enemies;
using UnityEngine;

namespace Players
{
    [RequireComponent(typeof(PlayerHealth))]
    public class Player : MonoBehaviour
    {
        private PlayerAnimator _playerAnimator;
        private PlayerDetector _playerDetector;
        private PlayerHealth _playerHealth;
        private Enemy _currentTarget;
        private Enemies.EnemyHealth _enemyEnemyHealth;

        public event Action<Enemy> PlayerGiveDamage;

        private void Awake()
        {
            _playerAnimator = GetComponentInChildren<PlayerAnimator>();
            _playerDetector = GetComponentInChildren<PlayerDetector>();
            _playerHealth = GetComponent<PlayerHealth>();
        }

        private void OnEnable()
        {
            _playerAnimator.AttackAnimationEnd += OnGiveDamage;
            _playerDetector.EnemyClose += OnSelectTarget;
            _playerDetector.EnemyFar += OnIdle;
            _playerHealth.PlayerDestroy += OnDestroy;
        }

        private void OnDisable()
        {
            _playerAnimator.AttackAnimationEnd -= OnGiveDamage;
            _playerDetector.EnemyClose -= OnSelectTarget;
            _playerDetector.EnemyFar -= OnIdle;
            _playerHealth.PlayerDestroy -= OnDestroy;
        }

        private void OnDestroy() => 
            Destroy(gameObject);

        public void CollectedAidKit(int health) =>
            _playerHealth.CollectedAidKit(health);

        public void TakeDamage(int damage) =>
            _playerHealth.TakeDamage(damage);

        private void OnGiveDamage() => 
            PlayerGiveDamage?.Invoke(_currentTarget);
    
        private void OnIdle() => 
            _playerAnimator.StopAttack();
    
        private void OnSelectTarget(Enemy enemy)
        {
            _playerAnimator.StartAttack();

            if (_currentTarget is null)
            {
                _currentTarget = enemy;

                if (_currentTarget.TryGetComponent(out Enemies.EnemyHealth enemyHealth))
                {
                    _enemyEnemyHealth = enemyHealth;
                    _enemyEnemyHealth.EnemyDestroy += OnClearTarget;
                }
            }
        }

        private void OnClearTarget()
        {
            if (_enemyEnemyHealth is not null)
            {
                _enemyEnemyHealth.EnemyDestroy -= OnClearTarget;
                _enemyEnemyHealth = null;
                _currentTarget = null;
            }
        }
    }
}