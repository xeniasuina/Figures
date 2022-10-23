using System.Drawing;
using Figure.Figures;

var recPoints = new List<PointF>()
{
    new(1.0f, 1.0f),
    new(1.0f, 3.0f),
    new(4.0f, 3.0f),
    new(4.0f, 1.0f)
};

var sqPoints = new List<PointF>
{
    new(3.0f, 2.0f),
    new(3.0f, 4.0f),
    new(5.0f, 4.0f),
    new(5.0f, 2.0f)
};

var rhPoints = new List<PointF>
{
    new(1.0f, 0.0f),
    new(-1.0f, 3.0f),
    new(1.0f, 6.0f),
    new(3.0f, 3.0f)
};

var trPoints = new List<PointF>
{
    new(0.0f, 0.0f),
    new(1.0f, 2.0f),
    new(4.0f, 2.0f),
    new(6.0f, 0.0f)
};

var prPoints = new List<PointF>
{
    new(0.0f, 0.0f),
    new(1.0f, 2.0f),
    new(4.0f, 2.0f),
    new(3.0f, 0.0f)
};

var ndPoints = new List<PointF>
{
    new(0.0f, 0.0f),
    new(1.0f, 2.0f),
    new(4.0f, 2.0f),
    new(0.0f, 6.0f)
};

var factory = new FigureFactory();

var f1 = factory.CreateFigure(recPoints);
Console.WriteLine($"{nameof(f1)}: {f1}");

var f2 = factory.CreateFigure(sqPoints);
Console.WriteLine($"{nameof(f2)}: {f2}");

var f3 = factory.CreateFigure(rhPoints);
Console.WriteLine($"{nameof(f3)}: {f3}");

var f4 = factory.CreateFigure(trPoints);
Console.WriteLine($"{nameof(f4)}: {f4}");

var f5 = factory.CreateFigure(prPoints);
Console.WriteLine($"{nameof(f5)}: {f5}");

var f6 = factory.CreateFigure(ndPoints);
Console.WriteLine($"{nameof(f6)}: {f6}");

Console.WriteLine($"f2 contained in f1: {f2.IsContainedIn(f1)}, f2 intersect with f1: {f2.IsIntersect(f1)}");