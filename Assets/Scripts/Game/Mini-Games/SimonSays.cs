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
        private int _actualKey;

        protected override void Start()
        {
            base.Start();
            _gameKeys = new List<int>();
            _player1Keys = new List<int>();
            _player2Keys = new List<int>();
        }

        public override void Update()
        {
            base.Update();
            if (GameStatus == GameStatus.NONE)
            {
                Debug.Log("test1");
                StartGame();
                GameStatus = GameStatus.STARTED;
            }
            if (GameStatus == GameStatus.STARTED) {
                if (_status == Status.Pattern) {
                    Debug.Log("test2");
                    GenerateKeys();
                    _status = Status.None;
                    for (int i = 0; i < _numKeys; i++)
                    {
                        _actualKey = i;
                        Invoke(nameof(LedOn), i*_latence);
                        if (i == _numKeys - 1)
                        {
                            Invoke(nameof(StartPlaying), i*_latence + 0.2f);
                        }
                    }
                }

                if (_status == Status.Playing)
                {
                    Debug.Log("test3");
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
                        _latence -= _latence > 0.2f ? 0.2f : 0f;
                    }
                }
            }
        }

        public override void StartGame()
        {
            base.StartGame();
            _latence = 2.0f;
            _status = Status.Pattern;
            _numKeys = 3;
        }
        
        public override void OnGamePause()
        {
            base.OnGamePause();
        }

        public override void GameEnded()
        {
            base.GameEnded();
            Debug.Log("fin");
        }

        private void StartPlaying()
        {
            _status = Status.Playing;
        }

        private void GenerateKeys()
        {
            for (int i = 0; i < _numKeys; i++) {
                int num = Random.Range(0, 6);
                _gameKeys.Add(num);
            }
        }

        private void LedOn()
        {
            player1.GetComponent<CelluloAgentRigidBody>().SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.red, _gameKeys[_actualKey]);
            player2.GetComponent<CelluloAgentRigidBody>().SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.red, _gameKeys[_actualKey]);
            Invoke(nameof(LedOff), 0.2f);
        }
        
        private void LedOff()
        {
            player1.GetComponent<CelluloAgentRigidBody>().SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.black, _gameKeys[_actualKey]);
            player2.GetComponent<CelluloAgentRigidBody>().SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.black, _gameKeys[_actualKey]);
        }

        private enum Status
        {
            None = -1,
            Pattern,
            Playing,
        }
    }
}