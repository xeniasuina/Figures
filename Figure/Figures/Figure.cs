using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Numerics;
using System.Text;
using Figure.Enums;
using Figure.Extensions;

namespace Figure.Figures;

public abstract class Figure
{
    private protected FigureType _figureType;
    private protected readonly List<PointF> _points = new();
    private protected readonly List<Vector2> _sides = new();
    private protected double _perimeter;
    private protected double _area;

    public List<PointF> Points => _points;
    
    public FigureType FigureType => _figureType;

    public double Perimeter => _perimeter;

    public double Area => _area;

    protected (Vector2, Vector2) FindDiagonals()
    {
        var firstDiagonal = _points[2].ToVector2() - _points[0].ToVector2();
        var secondDiagonal = _points[3].ToVector2() - _points[1].ToVector2();

        return new ValueTuple<Vector2, Vector2>(firstDiagonal, secondDiagonal);
    }

    public bool IsConvex()
    {
        for (var i = 0; i < _sides.Count; i += 1)
        {
            var otherSide = i == _sides.Count - 1 ? _sides[0] : _sides[i + 1];

            var product = _sides[i].X * otherSide.Y - _sides[i].Y * otherSide.X;

            if (Math.Sign(product) > 0)
            {
                return false;
            }
        }

        return true;
    }

    public bool IsIntersect(Figure otherFigure) => _points.Any(x => x.IsPointContainedIn(otherFigure));

    public bool IsContainedIn(Figure otherFigure) => _points.All(x => x.IsPointContainedIn(otherFigure));

    public static void ValidatePoints(List<PointF> points)
    {
        if (points is not {Count: 4})
            throw new ValidationException("Incorrect points count");
    }

    protected static void ValidateSides(List<Vector2> sides)
    {
        if (sides.Any(x => x.Length() < PointExtensions.Epsilon))
            throw new ValidationException("Incorrect sides");
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        var (firstDiagonal, secondDiagonal) = FindDiagonals();

        sb.Append(
            $"{FigureType}\n\tArea: {Area}\n\tPerimeter: {Perimeter}\n\tIsConvex: {IsConvex()}\n\tDiagonals length: {firstDiagonal.Length()}, {secondDiagonal.Length()}");

        return sb.ToString();
    }

    private bool IsPointContainedIn(PointF point, Figure other)
    {
        var figureArea = _area;
        var trianglesArea = 0.0;

        for (var i = 0; i < other.Points.Count; i += 1)
        {
            var j = i == other.Points.Count - 1 ? 0 : i + 1;

            trianglesArea += point.GetTriangleArea(other.Points[i], other.Points[j]);
        }

        return trianglesArea < figureArea;
    }

    private protected abstract double CalculateArea();

    private protected abstract double CalculatePerimeter();
}