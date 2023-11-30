using UnityEngine;

public class AidKit : MonoBehaviour
{
    private int _health;
    
    private void Start()
    {
        _health = Random.Range(5, 25);
        Destroy(gameObject, 10f);
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.transform.TryGetComponent(out Player player))
        {
            player.CollectedAidKit(_health);
            Destroy(gameObject);
        }
    }
}