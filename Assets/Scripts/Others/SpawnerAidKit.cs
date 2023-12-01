using System.Collections;
using UnityEngine;

public class SpawnerAidKit : MonoBehaviour
{
    [SerializeField] private AidKit _aidKitPrefab;
    [SerializeField] private float _waitTime;

    private Coroutine _coroutineSpawn;
    
    private void Start() => 
        _coroutineSpawn = StartCoroutine(SpawnCoin());

    private void OnDisable()
    {
        if (_coroutineSpawn is not null) 
            StopCoroutine(_coroutineSpawn);
    }

    private IEnumerator SpawnCoin()
    {
        int positionX = 30;
        int positionY = 15;
        WaitForSeconds waitTime = new WaitForSeconds(_waitTime);
        
        while (true)
        {
            Vector3 randomSpawnPoint = new Vector3(Random.Range(positionX * -1 , positionX), positionY, 0);
            Instantiate(_aidKitPrefab, randomSpawnPoint, Quaternion.identity);
            
            yield return waitTime;
        }
    }
}