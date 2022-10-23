using System.Drawing;
using System.Numerics;
using Figure.Enums;

namespace Figure.Figures;

public sealed class Square : Figure
{ 
    public Square(List<PointF> points, List<Vector2> sides)
    {
        ValidatePoints(points);
        ValidateSides(sides);

        _figureType = FigureType.Square;
        _points.AddRange(points);
        _sides.AddRange(sides);

        _perimeter = CalculatePerimeter();
        _area = CalculateArea();
    }
    
    private protected override double CalculateArea() => Math.Pow(_sides[0].Length(), 2);
    

    private protected override double CalculatePerimeter() => 4 * _sides[0].Length();
    
}