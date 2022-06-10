using System;
using Game.Mini_Games;
using UnityEngine;

namespace Game.Cellulos
{
    public class CelluloBot : AgentBehaviour
    {
        public CelluloAgent celluloAgent;
        public CelluloPlayer player_one;
        public CelluloPlayer player_two;
        public Curling curl;

        private void OnCollisionEnter(Collision collision)
        {
            if (curl.OkOne && collision.collider.CompareTag("Player_one"))
            {
                player_one.celluloAgent.SetSteering(new Steering());
            }

            if (curl.OkTwo && collision.collider.CompareTag("Player_two"))
            {
                player_two.celluloAgent.SetSteering(new Steering());
            }
        }
    }
}