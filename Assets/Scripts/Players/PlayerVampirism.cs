using System;
using System.Collections;
using Enemies;
using UnityEngine;

namespace Players
{
    public class PlayerVampirism : MonoBehaviour
    {
        [SerializeField] private float _duration = 6.0f;

        private Player _player;
        private Enemy _enemy;
        private float _currentFill;
        private Coroutine _coroutineVampirism;
        
        public event Action<float> FillChanged;
        public event Action VampireActivated;
        public event Action VampireDeactivated;

        private void OnDestroy()
        {
            if (_coroutineVampirism != null)
                StopCoroutine(_coroutineVampirism);
        }

        public void SetCurrentFill(float currentFill) => 
            _currentFill = currentFill;

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
            VampireActivated?.Invoke();
            
            if (_coroutineVampirism == null)
                _coroutineVampirism = StartCoroutine(RefreshBar());
        }

        private void StopVampirism()
        {
            VampireDeactivated?.Invoke();
            
            if (_coroutineVampirism != null)
            {
                StopCoroutine(_coroutineVampirism);
                _coroutineVampirism = null;
            }
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
                FillChanged?.Invoke(_currentFill);
                
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