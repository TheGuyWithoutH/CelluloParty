using System;
using System.Collections;
using Game.Cellulos;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Mini_Games
{
    public class Curling : Mini_Game
    {
        private InnerGameStatus _innerStatus = InnerGameStatus.PREPARATION;

        private const int PowerFactor = 3;
        private Vector3 _target = new Vector3(7.12f, 0f, -4.76f);

        private Vector3 _throw_one;
        private Vector3 _throw_two;

        private bool OK_one;
        private bool OK_two;
        public bool OkOne => OK_one;
        public bool OkTwo => OK_two;

        
        protected override void Start()
        {
            base.Start();
        }
        
        public override void Update()
        {
            base.Update();

            if (GameStatus == GameStatus.STARTED)
            {
                if (_innerStatus == InnerGameStatus.PREPARATION && player1.getOneTouch())
                {
                    _innerStatus = InnerGameStatus.FIRST_THROW;
                    player1.GetComponent<CelluloAgentRigidBody>().SetVisualEffect(VisualEffect.VisualEffectBlink, Color.cyan, 20);
                }

                if (_innerStatus == InnerGameStatus.FIRST_THROW)
                {
                    Throw(player1, StartOne);
                    OK_one = true;
                    player1.GetComponent<CelluloAgentRigidBody>().SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.cyan, 0);
                }

                if (_innerStatus == InnerGameStatus.PREPARATION && player2.getOneTouch())
                {
                    _innerStatus = InnerGameStatus.SECOND_THROW;
                    player2.GetComponent<CelluloAgentRigidBody>().SetVisualEffect(VisualEffect.VisualEffectBlink, Color.red, 20);
                }

                if (_innerStatus == InnerGameStatus.SECOND_THROW)
                {
                    Throw(player2, StartTwo);
                    OK_two = true;
                    player2.GetComponent<CelluloAgentRigidBody>().SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.red, 0);
                }

                if (_innerStatus == InnerGameStatus.PREPARATION && OK_one && OK_two) { _innerStatus = InnerGameStatus.END; }

                if (_innerStatus == InnerGameStatus.END 
                    && player2.GetSteering().linear == Vector3.zero 
                    && player1.GetSteering().linear == Vector3.zero)
                { GameEnded(); }
            }
        }

        public override void StartGame()
        {
            base.StartGame();
            OK_one = false;
            OK_two = false;
            float posZ = Random.Range(-9, -1);
            bot.celluloAgent.SetGoalPosition(StartBot.x, posZ, 2f);
        }

        protected override void PlayerReady()
        {
            ExecuteAfterDelay(5f, () => { _innerStatus = InnerGameStatus.PREPARATION; });
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

            OK_one = false;
            OK_two = false;
            
            base.GameEnded();
        }
        
        private void Throw(CelluloPlayer player, Vector3 start)
        {
            _innerStatus = InnerGameStatus.PREPARATION;
            
            ExecuteAfterDelay(5f, () =>
            {
                Vector3 powerThrow = start + (start - player.transform.position) * PowerFactor;
                player.celluloAgent.SetGoalPosition(powerThrow.x, powerThrow.z, 2f);
            });
        }

        private double eucl_dist(Vector3 vec_a, Vector3 vec_b)
        {
            return Math.Sqrt(Math.Pow(vec_a.x - vec_b.x, 2) + Math.Pow(vec_a.z - vec_b.z, 2));
        }

        public enum InnerGameStatus
        {
            PREPARATION,
            FIRST_THROW,
            SECOND_THROW,
            END
        }
        
        private bool _isCoroutineExecuting = false;
        
        private IEnumerator ExecuteAfterTime(float time, Action task)
        {
            if (_isCoroutineExecuting)
                yield break;
            _isCoroutineExecuting = true;
            yield return new WaitForSeconds(time);
            task();
            _isCoroutineExecuting = false;
        }

        private void ExecuteAfterDelay(float time, Action task)
        {
            StartCoroutine(ExecuteAfterTime(time, task));
        }
    }
}