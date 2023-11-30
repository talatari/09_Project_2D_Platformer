using System;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed = 6f;
    
    private Player _player;
    private Vector3 _target;

    public event Action PlayerClose;

    private void Update()
    {
        if (_player is not null)
        {
            _target = _player.transform.position;
            
            transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
        }

        if ((transform.position - _target).magnitude < 3f)
        {
            PlayerClose?.Invoke();
        }
    }

    public void SetTarget(Player player) => 
        _player = player;
}