using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerAnimator _playerAnimator;
    private PlayerDetector _playerDetector;
    private PlayerHealth _playerHealth;
    private Enemy _currentTarget;
    private EnemyHealth _enemyHealth;

    public event Action<Enemy> PlayerGiveDamage;
    public event Action<int> PlayerTakeDamage;
    public event Action<int> PlayerHealthed;

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
        PlayerHealthed?.Invoke(health);

    public void Take(int damage) => 
        PlayerTakeDamage?.Invoke(damage);

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

        if (_currentTarget.Equals(enemy))
            _currentTarget = enemy;
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