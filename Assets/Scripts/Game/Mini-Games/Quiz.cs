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

        private const int NumSets = 4;
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
                timerText.SetText(string.Format("{0:00}:{1:00}", timer.Minutes, TimeQuestions - timer.Seconds));
                LedBot();

                if (_innerStatus == InnerGameStatus.NEXT)
                {
                    switch (_currentQuestion.Answer)
                    {
                        case 0:
                            a2.text = "";
                            a3.text = "";
                            break;
                        case 2:
                            a1.text = "";
                            a3.text = "";
                            break;
                        case 4:
                            a1.text = "";
                            a2.text = "";
                            break;
                            
                    }
                    _innerStatus = InnerGameStatus.NONE;
                    Invoke(nameof(NextQuestion), 5f);
                }

                if (_innerStatus == InnerGameStatus.REFLEXION && (timer.Seconds >= TimeQuestions ||
                    _false_one && _false_two))
                {
                    timer.PauseTimer();
                    _innerStatus = InnerGameStatus.NEXT;
                }

                if (_innerStatus == InnerGameStatus.REFLEXION && HasAnswered(player1) && !_false_one)
                {
                    timer.PauseTimer();
                    if (CheckAnswer(player1, _currentQuestion))
                    {
                        ++player1.Score;
                        _innerStatus = InnerGameStatus.NEXT;
                    }
                    else
                    {
                        timer.ResumeTimer();
                        _false_one = true;
                    }
                }
            
                if (_innerStatus == InnerGameStatus.REFLEXION && HasAnswered(player2) && !_false_two)
                {
                    Debug.Log("Answer of player two : " + player2.Key + "\n");
                    timer.PauseTimer();
                    if (CheckAnswer(player2, _currentQuestion))
                    {
                        ++player2.Score;
                        _innerStatus = InnerGameStatus.NEXT;
                    }
                    else
                    {
                        timer.ResumeTimer();
                        _false_two = true;
                    }
                }
            }
        }

        public override void StartGame()
        {
            base.StartGame();
            
            _innerStatus = InnerGameStatus.NONE;
            _sets = new List<Question[]>{ _questions_set_one, _questions_set_two, 
                _questions_set_three, _questions_set_four };
            int rand = Random.Range(0, NumSets - 1);
            Debug.Log("Set : " + rand + "\n");
            _current_set = _sets[rand];

            _false_one = false;
            _false_two = false;

            _curr_index = 0;
            
            player1.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.black, 0);
            player2.celluloAgent.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.black, 0);

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
            questionLayout.gameObject.SetActive(false);
        }

        private bool HasAnswered(CelluloPlayer player) { return player.getOneTouch(); }

        private bool CheckAnswer(CelluloPlayer player, Question question)
        {
            return player.Key == question.Answer;
        }

        private void NextQuestion()
        {
            if (_curr_index == NumQuestions)
            {
                GameEnded();
                return;
            }
            
            _currentQuestion = _current_set[_curr_index];
            ++_curr_index;

            q.text = _currentQuestion.Question1;
            a1.text = _currentQuestion.Responses[0];
            a2.text = _currentQuestion.Responses[2];
            a3.text = _currentQuestion.Responses[4];

            questionLayout.gameObject.SetActive(true);
            
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
                    {4, "7'641"}
                }),
            new Question("What is the weirdest flavor McDonald' once made for their bubblegums ?", 2,
                new Dictionary<int, string>
                {
                    {0, "Petrol"},
                    {2, "Broccoli"},
                    {4, "Olive"}
                }),
            new Question("Biggest feature of the tropical fungus Ophiocordyceps is that ?", 0, 
                new Dictionary<int, string>
                {
                    {0, "It can control ants"},
                    {2, "It has amazing vertues for skincare"},
                    {4, "It the world's second biggest type of fungus"}
                }),
            new Question("Which single letter does not appear in the states of the United States ?", 4, 
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
        
        private Question[] _questions_set_three =
        {
            new Question("What armadillo's shells are known for ?", 2,
                new Dictionary<int, string>
                {
                    {0, "Being able to resist to predators"},
                    {2, "Being bulletproof"},
                    {4, "Being waterproof"}
                }),
            new Question("How many letters long is the longest word in the Oxford English dictionnary ?", 0,
                new Dictionary<int, string>
                {
                    {0, "45"},
                    {2, "189'819"},
                    {4, "4'641"}
                }),
            new Question("What Kleenex was originally intended ?", 4,
                new Dictionary<int, string>
                {
                    {0, "As tissues"},
                    {2, "As sex related toys"},
                    {4, "For gas masks"}
                }),
            new Question("Who designed to current American flag ?", 0, 
                new Dictionary<int, string>
                {
                    {0, "A student"},
                    {2, "Ralph Lauren"},
                    {4, "Roy Halston Frowick"}
                }),
            new Question("Which percentage of the Sahara desert is made out of sand", 4, 
                new Dictionary<int, string>
                {
                    {0, "5%"},
                    {2, "100%"},
                    {4, "25%"}
                }),
            new Question("What natural catastrophe happened on the moon ?", 2, 
                new Dictionary<int, string>
                {
                    {0, "Earthquakes"},
                    {2, "Volcano erruptions"},
                    {4, "Tsunamis"}
                })
        };
        
        private Question[] _questions_set_four =
        {
            new Question("Which letter isn't contained in the numbers before 1000 ?", 4,
                new Dictionary<int, string>
                {
                    {0, "Y"},
                    {2, "U"},
                    {4, "A"}
                }),
            new Question("When where movie trailers originally played ?", 0,
                new Dictionary<int, string>
                {
                    {0, "After movies"},
                    {2, "During movies"},
                    {4, "Before movies"}
                }),
            new Question("What does H&M stands for ?", 2,
                new Dictionary<int, string>
                {
                    {0, "Her&Me"},
                    {2, "Hennes&Mauritz"},
                    {4, "Halt&Malt"}
                }),
            new Question("How long can be giraffes' tongues", 2, 
                new Dictionary<int, string>
                {
                    {0, "30 inches"},
                    {2, "20 inches"},
                    {4, "10 inches"}
                }),
            new Question("How many dollars did the inventor of the microwave won ?", 0, 
                new Dictionary<int, string>
                {
                    {0, "2$"},
                    {2, "Millions of dollars"},
                    {4, "28$ per microwave constructed"}
                }),
            new Question("For which price was the script of Terminator sold for ?", 4, 
                new Dictionary<int, string>
                {
                    {0, "15'000$"},
                    {2, "9'000$"},
                    {4, "1$"}
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
            NONE,
            REFLEXION,
            NEXT
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