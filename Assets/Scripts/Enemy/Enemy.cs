using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health = 100;

    private Player _target;
    private EnemyPatrol _enemyPatrol;
    private EnemyDetector _enemyDetector;
    private Animator _animator;
    private int _damage = 10;

    public event Action EnemyDestroy;

    private void Awake()
    {
        _enemyPatrol = GetComponent<EnemyPatrol>();
        _enemyDetector = GetComponentInChildren<EnemyDetector>();
    }

    private void OnEnable()
    {
        _enemyDetector.PlayerClose += OnAttack;
    }

    private void OnDisable()
    {
        _enemyDetector.PlayerClose -= OnAttack;
    }

    public void Take(int damage)
    {
        print($"Current enemy health: {_health}, damage by: -{damage}");
        
        _health -= damage;

        if (_health <= 0)
        {
            EnemyDestroy?.Invoke();
            Destroy(gameObject);
        }
    }

    private void Attack(Player player) => 
        player.Take(_damage);

    private void OnAttack(Player player)
    {
        

        if (_target is null)
        {
            _target = player;
            _enemyPatrol.StopPatrol();
        }
            
    }
}