using System;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    public event Action<Enemy> EnemyClose;
    public event Action EnemyFar;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy))
            EnemyClose?.Invoke(enemy);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Enemy enemy))
            EnemyFar?.Invoke();
    }
}