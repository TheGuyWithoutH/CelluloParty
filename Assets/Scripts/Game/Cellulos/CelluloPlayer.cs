using System;
using Game.Map;
using UnityEngine;

namespace Game.Cellulos
{
    public class CelluloPlayer : AgentBehaviour
    {
        public CelluloAgent player;
        //Félix veut qu'on puisse gérer les texts à partir d'ici, j'ai pas très bien compris mais chill
        
        private bool _isReady = false;
        private bool _isActive = false;
        private Map.GameCell _cell; //position on the map
        private int _score;
        private bool _isTouch;
        private int _key;
        private Vector3 _move;
        private Map.GameCell _targetCell;

        void Start()
        { 
            _score = 0;
            _cell = GameCell.Cell1;
        }

        void Update()
        {
            //update the position in function of the outcome of the dice throw
            
            //update the score in function of the outcome of the game
        }

        private void UpdateMove() // problem this will be called a 100 times per second, so cell will be == to target very very fast
        {
            if (_isActive)
            {//really need this check?
                if(_cell != _targetCell)
                {
                    
                    UpdateCell();

                    if (Enum.IsDefined(typeof(Map.GameCell), _cell))
                    {
                        if (!Map.GameCells.GetCellOccupied(_cell))
                        {
                            _move = MoveFromPosition(Map.GameCells.GetCellPosition(_cell));
                            
                        }
                        else
                        {
                            _move = MoveFromPosition(Map.GameCells.GetCellShiftedPosition(_cell));
                        }
                    }
                }
            }
        }

        private Vector3 MoveFromPosition(Vector3 coord)
        {
            Vector3 currentPos = transform.position; //in theory if the cellulo is already in the correct cell the movement will be 0
            Vector3 move = (coord - currentPos).normalized;
            return new Vector3 (move.x, 0, move.z);

        }

        //update cell only if close enough to the position desired
        private void UpdateCell()
        {
            if (!Map.GameCells.GetCellOccupied(_cell))
            {
                if ((transform.position - Map.GameCells.GetCellPosition(_cell)).magnitude < 0.5) //to check if good enough
                {
                    ++_cell;               
                }

            }
            else
            {
                if ((transform.position - Map.GameCells.GetCellShiftedPosition(_cell)).magnitude < 0.5) //to check if good enough
                {
                    ++_cell;
                }
            }
        }

        public override Steering GetSteering()
        {
            Steering steering = new Steering();

            if (_isActive)
            {
                UpdateMove();
                steering.linear = _move;
                steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(steering.linear, agent.maxAccel));
                return steering;
            }
            return steering;
            
        }

        public void SetTargetCell(Map.GameCell a)
        {
            _targetCell = a;
        }

        public override void OnCelluloLongTouch(int key)
        {
            base.OnCelluloLongTouch(key);
            _isReady = true;
            _key = key;
        }

        public override void OnCelluloTouchBegan(int key)
        {
            base.OnCelluloTouchBegan(key);
            _isTouch = true;
            _key = key;
        }

        public override void OnCelluloTouchReleased(int key)
        {
            base.OnCelluloTouchReleased(key);
            _isTouch = false;
        }
        
        public bool IsReady => _isReady;

        public void SetNotReady()
        {
            _isReady = false;
        }

        public void SteeringReactivate()
        {
            _isActive = true;
        }
        
        public void SteeringDesactivate()
        {
            _isActive = false;
        }

        public int Score
        {
            get => _score;
            set => _score = value;
        }

        public bool IsTouch => _isTouch;

        public int Key
        {
            get => _key;
            set => _key = value;
        }
    }
}