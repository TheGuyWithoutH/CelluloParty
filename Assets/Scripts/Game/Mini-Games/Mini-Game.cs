using System;
using Game.Cellulos;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Game.Mini_Games
{
    public abstract class Mini_Game : MonoBehaviour
    {
        public GameManager manager;
        public CelluloPlayer player1;
        public CelluloPlayer player2;
        public CelluloPlayer bot;
        public Timer timer;
        public Button playButton;
        public Canvas startScreen;
        public Canvas endScreen;

        protected GameStatus GameStatus;
        
        private GameManager.Player _winner;

        protected virtual void Start()
        {
            GameStatus = GameStatus.NONE;
            _winner = GameManager.Player.NONE;
        }

        public virtual void Update()
        {
            if (GameStatus == GameStatus.READY && player1.IsReady && player2.IsReady)
            {
                PlayerReady();
                player1.SetNotReady();
                player2.SetNotReady();
            }
        }

        protected virtual void PlayerReady()
        {
            player1.SteeringReactivate();
            player2.SteeringReactivate();
            GameStatus = GameStatus.STARTED;
        }

        public virtual void StartGame()
        {
            GameStatus = GameStatus.READY;
            startScreen.enabled = true;
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
            startScreen.enabled = false;
            endScreen.enabled = true;
            if (player1.Score > player2.Score)
            {
                _winner = GameManager.Player.PLAYER1;
            } 
            else if (player1.Score < player2.Score)
            {
                _winner = GameManager.Player.PLAYER2;
            }
        }
        
        private void GameQuit()
        {
            endScreen.enabled = false;
            GameStatus = GameStatus.NONE;
            _winner = GameManager.Player.NONE;
            manager.MiniGameQuit(_winner);
        }
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