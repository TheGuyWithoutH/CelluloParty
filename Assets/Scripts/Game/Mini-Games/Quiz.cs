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
        private const int TimeQuestions = 18;
        
        private int _curr_seconds;
        private int _curr_value;

        public TextMeshProUGUI timerText;
        public TextMeshProUGUI q;
        public TextMeshProUGUI a1;
        public TextMeshProUGUI a2;
        public TextMeshProUGUI a3;
        public Timer timer;
        
        protected override void Start()
        {
            base.Start();
        }

        public override void Update()
        {
            base.Update();
            
            timerText.SetText(string.Format("{0:00}:{1:00}", timer.Minutes, timer.Seconds));
            LedBot();

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

            player1.GetComponent<CelluloAgentRigidBody>().SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.cyan, 0);
            player1.GetComponent<CelluloAgentRigidBody>().SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.green, 2);
            player1.GetComponent<CelluloAgentRigidBody>().SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.red, 4);
            
            player2.GetComponent<CelluloAgentRigidBody>().SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.cyan, 0);
            player2.GetComponent<CelluloAgentRigidBody>().SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.green, 2);
            player2.GetComponent<CelluloAgentRigidBody>().SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.red, 4);
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
            
            _curr_seconds = 0;
            _curr_value = 0;
            
            _innerStatus = InnerGameStatus.REFLEXION;
        }

        private void LedBot()
        {
            if (timer.Seconds == _curr_seconds + 3)
            {
                _curr_seconds += 3;
                _curr_value += 42;
                bot.GetComponent<CelluloAgentRigidBody>().SetVisualEffect(VisualEffect.VisualEffectProgress, Color.red, _curr_value);
            }
        }

        private Question[] _questions_set_one =
        {
            new Question("Which city is the capital of Spain ?", 2, 
                new Dictionary<int, string>
                {
                    {0, "Barcelona"},
                    {2, "Madrid"},
                    {4, "Liverpool"}
                }),
            new Question("How many synonyms for snow has Scotland ?", 0,
                new Dictionary<int, string>
                {
                    {0, "421"},
                    {2, "373"},
                    {4, "53"}
                }),
            new Question("What is the prefect size ?", 0,
                new Dictionary<int, string>
                {
                    {0, "5 inches"},
                    {2, "2 inches"},
                    {4, "It doesnt matter"}
                }),
            new Question("Which country has the most pyramids in the world ?", 0, 
                new Dictionary<int, string>
                {
                    {0, "Sudan"},
                    {2, "Egypt"},
                    {4, "Peru"}
                }),
            new Question("Which part of the body does not contain a sigle blood vessel ?", 4,
                new Dictionary<int, string>
                {
                    {0, "Ear lobes"},
                    {2, "Tits"},
                    {4, "Cornea"}
                }),
            new Question("Which aliment is entirely consumed by the human body ?", 2, 
                new Dictionary<int, string>
                {
                    {0, "Rice"},
                    {2, "Honey"},
                    {4, "Milk"}
                })
        };
        
        private Question[] _questions_set_two =
        {
            new Question("Where was the first animated feature movie made ?", 0,
                new Dictionary<int, string>
                {
                    {0, "Argentina"},
                    {2, "United States"},
                    {4, "France"}
                }),
            new Question("How many islands constitute de Philippines ?", 4,
                new Dictionary<int, string>
                {
                    {0, "15"},
                    {2, "52"},
                    {4, "7641"}
                }),
            new Question("What is the weirdest flavor McDonald' once made for their bubblegums ?", 2,
                new Dictionary<int, string>
                {
                    {0, "Petrol"},
                    {2, "Broccoli"},
                    {4, "Olive"}
                }),
            new Question("Biggest feature of the tropical fungus Ophiocordyceps", 0, 
                new Dictionary<int, string>
                {
                    {0, "It can control ants"},
                    {2, "It has amazing vertues for skincare"},
                    {4, "It the world's second biggest type of fungus"}
                }),
            new Question("Which single letter does not appear in the states of the United States", 4, 
                new Dictionary<int, string>
                {
                    {0, "Z"},
                    {2, "X"},
                    {4, "Q"}
                }),
            new Question("How is named a cow-bison hypbrid ?", 0, 
                new Dictionary<int, string>
                {
                    {0, "Beefalo"},
                    {2, "Cow-Bison"},
                    {4, "Cowfalo"}
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