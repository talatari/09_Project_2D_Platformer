using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float _speedMove = 2f;

    private Animator _animator;
    private float _timeMove;
    private float _timeIdle;
    private float _directionMove = 1f;
    private Coroutine _coroutinePatrol;
    
    private void Start() => _coroutinePatrol = StartCoroutine(Patrol());

    private void OnValidate() => _animator ??= GetComponent<Animator>();

    private void OnDisable()
    {
        if (_coroutinePatrol != null)
        {
            StopCoroutine(_coroutinePatrol);
        }
    }
    
    private IEnumerator Patrol()
    {
        while (true)
        {
            _timeMove = 5f;
            _timeIdle = 2f;
            _directionMove *= -1;
            _animator.SetBool("isMoving", true);
            
            while (_timeMove > 0)
            {
                transform.position = new Vector3(transform.position.x + _speedMove * _directionMove * Time.deltaTime, 
                                                 transform.position.y, transform.position.z);

                _timeMove -= Time.deltaTime;
                
                yield return null;
            }
            
            _animator.SetBool("isMoving", false);

            while (_timeIdle > 0)
            {
                _timeIdle -= Time.deltaTime;

                yield return null;
            }
        }
    }
    
    
}