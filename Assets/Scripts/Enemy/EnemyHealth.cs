using System;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _health = 100;

    private Enemy _enemy;

    public event Action EnemyDestroy;

    private void Awake() => 
        _enemy = GetComponent<Enemy>();

    private void OnEnable() => 
        _enemy.EnemyTakeDamage += OnTake;

    private void OnDisable() => 
        _enemy.EnemyTakeDamage -= OnTake;

    private void OnTake(int damage)
    {
        print($"Current Enemy health: {_health}, damage by: -{damage}");
        _health -= damage;

        if (_health <= 0)
            EnemyDestroy?.Invoke();
    }
}