using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using Figure.Enums;
using Figure.Extensions;

namespace Figure.Figures;

public sealed class Trapezium : Figure
{
    public Trapezium(List<PointF> points, List<Vector2> sides)
    {
        ValidatePoints(points);
        ValidateSides(sides);

        _figureType = FigureType.Trapezium;
        _points.AddRange(points);
        _sides.AddRange(sides);

        _perimeter = CalculatePerimeter();
        _area = CalculateArea();
    }

    private protected override double CalculateArea()
    {
        var eps = PointExtensions.Epsilon;
        var firstDotProduct = Vector2.Dot(_sides[0], _sides[2]);

        var (smallBase, greaterBase, lateral1, lateral2) = Math.Abs(firstDotProduct) < eps 
            ? 
            (_sides[0].Length() < _sides[2].Length() ? _sides[0] : _sides[2], 
                _sides[0].Length() > _sides[2].Length() ? _sides[0] : _sides[2],
                _sides[1], _sides[3])
            : 
            (_sides[1].Length() < _sides[3].Length() ? _sides[1] : _sides[3], 
                _sides[1].Length() > _sides[3].Length() ? _sides[1] : _sides[3], 
                _sides[0], _sides[2]);

        var a = greaterBase;
        var b = smallBase;
        var c = lateral1;
        var d = lateral2;

        var h = c.Length() * Math.Sin(c.Angle(a));

        return 0.5 * Math.Abs(h * (a.Length() + b.Length()));
    }

    private protected override double CalculatePerimeter() => _sides.Sum(x => x.Length());
}