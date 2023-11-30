using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health = 100;

    private Player _currentTarget;
    private EnemyMover _enemyMover;
    private EnemyPatrol _enemyPatrol;
    private EnemyDetector _enemyDetector;
    private EnemyAnimator _enemyAnimator;
    private int _damage = 3;

    public event Action EnemyDestroy;

    private void Awake()
    {
        _enemyMover = GetComponent<EnemyMover>();
        _enemyPatrol = GetComponent<EnemyPatrol>();
        _enemyDetector = GetComponentInChildren<EnemyDetector>();
        _enemyAnimator = GetComponentInChildren<EnemyAnimator>();
    }

    private void OnEnable()
    {
        _enemyMover.PlayerClose += OnAttack;
        _enemyDetector.PlayerDetected += OnMoveTarget;
        _enemyDetector.PlayerFar += OnIdle;
        _enemyAnimator.AttackAnimationEnd += OnTakeDamage;
    }

    private void OnDisable()
    {
        _enemyMover.PlayerClose -= OnAttack;
        _enemyDetector.PlayerDetected -= OnMoveTarget;
        _enemyDetector.PlayerFar -= OnIdle;
        _enemyAnimator.AttackAnimationEnd -= OnTakeDamage;
    }

    public void Take(int damage)
    {
        print($"Current Enemy health: {_health}, damage by: -{damage}");
        
        _health -= damage;

        if (_health <= 0)
        {
            EnemyDestroy?.Invoke();
            Destroy(gameObject);
        }
    }

    public void OnAttack(Player player)
    {
        _enemyAnimator.StopMove();
        
        _enemyAnimator.PlayAttackAnimation();

        if (_currentTarget is null)
        {
            _currentTarget = player;
            _currentTarget.PlayerDestroy += OnTargetClear;
        }

        if (_currentTarget.Equals(player))
            _currentTarget = player;
    }

    private void OnTakeDamage()
    {
        if (_currentTarget is not null)
            _currentTarget.Take(_damage);
    }
    
    private void OnIdle() => 
        _enemyAnimator.StopAttackAnimation();

    private void OnTargetClear()
    {
        _currentTarget.PlayerDestroy -= OnTargetClear;
        _currentTarget = null;
    }

    private void OnMoveTarget(Player player)
    {
        if (_currentTarget is null)
        {
            _currentTarget = player;
            _enemyPatrol.StopPatrol();
        }

        _enemyMover.SetTarget(player);
    }
}