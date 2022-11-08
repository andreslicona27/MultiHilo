using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio5
{
    internal class Horse
    {
        static readonly object l = new object();
        static bool finished = false;
        private int position;
        public int Position
        {
            set { position = value; }
            get { return position; }
        }

        public void Run()
        {
            while (!finished)
            {
                lock (l)
                {
                    if (!finished)
                    {
                        for (int i = 0; i < 40; i++)
                        {
                            Console.WriteLine("*");
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
