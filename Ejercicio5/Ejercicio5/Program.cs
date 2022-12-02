using System.Collections;
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
            Console.CursorVisible = false;
            do
            {
                lock (l)
                {
                    if (!winner)
                    {
                        // Placing the horses in their own race lane 
                        Console.SetCursorPosition(0, competitor.PositionY);
                        Console.Write("{0,9} {1})", competitor.Name, competitor.TrackNumber);

                        // design of the speedway
                        Console.SetCursorPosition(14, competitor.PositionY);
                        Console.WriteLine("|");
                        Console.SetCursorPosition(99, competitor.PositionY);
                        Console.WriteLine("|");
                        for (int j = 15; j < 99; j++)
                        {
                            Console.SetCursorPosition(j, 0);
                            Console.WriteLine("_");
                        }
                        for (int i = 15; i < 99; i++)
                        {
                            Console.SetCursorPosition(i, competitor.PositionY);
                            Console.WriteLine("_");
                        }


                        // Erase the old position of the horse
                        Console.SetCursorPosition(competitor.PositionX, competitor.PositionY);
                        Console.Write(" ");

                        // Printing the position of the horse during the race
                        competitor.Run(random.Next(1, 8));
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
            String[] godsNames = {"ZEUS", "ODIN", "AMATERASU", "INDRA", "RA"};
            String[] godsReigns =
            {
                "God of the sky and thunder",
                "Supreme god of all the existence",
                "Goddess of the sun and ruler of heaven",
                "The one who controls and watches over everything",
                "God of the sun and the origin of life itself"
            };
            bool win = false;
            bool iwin = false;
            int betHorse;
            int repeatBet;


            do
            {
                win = false;
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
                            horses[i] = new Horse(godsNames[i], i + 1, 13, i + 1);
                            threads[i] = new Thread(runHorseRun);
                        }
                        for (int i = 0; i < threads.Length; i++)
                        {
                            threads[i].Start(horses[i]);
                        }
                        Console.SetCursorPosition(0, threads.Length + 1);
                        Console.WriteLine("\nYour faith lies on: {0} {1}", godsNames[betHorse - 1], godsReigns[betHorse - 1]);
                        for (int i = 0; i < Console.WindowWidth; i++)
                        {
                            Console.Write("-");
                        }
                        Monitor.Wait(l);
                        Console.CursorVisible = true;
                    }
                    Console.SetCursorPosition(0, 10);

                    for (int i = 0; i < horses.Length; i++)
                    {
                        if (horses[i].winnerHorse)
                        {
                            Console.WriteLine($"The winner is the almigty: {godsNames[i]}");
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
                                    winner = false;
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