using System;
using System.Collections;
using Players;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image _fillBar;
        [SerializeField] private Health _health;
    
        private Coroutine _refreshHealthText;

        private void Start() => 
            _health.HealthChanged += OnRefreshHealthBar;

        private void OnDestroy()
        {
            _health.HealthChanged -= OnRefreshHealthBar;
        
            if (_refreshHealthText != null)
                StopCoroutine(_refreshHealthText);
        }

        private void OnRefreshHealthBar(int currentHealth, int maxHealth)
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