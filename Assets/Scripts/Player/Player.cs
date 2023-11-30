using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _health = 100;

    private PlayerAnimator _playerAnimator;
    private PlayerDetector _playerDetector;
    private int _coinsCollected;
    private int _damage = 2;

    private void Awake()
    {
        _playerAnimator = GetComponent<PlayerAnimator>();
        _playerDetector = GetComponent<PlayerDetector>();
    }

    private void OnEnable()
    {
        _playerDetector.EnemyClose += OnAttack;
        _playerDetector.EnemyFar += OnIdle;
    }

    private void OnDisable()
    {
        _playerDetector.EnemyClose -= OnAttack;
        _playerDetector.EnemyFar -= OnIdle;
    }

    public void CollectedCoin() => 
        _coinsCollected++;

    public void CollectedAidKit(int health)
    {
        print($"Current health: {_health}, AidKit bust health on {health}");
        _health += health;
    }

    private void OnAttack(Enemy enemy)
    {
        if (enemy != null)
        {
            enemy.Take(_damage);
            _playerAnimator.PlayAttackAnimation();
        }
        else
        {
            OnIdle();
        }
    }

    public void Take(int damage) => 
        _health -= damage;

    private void OnIdle() => 
        _playerAnimator.StopAttackAnimation();
}