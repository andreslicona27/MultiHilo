using System;
using System.IO.IsolatedStorage;
using static System.Collections.Specialized.BitVector32;

namespace Ejercicio4
{
    internal class Program
    {
        static readonly object l = new object();
        static bool finished = false;
        static int cont = 0;

        static void Increase()
        {
            while (!finished)
            {
                lock (l)
                {
                    if(!finished)
                    {
                        cont++;
                        Console.WriteLine("{0} Increase", cont);
                        if (cont == 1000)
                        {
                            finished = true;
                        }
                    }
                }
            }
        }

        static void Decrease()
        {
            while (!finished)
            {
                lock (l)
                {
                    if (!finished)
                    {
                        cont--;
                        Console.WriteLine("{0} Decrease", cont);
                        if (cont == -1000)
                        {
                            finished = true;
                        }
                    }
                }
            }
        }



        static void Main(string[] args)
        {
            Thread thread1 = new Thread(Increase);
            Thread thread2 = new Thread(Decrease);

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

            if (cont == 1000)
            {
                Console.WriteLine("Thread 1 Wins");
            } else
            {
                Console.WriteLine("Thread 2 Wins");
            }
        }


    }
}

