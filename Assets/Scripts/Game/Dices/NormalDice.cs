using UnityEngine;

namespace Game.Dices
{
    public class NormalDice : Dice
    {
        public override void SetDiceResult(DiceFace face)
        {
            switch (face)
            {
                case DiceFace.Face1:
                    _result = 1;
                    break;
                case DiceFace.Face2:
                    _result = 2;
                    break;
                case DiceFace.Face3:
                    _result = 3;
                    break;
                case DiceFace.Face4:
                    _result = 4;
                    break;
                case DiceFace.Face5:
                    _result = 5;
                    break;
                case DiceFace.Face6:
                    _result = 6;
                    break;
            }

            _done = true;
        }
    }
}