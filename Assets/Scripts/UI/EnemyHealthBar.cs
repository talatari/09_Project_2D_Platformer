using System;
using System.Collections;
using Enemies;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(EnemyHealth))]
    public class EnemyHealthBar : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _fillBar;
        [SerializeField] private Gradient _gradient;
        
        private EnemyHealth _enemyHealth;
        private Coroutine _refreshHealthText;

        private void Start()
        {
            _enemyHealth = GetComponent<EnemyHealth>();
            _enemyHealth.HealthChanged += OnRefreshEnemyHealth;
        }

        private void OnDestroy()
        {
            _enemyHealth.HealthChanged -= OnRefreshEnemyHealth;
        
            if (_refreshHealthText != null)
                StopCoroutine(_refreshHealthText);
        }

        private void OnRefreshEnemyHealth(float currentHealth, float maxHealth)
        {
            if (_refreshHealthText != null)
                StopCoroutine(_refreshHealthText);

            _refreshHealthText = StartCoroutine(RefreshHealthText(currentHealth, maxHealth));
        }

        private IEnumerator RefreshHealthText(float targetHealth, float maxHealth)
        {
            float offset = 0.05f;
            float slowSpeed = 0.1f;
            float almostZero = 0.01f;
            float target = targetHealth / maxHealth;
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