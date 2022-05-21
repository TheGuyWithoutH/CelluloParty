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
        protected GameStatus GameStatus;
        
        private Winner _winner;

        protected virtual void Start()
        {
            GameStatus = GameStatus.NONE;
            _winner = Winner.NONE;
        }

        public virtual void Update()
        {
            if (GameStatus == GameStatus.READY && player1.IsReady && player2.IsReady)
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
            GameStatus = GameStatus.STARTED;
        }

        public virtual void StartGame()
        {
            GameStatus = GameStatus.READY;
            startScreen.SetActive(true);
        }
        
        public virtual void OnGamePause()
        {
            GameStatus = GameStatus.PAUSED;
            player1.SteeringDesactivate();
            player2.SteeringDesactivate();
        }

        public virtual void GameEnded()
        {
            GameStatus = GameStatus.ENDED;
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
            manager.MiniGameQuit();
            endScreen.SetActive(false);
            GameStatus = GameStatus.NONE;
            _winner = Winner.NONE;
            player1.SetNotReady();
            player2.SetNotReady();
        }
    }
    
    public enum Winner
    {
        NONE,
        PLAYER1,
        PLAYER2
    }
    
    public enum GameStatus
    {
        NONE = -1,
        READY,
        STARTED,
        ENDED,
        PAUSED
    }

}