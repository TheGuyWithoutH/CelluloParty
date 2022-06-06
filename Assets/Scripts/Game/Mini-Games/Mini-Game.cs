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
        public CelluloBot bot;
        public Button playButton;
        public Canvas startScreen;
        public Canvas endScreen;

        protected GameStatus GameStatus;
        
        private GameManager.Player _winner;
        
        protected Vector3 StartOne = new Vector3(2.26f, 0, -4.76f);
        protected Vector3 StartTwo = new Vector3(11.98f, 0f, -4.76f);

        protected virtual void Start()
        {
            GameStatus = GameStatus.NONE;
            _winner = GameManager.Player.NONE;
        }

        public virtual void Update()
        {
            if (player1.IsReady && player2.IsReady)
            {
                if (GameStatus == GameStatus.READY)
                {
                    PlayerReady();
                    player1.SetNotReady();
                    player2.SetNotReady();
                }
                else if (GameStatus == GameStatus.PAUSED)
                {
                    OnGameResume();
                    player1.SetNotReady();
                    player2.SetNotReady();
                }
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
            player1.celluloAgent.SetGoalPosition(StartOne.x, StartOne.z, 2f);
            player2.celluloAgent.SetGoalPosition(StartTwo.x, StartTwo.z, 2f);
            GameStatus = GameStatus.READY;
            //startScreen.enabled = true;
        }
        
        public virtual void OnGamePause()
        {
            GameStatus = GameStatus.PAUSED;
            player1.SteeringDesactivate();
            player2.SteeringDesactivate();
        }

        public virtual void OnGameResume()
        {
            player1.SteeringReactivate();
            player2.SteeringReactivate();
            GameStatus = GameStatus.STARTED;
        }

        public virtual void GameEnded()
        {
            GameStatus = GameStatus.ENDED;
            //startScreen.enabled = false;
            //endScreen.enabled = true;
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