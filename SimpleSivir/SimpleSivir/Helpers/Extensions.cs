using System;
using System.Collections.Generic;
using SharpDX;

namespace SimpleSivir.Helpers
{
    internal static class Extensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (action == null) throw new ArgumentNullException("action");

            foreach (var item in source)
            {
                action(item);
            }
        }


        public static bool PointOnLineSegment(this Vector3 CheckPoint, Vector3 Start, Vector3 End)
        {
            return (Start.X <= CheckPoint.X && CheckPoint.X <= End.X || End.X <= CheckPoint.X && CheckPoint.X <= Start.X) &&
                   (Start.Y <= CheckPoint.Y && CheckPoint.Y <= End.Y || End.Y <= CheckPoint.Y && CheckPoint.Y <= Start.Y);
        }
    }
}