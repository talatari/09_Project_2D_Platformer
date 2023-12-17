using System;
using Enemies;
using UI;
using UnityEngine;

namespace Players
{
    [RequireComponent(typeof(PlayerHealth), typeof(PlayerVampirism))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerVampirismButton _playerVampirismButton;

        private PlayerAnimator _playerAnimator;
        private PlayerDetector _playerDetector;
        private PlayerHealth _playerHealth;
        private Enemy _currentTarget;
        private EnemyHealth _enemyEnemyHealth;
        private PlayerVampirism _playerVampirism;

        public event Action<Enemy> PlayerGiveDamage;

        private void OnValidate()
        {
            if (_playerVampirismButton == null)
                _playerVampirismButton = FindObjectOfType<PlayerVampirismButton>();
        }

        private void Awake()
        {
            _playerAnimator = GetComponentInChildren<PlayerAnimator>();
            _playerDetector = GetComponentInChildren<PlayerDetector>();
            _playerHealth = GetComponent<PlayerHealth>();
            _playerVampirism = GetComponent<PlayerVampirism>();
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

        public void Heal(float health) =>
            _playerHealth.Heal(health);

        public void TakeDamage(float damage) =>
            _playerHealth.TakeDamage(damage);
        
        private void OnGiveDamage() => 
            PlayerGiveDamage?.Invoke(_currentTarget);
    
        private void OnIdle()
        {
            _currentTarget = null;
            
            _playerVampirismButton.SetInactive();
            _playerVampirism.ClearTarget();
            
            _playerAnimator.StopAttack();
        }

        private void OnSelectTarget(Enemy enemy)
        {
            if (_currentTarget == null)
            {
                _currentTarget = enemy;
                
                _playerVampirismButton.SetActive();
                _playerVampirism.SetTarget(enemy, this);
            
                _playerAnimator.StartAttack();

                if (_currentTarget.TryGetComponent(out EnemyHealth enemyHealth))
                {
                    _enemyEnemyHealth = enemyHealth;
                    _enemyEnemyHealth.EnemyDestroy += OnClearTarget;
                }
            }
        }

        private void OnClearTarget()
        {
            if (_enemyEnemyHealth != null)
            {
                _enemyEnemyHealth.EnemyDestroy -= OnClearTarget;
                _enemyEnemyHealth = null;
                _currentTarget = null;
            }
        }
    }
}