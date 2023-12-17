using System;
using Enemies;
using UnityEngine;

namespace Players
{
    public class PlayerDetector : MonoBehaviour
    {
        public event Action<Enemy> EnemyDetected;
        public event Action EnemyHided;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Enemy enemy))
                EnemyDetected?.Invoke(enemy);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Enemy enemy))
                EnemyHided?.Invoke();
        }
    }
}