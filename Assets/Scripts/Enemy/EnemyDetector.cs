using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]

public class EnemyDetector : MonoBehaviour
{
    private CircleCollider2D _circleCollider2D;

    public event Action<Player> PlayerClose;

    private void Awake() =>
        _circleCollider2D = GetComponent<CircleCollider2D>();

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.TryGetComponent(out Player player))
        {
            PlayerClose?.Invoke(player);
        }
    }
}