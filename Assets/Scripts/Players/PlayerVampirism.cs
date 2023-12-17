using System;
using System.Collections;
using Enemies;
using UI;
using UnityEngine;

namespace Players
{
    [RequireComponent(typeof(PlayerVampirismBar))]
    public class PlayerVampirism : MonoBehaviour
    {
        [SerializeField] private float _durationVampirism = 6.0f;
        [SerializeField] private PlayerVampirismBar _playerVampirismBar;

        private Player _player;
        private Enemy _enemy;
        private float _currentFillVampirismBar;
        private Coroutine _coroutinePlayerVampirism;
        
        public event Action<float> Vampired;

        private void OnValidate()
        {
            if (_playerVampirismBar == null)
                _playerVampirismBar = GetComponent<PlayerVampirismBar>();
        }

        private void OnDestroy()
        {
            if (_coroutinePlayerVampirism != null)
                StopCoroutine(_coroutinePlayerVampirism);
        }

        public void SetTarget(Enemy enemy, Player player)
        {
            _enemy = enemy;
            _player = player;
        }

        public void ClearTarget()
        {
            _enemy = null;

            StopVampirism();
        }

        public void StartVampirism()
        {
            _playerVampirismBar.SetActive();
            
            _currentFillVampirismBar = _playerVampirismBar.GetCurrentFillVampirismBar();
            
            if (_coroutinePlayerVampirism == null)
                _coroutinePlayerVampirism = StartCoroutine(RefreshVampirismBar());
        }

        private void StopVampirism()
        {
            if (_coroutinePlayerVampirism != null)
            {
                StopCoroutine(_coroutinePlayerVampirism);
                _coroutinePlayerVampirism = null;
            }

            _playerVampirismBar.SetInactive();
        }

        private IEnumerator RefreshVampirismBar()
        {
            float elapsedTime = 0f;
            float speed = _currentFillVampirismBar / _durationVampirism;
            
            while (elapsedTime < _durationVampirism)
            {
                elapsedTime += Time.deltaTime;
                
                _currentFillVampirismBar = Mathf.MoveTowards(_currentFillVampirismBar, 0, speed * Time.deltaTime);
                
                ApplyVampirism(_currentFillVampirismBar);
                Vampired?.Invoke(_currentFillVampirismBar);
                
                yield return null;
            }

            StopVampirism();
            _player.VampirismButtonSetActive();
        }

        private void ApplyVampirism(float impact)
        {
            if (_enemy != null)
            {
                float rario = 0.25f;
                
                _enemy.TakeDamage(impact * rario);
                _player.Heal(impact * rario);
            }
        }
    }
}