using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _health = 100;

    private Player _target;
    private int _damage = 10;
    
    private void Attack(Player player) => 
        player.Take(_damage);

    public void Take(int damage)
    {
        _health -= damage;
        print(_health);
        
        if (_health < 0)
            Destroy(gameObject);
    }
}