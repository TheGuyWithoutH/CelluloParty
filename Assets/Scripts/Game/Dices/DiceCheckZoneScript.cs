using UnityEngine;

namespace Game.Dices
{
    public class DiceCheckZoneScript : MonoBehaviour
    {
        public Dice dice;
        Vector3 _diceVelocity;

        // Update is called once per frame
        void FixedUpdate () {
            _diceVelocity = dice.diceVelocity;
        }

        void OnTriggerStay(Collider col)
        {
            if (_diceVelocity.x == 0f && _diceVelocity.y == 0f && _diceVelocity.z == 0f)
            {
                switch (col.gameObject.name) {
                    case "Side1":
                        dice.SetDiceResult(DiceFace.Face6);
                        break;
                    case "Side2":
                        dice.SetDiceResult(DiceFace.Face5);
                        break;
                    case "Side3":
                        dice.SetDiceResult(DiceFace.Face4);
                        break;
                    case "Side4":
                        dice.SetDiceResult(DiceFace.Face3);
                        break;
                    case "Side5":
                        dice.SetDiceResult(DiceFace.Face2);
                        break;
                    case "Side6":
                        dice.SetDiceResult(DiceFace.Face1);
                        break;
                }
            }
        }
    }
}