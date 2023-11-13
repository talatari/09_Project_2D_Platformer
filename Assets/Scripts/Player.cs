using UnityEngine;

public class Player : MonoBehaviour
{
    private int _coinsCollected;

    public void CollectedCoin()
    {
        _coinsCollected++;
        
        print($"_coinsCollected = {_coinsCollected}");
    }
    
    
}