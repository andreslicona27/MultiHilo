namespace Ejercicio7
{
    internal class Program
    {
        private static readonly object l = new object();
        public static bool gameFinish = false;
        public static Random ran = new Random();
        static int comunCont = 0;

        public static void Player1Function()
        {
            int num = 0;
            while (!gameFinish)
            {
                lock (l)
                {
                    if (!gameFinish)
                    {
                        num = ran.Next(1, 11);
                        Console.SetCursorPosition(5, 5);
                        Console.Write(num);
                        if(num == 5 || num == 7)
                        {

                        }

                    }
                }
            }
            Thread.Sleep(ran.Next(100, num * 100));

        }

        public static void Player2Function()
        {

        }

        string[] displayIcon = { "|", "/", "-", "\\" };
        public static void Display()
        {

        }

        static void Main(string[] args)
        {
            Thread player1 = new Thread(Player1Function);
            Thread player2 = new Thread(Player2Function);
            player1.Start();
            player2.Start();

        }
    }
}