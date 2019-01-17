using System;

namespace frank_wolfe_algoritm
{
    class Program
    {
        static double[] gradient(double[] x)
        {
            double[] result = new double[2];
            result[0] = 5 - 2 * x[0];
            result[1] = 8 - 4 * x[1];
            return result;
        }
        static double lp(double[] x, double[] gra)
        {
            double obj = gra[0] * x[0] + gra[1] * x[1];
            return obj;
        }
        static double[] maxlp(double[] x, double[] gra, double[,] corner)
        {
            double[] max = new double[2];
            max[0] = corner[0, 0];
            max[1] = corner[0, 1];
            for (int i = 1; i < corner.GetLength(0); i++)
            {
                double[] x1 = new double[2];
                x1[0] = corner[i, 0];
                x1[1] = corner[i, 1];
                double[] x2 = new double[2];
                x2[0] = corner[i - 1, 0];
                x2[1] = corner[i - 1, 1];
                if (lp(x1, gra) > lp(x2, gra))
                {
                    max[0] = corner[i, 0];
                    max[1] = corner[i, 1];
                }
            }
            return max;
        }
        static double f_nabla(double[] maxlp, double[] x, double t)
        {
            double result = 5 * (x[0] + t * (maxlp[0] - x[0])) - Math.Pow((x[0] + t * (maxlp[0] - x[0])), 2) + 8 * (x[1] + t * (maxlp[1] - x[1])) - 2 * Math.Pow((x[1] + t * (maxlp[1] - x[1])), 2);
            return result;
        }
        static double der_nabla(double[] maxlp, double[] x, double t)
        {
            double result = (f_nabla(maxlp, x, t + Math.Pow(10, -5)) - f_nabla(maxlp, x, t)) * Math.Pow(10, 5);
            return result;
        }
        static double s_der_nabla(double[] maxlp, double[] x, double t)
        {
            double result = (der_nabla(maxlp, x, t + Math.Pow(10, -5)) - der_nabla(maxlp, x, t)) * Math.Pow(10, 5);
            return result;
        }
        static void Main(string[] args)
        {
            double epsilon = Math.Pow(10, -5);
            double[] gra = new double[2];
            double[] x = new double[2];
            Console.Write("please enter the 1st element of initial point: ");
            x[0] = Convert.ToDouble(Console.ReadLine());
            x[0] = Math.Round(x[0], 5);
            Console.Write("please enter the 2nd element of initial point: ");
            x[1] = Convert.ToDouble(Console.ReadLine());
            x[1] = Math.Round(x[1], 5);
            double[] preX = new double[2];
            double[] xlp = new double[2];
            double[,] corner = { { 0, 0 }, { 0, 3 }, { 2, 0 } };
            double t;
            double count = 1;
            do
            {
                preX[0] = x[0];
                preX[1] = x[1];
                gra = gradient(x);
                xlp = maxlp(x, gra, corner);
                t = 0;
                do
                {
                    if (der_nabla(xlp, x, t) == 0)
                    {
                        break;
                    }
                    else
                    {
                        t = t - (der_nabla(xlp, x, t) / s_der_nabla(xlp, x, t));
                    }
                }
                while (Math.Abs(der_nabla(xlp, x, t)) > epsilon);
                if (t > 1)
                {
                    t = 1;
                }

                double[] max = new double[2];
                max = maxlp(x, gra, corner);
                x[0] = Math.Round((x[0] + t * (max[0] - x[0])), 5);
                x[1] = Math.Round((x[1] + t * (max[1] - x[1])), 5);
                Console.WriteLine("\n\niteration[" + count + "]\nx[" + count + "] = " + "(" + x[0] + ";" + x[1] + ")");
                count++;
                if (count == 512) break; //after 510th step iterations stuck in a loop! 
            }
            while (Math.Sqrt(Math.Pow((x[0] - preX[0]), 2) + Math.Pow((x[1] - preX[1]), 2)) > epsilon);
            Console.ReadKey();
        }
    }
}
