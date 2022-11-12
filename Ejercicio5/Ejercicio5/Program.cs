namespace Ejercicio5
{

    internal class Program
    {
        static readonly private object l = new object();
        static bool winner;

        public static void runHorseRun(Object competitor)
        {
            runHorseRun((Horse)competitor);
        }
        public static void runHorseRun(Horse competitor)
        {
            Random rand = new Random();
            do
            {
                lock (l)
                {
                    if (!winner)
                    {
                        competitor.Run(rand.Next(1, 6));

                        Console.SetCursorPosition(0, competitor.PositionY);
                        Console.Write("".PadRight(Console.WindowWidth));
                        Console.SetCursorPosition(0, competitor.PositionY);
                        Console.Write(competitor.PositionY + 1 + ")");

                        competitor.Posicion();
                        Console.SetCursorPosition(competitor.PositionX, competitor.PositionY);
                        Console.Write("♞");
                        if (competitor.PositionX >= 100)
                        {
                            competitor.winnerHorse = true;
                            winner = false;
                        }
                    }
                }
                Thread.Sleep(rand.Next(100, 300));
            } while (!competitor.winnerHorse && winner);
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
            String[] names = new String[5];
            bool win = false;
            int betHorse;

            names[0] = "Zeus";
            names[1] = "Odin";
            names[2] = "Ra";
            names[3] = "Amaterasu";
            names[4] = "Indra";

            do
            {
                lock (l)
                {
                    Monitor.PulseAll(l);
                }
                Console.Clear();
                Console.WriteLine("MIGHTY HORSES --It is time for you to choose the best mitology--\n1)Zeus\n2)Odin\n3)Ra\n4)Amaterasu\n5)Indra\n\nWhat horse do you bet on?");
                betHorse = int.Parse(Console.ReadLine());

                while (betHorse < 1 || betHorse > 5)
                {
                    Console.Clear();
                    Console.WriteLine("Bet only for horses on the list");
                    Console.WriteLine("HORSES\n1)Freud\n2)Kahneman\n3)Peterson\n4)Grant\n5)Goleman\nWhat horse do you bet on?");
                    betHorse = int.Parse(Console.ReadLine());
                }

                Console.WriteLine("\nYour faith lies in: {0}, let´s hope you're right", names[betHorse - 1]);

                lock (l)
                {
                    for (int i = 0; i < threads.Length; i++)
                    {
                        horses[i] = new Horse(names[i], i, i + 1);
                        threads[i] = new Thread(runHorseRun);
                        threads[i].Start(horses[i]);
                    }
                    Console.Clear();
                    Console.SetCursorPosition(0, threads.Length + 1);
                    Console.WriteLine("Chosen horse: {0}", horses[betHorse - 1].Name);
                    Monitor.Wait(l);
                }
                Console.SetCursorPosition(0, 8);

                for (int i = 0; i < horses.Length; i++)
                {
                    if (horses[i].winnerHorse)
                    {
                        Console.WriteLine($"The winner is Horse number {i + 1}");
                        if (i == betHorse - 1)
                        {
                            Console.WriteLine("Congrats, you're the winner!");
                        }
                        else
                        {
                            Console.WriteLine("What a pathetic loser");
                        }
                    }
                    win = true;
                }

            } while (win);



        }
    }
}