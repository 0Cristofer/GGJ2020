using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
    private static readonly Vector2 TranslationVector = new Vector2(7f, 4f);
    private const float XFactor = 1.111f;

    public static Vector2 ToGridPos(Vector2 pos)
    {
        Vector2 newPos = pos + TranslationVector;
        newPos.x /= XFactor;

        newPos.x = (int) Math.Floor(newPos.x);
        newPos.y = (int) Math.Floor(newPos.y);

        return newPos;
    }
}