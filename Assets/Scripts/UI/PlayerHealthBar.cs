using System;
using System.Collections;
using Players;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(PlayerHealth))]
    public class PlayerHealthBar : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _fillBar;

        private PlayerHealth _playerHealth;
        private Coroutine _refreshHealthText;

        private void Start()
        {
            _playerHealth = GetComponent<PlayerHealth>();
            _playerHealth.HealthChanged += OnRefreshPlayerHealthBar;
        }

        private void OnDestroy()
        {
            _playerHealth.HealthChanged -= OnRefreshPlayerHealthBar;
        
            if (_refreshHealthText != null)
                StopCoroutine(_refreshHealthText);
        }

        private void OnRefreshPlayerHealthBar(int currentHealth, int maxHealth)
        {
            if (_refreshHealthText != null)
                StopCoroutine(_refreshHealthText);

            _refreshHealthText = StartCoroutine(RefreshHealthText(currentHealth, maxHealth));
        }

        private IEnumerator RefreshHealthText(int targetHealth, int maxHealth)
        {
            float slowSpeed = 0.1f;
            float target = (float) targetHealth / maxHealth;
            float fillBar = _fillBar.transform.localScale.x - target;
            
            while (Math.Abs(fillBar - target) > 0.01f)
            {
                float currentHealth = Mathf.MoveTowards(fillBar, target, slowSpeed * Time.deltaTime);
                fillBar = currentHealth;
            
                yield return null;
            }
        }
    }
}