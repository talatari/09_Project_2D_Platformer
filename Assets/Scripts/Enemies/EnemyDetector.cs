using System;
using Others;
using Players;
using UnityEngine;

namespace Enemies
{
    public class EnemyDetector : MonoBehaviour
    {
        public event Action<Player> PlayerDetected;
        public event Action PlayerGone;
        public event Action Died;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player player))
                PlayerDetected?.Invoke(player);
            
            if (other.TryGetComponent(out Ground ground))
                Died?.Invoke();
        }
    
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player player))
                PlayerGone?.Invoke();
        }
    }
}