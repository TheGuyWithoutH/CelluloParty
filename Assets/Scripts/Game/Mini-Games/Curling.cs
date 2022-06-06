using System;
using System.Collections;
using Game.Cellulos;
using UnityEngine;

namespace Game.Mini_Games
{
    public class Curling : Mini_Game
    {
        private InnerGameStatus _innerStatus;
        
        private const int PowerFactor = 7;
        private Vector3 _vect_null = new Vector3(0, 0, 0);
        private Vector3 _target = new Vector3(7.12f, 0f, -4.76f);
        
        protected override void Start()
        {
            base.Start();
        }
        
        public override void Update()
        {
            base.Update();

            if (GameStatus == GameStatus.STARTED)
            {
                if (_innerStatus == InnerGameStatus.PREPARATION && player1.IsTouch) { _innerStatus = InnerGameStatus.FIRST_THROW; }

                if (_innerStatus == InnerGameStatus.FIRST_THROW && !player1.IsTouch)
                {
                    Throw(player1, StartOne);
                    _innerStatus = InnerGameStatus.PREPARATION;
                }

                if (_innerStatus == InnerGameStatus.PREPARATION && player2.IsTouch)
                {
                    _innerStatus = InnerGameStatus.SECOND_THROW;
                }

                if (_innerStatus == InnerGameStatus.SECOND_THROW && !player2.IsTouch)
                {
                    Throw(player2, StartTwo);
                    _innerStatus = InnerGameStatus.END;
                }

                if (_innerStatus == InnerGameStatus.END && player2.GetSteering().linear == _vect_null) { GameEnded(); }
            }
        }

        public override void StartGame()
        {
            base.StartGame();
            _innerStatus = InnerGameStatus.PREPARATION;
        }
        
        public override void OnGamePause()
        {
            base.OnGamePause();
        }

        public override void GameEnded()
        {
            double eucl_dist_one = eucl_dist(player1.transform.position, _target);
            double eucl_dist_two = eucl_dist(player2.transform.position, _target);
            
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
        
        private void Throw(CelluloPlayer player, Vector3 start)
        {
            Vector3 power_throw = (start - player.transform.position) * PowerFactor;
            player.celluloAgent.SetGoalPosition(power_throw.x, power_throw.z, 2f);
        }

        private double eucl_dist(Vector3 vec_a, Vector3 vec_b)
        {
            return Math.Sqrt(Math.Pow(vec_a.x - vec_b.x, 2) + Math.Pow(vec_a.z - vec_b.z, 2));
        }

        private enum InnerGameStatus
        {
            PREPARATION,
            FIRST_THROW,
            SECOND_THROW,
            END
        }
    }
}