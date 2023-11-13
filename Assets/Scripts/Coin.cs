using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.transform.TryGetComponent(out Player player))
        {
            player.CollectedCoin();
            
            Destroy(gameObject);
        }
    }
    
    
}