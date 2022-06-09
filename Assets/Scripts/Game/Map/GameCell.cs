using System;
using System.Reflection;
using UnityEngine;

namespace Game.Map
{
    public enum GameCell
    {
        //Example pour la première cellule, à compléter
        [CellAttribute(5.9, -8.35, 7, -8.35)] Cell1,
        [CellAttribute(5.9, -9.3,  7, -9.3)] Cell2,
        [CellAttribute(4.8, -9.3,  4.8, -8.2)] Cell3,
        [CellAttribute(3.8, -9.3,  3.8, -7)] Cell4,
        [CellAttribute(2.8, -9.3,  2.8, -8.2)] Cell5,
        [CellAttribute(1.8, -9.3,  1.8, -7.1)] Cell6,
        [CellAttribute(0.75, -9.3,  1.8, -7.1)] Cell7,
        [CellAttribute(0.75, -8.4,  1.8, -7.1)] Cell8,
        [CellAttribute(0.75, -7.4,  0.75, -7.4)] CellRiver,
        [CellAttribute(0.75, -6.5,  1.8, -6.5)] Cell9,
        [CellAttribute(0.75, -5.5,  1.8, -5.5)] Cell10,
        [CellAttribute(0.75, -4.6,  1.8, -4.6)] Cell11,
        [CellAttribute(0.75, -3.6,  1.8, -3.6)] Cell12,
        [CellAttribute(0.75, -2.6,  1.8, -2.6)] Cell13,
        [CellAttribute(0.75, -1.7,  1.8, -1.7)] Cell14,
        [CellAttribute(0.75, -0.8,  1.8, -0.8)] Cell15,
        [CellAttribute(1.8, -0.8,  1.8, -0.8)] Cell16,
        [CellAttribute(2.8, -0.8,  2.8, -2.8)] Cell17,
        [CellAttribute(3.85, -0.8, 3.85, -1.9)] Cell18,
        [CellAttribute(5, -0.8, 5, -1.9)] Cell19,
        [CellAttribute(6.1, -0.8, 6.1, -1.9)] Cell20,
        [CellAttribute(7.15, -0.8,  7.15, -1.9)] Cell21,
        [CellAttribute(8.25, -0.8, 8.25, -1.9)] Cell22,
        [CellAttribute(9.3, -0.8, 9.3, -1.9)] Cell23,
        [CellAttribute(10.35, -0.8, 10.35, -1.9)] Cell24,
        [CellAttribute(11.35, -0.8,11.35, -1.9)] CellVolcano,
        [CellAttribute(12.4, -0.8, 12.4, -1.9)] Cell25,
        [CellAttribute(13.4, -0.8, 12.4, -1.9)] Cell26,
        [CellAttribute(13.4, -1.75, 12.4, -1.9)] Cell27,
        [CellAttribute(13.4, -2.75, 12.4, -2.75)] Cell28,
        [CellAttribute(13.4, -3.7, 12.4, -3.7)] Cell29,
        [CellAttribute(13.4, -4.65, 12.4, -4.65)] Cell30,
        [CellAttribute(13.4, -5.55, 12.4, -5.55)] Cell31,
        [CellAttribute(13.4, -6.45, 12.4, -6.45)] CellPlane,
        [CellAttribute(13.4, -7.45, 12.4, -7.45)] Cell32,
        [CellAttribute(13.4, -8.35, 12.4, -8.2)] Cell33,
        [CellAttribute(13.4, -9.3, 12.4, -8.2)] Cell34,
        [CellAttribute(12.4, -9.3, 12.4, -8.2)] Cell35,
        [CellAttribute(11.35, -9.3, 11.35, -7)] Cell36,
        [CellAttribute(10.3, -9.3, 10.3, -8.2)] Cell37,
        [CellAttribute(9.3, -9.3, 9.3, -8.2)] Cell38,
        [CellAttribute(8.3, -9.3, 9.3, -8.2)] Cell39,
        [CellAttribute(8.3, -8.35,  9.3, -8.2)] Cell40,
    }

    public static class GameCells
    {
        public static Vector3 GetCellPosition(this GameCell cell)
        {
            CellAttribute attr = GetAttr(cell);
            return new Vector3((float) attr.x, 0f, (float) attr.z);
        }
        
        public static Vector3 GetCellShiftedPosition(this GameCell cell)
        {
            CellAttribute attr = GetAttr(cell);
            return new Vector3((float) attr.x2, 0f, (float) attr.z2);
        }

        private static CellAttribute GetAttr(GameCell p)
        {
            return (CellAttribute)Attribute.GetCustomAttribute(ForValue(p), typeof(CellAttribute));
        }

        private static MemberInfo ForValue(GameCell p)
        {
            return typeof(GameCell).GetField(Enum.GetName(typeof(GameCell), p));
        }
    }

    class CellAttribute : Attribute
    {
        internal CellAttribute(double x, double z, double x2, double z2)
        {
            this.x = x;
            this.z = z;
            this.x2 = x2;
            this.z2 = z2;

        }
        public double x { get; private set; }
        public double z { get; private set; }

        public double x2 { get; private set; }

        public double z2 { get; private set; }

    }
}