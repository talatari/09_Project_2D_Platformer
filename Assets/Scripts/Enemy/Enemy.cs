using System;
using UnityEngine;

[RequireComponent(typeof(EnemyMover), typeof(EnemyPatrol), typeof(EnemyHealth))]
public class Enemy : MonoBehaviour
{
    private Player _currentTarget;
    private PlayerHealth _playerHealth;
    private EnemyMover _enemyMover;
    private EnemyPatrol _enemyPatrol;
    private EnemyDetector _enemyDetector;
    private EnemyAnimator _enemyAnimator;
    private EnemyHealth _enemyHealth;

    public event Action<Player> EnemyGiveDame;

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
        _enemyMover.PlayerClose += OnAttackAnimation;
        _enemyHealth.EnemyDestroy += OnDestroy;
        _enemyDetector.PlayerDetected += OnMoveTarget;
        _enemyDetector.PlayerFar += OnIdle;
        _enemyAnimator.AttackAnimationEnd += OnGiveDamage;
    }

    private void OnDisable()
    {
        _enemyMover.PlayerClose -= OnAttackAnimation;
        _enemyHealth.EnemyDestroy -= OnDestroy;
        _enemyDetector.PlayerDetected -= OnMoveTarget;
        _enemyDetector.PlayerFar -= OnIdle;
        _enemyAnimator.AttackAnimationEnd -= OnGiveDamage;
    }

    private void OnDestroy() => 
        Destroy(gameObject);

    public void TakeDamage(int damage) => 
        _enemyHealth.TakeDamage(damage);

    public void OnAttackAnimation()
    {
        _enemyAnimator.StopMove();
        _enemyAnimator.PlayAttack();
    }

    private void OnGiveDamage() => 
        EnemyGiveDame?.Invoke(_currentTarget);

    private void OnIdle() => 
        _enemyAnimator.StopAttack();

    private void OnClearTarget()
    {
        if (_playerHealth is not null)
        {
            _playerHealth.PlayerDestroy -= OnClearTarget;
            _playerHealth = null;
            _currentTarget = null;
        }
        
        _enemyMover.ClearTarger();
    }

    private void OnMoveTarget(Player player)
    {
        _enemyPatrol.StopPatrol();
        
        if (_currentTarget is null)
        {
            _currentTarget = player;
            _enemyMover.SetTarget(_currentTarget);
            
            if (_currentTarget.TryGetComponent(out PlayerHealth playerHealth))
            {
                _playerHealth = playerHealth;
                _playerHealth.PlayerDestroy += OnClearTarget;
            }
        }
    }
}