using System;
using System.Collections;
using System.Collections.Generic;
using Game.Cellulos;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Mini_Games
{
    public class Quiz : Mini_Game
    {
        private Timer _timer;
        
        private GameStatus _innerStatus;
        private Question _currentQuestion;
        
        private bool _answer_both;
        private bool _answer1;
        private bool _answer2;

        private const int NUM_QUESTIONS = 15;
        protected override void Start()
        {
            base.Start();
            MaxSeconds = 15;
        }
        
        public override void Update()
        {
            base.Update();
            
            
            
        }

        public override void StartGame()
        {
            base.StartGame();
            
            player1.Key = -1;
            player2.Key = -1;
            
            NextQuestion();
            
            player1.player.SetVisualEffect(VisualEffect.VisualEffectDirection, Color.cyan, 0);
            player1.player.SetVisualEffect(VisualEffect.VisualEffectDirection, Color.green, 85);
            player1.player.SetVisualEffect(VisualEffect.VisualEffectDirection, Color.red, 170);
        }
        
        public override void OnGamePause()
        {
            base.OnGamePause();
        }

        public override void GameEnded()
        {
            base.GameEnded();
        }

        private bool HasAnswered(CelluloPlayer player) { return player.Key != -1; }
        private bool CheckAnswer(CelluloPlayer player, Question question) { return player.Key == question.Answer; }

        private void NextQuestion()
        {
            int rand = Random.Range(0, NUM_QUESTIONS - 1);
            _currentQuestion = _questions[rand];
            timer.StartTimer(15, this);
        }

        private Question[] _questions =
        {
            new Question("l", 2, new Dictionary<int, String>())
        };

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
            public int Answer => _answer;
        }
        
        private enum GameStatus
        {
            REFLEXION,
            CHECK,
            NEXT
        }
    }
}