using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health = 100;
    [SerializeField] private int _damage = 7;

    private PlayerAnimator _playerAnimator;
    private PlayerDetector _playerDetector;
    private Enemy _currentTarget;
    private int _coinsCollected;

    public event Action PlayerDestroy;

    private void Awake()
    {
        _playerAnimator = GetComponentInChildren<PlayerAnimator>();
        _playerDetector = GetComponentInChildren<PlayerDetector>();
    }

    private void OnEnable()
    {
        _playerAnimator.AttackAnimationEnd += OnTakeDamage;
        _playerDetector.EnemyClose += OnAttack;
        _playerDetector.EnemyFar += OnIdle;
    }

    private void OnDisable()
    {
        _playerAnimator.AttackAnimationEnd -= OnTakeDamage;
        _playerDetector.EnemyClose -= OnAttack;
        _playerDetector.EnemyFar -= OnIdle;
    }

    public void CollectedCoin() => 
        _coinsCollected++;

    public void CollectedAidKit(int health)
    {
        print($"Current health: {_health}, AidKit bust health on {health}");
        _health += health;
    }

    public void Take(int damage)
    {
        print($"Current Player health: {_health}, damage by: -{damage}");
        
        _health -= damage;
        
        if (_health <= 0)
        {
            PlayerDestroy?.Invoke();
            gameObject.SetActive(false);
        }
    }

    private void OnAttack(Enemy enemy)
    {
        _playerAnimator.PlayAttackAnimation();

        if (_currentTarget is null)
        {
            _currentTarget = enemy;
            _currentTarget.EnemyDestroy += OnTargetClear;
        }

        if (_currentTarget.Equals(enemy))
            _currentTarget = enemy;
    }

    private void OnTakeDamage()
    {
        if (_currentTarget is not null)
            _currentTarget.Take(_damage);
    }

    private void OnIdle() => 
        _playerAnimator.StopAttackAnimation();

    private void OnTargetClear()
    {
        _currentTarget.EnemyDestroy -= OnTargetClear;
        _currentTarget = null;
    }
}