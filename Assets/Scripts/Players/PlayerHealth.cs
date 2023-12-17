using System;
using UnityEngine;

namespace Players
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float _maxHealth = 200;
    
        private float _currentHealth;
        private float _minHealth = 0;

        public event Action Destroyed;
        public event Action<float, float> HealthChanged;

        private void Awake() => 
            _currentHealth = _maxHealth;

        private void Start() => 
            HealthChanged?.Invoke(_currentHealth, _maxHealth);
    
        public void Heal(float health)
        {
            _currentHealth = Mathf.Clamp(_currentHealth += health, _minHealth, _maxHealth);
            HealthChanged?.Invoke(_currentHealth, _maxHealth);
        }
    
        public void TakeDamage(float damage)
        {
            _currentHealth = Mathf.Clamp(_currentHealth -= damage, _minHealth, _maxHealth);
            HealthChanged?.Invoke(_currentHealth, _maxHealth);
            
            if (_currentHealth <= _minHealth)
                Destroyed?.Invoke();
        }
    }
}