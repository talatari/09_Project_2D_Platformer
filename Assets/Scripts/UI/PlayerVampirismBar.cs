using Players;
using UnityEngine;

namespace UI
{
    public class PlayerVampirismBar : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _backgroundBar;
        [SerializeField] private SpriteRenderer _fillBar;
        [SerializeField] private PlayerVampirism _playerVampirism;
        
        private Vector3 _startFillVampirismBar;
        
        private void OnValidate()
        {
            if (_playerVampirism == null)
                _playerVampirism = FindObjectOfType<PlayerVampirism>();
        }
        
        private void Start()
        {
            _playerVampirism.FillChanged += OnRefreshPlayerVampirismBar;
            _playerVampirism.VampireActivated += OnSetActive;
            _playerVampirism.VampireDeactivated += OnSetInactive;
            _startFillVampirismBar = _fillBar.transform.localScale;
        }

        private void OnDestroy()
        {
            _playerVampirism.FillChanged -= OnRefreshPlayerVampirismBar;
            _playerVampirism.VampireActivated -= OnSetActive;
            _playerVampirism.VampireDeactivated -= OnSetInactive;
        }

        private void OnSetActive()
        {
            _backgroundBar.enabled = true;
            _fillBar.enabled = true;
            
            _playerVampirism.SetCurrentFill(_startFillVampirismBar.x);
        }

        private void OnSetInactive()
        {
            _fillBar.transform.localScale = _startFillVampirismBar;
            _backgroundBar.enabled = false;
            _fillBar.enabled = false;
        }

        private void OnRefreshPlayerVampirismBar(float fill)
        {
            Vector3 fillBar = _fillBar.transform.localScale;
            _fillBar.transform.localScale = new Vector3(fill, fillBar.y, fillBar.z);
        }
    }
}