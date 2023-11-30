using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health = 100;

    private Player _target;
    private int _damage = 10;

    public event Action EnemyDestroy;
    
    private void Attack(Player player) => 
        player.Take(_damage);

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
}