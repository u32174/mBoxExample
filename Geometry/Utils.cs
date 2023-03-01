namespace Geometry
{
    public static class Utils
    {
        ///<summary>
        ///Сравнивает два double с заданной точностью.
        ///Значения считаются равными, если разница между ними меньше 0.00001d.
        ///<remark> сделал эту перегрузку вместо дефолтного значения для параметра precision, чтобы не сломать
        ///binary compatibility. (чтобы дефолтное значение точности не заинлайнилось в вызовах из ссылающихся assembly(</remark>
        ///</summary>
        public static bool EqualWithPrecision(this double a, double b)
        {
            return EqualWithPrecision(a, b, 0.00001d);
        }


        /// <summary>
        ///Сравнивает два double с указанной точностью.
        /// </summary>
        ///<param name="precision">Значения считаются равными, если разница между ними меньше этого параметра.</param>
        public static bool EqualWithPrecision(this double a, double b, double precision)
        {
            return Math.Abs(a - b) < precision;
        }
    }
}