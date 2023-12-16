using Others;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(EnemyHealth))]
    public class EnemyCoinSpawner : MonoBehaviour
    {
        [SerializeField] private Coin _coinPrefab;

        private EnemyHealth _enemyEnemyHealth;

        private void Awake() => 
            _enemyEnemyHealth = GetComponent<EnemyHealth>();

        private void OnEnable() => 
            _enemyEnemyHealth.EnemyDestroy += OnSpawnCoin;

        private void OnDisable() => 
            _enemyEnemyHealth.EnemyDestroy -= OnSpawnCoin;

        private void OnSpawnCoin() => 
            Instantiate(_coinPrefab, transform.position, Quaternion.identity);
    }
}