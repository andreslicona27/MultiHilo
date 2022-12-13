using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_RabbitVsTurtle
{
    internal class EsopoSimulation
    {
        // PROPERTIES
        private readonly object l = new object();
        Random rand = new Random();

        int turtleSteps = 0;
        int rabbitSteps = 0;
        int finishLine = 50;
        bool raceFinish = false;
        bool sleeping;




        // FUNCITONS
        public string Init()
        {
            // LEER DATO DE UN ARCHIVO BINARIO - faltaría agregar el try catch
            //using (BinaryReader br = new BinaryReader(new FileStream(Environment.GetEnvironmentVariable("userprofile") + "\\meta.bin", FileMode.Open)))
            //{
            //    finishLine = br.ReadInt32();
            //}

            Thread turtle = new Thread(RunTurtleRun);
            Thread rabbit = new Thread(RunAndSleepRabbit);

            turtle.Start();
            rabbit.Start();

            turtle.Join();
            rabbit.Join();

            if (turtleSteps >= finishLine)
            {
                Console.Write("\nAnd the all times winner is the leader of the turtle teen mutants ninja turtles, LEONARDO!!!");
                return "Leonardo";
            }
            else
            {
                Console.Write("\nAnd the all times winner is the charismatic and humoristic, BUGS BUNNY!!!");
                return "Bugs Bunny";
            }

        }
        public void RunTurtleRun()
        {
            while (!raceFinish)
            {
                lock (l)
                {
                    if (!raceFinish)
                    {
                        turtleSteps++;
                        Console.WriteLine($"Leonardo move {turtleSteps} steps");

                        if (turtleSteps == rabbitSteps && sleeping)
                        {
                            int num = rand.Next(0, 100);
                            if (num < 50)
                            {
                                Console.WriteLine("Leonardo makes noise");
                                Monitor.Pulse(l);
                            }
                        }

                        if (turtleSteps >= finishLine || rabbitSteps >= finishLine)
                        {
                            raceFinish = true;
                        }
                    }
                }
                Thread.Sleep(300);
            }
        }

        public void RunAndSleepRabbit()
        {
            while (!raceFinish)
            {
                lock (l)
                {
                    if (!raceFinish)
                    {
                        rabbitSteps += 4;
                        Console.WriteLine($"Bugs Bunny move {rabbitSteps} steps");

                        if (turtleSteps >= finishLine || rabbitSteps >= finishLine)
                        {
                            raceFinish = true;
                        }
                        else
                        {
                            if (rand.Next(1, 101) <= 60)
                            {
                                int auxSteps = rabbitSteps;
                                Console.WriteLine($"Seems like Bugs thinks is good time to take a nap when he made {rabbitSteps} steps");
                                
                                Thread dreams = new Thread(() => SweetDreams(auxSteps));
                                dreams.Start();
                                Monitor.Wait(l);

                                Console.WriteLine($"Bugs Bunny finally wakes up at {rabbitSteps} steps");
                            }
                        }
                    }
                }
                Thread.Sleep(200);
            }
        }

        public void SweetDreams(int currentsteps)
        {
            Thread.Sleep(2500);
            lock (l)
            {
                if(currentsteps == rabbitSteps)
                {
                    Monitor.Pulse(l);
                }
            }
        }

    }
}
}
