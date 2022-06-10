using System;
using System.Collections;
using Game.Cellulos;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Mini_Games
{
    public class Curling : Mini_Game
    {
        private InnerGameStatus _innerStatus = InnerGameStatus.NONE;

        private const float PowerFactor = 2f;
        private Vector3 _target = new Vector3(7.12f, 0f, -4.76f);

        private Vector3 _curl_start_one;
        private Vector3 _curl_start_two;

        private Vector3 _throw_one;
        private Vector3 _throw_two;

        private bool OK_one;
        private bool OK_two;
        
        public bool OkOne => OK_one;
        public bool OkTwo => OK_two;
        public Vector3 ThrowOne => _throw_one;

        public Vector3 ThrowTwo => _throw_two;

        
        protected override void Start()
        {
            base.Start();
        }
        
        public override void Update()
        {
            base.Update();

            if (GameStatus == GameStatus.STARTED)
            {
                if (_innerStatus == InnerGameStatus.PREPARATION && player1.IsTouch)
                {
                    _innerStatus = InnerGameStatus.FIRST_THROW;
                    player1.celluloAgent
                        .SetVisualEffect(VisualEffect.VisualEffectBlink, player1.celluloAgent.initialColor, 20);
                }

                if (_innerStatus == InnerGameStatus.FIRST_THROW && !player1.IsTouch)
                {
                    Throw(player1, _curl_start_one, _throw_one, StartOne);
                    OK_one = true;
                    player1.celluloAgent
                        .SetVisualEffect(VisualEffect.VisualEffectConstAll, player1.celluloAgent.initialColor, 0);
                }

                if (_innerStatus == InnerGameStatus.PREPARATION && player2.IsTouch)
                {
                    _innerStatus = InnerGameStatus.SECOND_THROW;
                    player2.celluloAgent
                        .SetVisualEffect(VisualEffect.VisualEffectBlink, player2.celluloAgent.initialColor, 20);
                }

                if (_innerStatus == InnerGameStatus.SECOND_THROW && !player2.IsTouch)
                {
                    Throw(player2, _curl_start_two, _throw_two, StartTwo);
                    OK_two = true;
                    player2.celluloAgent
                        .SetVisualEffect(VisualEffect.VisualEffectConstAll, player2.celluloAgent.initialColor, 0);
                }

                if (_innerStatus == InnerGameStatus.PREPARATION && OK_one && OK_two)
                {
                    _innerStatus = InnerGameStatus.NONE;
                    Invoke(nameof(WaitToEnd), 20f);
                }

                if (_innerStatus == InnerGameStatus.END 
                    && player2.GetComponent<Rigidbody>().velocity == Vector3.zero 
                    && player1.GetComponent<Rigidbody>().velocity == Vector3.zero)
                { GameEnded(); }
            }
        }

        private void WaitToEnd() { _innerStatus = InnerGameStatus.END; }

        public override void StartGame()
        {
            base.StartGame();
            _innerStatus = InnerGameStatus.NONE;
            OK_one = false;
            OK_two = false;
            float posZ = Random.Range(-9, -1);
            _target.z = posZ;
            bot.celluloAgent.SetGoalPosition(StartBot.x, posZ, 2f);
        }

        protected override void PlayerReady()
        {
            base.PlayerReady();
            
            _curl_start_one = player1.transform.position;
            _curl_start_two = player2.transform.position;
            
            bot.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectBlink, Color.white, 20);
            ExecuteAfterDelay(5f, () =>
            {
                _innerStatus = InnerGameStatus.PREPARATION;
                bot.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.white, 0);
            });
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
        
        private void Throw(CelluloPlayer player, Vector3 start, Vector3 throw_x, Vector3 yellowCircle)
        {
            _innerStatus = InnerGameStatus.PREPARATION;
            throw_x = start - player.transform.position;
            Vector3 powerThrow = yellowCircle + throw_x * PowerFactor;
            player.celluloAgent.SetGoalPosition(powerThrow.x, powerThrow.z, 2f);
        }

        private double eucl_dist(Vector3 vec_a, Vector3 vec_b)
        {
            return Math.Sqrt(Math.Pow(vec_a.x - vec_b.x, 2) + Math.Pow(vec_a.z - vec_b.z, 2));
        }

        public enum InnerGameStatus
        {
            NONE,
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