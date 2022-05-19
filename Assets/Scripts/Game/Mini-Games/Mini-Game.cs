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

        protected int maxSeconds;

        private Vector3 _mapPos;
        private GameStatus _gameStatus;

        private void Start()
        {
            throw new NotImplementedException();
        }

        public virtual void Update()
        {
            if (_gameStatus == GameStatus.NONE)
            {
                if (player1.isReady && player2.isReady)
                {
                    playButton.SetActive(false);
                    PlayerReady();
                }
            }
        }

        public void PlayerReady()
        {
            timer.StartTimer(maxSeconds, this);
            //player1.SteeringReactivate();
            //player2.SteeringReactivate();
            _gameStatus = GameStatus.STARTED;
        }

        /**
         * 
         */
        public abstract void StartGame();
        
        /**
         * 
         */
        public abstract void OnGamePause();

        /**
         * 
         */
        public abstract void GameEnded();

        /**
         * 
         */
        private void GameQuit()
        {
         
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