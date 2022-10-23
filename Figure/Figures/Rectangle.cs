using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;
using Figure.Enums;
using Figure.Extensions;

namespace Figure.Figures;

public sealed class Rectangle : Figure
{
    public Rectangle(List<PointF> points, List<Vector2> sides)
    {
        ValidatePoints(points);
        ValidateSides(sides);
        
        _figureType = FigureType.Rectangle;
        _points.AddRange(points);
        _sides.AddRange(sides);

        _perimeter = CalculatePerimeter();
        _area = CalculateArea();
    }
    
    private protected override double CalculateArea() => _sides[0].Length() * _sides[1].Length();

    private protected override double CalculatePerimeter() => 2 * (_sides[0].Length() + _sides[1].Length());
}