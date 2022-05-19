using System;
using UnityEngine;

namespace Game.Cellulos
{
    public class CelluloPlayer : AgentBehaviour
    {
        public CelluloAgent player;
        private int cell; //position on the map
        private int score;
        public bool isReady = false;
        //Félix veut qu'on puisse gérer les texts à partir d'ici, j'ai pas très bien compris mais chill

        void Start()
        { 
            score = 0;
            cell = 0;
        }

        void Update()
        {
            //update the position in function of the outcome of the dice throw
            //update the score in function of the outcome of the game
        }
        
        public override void OnCelluloLongTouch(int key)
        {
            base.OnCelluloLongTouch(key);
            isReady = true;
        }
    }
}