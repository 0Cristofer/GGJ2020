using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Util
{
    public static class EnumUtil
    {
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        public static string GetEnumValueName<T>(T value)
        {
            return Enum.GetName(typeof(T), value);
        }
    }

    public static class WorldUtil
    {
        private static readonly Vector2 TranslationVector = new Vector2(8f, 3f);
        private const float XFactor = 1.111f;

        public static Vector2 ToGridPos(Vector2 pos)
        {
            Vector2 newPos = pos + TranslationVector;
            newPos.x /= XFactor;

            newPos.x = (int) Math.Round(newPos.x);
            newPos.y = (int) Math.Round(newPos.y);

            return newPos;
        }

        public static Vector2 ToWorldPos(Vector2 pos)
        {
            pos.x *= XFactor;
            Vector2 newPos = pos - TranslationVector;

            return newPos;
        }
    }
}