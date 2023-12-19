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
        [SerializeField] private float _duration = 6.0f;
        [SerializeField] private PlayerVampirismBar _bar;

        private Player _player;
        private Enemy _enemy;
        private float _currentFill;
        private Coroutine _coroutineVampirism;
        
        public event Action<float> Vampired;

        private void OnValidate()
        {
            if (_bar == null)
                _bar = GetComponent<PlayerVampirismBar>();
        }

        private void OnDestroy()
        {
            if (_coroutineVampirism != null)
                StopCoroutine(_coroutineVampirism);
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
            _bar.SetActive();
            
            _currentFill = _bar.GetCurrentFillVampirismBar();
            
            if (_coroutineVampirism == null)
                _coroutineVampirism = StartCoroutine(RefreshBar());
        }

        private void StopVampirism()
        {
            if (_coroutineVampirism != null)
            {
                StopCoroutine(_coroutineVampirism);
                _coroutineVampirism = null;
            }

            _bar.SetInactive();
        }

        private IEnumerator RefreshBar()
        {
            float elapsedTime = 0f;
            float speed = _currentFill / _duration;
            
            while (elapsedTime < _duration)
            {
                elapsedTime += Time.deltaTime;
                
                _currentFill = Mathf.MoveTowards(_currentFill, 0, speed * Time.deltaTime);
                
                ApplyVampirism(_currentFill);
                Vampired?.Invoke(_currentFill);
                
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