using System;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public event Action<Player> PlayerDetected = delegate { };
    public event Action PlayerFar = delegate { };

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
            PlayerDetected(player);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
            PlayerFar();
    }
}