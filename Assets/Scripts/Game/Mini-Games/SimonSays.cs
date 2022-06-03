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
        private int _actualIndex;

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
            if (GameStatus == GameStatus.STARTED) {
                if (_status == Status.Pattern) {
                    GenerateKeys();
                    _status = Status.None;
                    _actualIndex = 0;
                    for (int i = 0; i < _numKeys; i++)
                    {
                        Debug.Log("key : " + _gameKeys[i]);
                        Invoke(nameof(LedOn), (i+1)*_latence);
                        if (i == _numKeys - 1)
                        {
                            Invoke(nameof(StartPlaying), (i+2)*_latence);
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
                        Debug.Log("Player 1 : " + key);
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
                        Debug.Log("Player 2 : " + key);
                        if (player2KeysCount < _numKeys)
                        {
                            if (_gameKeys[player2KeysCount] == key)
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
                        _gameKeys = new List<int>();
                        _player1Keys = new List<int>();
                        _player2Keys = new List<int>();
                        _numKeys += 1;
                        _latence -= _latence > 0.2f ? 0.2f : 0f;
                        _status = Status.Pattern;
                    }
                }
            }
        }

        public override void StartGame()
        {
            base.StartGame();
            _latence = 2.0f;
            _numKeys = 3;
            _status = Status.Pattern;
        }
        
        public override void OnGamePause()
        {
            base.OnGamePause();
        }

        public override void GameEnded()
        {
            base.GameEnded();
            Debug.Log("fin : " + player1.Score + " " + player2.Score);
        }

        private void StartPlaying()
        {
            _status = Status.Playing;
        }

        private void GenerateKeys()
        {
            for (int i = 0; i < _numKeys; ++i) {
                int num = Random.Range(0, 6);
                _gameKeys.Add(num);
            }
        }

        private void LedOn()
        {
            player1.GetComponent<CelluloAgentRigidBody>().SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.red, _gameKeys[_actualIndex]);
            player2.GetComponent<CelluloAgentRigidBody>().SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.red, _gameKeys[_actualIndex]);
            Invoke(nameof(LedOff), 0.2f);
        }
        
        private void LedOff()
        {
            player1.GetComponent<CelluloAgentRigidBody>().SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.black, _gameKeys[_actualIndex]);
            player2.GetComponent<CelluloAgentRigidBody>().SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.black, _gameKeys[_actualIndex++]);
        }

        private enum Status
        {
            None = -1,
            Pattern,
            Playing,
        }
        
        private bool _isCoroutineExecuting = false;
        
        private IEnumerator ExecuteAfterTime(float time, Action task)
        {
            if (_isCoroutineExecuting)
                yield break;
            _isCoroutineExecuting = true;
            yield return new WaitForSeconds(time);
            task();
            _isCoroutineExecuting = false;
        }

        private void ExecuteAfterDelay(float time, Action task)
        {
            StartCoroutine(ExecuteAfterTime(time, task));
        }
    }
}