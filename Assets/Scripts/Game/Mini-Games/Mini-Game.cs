using Game.Cellulos;
using UnityEngine;
using System.Collections;

namespace Game.Mini_Games
{
    public abstract class Mini_Game : MonoBehaviour
    {
        public GameManager manager;
        public CelluloPlayer player;
        public CelluloBot bot;
        private Vector3 MapPos;
        
        public Timer timer;

        /**
         * 
         */
        public abstract void StartGame();

        public abstract void OnGamePause();

        public abstract void GameEnded();

        public abstract Winner GameWinner();

    }

    public enum Winner
    {
        NONE,
        PLAYER1,
        PLAYER2
    }
    
}