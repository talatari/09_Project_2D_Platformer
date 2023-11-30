using System.Collections;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private float _speedMove = 2f;

    private Animator _animator;
    private float _timeMove;
    private float _timeIdle;
    private float _directionMove = 1f;
    private Coroutine _coroutinePatrol;
    
    private void Awake() => 
        _animator = GetComponent<Animator>();

    private void Start() => 
        _coroutinePatrol = StartCoroutine(Patrol());

    private void OnDisable()
    {
        if (_coroutinePatrol != null)
        {
            StopCoroutine(_coroutinePatrol);
        }
    }
    
    private IEnumerator Patrol()
    {
        string isMovingText = "isMoving";
        
        while (true)
        {
            _timeMove = 5f;
            _timeIdle = 2f;
            _directionMove *= -1;
            _animator.SetBool(isMovingText, true);
            
            while (_timeMove > 0)
            {
                Vector3 position = transform.position;
                
                transform.position = new Vector3(
                    position.x + _speedMove * _directionMove * Time.deltaTime, position.y, position.z);

                _timeMove -= Time.deltaTime;
                
                yield return null;
            }
            
            _animator.SetBool(isMovingText, false);

            while (_timeIdle > 0)
            {
                _timeIdle -= Time.deltaTime;

                yield return null;
            }
        }
    }
}