using UnityEngine;

namespace Game.Mini_Games
{
    public class Mole : Mini_Game
    {
        public Timer timer;
        
        private float _latence;
        private int _led;
        private int _maxSeconds;

        protected override void Start()
        {
            base.Start();
            _maxSeconds = 60;
        }
        
        public override void Update()
        {
            base.Update();

            if (GameStatus == GameStatus.STARTED)
            {
                if (timer.CurrentTime >= 10 && timer.CurrentTime < 20)
                {
                    bot.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.black, 0);
                    _latence = 0.35f;
                } 
                if (timer.CurrentTime >= 20 && timer.CurrentTime < 30)
                {
                    bot.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.black, 1);
                    _latence = 0.3f;
                } 
                if (timer.CurrentTime >= 30 && timer.CurrentTime < 40)
                {
                    bot.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.black, 2);
                    _latence = 0.25f;
                } 
                if (timer.CurrentTime >= 40 && timer.CurrentTime < 50)
                {
                    bot.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.black, 3);
                    _latence = 0.2f;
                } 
                if (timer.CurrentTime >= 50 && timer.CurrentTime < 60)
                {
                    bot.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.black, 4);
                    _latence = 0.15f;
                }

                if (player1.getOneTouch())
                {
                    if (player1.Key == _led)
                    {
                        Debug.Log("Player 1 +");
                        player1.Score += 1;
                    }
                    else
                    {
                        Debug.Log("Player 1 -");
                        player1.Score -= 1;
                    }
                }
                
                if (player2.getOneTouch())
                {
                    if (player2.Key == _led)
                    {
                        Debug.Log("Player 2 +");
                        player2.Score += 1;
                    }
                    else
                    {
                        Debug.Log("Player 2 -");
                        player2.Score -= 1;
                    }
                }
            }
        }

        public override void StartGame()
        {
            base.StartGame();
            _latence = 0.4f;
            bot.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.black, 0);
            player1.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.white, 0);
            player2.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.white, 0);
            PlayerReady();
        }

        protected override void PlayerReady()
        {
            base.PlayerReady();
            bot.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectAlertAll, Color.red, 0);
            Invoke(nameof(GameBegin), 3.0f);
            Invoke(nameof(MoleAppear), Random.Range(3.0f, 6.0f));
        }

        public override void OnGamePause()
        {
            base.OnGamePause();
            timer.PauseTimer();
            bot.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectWaiting, Color.red, 0);
            _led = -1;
        }

        public override void OnGameResume()
        {
            base.OnGameResume();
            bot.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectAlertAll, Color.red, 0);
            Invoke(nameof(timer.ResumeTimer), 3.0f);
            Invoke(nameof(BotLedsOn), 3.0f);
            Invoke(nameof(MoleAppear), Random.Range(3.0f, 6.0f));
        }

        public override void GameEnded()
        {
            base.GameEnded();
            bot.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.black, 0);
        }

        private void GameBegin()
        {
            timer.StartTimer(_maxSeconds, this);
            BotLedsOn();
        }

        private void BotLedsOn()
        {
            bot.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.red, 0);
        }

        private void MoleAppear()
        {
            if (GameStatus == GameStatus.STARTED) {
                _led = Random.Range(0, 6);
                player1.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.red, _led);
                player2.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.red, _led);
                Invoke(nameof(MoleDisappear), _latence);
                Invoke(nameof(MoleAppear), Random.Range(0.5f, 3.0f));
            }
        }

        private void MoleDisappear()
        {
            if (GameStatus == GameStatus.STARTED)
            {
                player1.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.white, _led);
                player2.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.white, _led);
                _led = -1;
            }
        }
    }
}