namespace Geometry
{
    public record Triangle : Shape
    {
        public double SideA { get; init; }
        public double SideB { get; init; }
        public double SideC { get; init; }

        private Lazy<bool> _isRightAngled;

        //для случаев, когда бросание исключения нежелательно
        public static bool TryCreate(double sideA, double sideB, double sideC, out Triangle triangle)
        {
            triangle = null;

            // можно было для красоты передавать стороны массивом и
            // проверять их в цикле, но это добавит лишнюю нагрузку на память и цпу
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

            triangle = new Triangle() { SideA = sideA, SideB = sideB, SideC = sideC };
            return true;
        }

        public Triangle(double sideA, double sideB, double sideC) : this()
        {
            ThrowOnInvalidSides(sideA, sideB, sideC);
            ThrowOnInvalidTriangle(sideA, sideB, sideC);

            SideA = sideA;
            SideB = sideB;
            SideC = sideC;
        }

        private Triangle()
        {
            _isRightAngled = new Lazy<bool>(() => CheckIfIsRightAngled(), true);
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

            double s = (SideA + SideB + SideC) / 2d;
            double area = Math.Sqrt(s * (s - SideA) * (s - SideB) * (s - SideC));
            return area;
        }

        // можно было сделать через свойство,но тогда бы оно учавствовало в sequential equality этого record'а по умолчанию
        // в этом примере сделал методом, чтобы не переписывать Equals(), GetHashCode() и ToString()
        public bool CheckIfRightAngled()
        {
            return _isRightAngled.Value;
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

            DistinguishHypotenuse(out hypotenuse, out cathetus1, out cathetus2);

            hypotenuse = Math.Pow(hypotenuse, 2);
            cathetus1 = Math.Pow(cathetus1, 2);
            cathetus2 = Math.Pow(cathetus2, 2);

            return hypotenuse.EqualWithPrecision(cathetus1 + cathetus2);
        }

        private void DistinguishHypotenuse(out double hypotenuse, out double cathetus1, out double side3)
        {
            //для красоты можно было отсортировать массив сторон и выбрать гипотенузу, но это медленнее
            if (SideA > SideB && SideA > SideC)
            {
                hypotenuse = SideA;
                cathetus1 = SideB;
                side3 = SideC;
            }
            else if (SideB > SideA && SideB > SideC)
            {
                hypotenuse = SideB;
                cathetus1 = SideA;
                side3 = SideC;
            }
            else
            {
                hypotenuse = SideC;
                cathetus1 = SideA;
                side3 = SideB;
            }
        }


        private void ThrowOnInvalidTriangle(double sideA, double sideB, double sideC)
        {
            if (!CheckIfTriangleIsValid(sideA, sideB, sideC))
            {
                throw new ArgumentException("Triangle is invalid");
            }
        }

    }
}