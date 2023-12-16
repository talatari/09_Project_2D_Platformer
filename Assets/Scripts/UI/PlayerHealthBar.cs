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
        [SerializeField] private Gradient _gradient;
        
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
            float offset = 0.05f;
            float slowSpeed = 0.1f;
            float almostZero = 0.01f;
            float target = (float) targetHealth / maxHealth;
            Vector3 fillBar = _fillBar.transform.localScale;

            while (Math.Abs(fillBar.x + offset - target) > almostZero)
            {
                float currentHealth = Mathf.MoveTowards(target, fillBar.x + offset, slowSpeed * Time.deltaTime);
                _fillBar.transform.localScale = new Vector3(currentHealth - offset, fillBar.y, fillBar.z);
                _fillBar.color = _gradient.Evaluate(_fillBar.transform.localScale.x);
                
                yield return null;
            }
        }
    }
}