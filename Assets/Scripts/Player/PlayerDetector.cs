using System;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]

public class PlayerDetector : MonoBehaviour
{
    private CircleCollider2D _circleCollider2D;

    public event Action<Enemy> EnemyClose;
    public event Action EnemyFar;
    
    private void Awake() => 
        _circleCollider2D = GetComponent<CircleCollider2D>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy))
            EnemyClose?.Invoke(enemy);
    }

    private void OnTriggerExit2D(Collider2D other) => 
        EnemyFar?.Invoke();
}