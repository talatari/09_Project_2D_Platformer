using Enemies;
using UnityEngine;

namespace Players
{
    [RequireComponent(typeof(Player))]
    public class PlayerAttacker : MonoBehaviour
    {
        [SerializeField] private int _damage = 14;

        private Player _player;

        private void Awake() => 
            _player = GetComponent<Player>();

        private void OnEnable() => 
            _player.PlayerGivenDamage += OnPlayerGiveDamage;

        private void OnDisable() => 
            _player.PlayerGivenDamage -= OnPlayerGiveDamage;

        private void OnPlayerGiveDamage(Enemy enemy)
        {
            if (enemy != null)
                enemy.TakeDamage(_damage);
        }
    }
}