using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Numerics;
using Figure.Enums;
using Figure.Extensions;

namespace Figure.Figures;

public sealed class Parallelogram : Figure
{
    public Parallelogram(List<PointF> points, List<Vector2> sides)
    {
        ValidatePoints(points);
        ValidateSides(sides);
        
        _figureType = FigureType.Parallelogram;
        _points.AddRange(points);
        _sides.AddRange(sides);

        _perimeter = CalculatePerimeter();
        _area = CalculateArea();
    }

    private protected override double CalculateArea()
    {
        var angle = _sides[0].Angle(_sides[1]);

        return Math.Abs(_sides[0].Length() * _sides[1].Length() * Math.Sin(angle));
    }

    private protected override double CalculatePerimeter() => _sides.Sum(x => x.Length());
}