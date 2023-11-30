using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]

public class EnemyDetector : MonoBehaviour
{
    public event Action<Player> PlayerClose;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.TryGetComponent(out Player player))
            PlayerClose?.Invoke(player);
    }
}