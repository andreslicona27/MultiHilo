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

        private readonly object l = new object();
        private bool isRunning = false;
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
            // El while true es, mientras el hilo sea background entonces que se ejecute el codigo
            // Mientras se este ejecutando la funcion
            while (true)
            {
                lock (l)
                {
                    if (!isRunning)
                    {
                        Monitor.Wait(l);
                    }
                    else
                    {
                        function.Invoke();
                        Thread.Sleep(interval);
                    }
                }
            }
        }

        public void run()
        {
            lock (l)
            {
                isRunning = true;
                Monitor.Pulse(l);
            }
        }

        public void pause()
        {
            lock (l)
            {
                isRunning = false;
            }
        }


    }
}
