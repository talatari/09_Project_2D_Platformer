using Others;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Health))]
    public class CoinSpawner : MonoBehaviour
    {
        [SerializeField] private Coin _coinPrefab;

        private Health _enemyHealth;

        private void Awake() => 
            _enemyHealth = GetComponent<Health>();

        private void OnEnable() => 
            _enemyHealth.EnemyDestroy += OnSpawnCoin;

        private void OnDisable() => 
            _enemyHealth.EnemyDestroy -= OnSpawnCoin;

        private void OnSpawnCoin() => 
            Instantiate(_coinPrefab, transform.position, Quaternion.identity);
    }
}