using System;
using Players;
using UnityEngine;

namespace Enemies
{
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private float _speed = 6f;
    
        private Enemy _enemy;
        private Transform _target;
        private float _minDistance = 4f;

        public event Action PlayerReached;

        private void Update()
        {
            if (_target != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);

                if ((transform.position - _target.position).magnitude < _minDistance)
                {
                    PlayerReached?.Invoke();

                    _enemy.OnClearTarget();
                }
            }
        }

        public void SetTarget(Player player, Enemy enemy)
        {
            _target = player.transform;
            _enemy = enemy;
        }

        public void ClearTarget() => 
            _target = null;
    }
}