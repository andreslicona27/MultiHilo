﻿using System.Collections;
using System.IO.IsolatedStorage;

namespace Ejercicio5
{
    internal class Program
    {
        static readonly private object l = new object();
        static bool winner = false;

        public static void runHorseRun(Object competitor)
        {
            runHorseRun((Horse)competitor);
        }

        public static void runHorseRun(Horse competitor)
        {
            Random random = new Random();
            do
            {
                lock (l)
                {
                    if (!winner)
                    {
                        // Placing the horses in their own race lane 
                        Console.SetCursorPosition(0, competitor.PositionY);
                        Console.Write("horse " + competitor.TrackNumber + ")");
                        // Printing the ubication of the horse
                        competitor.Run(random.Next(1, 6));
                        Console.SetCursorPosition(competitor.PositionX, competitor.PositionY);
                        Console.Write("*");

                        if (competitor.PositionX >= 100)
                        {
                            competitor.winnerHorse = true;
                            winner = true;
                        }
                    }
                }
                Thread.Sleep(random.Next(100, 500));
            } while (!winner);

            lock (l)
            {
                if (competitor.winnerHorse)
                {
                    Monitor.Pulse(l);
                }
            }
        }

        static void Main(string[] args)
        {
            Horse[] horses = new Horse[5];
            Thread[] threads = new Thread[5];
            String[] godsNames = new String[5];
            String[] godsReigns = new String[5];
            bool win = false;
            bool iwin = false;
            int betHorse;
            int repeatBet;

            godsNames[0] = "ZEUS";
            godsNames[1] = "ODIN";
            godsNames[2] = "AMATERASU";
            godsNames[3] = "INDRA";
            godsNames[4] = "RA";

            godsReigns[0] = "God of the sky and thunder";
            godsReigns[1] = "Supreme god of all the existence";
            godsReigns[2] = "Goddess of the sun and ruler of heaven.";
            godsReigns[3] = "The one who controls and watches over everything";
            godsReigns[4] = "God of the sun and the origin of life itself";

            do
            {
                lock (l)
                {
                    Monitor.PulseAll(l);
                }

                try
                {
                    Console.Clear();
                    Console.WriteLine("MIGHTY HORSES --It is time for you to choose the best mitology--" +
                        "\n1)Zeus\n2)Odin\n3)Amaterasu\n4)Indra\n5)Ra\n\nWhat horse do you bet on?");
                    betHorse = int.Parse(Console.ReadLine());

                    while (betHorse < 1 || betHorse > 5)
                    {
                        Console.Clear();
                        Console.WriteLine("Bet only for horses on the list");
                        Console.WriteLine("MIGHTY HORSES --It is time for you to choose the best mitology--" +
                            "\n1)Zeus\n2)Odin\n3)Amaterasu\n4)Indra\n5)Ra\n\nWhat horse do you bet on?");
                        betHorse = int.Parse(Console.ReadLine());
                    }

                    lock (l)
                    {
                        Console.Clear();
                        for (int i = 0; i < threads.Length; i++)
                        {
                            horses[i] = new Horse(i + 1, 8, i);
                            threads[i] = new Thread(runHorseRun);
                            threads[i].Start(horses[i]);
                        }
                        Console.SetCursorPosition(0, threads.Length);
                        Console.WriteLine("\nYour faith lies on: {0} {1}", godsNames[betHorse - 1], godsReigns[betHorse - 1]);
                        for (int i = 0; i < Console.WindowWidth; i++)
                        {
                            Console.Write("-");
                        }
                        Monitor.Wait(l);
                    }
                    Console.SetCursorPosition(0, 8);

                    for (int i = 0; i < horses.Length; i++)
                    {
                        if (horses[i].winnerHorse)
                        {
                            Console.WriteLine($"The winner is the allmigty: {godsNames[i]}");
                            if (horses[i].TrackNumber == betHorse)
                            {
                                Console.WriteLine("You really have an unshakable faith\n");
                                Console.WriteLine("Would you like to put your beliefs to the test again?" +
                                    "\n1) Of course, who do you think you´re dealing with\n2) No, one time was more than enough");
                                repeatBet = int.Parse(Console.ReadLine());

                                while (repeatBet < 1 || repeatBet > 5)
                                {
                                    Console.WriteLine("Please answer the question with on of the options");
                                    Console.WriteLine("Would you like to put your beliefs to the test again?" +
                                        "\n1)Of course, who do you think you´re dealing with\n2)No, one time was more than enough");
                                    repeatBet = int.Parse(Console.ReadLine());
                                }

                                if (repeatBet == 1)
                                {
                                    win = true;
                                }
                                else
                                {
                                    iwin = true;
                                }
                            }
                        }
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("There´s has been an error on your bet, try again.");
                }

            } while (win);
            if (iwin)
            {
                Console.WriteLine("That´s ok, but soon we will see each other again");
            }
            else
            {
                Console.WriteLine("So, it seems that your faith was not enough.");
            }



        }
    }
}