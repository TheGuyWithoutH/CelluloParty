using System;
using System.Collections;
using Game.Cellulos;
using UnityEngine;

namespace Game.Mini_Games
{
    public class Curling : Mini_Game
    {
        private GameStatus _innerStatus;
        
        private int POWER_FACTOR = 7;
        private Vector3 VECT_NULL = new Vector3(0, 0, 0);
        private Vector3 START = new Vector3(2.26f, 0, -4.76f);
        private Vector3 TARGET = new Vector3(11.98f, 0f, -4.76f);
        
        protected override void Start()
        {
            base.Start();
        }
        
        public override void Update()
        {
            base.Update();
            if (_innerStatus == GameStatus.PREPARATION && player1.IsTouch) { _innerStatus = GameStatus.FIRST_THROW; }

            if (_innerStatus == GameStatus.FIRST_THROW && !player1.IsTouch)
            {
                Throw(player1);
                _innerStatus = GameStatus.PREPARATION;
            }

            if (_innerStatus == GameStatus.PREPARATION && player2.IsTouch) { _innerStatus = GameStatus.SECOND_THROW; }

            if (_innerStatus == GameStatus.SECOND_THROW && !player2.IsTouch)
            {
                Throw(player2);
                _innerStatus = GameStatus.END;
            }

            if (_innerStatus == GameStatus.END && player2.GetSteering().linear == VECT_NULL) { GameEnded(); }
        }

        public override void StartGame()
        {
            base.StartGame();
            _innerStatus = GameStatus.PREPARATION;
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
                player1.Score = 1;
                player2.Score = 0;
            } 
            else if (eucl_dist_one > eucl_dist_two) 
            {
                player1.Score = 0;
                player2.Score = 1;
            }
            
            base.GameEnded();
        }
        
        private void Throw(CelluloPlayer player)
        {
            Vector3 power_throw = (START - player.transform.position) * POWER_FACTOR;
            player.player.SetGoalPosition(power_throw.x, power_throw.z, 2f);
        }

        private double eucl_dist(Vector3 vec_a, Vector3 vec_b)
        {
            return Math.Sqrt(Math.Pow(vec_a.x - vec_b.x, 2) + Math.Pow(vec_a.z - vec_b.z, 2));
        }

        private enum GameStatus
        {
            PREPARATION,
            FIRST_THROW,
            SECOND_THROW,
            END
        }
    }
}