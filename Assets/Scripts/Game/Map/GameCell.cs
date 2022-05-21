using System;

namespace Game.Map
{
    public enum GameCell
    {
        //Example pour la première cellule, à compléter
        [CellAttribute(5.9, -8.4)] Cell1,
    }

    class CellAttribute : Attribute
    {
        internal CellAttribute(double x, double z)
        {
            this.x = x;
            this.z = z;
        }
        public double x { get; private set; }
        public double z { get; private set; }
    }
}