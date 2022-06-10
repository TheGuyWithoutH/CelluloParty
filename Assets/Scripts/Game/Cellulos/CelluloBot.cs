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
                Vector3 rebound = curl.Target + (curl.Target - curl.ThrowOne);
                player_one.celluloAgent.SetGoalPosition(rebound.x, rebound.z, 1f);
            }

            if (curl.OkTwo && collision.collider.CompareTag("Player_two"))
            {
                player_two.celluloAgent.SetSteering(new Steering());
                Vector3 rebound = curl.Target + (curl.Target - curl.ThrowTwo);;
                player_two.celluloAgent.SetGoalPosition(rebound.x, rebound.z, 1f);
            }
        }
    }
}