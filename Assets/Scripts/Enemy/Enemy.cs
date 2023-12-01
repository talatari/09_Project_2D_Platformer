using System;
using UnityEngine;

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
    public event Action<int> EnemyTakeDamage;

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
        _enemyAnimator.AttackAnimationEnd += OnGiveDamage;
        _enemyMover.PlayerClose += OnSelectTarget;
        _enemyDetector.PlayerDetected += OnMoveTarget;
        _enemyDetector.PlayerFar += OnIdle;
        _enemyHealth.EnemyDestroy += OnDestroy;
    }

    private void OnDisable()
    {
        _enemyAnimator.AttackAnimationEnd -= OnGiveDamage;
        _enemyMover.PlayerClose -= OnSelectTarget;
        _enemyDetector.PlayerDetected -= OnMoveTarget;
        _enemyDetector.PlayerFar -= OnIdle;
        _enemyHealth.EnemyDestroy -= OnDestroy;
    }

    private void OnDestroy() => 
        Destroy(gameObject);

    public void Take(int damage) => 
        EnemyTakeDamage?.Invoke(damage);

    public void OnSelectTarget(Player player)
    {
        _enemyAnimator.StopMove();
        
        _enemyAnimator.PlayAttack();

        if (_currentTarget is null)
        {
            _currentTarget = player;

            if (_currentTarget.TryGetComponent(out PlayerHealth playerHealth))
            {
                _playerHealth = playerHealth;
                _playerHealth.PlayerDestroy += OnClearTarget;
            }
        }

        if (_currentTarget.Equals(player))
            _currentTarget = player;
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