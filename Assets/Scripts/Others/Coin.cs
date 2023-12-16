using UnityEngine;

namespace Others
{
    public class Coin : MonoBehaviour
    {
        public void Destroy() => 
            Destroy(gameObject);
    }
}