using System;
using System.Collections;
using Enemies;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class EnemyHealthBar : MonoBehaviour
    {
        [SerializeField] private Image _fillBar;
        [SerializeField] private EnemyHealth _enemyHealth;
    
        private Coroutine _refreshHealthText;

        private void Start() => 
            _enemyHealth.HealthChanged += OnRefreshEnemyHealth;

        private void OnDestroy()
        {
            _enemyHealth.HealthChanged -= OnRefreshEnemyHealth;
        
            if (_refreshHealthText != null)
                StopCoroutine(_refreshHealthText);
        }

        private void OnRefreshEnemyHealth(int currentHealth, int maxHealth)
        {
            if (_refreshHealthText != null)
                StopCoroutine(_refreshHealthText);

            _refreshHealthText = StartCoroutine(RefreshHealthText(currentHealth, maxHealth));
        }

        private IEnumerator RefreshHealthText(int targetHealth, int maxHealth)
        {
            float slowSpeed = 0.1f;
            float target = (float) targetHealth / maxHealth;
        
            while (Math.Abs(_fillBar.fillAmount - target) > 0.01f)
            {
                float currentHealth = Mathf.MoveTowards(_fillBar.fillAmount, target, slowSpeed * Time.deltaTime);
                _fillBar.fillAmount = currentHealth;
            
                yield return null;
            }
        }
    }
}