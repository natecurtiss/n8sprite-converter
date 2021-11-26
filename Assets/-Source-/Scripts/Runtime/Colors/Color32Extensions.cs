using System.Collections.Generic;
using UnityEngine;

namespace N8SpriteConverter.Colors
{
    static class Color32Extensions
    {
        public static bool IsEqualTo(this Color32 first, Color32 second) =>
            first.r == second.r && first.g == second.g && first.b == second.b && first.a == second.a;

        public static Color32 FindClosestColor(this Color32 color, IEnumerable<Color32> colorsToCompareTo)
        {
            var closestColor = new Color32();
            var closestDistance = Mathf.Infinity;
            foreach (var colorToCompareTo in colorsToCompareTo)
            {
                var distance = Vector3.Distance(color.AsVector3(), colorToCompareTo.AsVector3());
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestColor = colorToCompareTo;
                }
            }
            return closestColor;
        }

        static Vector3 AsVector3(this Color32 color) => new Vector3(color.r, color.g, color.b);
    }
}