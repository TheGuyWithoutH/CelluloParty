using System;
using Game.Cellulos;
using UnityEngine;
using System.Collections;
using TMPro;
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
        public Image startScreen;
        public Image endScreen;

        protected GameStatus GameStatus;
        
        private GameManager.Player _winner;
        
        protected Vector3 StartOne = new Vector3(2.26f, 0, -4.76f);
        protected Vector3 StartTwo = new Vector3(11.98f, 0f, -4.76f);
        protected Vector3 StartBot = new Vector3(7.12f, 0f, -4.76f);

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
                    Debug.Log("Ready");
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
            bot.celluloAgent.SetGoalPosition(StartBot.x, StartBot.z, 2f);
            GameStatus = GameStatus.READY;
            startScreen.gameObject.SetActive(true);
            _winner = GameManager.Player.NONE;
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
            startScreen.gameObject.SetActive(false);
            if (player1.Score > player2.Score)
            {
                _winner = GameManager.Player.PLAYER1;
            } 
            else if (player1.Score < player2.Score)
            {
                _winner = GameManager.Player.PLAYER2;
            }
            
            switch (_winner)
            {
                case GameManager.Player.PLAYER1:
                    endScreen.GetComponentsInChildren<TextMeshProUGUI>()[2].text = player1.playerName;
                    break;
                case GameManager.Player.PLAYER2:
                    endScreen.GetComponentsInChildren<TextMeshProUGUI>()[2].text = player2.playerName;
                    break;
                case GameManager.Player.NONE:
                    endScreen.GetComponentsInChildren<TextMeshProUGUI>()[2].text = "Nobody";
                    break;
            }
            endScreen.gameObject.SetActive(true);
            
            Invoke(nameof(GameQuit), 5f);
        }
        
        private void GameQuit()
        {
            endScreen.gameObject.SetActive(false);
            GameStatus = GameStatus.NONE;
            player1.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstAll, player1.celluloAgent.initialColor, 0);
            player2.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstAll, player2.celluloAgent.initialColor, 0);
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