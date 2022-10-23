using System.Numerics;

namespace Figure.Extensions;

public static class VectorExtensions
{
    public static float Angle(this Vector2 from, Vector2 to) => 
        (float)Math.Acos(Math.Clamp(Vector2.Dot(Vector2.Normalize(from), Vector2.Normalize(to)), -1f, 1f)) * 57.29578f;
    

    public static double PseudoScalar(this Vector2 v1, Vector2 v2) =>
        v1.Length() * v2.Length() * Math.Sin(v1.Angle(v2));

}