using UnityEngine;

namespace Game.Mini_Games
{
    public class SimonSays : Mini_Game
    {
        protected override void Start()
        {
            base.Start();
            MaxSeconds = 10;
        }

        public override void Update()
        {
            base.Update();
        }

        public override void StartGame()
        {
            base.StartGame();
        }
        
        public override void OnGamePause()
        {
            base.OnGamePause();
        }

        public override void GameEnded()
        {
            base.GameEnded();
        }
    }
}