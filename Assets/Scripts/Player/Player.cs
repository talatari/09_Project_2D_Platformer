using System;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
public class Player : MonoBehaviour
{
    private PlayerAnimator _playerAnimator;
    private PlayerDetector _playerDetector;
    private PlayerHealth _playerHealth;
    private Enemy _currentTarget;
    private EnemyHealth _enemyHealth;

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

            if (_currentTarget.TryGetComponent(out EnemyHealth enemyHealth))
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