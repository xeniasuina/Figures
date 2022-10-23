using System.Drawing;

namespace Figure.Extensions;

public static class PointExtensions
{
    public const double Epsilon = 0.001;

    public static double TriangleArea(PointF p1, PointF p2, PointF p3)
        => 0.5 * Math.Abs((p1.X - p3.X) * (p2.Y - p3.Y) - (p2.X - p3.X) * (p1.Y - p3.Y));

    public static double GetTriangleArea(this PointF a, PointF b, PointF c)
    {
        var abVec = b.ToVector2() - a.ToVector2();
        var acVec = c.ToVector2() - a.ToVector2();
        var angle = abVec.Angle(acVec);

        return 0.5 * acVec.Length() * abVec.Length() * Math.Sin(angle);
    }

    public static bool EpsilonEquals(this PointF p1, PointF other) => 
        Math.Abs(p1.X - other.X) < Epsilon || Math.Abs(p1.Y - other.Y) < Epsilon;

    public static bool IsPointContainedIn(this PointF point, Figures.Figure figure)
    {
        var result = false;
        var j = figure.Points.Count - 1;
        var p = figure.Points;

        for (var i = 0; i < figure.Points.Count; i += 1)
        {
            if ((p[i].Y < point.Y && p[j].Y >= point.Y || p[j].Y < point.Y && p[i].Y >= point.Y) 
                && p[i].X + (point.Y - p[i].Y) / (p[j].Y - p[i].Y) * (p[j].X - p[i].X) < point.X)
                result = !result;
            j = i;
        }

        return result; 
    }
}