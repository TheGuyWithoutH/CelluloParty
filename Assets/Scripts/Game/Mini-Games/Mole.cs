using UnityEngine;

namespace Game.Mini_Games
{
    public class Mole : Mini_Game
    {
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
                if (timer.CurrentTime % 10 == 0)
                {
                    _latence -= 0.05f;
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
            player1.GetComponent<CelluloAgentRigidBody>().SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.red, _led);
            player2.GetComponent<CelluloAgentRigidBody>().SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.red, _led);
            Invoke(nameof(MoleDisappear), _latence);
            Invoke(nameof(MoleAppear), Random.Range(0.5f, 3.0f));
        }

        private void MoleDisappear()
        {
            _led = -1;
            player1.GetComponent<CelluloAgentRigidBody>().SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.black, _led);
            player2.GetComponent<CelluloAgentRigidBody>().SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.black, _led);
        }

        private void FinishDelay()
        {
            _delay = false;
        }
    }
}