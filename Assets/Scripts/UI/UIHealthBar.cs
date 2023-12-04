using System;
using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    [SerializeField] private TMP_Text _addHealthButtonText;
    [SerializeField] private TMP_Text _removeHealthButtonText;
    [SerializeField] private TMP_Text _tmpText;
    [SerializeField] private TMP_Text _tmpSlowText;
    [SerializeField] private Image _fillBar;
    [SerializeField] private Image _slowFillBar;
    
    private PlayerHealth _playerHealth;
    private int _impactHealth = 20;
    private int _previousHealth;
    private Coroutine _coroutineSlowRefreshHealthText;

    private void Start()
    {
        _playerHealth = FindObjectOfType<PlayerHealth>();
        _previousHealth = _playerHealth.CurrentHealth;

        _playerHealth.HealthChanged += OnRefreshHealthBar;
        _playerHealth.HealthChanged += OnSlowRefreshHealthBar;

        OnRefreshHealthBar(_previousHealth);
        OnSlowRefreshHealthBar(_previousHealth);

        _addHealthButtonText.text += $" (+{_impactHealth})";
        _removeHealthButtonText.text += $" (-{_impactHealth})";
    }

    private void OnDestroy()
    {
        _playerHealth.HealthChanged -= OnRefreshHealthBar;
        _playerHealth.HealthChanged -= OnSlowRefreshHealthBar;
        
        if (_coroutineSlowRefreshHealthText is not null)
            StopCoroutine(_coroutineSlowRefreshHealthText);
    }

    public void AddHealth() => 
        _playerHealth.AddHealth(_impactHealth);

    public void RemoveHealth() =>
        _playerHealth.RemoveHealth(_impactHealth);

    private void OnRefreshHealthBar(int currentHealth)
    {
        string separator = " / ";
        int maxHealth = _playerHealth.MaxHealth;
        
        _tmpText.text = currentHealth + separator + maxHealth;
        _fillBar.fillAmount = (float) currentHealth / maxHealth;
    }

    private void OnSlowRefreshHealthBar(int currentHealth)
    {
        _coroutineSlowRefreshHealthText = StartCoroutine(SlowRefreshHealthText(currentHealth));
        _previousHealth = currentHealth;
    }

    private IEnumerator SlowRefreshHealthText(int targetHealth)
    {
        int normalize = 100;
        float slowSpeed = 0.1f;
        float fullHealth = 0.99f;
        string separator = " / ";
        int maxHealth = _playerHealth.MaxHealth;
        float target = (float) targetHealth / maxHealth;

        if (_slowFillBar.fillAmount > fullHealth)
        {
            _tmpSlowText.text = targetHealth + separator + maxHealth;
            yield return null;
        }
        
        while (_slowFillBar.fillAmount > target)
        {
            float currentHealth = Mathf.MoveTowards(_slowFillBar.fillAmount, target, slowSpeed * Time.deltaTime);
            
            _tmpSlowText.text = Math.Floor(currentHealth * normalize) + separator + maxHealth;
            _slowFillBar.fillAmount = currentHealth;
            
            yield return null;
        }
        
        while (_slowFillBar.fillAmount < target)
        {
            float currentHealth = Mathf.MoveTowards(_slowFillBar.fillAmount, target, slowSpeed * Time.deltaTime);
            
            _tmpSlowText.text = Math.Floor(currentHealth * normalize) + separator + maxHealth;
            _slowFillBar.fillAmount = currentHealth;
            
            yield return null;
        }
    }
}