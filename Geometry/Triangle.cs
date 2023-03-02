using System.Collections.ObjectModel;

namespace Geometry
{
    public class Triangle : Shape, IEquatable<Triangle>
    {
        public ReadOnlyCollection<double> Sides { get; private init; }
        public bool IsRightAngled { get => CheckIfIsRightAngled(); }

        //для случаев, когда бросание исключения нежелательно
        public static bool TryCreate(double sideA, double sideB, double sideC, out Triangle triangle)
        {
            triangle = null;

            if (!CheckIfSideIsValid(sideA))
            {
                return false;
            }

            if (!CheckIfSideIsValid(sideB))
            {
                return false;
            }

            if (!CheckIfSideIsValid(sideC))
            {
                return false;
            }

            if (!CheckIfTriangleIsValid(sideA, sideB, sideC))
            {
                return false;
            }

            triangle = new Triangle() { Sides = SidesToSortedCollection(sideA, sideB, sideC) };
            return true;
        }

        public Triangle(double sideA, double sideB, double sideC) : this()
        {
            ThrowOnInvalidSides(sideA, sideB, sideC);
            ThrowOnInvalidTriangle(sideA, sideB, sideC);


            Sides = SidesToSortedCollection(sideA, sideB, sideC);

        }


        private Triangle()
        {
        }

        public override double GetArea()
        {
            //можно было бы для прямоугольного треугольника считать площадь через основание * высота * 0.5,
            //но заметной разницы при расчете площади в цикле на 1000000 треугольников заметной разницы не было

            /*
             * Формула Герона: area = s(s-a)(s-b)(s-c)) ^ (1/2), где
             * s это половина периметра,
             * a,b,c стороны треугольника
             * 
             */

            double s = (Sides[0] + Sides[1] + Sides[2]) / 2d;
            double area = Math.Sqrt(s * (s - Sides[0]) * (s - Sides[1]) * (s - Sides[2]));
            return area;
        }

        public override bool Equals(object obj)
        {
            return obj is Triangle && Equals((Triangle)obj);
        }

        public bool Equals(Triangle other)
        {
            if (other == null)
            {
                return false;
            }

            const int SIDES_COUNT = 3; //у ReadOnlyCollection нет Length

            for (int i = 0; i < SIDES_COUNT; i++)
            {
                if (Sides[i] != other.Sides[i])
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Sides[0], Sides[1], Sides[2]);
        }

        private static bool CheckIfSideIsValid(double side)
        {
            return double.IsNormal(side) && side > 0d;
        }

        private static bool CheckIfTriangleIsValid(double sideA, double sideB, double sideC)
        {
            return (sideA < sideB + sideC) &&
                   (sideB < sideA + sideC) &&
                   (sideC < sideA + sideB);
        }

        private void ThrowOnInvalidSides(double sideA, double sideB, double sideC)
        {
            if (!CheckIfSideIsValid(sideA))
            {
                throw new ArgumentOutOfRangeException(nameof(sideA));
            }

            if (!CheckIfSideIsValid(sideB))
            {
                throw new ArgumentOutOfRangeException(nameof(sideB));
            }

            if (!CheckIfSideIsValid(sideC))
            {
                throw new ArgumentOutOfRangeException(nameof(sideC));
            }
        }

        private bool CheckIfIsRightAngled()
        {
            double hypotenuse;
            double cathetus1;
            double cathetus2;


            hypotenuse = Math.Pow(Sides[2], 2);
            cathetus1 = Math.Pow(Sides[1], 2);
            cathetus2 = Math.Pow(Sides[0], 2);

            return hypotenuse.EqualWithPrecision(cathetus1 + cathetus2);
        }

        private void ThrowOnInvalidTriangle(double sideA, double sideB, double sideC)
        {
            if (!CheckIfTriangleIsValid(sideA, sideB, sideC))
            {
                throw new ArgumentException("Triangle is invalid");
            }
        }

        private static ReadOnlyCollection<double> SidesToSortedCollection(double sideA, double sideB, double sideC)
        {
            double[] inputSides = new double[] { sideA, sideB, sideC };
            for (int i = 0; i < inputSides.Length; i++)
            {
                inputSides[i] = Math.Round(inputSides[i], Calculator.PRECISION);
            }

            Array.Sort(inputSides);

            return new ReadOnlyCollection<double>(inputSides);
        }

    }
}