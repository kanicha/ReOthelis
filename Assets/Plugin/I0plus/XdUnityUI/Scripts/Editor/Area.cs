﻿using UnityEngine;

namespace I0plus.XdUnityUI.Editor
{
    public class Area
    {
        public Area()
        {
            Empty = true;
        }

        public Area(Vector2 min, Vector2 max)
        {
            Min = min;
            Max = max;
            Empty = false;
        }

        public bool Empty { get; private set; }
        public Vector2 Min { get; private set; }
        public Vector2 Max { get; private set; }

        public Vector2 Avg => (Min + Max) / 2.0f;

        public Vector2 Center => (Min + Max) / 2.0f;

        public float Width => Mathf.Abs(Max.x - Min.x);

        public float Height => Mathf.Abs(Max.y - Min.y);

        public Vector2 Size => new Vector2(Width, Height);

        public static Area FromPositionAndSize(Vector2 position, Vector2 size)
        {
            return new Area(position, position + size);
        }

        public static Area None()
        {
            return new Area();
        }

        public void Merge(Area other)
        {
            if (other == null || other.Empty) return;
            if (Empty)
            {
                Min = other.Min;
                Max = other.Max;
                Empty = false;
                return;
            }

            if (other.Min.x < Min.x) Min = new Vector2(other.Min.x, Min.y);
            if (other.Min.y < Min.y) Min = new Vector2(Min.x, other.Min.y);
            if (other.Max.x > Max.x) Max = new Vector2(other.Max.x, Max.y);
            if (other.Max.y > Max.y) Max = new Vector2(Max.x, other.Max.y);
        }
    }
}