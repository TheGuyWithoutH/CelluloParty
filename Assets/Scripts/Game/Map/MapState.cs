using System;

namespace Game.Map
{
    public static class MapState
    {
        private static bool[] occupiedState = new bool[Enum.GetNames(typeof(GameCell)).Length];
        
        public static bool GetCellOccupied(this GameCell cell)
        {
            return occupiedState[(int) cell];
        }

        public static void SetCellOccupied(this GameCell cell, bool occup)
        {
            occupiedState[(int) cell] = occup;
        }
    }
}