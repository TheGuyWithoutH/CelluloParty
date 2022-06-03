using System;
using System.Collections.Generic;
using Game.Cellulos;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Mini_Games
{
    public class Quiz : Mini_Game
    {
        private InnerGameStatus _innerStatus;
        private List<Question[]> _sets;
        private Question[] _current_set;
        private Question _currentQuestion;

        private const int NumSets = 2;
        private const int NumQuestions = 6;
        private const int TimeQuestions = 15;

        public TextMeshProUGUI timerText;
        
        public TextMeshProUGUI q;
        public TextMeshProUGUI a1;
        public TextMeshProUGUI a2;
        public TextMeshProUGUI a3;

        protected override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            base.Update();
            
            timerText.SetText(string.Format("{0:00}:{1:00}", timer.Minutes, timer.Seconds));

            if (GameStatus == GameStatus.STARTED)
            {
                if (_innerStatus == InnerGameStatus.NEXT) { NextQuestion(_current_set); }

                if (_innerStatus == InnerGameStatus.REFLEXION && HasAnswered(player1))
                {
                    timer.PauseTimer();
                    if (CheckAnswer(player1, _currentQuestion))
                    {
                        ++player1.Score;
                        _innerStatus = InnerGameStatus.NEXT;
                        //faire un truc graphique pour dire que c'est tout bon
                    }
                    else
                    {
                        player1.Key = -1;
                        timer.ResumeTimer();
                    }
                }
            
                if (_innerStatus == InnerGameStatus.REFLEXION && HasAnswered(player2))
                {
                    timer.PauseTimer();
                    if (CheckAnswer(player2, _currentQuestion))
                    {
                        ++player2.Score;
                        _innerStatus = InnerGameStatus.NEXT;
                        //faire un truc graphique pour dire que c'est tout bon
                    }
                    else
                    {
                        player1.Key = -1;
                        timer.ResumeTimer();
                    }
                }
            }
            
            
        }

        public override void StartGame()
        {
            base.StartGame();

            _sets = new List<Question[]>{ _questions_set_one, _questions_set_two };
            int rand = Random.Range(0, NumSets - 1);
            _current_set = _sets[rand];

            player1.Key = -1;
            player2.Key = -1;

            NextQuestion(_current_set);

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

        private bool HasAnswered(CelluloPlayer player)
        {
            return player.Key != -1;
        }

        private bool CheckAnswer(CelluloPlayer player, Question question)
        {
            return player.Key == question.Answer;
        }

        private void NextQuestion(Question[] set)
        {
            int rand = Random.Range(0, NumQuestions - 1);
            _currentQuestion = set[rand];

            q.text = _currentQuestion.Question1;
            a1.text = _currentQuestion.Responses[1];
            a2.text = _currentQuestion.Responses[2];
            a3.text = _currentQuestion.Responses[3];
            
            timer.StartTimer(TimeQuestions, this);
            timerText.SetText(string.Format("{0:00}:{1:00}", 0, 0));
            
            _innerStatus = InnerGameStatus.REFLEXION;
        }

        private Question[] _questions_set_one =
        {
            new Question("Which city is the capital of Spain ?", 2, 
                new Dictionary<int, string>
                {
                    {1, "Barcelona"},
                    {2, "Madrid"},
                    {3, "Liverpool"}
                }),
            new Question("How many synonyms for snow has Scotland ?", 1,
                new Dictionary<int, string>
                {
                    {1, "421"},
                    {2, "373"},
                    {3, "53"}
                }),
            new Question("What is the prefect size ?", 1,
                new Dictionary<int, string>
                {
                    {1, "5 inches"},
                    {2, "2 inches"},
                    {3, "It doesnt matter"}
                }),
            new Question("Which country has the most pyramids in the world ?", 1, 
                new Dictionary<int, string>
                {
                    {1, "Sudan"},
                    {2, "Egypt"},
                    {3, "Peru"}
                }),
            new Question("Which part of the body does not contain a sigle blood vessel ?", 3,
                new Dictionary<int, string>
                {
                    {1, "Ear lobes"},
                    {2, "Tits"},
                    {3, "Cornea"}
                }),
            new Question("Which aliment is entirely consumed by the human body ?", 2, 
                new Dictionary<int, string>
                {
                    {1, "Rice"},
                    {2, "Honey"},
                    {3, "Milk"}
                })
        };
        
        private Question[] _questions_set_two =
        {
            new Question("Where was the first animated feature movie made ?", 1,
                new Dictionary<int, string>
                {
                    {1, "Argentina"},
                    {2, "United States"},
                    {3, "France"}
                }),
            new Question("How many islands constitute de Philippines ?", 3,
                new Dictionary<int, string>
                {
                    {1, "15"},
                    {2, "52"},
                    {3, "7641"}
                }),
            new Question("What is the weirdest flavor McDonald' once made for their bubblegums ?", 2,
                new Dictionary<int, string>
                {
                    {1, "Petrol"},
                    {2, "Broccoli"},
                    {3, "Olive"}
                }),
            new Question("Biggest feature of the tropical fungus Ophiocordyceps", 1, 
                new Dictionary<int, string>
                {
                    {1, "It can control ants"},
                    {2, "It has amazing vertues for skincare"},
                    {3, "It the world's second biggest type of fungus"}
                }),
            new Question("Which single letter does not appear in the states of the United States", 3, 
                new Dictionary<int, string>
                {
                    {1, "Z"},
                    {2, "X"},
                    {3, "Q"}
                }),
            new Question("How is named a cow-bison hypbrid ?", 1, 
                new Dictionary<int, string>
                {
                    {1, "Beefalo"},
                    {2, "Cow-Bison"},
                    {3, "Cowfalo"}
                })
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
            
            public string Question1 => _question;

            public Dictionary<int, string> Responses => _responses;
        }
        
        private enum InnerGameStatus
        {
            REFLEXION,
            NEXT
        }
    }
}