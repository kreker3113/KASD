using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace KASD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string file1 = "C:\\Users\\Uzer\\Documents\\file.txt";
            try
            {
                StreamReader streamReader = new StreamReader(file1);
                uint dimension = uint.Parse(streamReader.ReadLine());
                double[][] tensorMatrix = new double[dimension][];
                
                
                for (int i = 0; i < dimension; i++)
                {  
                    tensorMatrix[i] = streamReader.ReadLine().Split(' ').Select(x => double.Parse(x)).ToArray();
                }
                if (Symmetry(tensorMatrix))
                {
                    double[][] vector = new double[1][];
                    vector[0] = streamReader.ReadLine().Split(' ').Select(x => double.Parse(x)).ToArray();
                    VectorLench(dimension, vector, tensorMatrix);
                  
                }
                else
                {
                    Console.WriteLine("матрица не является симетричной");
                    streamReader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex}");
            }
            Thread.Sleep(10000);
        }
        public static bool Symmetry(double[][] tensorMatrix)
        {
            for(int i = 0;i < tensorMatrix.Length;i++)
            {
                for (int j = 0; j < tensorMatrix[i].Length; j++)
                {
                    if (tensorMatrix[i][j] != tensorMatrix[j][i]) return false;
                }  
            }
            return true;
        }
        public static void VectorLench(uint dimension, double[][] vector, double[][] tensorMatrix)
        {
            double[] mas = new double[dimension];
            double res = 0;
            for (int i = 0; i < tensorMatrix.Length; i++)
            {
                mas[i] = vector[0][i] * tensorMatrix[i][i];
            }
            double[][] Tvector = new double[dimension][]; 
            for (int i = 0; i < dimension; i++)
            {
                Tvector[i] = new double[1];
            }
            for (int i = 0; i < dimension; i++)
            {
                Tvector[i][0] = vector[0][i];
            }
            for(int i = 0;i < dimension; i++)
            {
                res += mas[i] * Tvector[i][0];
            }
            res = Math.Sqrt(res);
            Console.WriteLine($"Длина вектора {res}");
            
        }
    }
}
