using System;
using UnityEngine;

public static class GeometryExtensions
{
    public static RectInt ExtendRect(this RectInt rect, int value)
    {
        return new RectInt(rect.xMin - value, rect.yMin - value, rect.width + value * 2, rect.height + value * 2);
    }

    public static void Iterate(this RectInt rect, Action<Vector2Int> action)
    {
        for (int y = rect.y; y < rect.y + rect.height; y++)
        {
            for (int x = rect.x; x < rect.x + rect.width; x++)
            {
                action(new Vector2Int(x, y));
            }
        }
    }

    public static RectInt RectFromCenter(this Vector2Int center, int radius)
    {
        return new RectInt(center.x - radius, center.y - radius, radius * 2 + 1, radius * 2 + 1);
    }
}
