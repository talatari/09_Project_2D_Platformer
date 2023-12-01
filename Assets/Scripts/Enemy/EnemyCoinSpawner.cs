using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
public class EnemyCoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;

    private EnemyHealth _enemyHealth;

    private void Awake() => 
        _enemyHealth = GetComponent<EnemyHealth>();

    private void OnEnable() => 
        _enemyHealth.EnemyDestroy += OnSpawnCoin;

    private void OnDisable() => 
        _enemyHealth.EnemyDestroy -= OnSpawnCoin;

    private void OnSpawnCoin() => 
        Instantiate(_coinPrefab, transform.position, Quaternion.identity);
}