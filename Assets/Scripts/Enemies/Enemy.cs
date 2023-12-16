using System;
using Players;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Mover), typeof(EnemyPatrol), typeof(Health))]
    public class Enemy : MonoBehaviour
    {
        private Player _currentTarget;
        private Players.Health _playerHealth;
        private Mover _mover;
        private EnemyPatrol _enemyPatrol;
        private Detector _detector;
        private Animator _animator;
        private Health _health;

        public event Action<Player> EnemyGiveDame;

        private void Awake()
        {
            _mover = GetComponent<Mover>();
            _enemyPatrol = GetComponent<EnemyPatrol>();
            _health = GetComponent<Health>();
            _detector = GetComponentInChildren<Detector>();
            _animator = GetComponentInChildren<Animator>();
        }

        private void OnEnable()
        {
            _mover.PlayerClose += OnAttackAnimation;
            _health.EnemyDestroy += OnDestroy;
            _detector.PlayerDetected += OnMoveTarget;
            _detector.PlayerFar += OnIdle;
            _animator.AttackAnimationEnd += OnGiveDamage;
        }

        private void OnDisable()
        {
            _mover.PlayerClose -= OnAttackAnimation;
            _health.EnemyDestroy -= OnDestroy;
            _detector.PlayerDetected -= OnMoveTarget;
            _detector.PlayerFar -= OnIdle;
            _animator.AttackAnimationEnd -= OnGiveDamage;
        }

        private void OnDestroy() => 
            Destroy(gameObject);

        public void TakeDamage(int damage) => 
            _health.TakeDamage(damage);

        public void OnAttackAnimation()
        {
            _animator.StopMove();
            _animator.PlayAttack();
        }

        private void OnGiveDamage() => 
            EnemyGiveDame?.Invoke(_currentTarget);

        private void OnIdle() => 
            _animator.StopAttack();

        private void OnClearTarget()
        {
            if (_playerHealth is not null)
            {
                _playerHealth.PlayerDestroy -= OnClearTarget;
                _playerHealth = null;
                _currentTarget = null;
            }
        
            _mover.ClearTarger();
        }

        private void OnMoveTarget(Player player)
        {
            _enemyPatrol.StopPatrol();
        
            if (_currentTarget is null)
            {
                _currentTarget = player;
                _mover.SetTarget(_currentTarget);
            
                if (_currentTarget.TryGetComponent(out Players.Health playerHealth))
                {
                    _playerHealth = playerHealth;
                    _playerHealth.PlayerDestroy += OnClearTarget;
                }
            }
        }
    }
}