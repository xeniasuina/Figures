using System.Drawing;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using Figure.Enums;
using Figure.Extensions;

namespace Figure.Figures;

public class FigureFactory
{
    /// <summary>
    /// Порядок обхода - A B C D.
    /// </summary>
    private readonly List<PointF> _points = new();

    /// <summary>
    /// Порядок обхода - AB, BC, CD, DA.
    /// </summary>
    private readonly List<Vector2> _sides = new();

    /// <summary>
    /// Углы при сторонах, порядок - Angle(A), Angle(B), Angle(C), Angle(D).
    /// </summary>
    private readonly List<double> _angles = new();

    private void ChangePoints(List<PointF> points)
    {
        Figure.ValidatePoints(points);
        
        _points.Clear();
        _points.AddRange(points);
        FillSides();
        FillAngles();
    }
    

    public Figure CreateFigure(List<PointF> points)
    {
        ChangePoints(points);
        var figureType = GetFigureType();
        

        return figureType switch
        {
            FigureType.Rectangle => new Rectangle(_points, _sides),
            FigureType.Square => new Square(_points, _sides),
            FigureType.Rhombus => new Rhombus(_points, _sides),
            FigureType.Parallelogram => new Parallelogram(_points, _sides),
            FigureType.Trapezium => new Trapezium(_points, _sides),
            FigureType.NotDefined => new NotDefinedFigure(_points, _sides),
            _ => throw new Exception("Unreachable code")
        };
    }
    
    private void FillSides()
    {
        _sides.Clear();

        var pointsToVec = _points.Select(x => new Vector2(x.X, x.Y)).ToList();

        for (var i = 0; i < pointsToVec.Count; i += 1)
        {
            var j = i == 0 ? pointsToVec.Count - 1 : i - 1;

            var side = pointsToVec[i] - pointsToVec[j];
            _sides.Add(side);
        }
    }

    private void FillAngles()
    {
        if (_sides.Count == 0)
            return;
        
        _angles.Clear();

        for (var i = 0; i < _sides.Count; i += 1)
        {
            var j = i == 0 ? 3 : i - 1;
            var firstSide = _sides[j];
            var secondSide = _sides[i];
            
            _angles.Add(firstSide.Angle(secondSide));
        }
    }
    
    private FigureType GetFigureType()
    {
        var isSquare = IsSquare();
        var isParallelogram = IsParallelogram();
        var isRectangle = IsRectangle();
        var isRhombus = IsRhombus();
        var isTrapezium = IsTrapezium();

        if (isParallelogram)
        {
            if (isSquare)
                return FigureType.Square;
            if (isRectangle)
                return FigureType.Rectangle;
            if (isRhombus)
                return FigureType.Rhombus;

            return FigureType.Parallelogram;
        }

        if (isTrapezium)
            return FigureType.Trapezium;

        return FigureType.NotDefined;
    }

    private bool IsTrapezium()
    {
        var linearCount = 0;
        for (var i = 0; i < _sides.Count; i += 1)
        {
            for (var j = 0; j < _sides.Count; j += 1)
            {
                if (i == j)
                    continue;

                var dot = Vector2.Dot(_sides[i], _sides[j]);

                if (Math.Abs(Math.Abs(dot) - _sides[i].Length() * _sides[j].Length()) < PointExtensions.Epsilon)
                    linearCount += 1;
            }
        }

        return linearCount == 2;
    }

    private bool IsRhombus()
    {
        var eps = PointExtensions.Epsilon;
        
        var sidesEquality = _sides.All(x => Math.Abs(x.Length() - _sides[0].Length()) < eps);
        
        var firstDiagonal = _points[2].ToVector2() - _points[0].ToVector2();
        var secondDiagonal = _points[3].ToVector2() - _points[1].ToVector2();
        var angleBetweenDiagonals = firstDiagonal.Angle(secondDiagonal);

        for (var i = 0; i < _points.Count; i += 1)
        {
            var diagonal = i % 2 == 0 ? firstDiagonal : secondDiagonal;

            var j = i == 0 ? _points.Count - 1 : i - 1;
            var firstBisector = _sides[j].Angle(diagonal);
            var secondBisector = _sides[i].Angle(diagonal);

            if (Math.Abs(firstBisector - secondBisector) > eps)
                return false;
        }

        return sidesEquality && Math.Abs(angleBetweenDiagonals - 90) < eps;
    }

    private bool IsRectangle()
    {
        var eps = PointExtensions.Epsilon;

        var sidesEquality = Math.Abs(_sides[0].Length() - _sides[2].Length()) < eps
                            && Math.Abs(_sides[1].Length() - _sides[3].Length()) < eps;
        var allAnglesAreRight = _angles.All(x => Math.Abs(x - 90) < eps);

        var firstDiagonal = _points[2].ToVector2() - _points[0].ToVector2();
        var secondDiagonal = _points[3].ToVector2() - _points[1].ToVector2();
        var diagonalEquality = Math.Abs(firstDiagonal.Length() - secondDiagonal.Length()) < eps;

        return sidesEquality && allAnglesAreRight && diagonalEquality;
    }

    private bool IsParallelogram()
    {
        var eps = PointExtensions.Epsilon;
        
        var sidesEquality = Math.Abs(_sides[0].Length() - _sides[2].Length()) < eps
                            && Math.Abs(_sides[1].Length() - _sides[3].Length()) < eps;
        
        var anglesEquality = Math.Abs(_angles[0] - _angles[2]) < PointExtensions.Epsilon 
                             && Math.Abs(_angles[1] - _angles[3]) < PointExtensions.Epsilon;

        var anglesSideEquality = Math.Abs((int) (_angles[0] + _angles[1] - 180)) < eps
                                 && Math.Abs((int) (_angles[1] + _angles[2] - 180)) < eps
                                 && Math.Abs((int) (_angles[2] + _angles[3] - 180)) < eps
                                 && Math.Abs((int) (_angles[3] + _angles[0] - 180)) < eps;

        return sidesEquality && anglesEquality && anglesSideEquality;
    }

    private bool IsSquare()
    {
        var eps = PointExtensions.Epsilon;

        var allSideEquality = _sides.All(x => Math.Abs(x.Length() - _sides[0].Length()) < eps);
        var allAnglesRight = _angles.All(x => Math.Abs(x - 90) < eps);

        return allSideEquality && allAnglesRight;
    }
    
}