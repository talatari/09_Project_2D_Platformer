using System;
using Players;
using UnityEngine;

namespace Enemies
{
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private float _speed = 6f;
    
        private Player _player;
        private Vector3 _target;
        private float _minDistance = 3f;

        public event Action PlayerReached;

        private void Update()
        {
            if (_player != null)
            {
                _target = _player.transform.position;
                transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.deltaTime);
            
                if ((transform.position - _target).magnitude < _minDistance)
                    PlayerReached?.Invoke();
            }
        }

        public void SetTarget(Player player) => 
            _player = player;

        public void ClearTarger() => 
            _player = null;
    }
}