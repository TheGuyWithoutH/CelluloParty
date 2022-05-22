using System;
using System.Reflection;
using UnityEngine;

namespace Game.Map
{
    public enum GameCell
    {
        //Example pour la première cellule, à compléter
        [CellAttribute(5.9, -8.35, false, 0, 0)] Cell1,
        [CellAttribute(5.9, -9.3, false, 0, 0)] Cell2,
        [CellAttribute(4.8, -9.3, false, 0, 0)] Cell3,
        [CellAttribute(3.8, -9.3, false, 0, 0)] Cell4,
        [CellAttribute(2.8, -9.3, false, 0, 0)] Cell5,
        [CellAttribute(1.8, -9.3, false, 0, 0)] Cell6,
        [CellAttribute(0.75, -9.3, false, 0, 0)] Cell7,
        [CellAttribute(0.75, -8.4, false, 0, 0)] Cell8,
        [CellAttribute(0.75, -7.4, false, 0, 0)] CellRiver,
        [CellAttribute(0.75, -6.5, false, 0, 0)] Cell9,
        [CellAttribute(0.75, -5.5, false, 0, 0)] Cell10,
        [CellAttribute(0.75, -4.6, false, 0, 0)] Cell11,
        [CellAttribute(0.75, -3.6, false, 0, 0)] Cell12,
        [CellAttribute(0.75, -2.6, false, 0, 0)] Cell13,
        [CellAttribute(0.75, -1.7, false, 0, 0)] Cell14,
        [CellAttribute(0.75, -0.8, false, 0, 0)] Cell15,
        [CellAttribute(1.8, -0.8, false, 0, 0)] Cell16,
        [CellAttribute(2.8, -0.8, false, 0, 0)] Cell17,
        [CellAttribute(3.85, -0.8, false, 0, 0)] Cell18,
        [CellAttribute(5, -0.8, false, 0, 0)] Cell19,
        [CellAttribute(6.1, -0.8, false, 0, 0)] Cell20,
        [CellAttribute(7.15, -0.8, false, 0, 0)] Cell21,
        [CellAttribute(8.25, -0.8, false, 0, 0)] Cell22,
        [CellAttribute(9.3, -0.8, false, 0, 0)] Cell23,
        [CellAttribute(10.35, -0.8, false, 0, 0)] Cell24,
        [CellAttribute(11.35, -0.8, false, 0, 0)] CellVolcano,
        [CellAttribute(12.4, -0.8, false, 0, 0)] Cell25,
        [CellAttribute(13.4, -0.8, false, 0, 0)] Cell26,
        [CellAttribute(13.4, -1.75, false, 0, 0)] Cell27,
        [CellAttribute(13.4, -2.75, false, 0, 0)] Cell28,
        [CellAttribute(13.4, -3.7, false, 0, 0)] Cell29,
        [CellAttribute(13.4, -4.65, false, 0, 0)] Cell30,
        [CellAttribute(13.4, -5.55, false, 0, 0)] Cell31,
        [CellAttribute(13.4, -6.45, false, 0, 0)] CellPlane,
        [CellAttribute(13.4, -7.45, false, 0, 0)] Cell32,
        [CellAttribute(13.4, -8.35, false, 0, 0)] Cell33,
        [CellAttribute(13.4, -9.3, false, 0, 0)] Cell34,
        [CellAttribute(12.4, -9.3, false, 0, 0)] Cell35,
        [CellAttribute(11.35, -9.3, false, 0, 0)] Cell36,
        [CellAttribute(10.3, -9.3, false, 0, 0)] Cell37,
        [CellAttribute(9.3, -9.3, false, 0, 0)] Cell38,
        [CellAttribute(8.3, -9.3, false, 0, 0)] Cell39,
        [CellAttribute(8.3, -8.35, false, 0, 0)] Cell40,
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

        public static bool GetCellOccupied(this GameCell cell)
        {
            return GetAttr(cell).occup;
        }

        public static void SetCellOccupied(this GameCell cell, bool occup)
        {
            CellAttribute attr = GetAttr(cell);
            attr.occup = occup;
        }
        
        private static CellAttribute GetAttr(GameCell p)
        {
            return (CellAttribute)Attribute.GetCustomAttribute(ForValue(p), typeof(GameCell));
        }

        private static MemberInfo ForValue(GameCell p)
        {
            return typeof(GameCell).GetField(Enum.GetName(typeof(GameCell), p));
        }
    }

    class CellAttribute : Attribute
    {
        internal CellAttribute(double x, double z, bool occup, double x2, double z2)
        {
            this.x = x;
            this.z = z;
            this.occup = occup;
            this.x2 = x2;
            this.z2 = z2;

        }
        public double x { get; private set; }
        public double z { get; private set; }

        public bool occup { get; set; }

        public double x2 { get; private set; }

        public double z2 { get; private set; }

    }
}