using Game.Mini_Games;
using UnityEngine;
using System;

namespace Game
{
    public class Timer : MonoBehaviour
    {
        private int _maxSeconds;
        private float _currentTime;
        private float _lastTime;
        private bool _paused;
        private bool _end;

        private Mini_Game _currentMiniGame;

        private void Awake()
        {
            _maxSeconds = 0;
            _currentTime = 0;
            _lastTime = Time.time;
            _paused = false;
            _end = true;
        }

        public void StartTimer(int seconds, Mini_Game game)
        {
            _maxSeconds = seconds;
            _currentTime = 0;
            _lastTime = Time.time;
            _paused = false;

            _currentMiniGame = game;
        }
        
        public void Update()
        {
            if (!_paused && !_end)
            {
                _currentTime += Time.time - _lastTime;
            
                var minutes = (int)Math.Floor(_currentTime / 60);
                var seconds = (int)Math.Floor(_currentTime % 60);
                    
                if (!_end && _currentTime >= _maxSeconds)
                {
                    _end = true;
                    TimerEnded();
                }
                else if (!_end)
                {
                    //timerText.SetText(string.Format("{0:00}:{1:00}", minutes, seconds));
                }
            }
        
            _lastTime = Time.time;

        }

        public void PauseTimer()
        {
            _paused = true;
        }

        public void ResumeTimer()
        {
            _paused = false;
        }

        private void TimerEnded()
        {
            _currentMiniGame.GameEnded();
        }

        public float CurrentTime => _currentTime;
    }
}