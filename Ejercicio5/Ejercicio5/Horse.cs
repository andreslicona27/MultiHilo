using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio5
{
    internal class Horse
    {
        static readonly object l = new object();
        static bool finished = false;
        private int position;
        private string name;
        public int Position
        {
            set { this.position = value; }
            get { return position; }
        }
        public string Name
        {
            set { this.name = value; }
            get { return name; }
        }

        public void Run()
        {
            while (!finished)
            {
                lock (l)
                {
                    if (!finished)
                    {
                            var random = new Random();
                            position = random.Next(0,40);
                            //Console.WriteLine("[" + name + "]Ditancia  = " + position + " m");
                            Console.Write("[" + name + "]");
                        for (int i = 0; i < position; i++)
                        {
                            Console.Write("*");
                           
                        }
                        if (position == 40)
                        {
                            finished = true;
                        }


                    }
                }
            }
        }
    }
}
