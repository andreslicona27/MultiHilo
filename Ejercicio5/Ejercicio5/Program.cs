namespace Ejercicio5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Horse[] horses = new Horse[5];
            Thread[] threads = new Thread[5];
            bool win = false;
            int bet;

            horses[0] = new Horse();
            horses[1] = new Horse();
            horses[2] = new Horse();
            horses[3] = new Horse();
            horses[4] = new Horse();

            horses[0].Position = 0;
            horses[1].Position = 0;
            horses[2].Position = 0;
            horses[3].Position = 0;
            horses[4].Position = 0;


            do
            {
                Console.WriteLine("HORSES\n1)Freud\n2)Kahneman\n3)Peterson\n4)Grant\n5)Goleman\nWhat horse do you bet on?");
                bet = int.Parse(Console.ReadLine());

                while (bet < 1 || bet > 5)
                {
                    Console.WriteLine("Bet only for horses on the list");
                    Console.WriteLine("HORSES\n1)Freud\n2)Kahneman\n3)Peterson\n4)Grant\n5)Goleman\nWhat horse do you bet on?");
                    bet = int.Parse(Console.ReadLine());
                }

                for (int i =0; i < horses.Length; i++)
                {
                    threads[i] = new Thread(horses[i].Run);
                    threads[i].Start();
                }

                Array.ForEach(threads, item => item.Join());
                

            } while (win);
            


        }
    }
}