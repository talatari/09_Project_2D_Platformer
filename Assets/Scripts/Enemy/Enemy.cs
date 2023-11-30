using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health = 100;

    private Player _target;
    private EnemyPatrol _enemyPatrol;
    private EnemyDetector _enemyDetector;
    private EnemyMover _enemyMover;
    private Animator _animator;
    private int _damage = 3;

    public event Action EnemyDestroy;

    private void Awake()
    {
        _enemyPatrol = GetComponent<EnemyPatrol>();
        _enemyDetector = GetComponentInChildren<EnemyDetector>();
        _enemyMover = GetComponent<EnemyMover>();
    }

    private void OnEnable()
    {
        _enemyDetector.PlayerDetected += OnMoveTarget;
        _enemyMover.PlayerClose += OnAttack;
    }

    private void OnDisable()
    {
        _enemyDetector.PlayerDetected -= OnMoveTarget;
        _enemyMover.PlayerClose -= OnAttack;
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

    public void OnAttack()
    {
        if (_target is not null)
        {
            _target.Take(_damage);
        }
    }

    private void OnMoveTarget(Player player)
    {
        if (_target is null)
        {
            _target = player;
            _enemyPatrol.StopPatrol();
        }

        _enemyMover.SetTarget(player);
    }
}