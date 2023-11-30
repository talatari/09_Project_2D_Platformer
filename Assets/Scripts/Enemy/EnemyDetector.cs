using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]

public class EnemyDetector : MonoBehaviour
{
    public event Action<Player> PlayerDetected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
            PlayerDetected?.Invoke(player);
    }
}