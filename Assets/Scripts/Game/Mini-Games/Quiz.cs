using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Mini_Games
{
    public class Quiz : Mini_Game
    {
        protected override void Start()
        {
            base.Start();
            MaxSeconds = 15;
            player1.player.SetVisualEffect(VisualEffect.VisualEffectDirection, Color.cyan, 0);
            player1.player.SetVisualEffect(VisualEffect.VisualEffectDirection, Color.green, 85);
            player1.player.SetVisualEffect(VisualEffect.VisualEffectDirection, Color.red, 170);
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

        private Question[] _questions =
        {
            new Question("l", 2, new Dictionary<int, String>())
        };

        IEnumerator timer(float f) { yield return new WaitForSeconds(f); }

        public class Question
        {
            private String _question;
            private int _answer;
            private Dictionary<int, String> _responses;
            public Question(String question, int answer, Dictionary<int, String> responses)
            {
                _question = question;
                _answer = answer;
                _responses = responses;
            }
        }
    }
}