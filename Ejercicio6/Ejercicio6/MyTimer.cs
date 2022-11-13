using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio6
{
    public delegate void MyDelegate();

    internal class MyTimer
    {
        Thread thread;
        MyDelegate function;

        static readonly private object l = new object();
        private static bool isRunning = false;
        private static bool isPaused = false;
        public int interval;

        // BUILDER 
        public MyTimer(MyDelegate function)
        {
            this.function = new MyDelegate(function);
        }

        // FUNCTIONS
        public void run()
        {
            while (!isRunning)
            {
                lock (l)
                {
                    if (!isRunning)
                    {
                        isRunning = true;
                        thread = new Thread(printNumbers);
                        thread.Start();
                    }
                    else
                    {
                        isPaused = true;
                        Monitor.Pulse(l);
                    }
                }

            }
        }

        public void pause()
        {
            while (!isPaused)
            {
                lock (l)
                {
                    isPaused = true;
                }
            }
        }

        public void printNumbers()
        {
            while (!isPaused)
            {
                function.Invoke();
                Thread.Sleep(interval);
                lock(l)
                {
                    if(isPaused)
                    {
                        Monitor.Wait(l);
                    }
                }
            }
        }

    }
}
