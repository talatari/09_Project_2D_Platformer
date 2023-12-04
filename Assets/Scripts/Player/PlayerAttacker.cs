using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] private int _damage = 14;

    private Player _player;

    private void Awake() => 
        _player = GetComponent<Player>();

    private void OnEnable() => 
        _player.PlayerGiveDamage += OnPlayerGiveDamage;

    private void OnDisable() => 
        _player.PlayerGiveDamage -= OnPlayerGiveDamage;

    private void OnPlayerGiveDamage(Enemy enemy)
    {
        if (enemy is not null)
            enemy.TakeDamage(_damage);
    }
}