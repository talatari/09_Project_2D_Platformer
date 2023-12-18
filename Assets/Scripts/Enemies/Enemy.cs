using System;
using Players;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(EnemyMover), typeof(EnemyPatrol), typeof(EnemyHealth))]
    public class Enemy : MonoBehaviour
    {
        private Player _currentTarget;
        private PlayerHealth _playerPlayerHealth;
        private EnemyMover _enemyMover;
        private EnemyPatrol _enemyPatrol;
        private EnemyDetector _enemyDetector;
        private EnemyAnimator _enemyAnimator;
        private EnemyHealth _enemyHealth;

        public event Action<Player> GivenDamage;

        private void Awake()
        {
            _enemyMover = GetComponent<EnemyMover>();
            _enemyPatrol = GetComponent<EnemyPatrol>();
            _enemyHealth = GetComponent<EnemyHealth>();
            _enemyDetector = GetComponentInChildren<EnemyDetector>();
            _enemyAnimator = GetComponentInChildren<EnemyAnimator>();
        }

        private void OnEnable()
        {
            _enemyMover.PlayerReached += OnAttackAnimation;
            _enemyHealth.Destroyed += OnDestroy;
            _enemyDetector.PlayerDetected += OnMoveTarget;
            _enemyDetector.PlayerGone += OnIdle;
            _enemyDetector.Died += OnDestroy;
            _enemyAnimator.AttackAnimationEnded += OnGiveDamage;
        }

        private void OnDisable()
        {
            _enemyMover.PlayerReached -= OnAttackAnimation;
            _enemyHealth.Destroyed -= OnDestroy;
            _enemyDetector.PlayerDetected -= OnMoveTarget;
            _enemyDetector.PlayerGone -= OnIdle;
            _enemyDetector.Died -= OnDestroy;
            _enemyAnimator.AttackAnimationEnded -= OnGiveDamage;
        }

        private void OnDestroy() => 
            Destroy(gameObject);

        public void TakeDamage(float damage) => 
            _enemyHealth.TakeDamage(damage);

        public void OnAttackAnimation()
        {
            _enemyAnimator.StopMove();
            _enemyAnimator.PlayAttack();
        }

        public void OnClearTarget()
        {
            if (_playerPlayerHealth != null)
            {
                _playerPlayerHealth.Destroyed -= OnClearTarget;
                _playerPlayerHealth = null;
                _currentTarget = null;
            }
        
            _enemyMover.ClearTarget();
        }

        private void OnGiveDamage() => 
            GivenDamage?.Invoke(_currentTarget);

        private void OnIdle() => 
            _enemyAnimator.StopAttack();

        private void OnMoveTarget(Player player)
        {
            _enemyPatrol.StopPatrol();
        
            if (_currentTarget == null)
            {
                _currentTarget = player;
                _enemyMover.SetTarget(_currentTarget, this);
            
                if (_currentTarget.TryGetComponent(out PlayerHealth playerHealth))
                {
                    _playerPlayerHealth = playerHealth;
                    _playerPlayerHealth.Destroyed += OnClearTarget;
                }
            }
        }
    }
}