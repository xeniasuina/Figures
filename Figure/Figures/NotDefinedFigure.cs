using System.Drawing;
using System.Numerics;
using Figure.Enums;

namespace Figure.Figures;

public sealed class NotDefinedFigure : Figure
{
    public NotDefinedFigure(List<PointF> points, List<Vector2> sides)
    {
        ValidateSides(sides);
        _figureType = FigureType.NotDefined;

        _points.AddRange(points);
        _sides.AddRange(sides);

        _perimeter = CalculatePerimeter();
        _area = CalculateArea();
    }
    
    
    /// <summary>
    /// Формула площади Гаусса.
    /// </summary>
    /// <returns></returns>
    private protected override double CalculateArea()
    {
        var firstPart = _points[^1].X * _points[0].Y;
        var secondPart = _points[0].X * _points[^1].Y;

        for (var i = 0; i < _points.Count - 1; i += 1)
        {
            firstPart += _points[i].X * _points[i + 1].Y;
            secondPart += _points[i + 1].X * _points[i].Y;
        }

        return 0.5 * Math.Abs(firstPart - secondPart);
    }

    private protected override double CalculatePerimeter() => _sides.Sum(x => x.Length());
}