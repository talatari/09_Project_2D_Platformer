using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    [SerializeField] private TextMeshPro _tmpText;
    [SerializeField] private TextMeshPro _tmpSlowText;
    [SerializeField] private Image _fillBar;
    [SerializeField] private Image _slowFillBar;
    
    private PlayerHealth _playerHealth;
    private Coroutine _coroutineSlowRefreshHealthText;
    private string _separator = " / ";

    private void Start()
    {
        _playerHealth = FindObjectOfType<PlayerHealth>();
        _playerHealth.HealthChanged += OnRefreshHealthBar;
        _playerHealth.HealthChanged += OnSlowRefreshHealthBar;

        _tmpSlowText.text = _playerHealth.CurrentHealth + _separator + _playerHealth.MaxHealth;
    }

    private void OnDestroy()
    {
        _playerHealth.HealthChanged -= OnRefreshHealthBar;
        _playerHealth.HealthChanged -= OnSlowRefreshHealthBar;
        
        if (_coroutineSlowRefreshHealthText is not null)
            StopCoroutine(_coroutineSlowRefreshHealthText);
    }

    private void OnRefreshHealthBar(int currentHealth, int maxHealth)
    {
        _tmpText.text = currentHealth + _separator + maxHealth;
        _fillBar.fillAmount = (float) currentHealth / maxHealth;
    }

    private void OnSlowRefreshHealthBar(int currentHealth, int maxHealth)
    {
        if (_coroutineSlowRefreshHealthText is not null)
            StopCoroutine(_coroutineSlowRefreshHealthText);
        
        _coroutineSlowRefreshHealthText = StartCoroutine(SlowRefreshHealthText(currentHealth, maxHealth));
    }

    private IEnumerator SlowRefreshHealthText(int targetHealth, int maxHealth)
    {
        float slowSpeed = 0.1f;
        float target = (float) targetHealth / maxHealth;
        
        while (_slowFillBar.fillAmount != target)
        {
            float currentHealth = Mathf.MoveTowards(_slowFillBar.fillAmount, target, slowSpeed * Time.deltaTime);
            
            _tmpSlowText.text = Math.Floor(currentHealth * maxHealth) + _separator + maxHealth;
            _slowFillBar.fillAmount = currentHealth;
            
            yield return null;
        }
    }
}