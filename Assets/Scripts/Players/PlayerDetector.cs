using System;
using Enemies;
using Others;
using UnityEngine;

namespace Players
{
    public class PlayerDetector : MonoBehaviour
    {
        public event Action<Enemy> EnemyDetected;
        public event Action EnemyGone;
        public event Action Died;

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.TryGetComponent(out Enemy enemy))
                EnemyDetected?.Invoke(enemy);
            
            if (other.TryGetComponent(out DeadGround deadGround))
                Died?.Invoke();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Enemy enemy))
                EnemyGone?.Invoke();
        }
        
        
    }
}