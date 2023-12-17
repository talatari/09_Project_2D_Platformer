using System;
using UnityEngine;

namespace Enemies
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private float _maxHealth = 100;

        private float _currentHealth;
        private float _minHealth = 0;
        
        public event Action Destroyed;
        public event Action<float, float> HealthChanged;

        private void Awake() => 
            _currentHealth = _maxHealth;
        
        private void Start() => 
            HealthChanged?.Invoke(_currentHealth, _maxHealth);
        
        public void TakeDamage(float damage)
        {
            _currentHealth = Mathf.Clamp(_currentHealth -= damage, _minHealth, _maxHealth);
            HealthChanged?.Invoke(_currentHealth, _maxHealth);
            
            if (_currentHealth <= 0)
                Destroyed?.Invoke();
        }
    }
}