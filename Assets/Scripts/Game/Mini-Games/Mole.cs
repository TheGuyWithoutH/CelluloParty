using UnityEngine;

namespace Game.Mini_Games
{
    public class Mole : Mini_Game
    {
        public Timer timer;
        
        private float _latence;
        private int _led;
        private bool _delay;
        private int _maxSeconds;

        protected override void Start()
        {
            base.Start();
            _maxSeconds = 60;
        }
        
        public override void Update()
        {
            base.Update();
            
            if (GameStatus == GameStatus.PAUSED)
            {
                timer.PauseTimer();
            } 
            else if (GameStatus == GameStatus.STARTED)
            {
                timer.ResumeTimer();
                if (timer.CurrentTime >= 10 && timer.CurrentTime < 20)
                {
                    _latence = 0.45f;
                } 
                else if (timer.CurrentTime >= 20 && timer.CurrentTime < 30)
                {
                    _latence = 0.4f;
                } 
                else if (timer.CurrentTime >= 30 && timer.CurrentTime < 40)
                {
                    _latence = 0.35f;
                } 
                else if (timer.CurrentTime >= 40 && timer.CurrentTime < 50)
                {
                    _latence = 0.3f;
                } 
                else
                {
                    _latence = 0.25f;
                }

                if (player1.IsTouch && !_delay)
                {
                    if (player1.Key == _led)
                    {
                        player1.Score += 1;
                        _delay = true;
                    }
                    else
                    {
                        player1.Score -= 1;
                        _delay = true;
                    }
                    Invoke(nameof(FinishDelay), 1.0f);
                }
                
                if (player2.IsTouch && !_delay)
                {
                    if (player2.Key == _led)
                    {
                        player2.Score += 1;
                        _delay = true;
                    }
                    else
                    {
                        player2.Score -= 1;
                        _delay = true;
                    }
                    Invoke(nameof(FinishDelay), 1.0f);
                }
            }
        }

        public override void StartGame()
        {
            base.StartGame();
            _latence = 0.5f;
        }

        protected override void PlayerReady()
        {
            base.PlayerReady();
            timer.StartTimer(_maxSeconds, this);
            Invoke("MoleAppear", Random.Range(0.5f, 3.0f));
        }

        public override void OnGamePause()
        {
            base.OnGamePause();
        }

        public override void GameEnded()
        {
            base.GameEnded();
        }

        private void MoleAppear()
        {
            _led = Random.Range(0, 6);
            player1.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.red, _led);
            player2.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.red, _led);
            Invoke(nameof(MoleDisappear), _latence);
            Invoke(nameof(MoleAppear), Random.Range(0.5f, 3.0f));
        }

        private void MoleDisappear()
        {
            player1.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.black, _led);
            player2.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.black, _led);
        }

        private void FinishDelay()
        {
            _delay = false;
        }
    }
}