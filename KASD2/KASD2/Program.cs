using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASD2
{
    using System;

    struct ComplexNumber
    {
        public double RealPart;
        public double ImaginaryPart;

        public ComplexNumber(double real, double imaginary)
        {
            RealPart = real;
            ImaginaryPart = imaginary;
        }

        // Методы для операций с комплексными числами

        public ComplexNumber Add(ComplexNumber other)
        {
            return new ComplexNumber(RealPart + other.RealPart, ImaginaryPart + other.ImaginaryPart);
        }

        public ComplexNumber Subtract(ComplexNumber other)
        {
            return new ComplexNumber(RealPart - other.RealPart, ImaginaryPart - other.ImaginaryPart);
        }

        public ComplexNumber Multiply(ComplexNumber other)
        {
            return new ComplexNumber(RealPart * other.RealPart - ImaginaryPart * other.ImaginaryPart,
                                    RealPart * other.ImaginaryPart + ImaginaryPart * other.RealPart);
        }

        public ComplexNumber Divide(ComplexNumber divisor)
        {
            double denominator = divisor.RealPart * divisor.RealPart + divisor.ImaginaryPart * divisor.ImaginaryPart;
            if (denominator != 0)
                return new ComplexNumber((RealPart * divisor.RealPart + ImaginaryPart * divisor.ImaginaryPart) / denominator,
                                        (ImaginaryPart * divisor.RealPart - RealPart * divisor.ImaginaryPart) / denominator);
            else
                throw new DivideByZeroException("Деление на ноль недопустимо для комплексных чисел");
        }

        // Методы для вывода значений и вычисления модуля и аргумента

        public double Magnitude()
        {
            return Math.Sqrt(RealPart * RealPart + ImaginaryPart * ImaginaryPart);
        }

        public double Argument()
        {
            double rad = Math.Atan2(ImaginaryPart, RealPart);
            return rad < 0 ? rad + 2 * Math.PI : rad;
        }

        public override string ToString()
        {
            return $"{RealPart + " + " + ImaginaryPart}";
        }
    }
    internal class Program
    {
        private static readonly ComplexNumber DefaultComplexNumber = new ComplexNumber(0, 0);
        static void Main(string[] args)
        {
            ComplexNumber defaultComplex = new ComplexNumber(0, 0);
            Console.WriteLine("Программа для комплексных чисел:");

            while (true)
            {
                Console.Write("\nВведите одну из команд [Создать, Сложить, ВЫЧЕСТЬ, УМНОЖИТЬ, РАЗДЕЛИТЬ , МОДУЛЬ, АРГУМЕНТ]\n" +
                    "(для выхода введите 'Q' или 'q'): ");
                string input = Console.ReadLine().ToUpper();
                
                // Операции с комплексыми числами
                switch (input)
                {
                    case "СОЗДАТЬ":
                        Console.Write("Введите вещественную и мнимую части: ");
                        double real = double.Parse(Console.ReadLine());
                        double imaginary = double.Parse(Console.ReadLine());
                        ComplexNumber newComplex = new ComplexNumber(real, imaginary);
                        Console.WriteLine($"Создано комплексное число: {newComplex}");
                        break;
                    case "СЛОЖИТЬ":
                        Console.Write("Введите число: ");
                        double secondComplexReal = double.Parse(Console.ReadLine());
                        double secondComplexImaginary = double.Parse(Console.ReadLine());
                        Console.Write("Введите число: ");
                        double secondComplexReal2 = double.Parse(Console.ReadLine());
                        double secondComplexImaginary2 = double.Parse(Console.ReadLine());
                        ComplexNumber Complex2 = new ComplexNumber(secondComplexReal2, secondComplexImaginary2);
                        ComplexNumber sum = Complex2.Add(new ComplexNumber(secondComplexReal, secondComplexImaginary));
                        Console.WriteLine($"Сумма: {sum}");
                        break;

                    case "ВЫЧЕСТЬ":
                        Console.Write("Введите число: ");
                        double thirdComplexReal = double.Parse(Console.ReadLine());
                        double thirdComplexImaginary = double.Parse(Console.ReadLine());
                        Console.Write("Введите число: ");
                        double thirdComplexReal2 = double.Parse(Console.ReadLine());
                        double thirdComplexImaginary2 = double.Parse(Console.ReadLine());
                        ComplexNumber Complex3 = new ComplexNumber(thirdComplexReal2, thirdComplexImaginary2);
                        ComplexNumber dif = Complex3.Subtract(new ComplexNumber(thirdComplexReal, thirdComplexImaginary));
                        Console.WriteLine($"Разность: {dif}");
                        break;
                    case "УМНОЖИТЬ":
                        Console.Write("Введите число: ");
                        double fortyComplexReal = double.Parse(Console.ReadLine());
                        double fortyComplexImaginary = double.Parse(Console.ReadLine());
                        Console.Write("Введите число: ");
                        double fortyComplexReal2 = double.Parse(Console.ReadLine());
                        double fortyComplexImaginary2 = double.Parse(Console.ReadLine());
                        ComplexNumber Complex4 = new ComplexNumber(fortyComplexReal2, fortyComplexImaginary2);
                        ComplexNumber com = defaultComplex.Multiply(new ComplexNumber(fortyComplexReal, fortyComplexImaginary));
                        Console.WriteLine($"Произведение: {com}");
                        break;
                    case "РАЗДЕЛИТЬ":
                        Console.Write("Введите число: ");
                        double fiftyComplexReal = double.Parse(Console.ReadLine());
                        double fiftyComplexImaginary = double.Parse(Console.ReadLine());
                        Console.Write("Введите число: ");
                        double fiftyComplexReal2 = double.Parse(Console.ReadLine());
                        double fiftyComplexImaginary2 = double.Parse(Console.ReadLine());
                        ComplexNumber Complex5 = new ComplexNumber(fiftyComplexReal2, fiftyComplexImaginary2);
                        ComplexNumber priv = defaultComplex.Divide(new ComplexNumber(fiftyComplexReal, fiftyComplexImaginary));
                        Console.WriteLine($"Частное: {priv}");
                        break;
                    case "МОДУЛЬ":
                        Console.Write("Введите вещественную и мнимую части: ");
                        double real2 = double.Parse(Console.ReadLine());
                        double imaginary2 = double.Parse(Console.ReadLine());
                        ComplexNumber newComplex2 = new ComplexNumber(real2, imaginary2);
                        Console.WriteLine($"Модуль комплексного числа: {newComplex2.Magnitude()}");
                        break;
                    case "АРГУМЕНТ":
                        Console.Write("Введите вещественную и мнимую части: ");
                        double real3 = double.Parse(Console.ReadLine());
                        double imaginary3 = double.Parse(Console.ReadLine());
                        ComplexNumber newComplex3 = new ComplexNumber(real3, imaginary3);
                        Console.WriteLine($"Аргумент комплексного числа: {newComplex3.Argument()} рад.");
                        break;
                    case "ВЫХОД":
                    case "Выход":
                    case "q":
                    case "Q":
                        return;
                    default:
                        Console.WriteLine("Неизвестная команда. Пожалуйста, попробуйте еще раз.");
                        break;
                }
            }
        }
    }
}
