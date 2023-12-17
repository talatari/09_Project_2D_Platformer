using Players;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(PlayerVampirism))]
    public class PlayerVampirismBar : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _fillBar;
        [SerializeField] private GameObject _playerVampirismBar;
        
        private PlayerVampirism _playerVampirism;
        private Vector3 _startFillVampirismBar;
        
        private void Start()
        {
            _playerVampirism = GetComponent<PlayerVampirism>();
            _playerVampirism.Vampired += OnRefreshPlayerVampirismBar;
            _startFillVampirismBar = _fillBar.transform.localScale;
        }

        private void OnDestroy()
        {
            _playerVampirism.Vampired -= OnRefreshPlayerVampirismBar;
        }

        public void SetActive()
        {
            _playerVampirismBar.SetActive(true);
        }
        
        public void SetInactive()
        {
            _fillBar.transform.localScale = _startFillVampirismBar;
            _playerVampirismBar.SetActive(false);
        }

        public float GetCurrentFillVampirismBar()
        {
            return _startFillVampirismBar.x;
        }
        
        private void OnRefreshPlayerVampirismBar(float fill)
        {
            Vector3 fillBar = _fillBar.transform.localScale;
            _fillBar.transform.localScale = new Vector3(fill, fillBar.y, fillBar.z);
        }
    }
}