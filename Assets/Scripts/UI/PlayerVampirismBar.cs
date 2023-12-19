using Players;
using UnityEngine;

namespace UI
{
    public class PlayerVampirismBar : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _backgroundBar;
        [SerializeField] private SpriteRenderer _fillBar;
        [SerializeField] private PlayerVampirism _playerVampirism;
        
        private Vector3 _startFillBar;
        
        private void OnValidate()
        {
            if (_playerVampirism == null)
                _playerVampirism = FindObjectOfType<PlayerVampirism>();
        }
        
        private void Start()
        {
            _playerVampirism.FillChanged += OnRefreshBar;
            _playerVampirism.VampireActivated += OnActive;
            _playerVampirism.VampireDeactivated += OnInactive;
            
            _startFillBar = _fillBar.transform.localScale;
        }

        private void OnDestroy()
        {
            _playerVampirism.FillChanged -= OnRefreshBar;
            _playerVampirism.VampireActivated -= OnActive;
            _playerVampirism.VampireDeactivated -= OnInactive;
        }

        private void OnActive()
        {
            _backgroundBar.enabled = true;
            _fillBar.enabled = true;
            
            _playerVampirism.SetStartFill(_startFillBar.x);
        }

        private void OnInactive()
        {
            _fillBar.transform.localScale = _startFillBar;
            _backgroundBar.enabled = false;
            _fillBar.enabled = false;
        }

        private void OnRefreshBar(float fill)
        {
            Vector3 fillBar = _fillBar.transform.localScale;
            _fillBar.transform.localScale = new Vector3(fill, fillBar.y, fillBar.z);
        }
    }
}