using Players;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Enemy))]
    public class EnemyAttacker : MonoBehaviour
    {
        [SerializeField] private int _damage = 6;

        private Enemy _enemy;

        private void Awake() => 
            _enemy = GetComponent<Enemy>();

        private void OnEnable() => 
            _enemy.GivenDamage += OnEnemyGivenDamage;

        private void OnDisable() => 
            _enemy.GivenDamage -= OnEnemyGivenDamage;

        private void OnEnemyGivenDamage(Player player)
        {
            if (player != null)
                player.TakeDamage(_damage);
        }
    }
}