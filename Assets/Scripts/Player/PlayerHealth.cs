using System;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _currentHealth = 200;
    
    private Player _player;
    private int _maxHealth;

    public event Action PlayerDestroy = delegate { };
    public event Action<int> HealthChanged = delegate { };

    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _maxHealth;
    
    private void Awake()
    {
        _player = GetComponent<Player>();
        _player.PlayerHealthed += OnCollectedAidKit;
        _player.PlayerTakeDamage += OnTakeDamage;
        
        _maxHealth = _currentHealth;
        HealthChanged(_currentHealth);
    }

    private void OnDestroy()
    {
        _player.PlayerHealthed += OnCollectedAidKit;
        _player.PlayerTakeDamage -= OnTakeDamage;
    }

    public void AddHealth(int health) => 
        OnCollectedAidKit(health);

    public void RemoveHealth(int damage) =>
        OnTakeDamage(damage);

    private void OnCollectedAidKit(int health)
    {
        if (_currentHealth + health >= _maxHealth)
            _currentHealth = _maxHealth;
        else
            _currentHealth += health;
        
        HealthChanged(_currentHealth);
    }
    
    private void OnTakeDamage(int damage)
    {
        if (_currentHealth - damage <= 0)
        {
            _currentHealth = 0;
            PlayerDestroy();
        }
        else
        {
            _currentHealth -= damage;
        }
            
        HealthChanged(_currentHealth);
    }
}