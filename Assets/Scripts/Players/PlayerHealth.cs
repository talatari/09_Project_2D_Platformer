using System;
using UnityEngine;

namespace Players
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private int _maxHealth = 200;
    
        private int _currentHealth;
        private int _minHealth = 0;

        public event Action PlayerDestroy;
        public event Action<int, int> HealthChanged;

        private void Awake() => 
            _currentHealth = _maxHealth;

        private void Start() => 
            HealthChanged?.Invoke(_currentHealth, _maxHealth);
    
        public void Heal(int health)
        {
            _currentHealth = Mathf.Clamp(_currentHealth += health, _minHealth, _maxHealth);
            HealthChanged?.Invoke(_currentHealth, _maxHealth);
        }
    
        public void TakeDamage(int damage)
        {
            _currentHealth = Mathf.Clamp(_currentHealth -= damage, _minHealth, _maxHealth);
            HealthChanged?.Invoke(_currentHealth, _maxHealth);
            
            if (_currentHealth <= _minHealth)
                PlayerDestroy?.Invoke();
        }
    }
}