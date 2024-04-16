using UnityEngine;

public static class GeometryExtensions
{
    public static RectInt ExtendRect(this RectInt rect, int value)
    {
        return new RectInt(rect.xMin - value, rect.yMin - value, rect.width + value * 2, rect.height + value * 2);
    }
}
