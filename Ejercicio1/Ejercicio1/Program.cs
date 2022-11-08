using System;
using System.Diagnostics;
using System.Reflection.Emit;

namespace Ejercicio1
{
    class Program
    {

        static void Main(string[] args)
        {
            int[] v = { 2, 2, 6, 7, 1, 10, 3 };

            Array.ForEach(v, (item) =>
            {
                Console.ForegroundColor = item >= 5 ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine($"Student grade: {item,3}.");
                Console.ResetColor();
            });
            int res = Array.FindIndex(v, (item) => item >= 5);


            Console.WriteLine("There's one approved");
            Console.WriteLine(Array.Exists(v, item => item > 4));

            Console.WriteLine("The position of the last one approved");
            Console.WriteLine(Array.FindLastIndex(v, item => item >= 5));

            Console.WriteLine("Inverse of all the grades");
            Array.ForEach(v, item => Console.WriteLine(1.0 / item));
            

        }
    }
}