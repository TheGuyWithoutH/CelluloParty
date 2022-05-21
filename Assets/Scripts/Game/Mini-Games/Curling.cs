using System;
using System.Transactions;
using Game.Cellulos;
using UnityEngine;

namespace Game.Mini_Games
{
    public class Curling : Mini_Game
    {
        private GameStatus _innerStatus;
        private const int LINE = 3;
        private const Vector3 TARGET = new Vector3(10f, 0f, 10f);
        protected override void Start()
        {
            base.Start();
            MaxSeconds = 15;
        }
        
        public override void Update()
        {
            base.Update();
            if (base.GameStatus == Mini_Games.GameStatus.STARTED) { _innerStatus = GameStatus.FIRST_THROW; }

            if (ValidThrow(player1, LINE) && _innerStatus == GameStatus.FIRST_THROW)
            {
                //timer
                if (Throw(player1, LINE)) { _innerStatus = GameStatus.SECOND_THROW; }
            }

            if (ValidThrow(player2, LINE) && _innerStatus == GameStatus.SECOND_THROW)
            {
                //timer
                if(Throw(player2, LINE)){ GameEnded(); }
            }
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
            double eucl_dist_one = eucl_dist(player1.transform.position, TARGET);
            double eucl_dist_two = eucl_dist(player2.transform.position, TARGET);
            
            if (eucl_dist_one < eucl_dist_two)
            {
                
            }
            base.GameEnded();
        }

        private bool Line(CelluloPlayer player, int line)
        {
            return player.transform.position.x <= line;
        }

        private bool ValidThrow(CelluloPlayer player, int line)
        {
            return Line(player, line) && player.IsTouch;
        }

        private bool Throw(CelluloPlayer player, int line)
        {
            return !player.IsTouch && player.GetSteering().linear == new Vector3(0, 0, 0) && !Line(player, line);
        }

        private double eucl_dist(Vector3 vec_a, Vector3 vec_b)
        {
            return Math.Sqrt(Math.Pow(vec_a.x - vec_b.x, 2) + Math.Pow(vec_a.z - vec_b.z, 2));
        }

        private enum GameStatus
        {
            FIRST_THROW,
            SECOND_THROW,
            WAITING_PHASE,
            INVALID_THROW
        }
    }
}