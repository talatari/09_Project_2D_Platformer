using System;
using System.Collections;
using Players;
using UnityEngine;
using UnityEngine.UI;

namespace UIHealthBar
{
    public class UIHealthBar : MonoBehaviour
    {
        [SerializeField] private Image _slowFillBar;
        [SerializeField] private Health _health;
    
        private Coroutine _coroutineSlowRefreshHealthText;

        private void Start()
        {
            _health.HealthChanged += OnSlowRefreshHealthBar;
        }

        private void OnDestroy()
        {
            _health.HealthChanged -= OnSlowRefreshHealthBar;
        
            if (_coroutineSlowRefreshHealthText != null)
                StopCoroutine(_coroutineSlowRefreshHealthText);
        }

        private void OnSlowRefreshHealthBar(int currentHealth, int maxHealth)
        {
            if (_coroutineSlowRefreshHealthText != null)
                StopCoroutine(_coroutineSlowRefreshHealthText);

            _coroutineSlowRefreshHealthText = StartCoroutine(SlowRefreshHealthText(currentHealth, maxHealth));
        }

        private IEnumerator SlowRefreshHealthText(int targetHealth, int maxHealth)
        {
            float slowSpeed = 0.1f;
            float target = (float) targetHealth / maxHealth;
        
            while (Math.Abs(_slowFillBar.fillAmount - target) > 0.01f)
            {
                float currentHealth = Mathf.MoveTowards(_slowFillBar.fillAmount, target, slowSpeed * Time.deltaTime);
                _slowFillBar.fillAmount = currentHealth;
            
                yield return null;
            }
        }
    }
}