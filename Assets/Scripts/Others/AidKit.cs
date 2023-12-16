using UnityEngine;

namespace Others
{
    public class AidKit : MonoBehaviour
    {
        private int _health;

        public int Health => _health;

        private void Start()
        {
            int minHealth = 5;
            int maxHealth = 25;
            float selfDestroyDelay = 10f;
        
            _health = Random.Range(minHealth, maxHealth);
            Destroy(gameObject, selfDestroyDelay);
        }

        public void Destoy() => 
            Destroy(gameObject);
    }
}