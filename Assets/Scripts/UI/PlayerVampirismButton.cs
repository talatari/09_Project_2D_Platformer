using Players;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerVampirismButton : MonoBehaviour
    {
        [SerializeField] private Button _playerVampirismButton;
        [SerializeField] private PlayerVampirism _playerVampirism;

        private void OnValidate()
        {
            if (_playerVampirism == null)
                _playerVampirism = FindObjectOfType<PlayerVampirism>();
        }

        public void SetActive()
        {
            _playerVampirismButton.gameObject.SetActive(true);
            _playerVampirismButton.onClick.AddListener(OnVampirismButtonClick);
        }

        public void SetInactive()
        {
            _playerVampirismButton.onClick.RemoveListener(OnVampirismButtonClick);
            _playerVampirismButton.gameObject.SetActive(false);
        }

        private void OnVampirismButtonClick()
        {
            _playerVampirism.StartVampirism();
            SetInactive();
        }
    }
}