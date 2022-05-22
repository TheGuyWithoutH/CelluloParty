using System;

namespace Game.Map
{
    public enum GameCell
    {
        //Example pour la première cellule, à compléter
        [CellAttribute(5.9, -8.35)] Cell1,
        [CellAttribute(5.9, -9.3)] Cell2,
        [CellAttribute(4.8, -9.3)] Cell3,
        [CellAttribute(3.8, -9.3)] Cell4,
        [CellAttribute(2.8, -9.3)] Cell5,
        [CellAttribute(1.8, -9.3)] Cell6,
        [CellAttribute(0.75, -9.3)] Cell7,
        [CellAttribute(0.75, -8.4)] Cell8,
        [CellAttribute(0.75, -7.4)] CellRiver,
        [CellAttribute(0.75, -6.5)] Cell9,
        [CellAttribute(0.75, -5.5)] Cell10,
        [CellAttribute(0.75, -4.6)] Cell11,
        [CellAttribute(0.75, -3.6)] Cell12,
        [CellAttribute(0.75, -2.6)] Cell13,
        [CellAttribute(0.75, -1.7)] Cell14,
        [CellAttribute(0.75, -0.8)] Cell15,
        [CellAttribute(1.8, -0.8)] Cell16,
        [CellAttribute(2.8, -0.8)] Cell17,
        [CellAttribute(3.85, -0.8)] Cell18,
        [CellAttribute(5, -0.8)] Cell19,
        [CellAttribute(6.1, -0.8)] Cell20,
        [CellAttribute(7.15, -0.8)] Cell21,
        [CellAttribute(8.25, -0.8)] Cell22,
        [CellAttribute(9.3, -0.8)] Cell23,
        [CellAttribute(10.35, -0.8)] Cell24,
        [CellAttribute(11.35, -0.8)] CellVolcano,
        [CellAttribute(12.4, -0.8)] Cell25,
        [CellAttribute(13.4, -0.8)] Cell26,
        [CellAttribute(13.4, -1.75)] Cell27,
        [CellAttribute(13.4, -2.75)] Cell28,
        [CellAttribute(13.4, -3.7)] Cell29,
        [CellAttribute(13.4, -4.65)] Cell30,
        [CellAttribute(13.4, -5.55)] Cell31,
        [CellAttribute(13.4, -6.45)] CellPlane,
        [CellAttribute(13.4, -7.45)] Cell32,
        [CellAttribute(13.4, -8.35)] Cell33,
        [CellAttribute(13.4, -9.3)] Cell34,
        [CellAttribute(12.4, -9.3)] Cell35,
        [CellAttribute(11.35, -9.3)] Cell36,
        [CellAttribute(10.3, -9.3)] Cell37,
        [CellAttribute(9.3, -9.3)] Cell38,
        [CellAttribute(8.3, -9.3)] Cell39,
        [CellAttribute(8.3, -8.35)] Cell40, 
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