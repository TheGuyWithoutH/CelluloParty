using System;
using Game.Cellulos;
using UnityEngine;
using System.Collections;

namespace Game.Mini_Games
{
    public abstract class Mini_Game : MonoBehaviour
    {
        public GameManager manager;
        public CelluloPlayer player1;
        public CelluloPlayer player2;
        public CelluloPlayer bot;
        public Timer timer;
        public GameObject playButton;
        public GameObject startScreen;
        public GameObject endScreen;

        protected int MaxSeconds;

        private GameStatus _gameStatus;
        private Winner _winner;

        protected virtual void Start()
        {
            _gameStatus = GameStatus.NONE;
            _winner = Winner.NONE;
        }

        public virtual void Update()
        {
            if (_gameStatus == GameStatus.READY && player1.IsReady() && player2.IsReady())
            {
                playButton.SetActive(false);
                PlayerReady();
            }
        }

        public void PlayerReady()
        {
            timer.StartTimer(MaxSeconds, this);
            player1.SteeringReactivate();
            player2.SteeringReactivate();
            _gameStatus = GameStatus.STARTED;
        }

        public virtual void StartGame()
        {
            _gameStatus = GameStatus.READY;
            startScreen.SetActive(true);
        }
        
        public virtual void OnGamePause()
        {
            _gameStatus = GameStatus.PAUSED;
            player1.SteeringDesactivate();
            player2.SteeringDesactivate();
        }

        public virtual void GameEnded()
        {
            endScreen.SetActive(true);
            if (player1.Score > player2.Score)
            {
                _winner = Winner.PLAYER1;
            } 
            else if (player1.Score < player2.Score)
            {
                _winner = Winner.PLAYER2;
            }
        }
        
        private void GameQuit()
        {
            endScreen.SetActive(false);
            _gameStatus = GameStatus.NONE;
            _winner = Winner.NONE;
            player1.setNotReady();
            player2.setNotReady();
        }
        
        private enum GameStatus
        {
            NONE = -1,
            READY,
            STARTED,
            ENDED,
            PAUSED
        }
    }

    /**
     * 
     */
    public enum Winner
    {
        NONE,
        PLAYER1,
        PLAYER2
    }

}