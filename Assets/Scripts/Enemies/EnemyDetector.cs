using System;
using Players;
using UnityEngine;

namespace Enemies
{
    public class EnemyDetector : MonoBehaviour
    {
        public event Action<Player> PlayerDetected;
        public event Action PlayerFar;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player player))
                PlayerDetected?.Invoke(player);
        }
    
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player player))
                PlayerFar?.Invoke();
        }
    }
}