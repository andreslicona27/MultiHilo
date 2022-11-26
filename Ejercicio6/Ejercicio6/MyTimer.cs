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

        private static readonly object l = new object();
        private static bool isRunning = false;
        public int interval;

        // BUILDER 
        public MyTimer(MyDelegate function)
        {
            this.function = function;
            thread = new Thread(MainRun);
            thread.IsBackground = true;
            thread.Start();

        }

        // FUNCTIONS
        public void MainRun()
        {

            lock (l)
            {
                if (!isRunning)
                {
                    Monitor.Wait(l);
                }
            }


            while (isRunning)
            {
                function.Invoke();
                Thread.Sleep(interval);
            }
            MainRun();
        }

        public void run()
        {
            while (!isRunning)
            {
                lock (l)
                {
                    if (!isRunning)
                    {
                        isRunning = true;
                        Monitor.Pulse(l);
                    }
                }


            }

        }

        public void pause()
        {
            while (isRunning)
            {
                lock (l)
                {
                    if (isRunning)
                    {
                        isRunning = false;
                    }
                }

            }
        }


    }
}
