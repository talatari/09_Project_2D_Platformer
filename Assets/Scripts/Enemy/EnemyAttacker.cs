using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyAttacker : MonoBehaviour
{
    [SerializeField] private int _damage = 6;

    private Enemy _enemy;

    private void Awake() => 
        _enemy = GetComponent<Enemy>();

    private void OnEnable() => 
        _enemy.EnemyGiveDame += OnEnemyGiveDamage;

    private void OnDisable() => 
        _enemy.EnemyGiveDame -= OnEnemyGiveDamage;

    private void OnEnemyGiveDamage(Player player)
    {
        if (player is not null)
            player.TakeDamage(_damage);
    }
}