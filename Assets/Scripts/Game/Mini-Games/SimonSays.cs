using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Mini_Games
{
    public class SimonSays : Mini_Game
    {
        private int _numKeys;
        private List<int> _gameKeys;
        private List<int> _playerKeys;
        private float latence;
        
        protected override void Start()
        {
            base.Start();
            MaxSeconds = 10;
        }

        public override void Update()
        {
            base.Update();
            // foreach (var key in _gameKeys)
            // {
            //     
            // }
        }

        public override void StartGame()
        {
            base.StartGame();
            latence = 2000;
        }
        
        public override void OnGamePause()
        {
            base.OnGamePause();
        }

        public override void GameEnded()
        {
            base.GameEnded();
        }

        private void GenerateKeys()
        {
            for (int i = 0; i < _numKeys; i++) {
                int num = Random.Range(0, 6);
                _gameKeys.Add(num);
            }
        }

        private bool KeyIsValid(int key, int index)
        {
            return _gameKeys[index] == key;
        }
        
        private enum Status
        {
            READY,
            PATTERN,
            PLAYING,
        }
    }
}