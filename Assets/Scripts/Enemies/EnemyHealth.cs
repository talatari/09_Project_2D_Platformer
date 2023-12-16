using System;
using UnityEngine;

namespace Enemies
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private int _maxHealth = 100;

        private int _currentHealth;
        private int _minHealth = 0;
        
        public event Action EnemyDestroy;
        public event Action<int, int> HealthChanged;

        private void Awake() => 
            _currentHealth = _maxHealth;
        
        private void Start() => 
            HealthChanged?.Invoke(_currentHealth, _maxHealth);
        
        public void TakeDamage(int damage)
        {
            _currentHealth = Mathf.Clamp(_currentHealth -= damage, _minHealth, _maxHealth);
            HealthChanged?.Invoke(_currentHealth, _maxHealth);
            
            if (_currentHealth <= 0)
                EnemyDestroy?.Invoke();
        }
    }
}