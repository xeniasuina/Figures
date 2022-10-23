using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Numerics;
using Figure.Enums;

namespace Figure.Figures;

public sealed class Rhombus : Figure
{
    public Rhombus(List<PointF> points, List<Vector2> sides)
    {
        ValidatePoints(points);
        ValidateSides(sides);

        _figureType = FigureType.Rhombus;
        _points.AddRange(points);
        _sides.AddRange(sides);

        _perimeter = CalculatePerimeter();
        _area = CalculateArea();
    }

    private protected override double CalculateArea()
    {
        var (firstDiagonal, secondDiagonal) = FindDiagonals();
        
        return 0.5 * firstDiagonal.Length() * secondDiagonal.Length();
    }

    private protected override double CalculatePerimeter() => _sides.Sum(x => x.Length());
}