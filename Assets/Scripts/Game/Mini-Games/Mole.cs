using UnityEngine;

namespace Game.Mini_Games
{
    public class Mole : Mini_Game
    {
        public Timer timer;
        
        private float _latence;
        private int _led;
        private int _maxSeconds;
        private bool _isMole;
        private bool _firstStart;

        protected override void Start()
        {
            base.Start();
            _maxSeconds = 60;
        }
        
        public override void Update()
        {
            base.Update();

            if (GameStatus == GameStatus.STARTED && _firstStart)
            {
                if (timer.CurrentTime >= 10 && timer.CurrentTime < 20)
                {
                    bot.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.red, 0);
                    _latence = 1.4f;
                } 
                if (timer.CurrentTime >= 20 && timer.CurrentTime < 30)
                {
                    bot.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.red, 1);
                    _latence = 1.2f;
                } 
                if (timer.CurrentTime >= 30 && timer.CurrentTime < 40)
                {
                    bot.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.red, 2);
                    _latence = 1.0f;
                } 
                if (timer.CurrentTime >= 40 && timer.CurrentTime < 50)
                {
                    bot.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.red, 3);
                    _latence = 0.8f;
                } 
                if (timer.CurrentTime >= 50 && timer.CurrentTime < 60)
                {
                    bot.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.red, 4);
                    _latence = 0.6f;
                }
                if (timer.CurrentTime >= 60)
                {
                    bot.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.red, 5);
                }

                if (player1.getOneTouch())
                {
                    if (player1.Key == _led && _isMole)
                    {
                        player1.Score += 1;
                        player1.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.yellow, _led);
                        _isMole = false;
                    }
                    else
                    {
                        player1.Score -= 1;
                    }
                }
                
                if (player2.getOneTouch())
                {
                    if (player2.Key == _led && _isMole)
                    {
                        player2.Score += 1;
                        player2.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.white, _led);
                        _isMole = false;
                    }
                    else
                    {
                        player2.Score -= 1;
                    }
                }
            }
        }

        public override void StartGame()
        {
            base.StartGame();
            _latence = 1.6f;
            bot.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.black, 0);
        }

        protected override void PlayerReady()
        {
            base.PlayerReady();
            bot.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectWaiting, Color.red, 0);
            player1.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.white, 0);
            player2.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.white, 0);
            Invoke(nameof(GameBegin), 3.0f);
            Invoke(nameof(MoleAppear), Random.Range(3.0f, 6.0f));
        }

        public override void OnGamePause()
        {
            base.OnGamePause();
            timer.PauseTimer();
            bot.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectWaiting, Color.red, 0);
            _isMole = false;
        }

        public override void OnGameResume()
        {
            base.OnGameResume();
            bot.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectAlertAll, Color.red, 0);
            Invoke(nameof(timer.ResumeTimer), 3.0f);
            Invoke(nameof(MoleAppear), Random.Range(3.0f, 6.0f));
        }

        public override void GameEnded()
        {
            base.GameEnded();
            bot.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.red, 0);
        }

        private void GameBegin()
        {
            _firstStart = true;
            bot.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.black, 0);
            player1.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.white, 0);
            player2.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.white, 0);
            timer.StartTimer(_maxSeconds, this);
        }

        private void MoleAppear()
        {
            if (GameStatus == GameStatus.STARTED)
            {
                _isMole = true;
                _led = Random.Range(0, 6);
                player1.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.red, _led);
                player2.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.red, _led);
                Invoke(nameof(MoleDisappear), _latence);
                Invoke(nameof(MoleAppear), Random.Range(_latence + 0.2f, 3f));
            }
        }

        private void MoleDisappear()
        {
            player1.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.white, _led);
            player2.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.white, _led);
            _isMole = false;
        }
    }
}