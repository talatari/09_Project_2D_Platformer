using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    private Player _player;
    private int _coinsCollected;

    private void Awake() => 
        _player = GetComponentInParent<Player>();

    private void OnCollectedCoin()
    {
        _coinsCollected++;
        print($"Coin collected! Current score = {_coinsCollected}");
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.transform.TryGetComponent(out AidKit aidKit))
        {
            _player.CollectedAidKit(aidKit.Health);
            aidKit.Destoy();
        }
        
        if (collision2D.transform.TryGetComponent(out Coin coin))
        {
            OnCollectedCoin();
            coin.Destroy();
        }
    }
}