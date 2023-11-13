using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speedMove = 2f;
    [SerializeField] private float _timeMove = 5f;
    [SerializeField] private float _timeIdle = 2f;
    
    private Coroutine _coroutinePatrol;
    private float _timer;
    
    private void Start() => _coroutinePatrol = StartCoroutine(Patrol());

    private void OnDisable()
    {
        if (_coroutinePatrol != null)
        {
            StopCoroutine(_coroutinePatrol);
        }
    }
    
    // Как избавиться от дубляжа в корутине?
    private IEnumerator Patrol()
    {
        while (true)
        {
            _timer = 0;
            
            while (_timer <= _timeMove)
            {
                transform.position = new Vector3(transform.position.x + _speedMove * Time.deltaTime, 
                                                 transform.position.y, transform.position.z);
                
                _timer += Time.deltaTime;
                
                yield return null;
            }
            
            _timer = 0;
            
            while (_timer <= _timeIdle)
            {
                _timer += Time.deltaTime;
                
                yield return null;
            }
            
            _speedMove *= -1;
        }
    }
    
    
}