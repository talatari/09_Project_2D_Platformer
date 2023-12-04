using System;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int _health = 200;

    private Player _player;

    public event Action PlayerDestroy;
    
    private void Awake() => 
        _player = GetComponent<Player>();

    private void OnEnable()
    {
        _player.PlayerHealthed += OnCollectedAidKit;
        _player.PlayerTakeDamage += OnTake;
    }

    private void OnDisable()
    {
        _player.PlayerHealthed += OnCollectedAidKit;
        _player.PlayerTakeDamage -= OnTake;
    }

    private void OnCollectedAidKit(int health)
    {
        print($"Current health: {_health}, AidKit bust health on {health}");
        _health += health;
    }
    
    private void OnTake(int damage)
    {
        print($"Current Player health: {_health}, damage by: -{damage}");
        _health -= damage;

        if (_health <= 0) 
            PlayerDestroy?.Invoke();
    }
}