using System;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public event Action<Enemy> EnemyClose = delegate { };
    public event Action EnemyFar = delegate { };

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy))
            EnemyClose(enemy);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy))
            EnemyFar();
    }
}