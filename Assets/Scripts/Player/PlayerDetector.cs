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

    private void OnCollisionEnter2D(Collision2D other)
    {
        // print($"OnCollisionEnter2D.other.collider = {other.collider}");
        // if (other.collider.TryGetComponent(out Enemy enemy))
        //     EnemyClose?.Invoke(enemy);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        // print($"OnCollisionExit2D.other.collider = {other.collider}");
        // if (other.collider.TryGetComponent(out Enemy enemy))
        //     EnemyFar?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print($"OnTriggerEnter2D.other.collider = {other}");

        if (other.TryGetComponent(out BoxCollider2D boxCollider2D))
        {
            print(boxCollider2D);
            
            if (other.TryGetComponent(out Enemy enemy))
            {
                print(enemy);
                
                EnemyClose?.Invoke(enemy);
                
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        print($"OnTriggerExit2D.other.collider = {other}");

        if (other.TryGetComponent(out BoxCollider2D boxCollider2D))
        {
            if (other.TryGetComponent(out Enemy enemy))
                EnemyFar?.Invoke();
        }
    } 
}