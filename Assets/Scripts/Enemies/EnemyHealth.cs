using System;
using UnityEngine;

namespace Enemies
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private int _health = 100;

        public event Action EnemyDestroy;

        public void TakeDamage(int damage)
        {
            print($"Current Enemy health: {_health}, damage by: -{damage}");
            _health -= damage;

            if (_health <= 0)
                EnemyDestroy?.Invoke();
        }
    }
}