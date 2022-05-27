using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Mini_Games
{
    public class SimonSays : Mini_Game
    {
        private int _numKeys;
        private List<int> _gameKeys;
        private List<int> _player1Keys;
        private List<int> _player2Keys;
        private float _latence;
        private Status _status;
        private bool _touch1;
        private bool _touch2;

        protected override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            base.Update();
            
            /*if (GameStatus == GameStatus.STARTED) {
                if (_status == Status.Pattern) {
                    GenerateKeys();
                    _status = Status.None;
                    for (int i = 0; i < _numKeys; i++)
                    {
                        ShowKey(i);
                        if (i == _numKeys - 1)
                        {
                            var task7 = Task.Run(() => Status.Playing);
                            if (task7.Wait(TimeSpan.FromSeconds(i*_latence)))
                            {
                                _status = task7.Result;
                            }
                        }
                    }
                }

                if (_status == Status.Playing)
                {
                    int player1KeysCount = _player1Keys.Count;
                    int player2KeysCount = _player2Keys.Count;

                    if (player1.IsTouch && !_touch1)
                    {
                        _touch1 = true;
                        int key = player1.Key;
                        if (player1KeysCount < _numKeys) {
                            if (_gameKeys[player1KeysCount] == key)
                            {
                                _player1Keys.Add(key);
                            }
                            else
                            {
                                GameEnded();
                                player2.Score += 1;
                            }
                        }
                    }
                    _touch1 = player1.IsTouch;

                    if (player2.IsTouch && !_touch2)
                    {
                        _touch2 = true;
                        int key = player2.Key;
                        if (player2KeysCount < _numKeys)
                        {
                            if (_gameKeys[_player2Keys.Count] == key)
                            {
                                _player2Keys.Add(key);
                            }
                            else
                            {
                                GameEnded();
                                player1.Score += 1;
                            }
                        }
                    }
                    _touch2 = player2.IsTouch;

                    if (player1KeysCount == _numKeys && player2KeysCount == _numKeys)
                    {
                        _status = Status.Pattern;
                        _numKeys += 1;
                        _latence -= _latence > 200 ? 200 : 0;
                    }
                }
            }*/
        }

        public override void StartGame()
        {
            base.StartGame();
            _latence = 2000;
            _status = Status.Pattern;
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

        private void ShowKey(int index)
        {
            var task = Task.Run(() => true);
            if (task.Wait(TimeSpan.FromSeconds(index*_latence)))
            {
                if (task.Result)
                {
                    player1.GetComponent<CelluloAgentRigidBody>().SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.red, _gameKeys[index]);
                    player2.GetComponent<CelluloAgentRigidBody>().SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.red, _gameKeys[index]);
                }
            }
        }

        private enum Status
        {
            None = -1,
            Pattern,
            Playing,
        }
    }
}