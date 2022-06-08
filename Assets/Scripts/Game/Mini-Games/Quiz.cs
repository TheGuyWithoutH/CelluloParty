using System;
using System.Collections;
using System.Collections.Generic;
using Game.Cellulos;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
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

        private int _curr_index;
        
        private int _curr_seconds;
        private int _curr_value;
        
        private bool _false_one;
        private bool _false_two;
        
        public TextMeshProUGUI timerText;
        public TextMeshProUGUI q;
        public TextMeshProUGUI a1;
        public TextMeshProUGUI a2;
        public TextMeshProUGUI a3;
        public Timer timer;
        public Image questionLayout;

        public override void Update()
        {
            base.Update();

            if (GameStatus == GameStatus.STARTED)
            {
                timerText.SetText(string.Format("{0:00}:{1:00}", timer.Minutes, timer.Seconds));
                LedBot();
                
                if (_innerStatus == InnerGameStatus.NEXT) { NextQuestion(); }
                
                if (_innerStatus == InnerGameStatus.REFLEXION && timer.Seconds >= TimeQuestions) { _innerStatus = InnerGameStatus.NEXT; }

                if (_innerStatus == InnerGameStatus.REFLEXION && HasAnswered(player1) && !_false_one)
                {
                    Debug.Log("Answer of player one : " + player1.Key + "\n");
                    timer.PauseTimer();
                    if (CheckAnswer(player1, _currentQuestion))
                    {
                        ++player1.Score;
                        _innerStatus = InnerGameStatus.NEXT;
                        //faire un truc graphique pour dire que c'est tout bon
                    }
                    else { timer.ResumeTimer(); }
                }
            
                if (_innerStatus == InnerGameStatus.REFLEXION && HasAnswered(player2) && !_false_two)
                {
                    Debug.Log("Answer of player two : " + player2.Key + "\n");
                    timer.PauseTimer();
                    if (CheckAnswer(player2, _currentQuestion))
                    {
                        ++player2.Score;
                        _innerStatus = InnerGameStatus.NEXT;
                        //faire un truc graphique pour dire que c'est tout bon
                    }
                    else
                    { timer.ResumeTimer(); }
                }
            }
        }

        public override void StartGame()
        {
            base.StartGame();

            _sets = new List<Question[]>{ _questions_set_one, _questions_set_two };
            int rand = Random.Range(0, NumSets - 1);
            Debug.Log("Set : " + rand + "\n");
            _current_set = _sets[rand];

            _false_one = false;
            _false_two = false;

            _curr_index = 0;

            player1.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.cyan, 0);
                        player1.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.green, 2);
                        player1.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.red, 4);
                        
                        player2.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.cyan, 0);
                        player2.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.green, 2);
                        player2.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.red, 4);
        }

        protected override void PlayerReady()
        {
            base.PlayerReady();

            player1.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.cyan, 0);
            player1.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.green, 2);
            player1.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.red, 4);
            
            player2.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.cyan, 0);
            player2.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.green, 2);
            player2.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstSingle, Color.red, 4);
            
            Invoke(nameof(NextQuestion), 5f);
        }

        public override void OnGamePause()
        {
            base.OnGamePause();
        }

        public override void GameEnded()
        {
            base.GameEnded();
            questionLayout.enabled = false;
        }

        private bool HasAnswered(CelluloPlayer player) { return player.getOneTouch(); }

        private bool CheckAnswer(CelluloPlayer player, Question question)
        {
            return player.Key == question.Answer;
        }

        private void NextQuestion()
        {
            if(_curr_index == NumQuestions){ GameEnded(); }
            
            _currentQuestion = _current_set[_curr_index];
            ++_curr_index;

            Debug.Log("Question : " + _currentQuestion.Question1 + "\n");
            Debug.Log("Anwer : " + _currentQuestion.Answer + "\n");

            q.text = _currentQuestion.Question1;
            a1.text = _currentQuestion.Responses[0];
            a2.text = _currentQuestion.Responses[2];
            a3.text = _currentQuestion.Responses[4];

            questionLayout.enabled = true;
            
            timer.StartTimer(TimeQuestions + 10, this);
            timerText.SetText(string.Format("{0:00}:{1:00}", 0, 0));
            
            _curr_seconds = 0;
            _curr_value = 0;
            
            _false_one = false;
            _false_two = false;
            
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
        
        private bool _isCoroutineExecuting = false;
        
        private enum InnerGameStatus
        {
            REFLEXION,
            NEXT
        }
        
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