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
        public object l = new object();
        bool raceFinish = false;
        int turtleSteps = 0;
        int rabbitSteps = 0;
        int finishLine = 50;



        // FUNCITONS
        public string Init()
        {
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
                        //if (SweetDreams(rabbitSteps))
                        //{
                        //    Console.WriteLine("Seems like Bugs thinks is good time to take a nap");
                        //}
                    }
                }
                Thread.Sleep(200);
            }
        }

        public bool SweetDreams(int currentsteps)
        {
            bool sleeping = false;
            Random rand = new Random();
            while (!raceFinish)
            {
                lock (l)
                {
                    if (!raceFinish)
                    {
                        int num = rand.Next(0, 100);
                        if (num < 60)
                        {
                            Thread.Sleep(2500);
                            sleeping = true;
                        }
                        else
                        {
                            sleeping = false;
                        }

                    }
                }
            }
            return sleeping;

        }
    }
}
