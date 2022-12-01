
namespace Ejercicio7
{
    internal class Program
    {
        private static readonly object l = new object();
        public static bool gameFinish = comunCont == 20 || comunCont == -20;
        public static bool animationPlaying = false;
        public static Random ran = new Random();
        static int comunCont = 0;
        public static bool trying = comunCont == 20 || comunCont == -20;

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
                        Console.SetCursorPosition(2, 4);
                        Console.Write("PLAYER one: " + num);
                        if (num == 5 || num == 7)
                        {
                            if (animationPlaying)
                            {
                                Monitor.Wait(l);
                                animationPlaying = false;
                                comunCont++;
                                PrintNumbers(true, false);
                            }
                            else
                            {
                                comunCont += 5;
                                PrintNumbers(true, true);
                            }
                        }
                    }
                }
                Thread.Sleep(ran.Next(100, num * 100));
            }
        }

        public static void Player2Function()
        {
            int num = 0;
            while (!gameFinish)
            {
                lock (l)
                {
                    if (!gameFinish)
                    {
                        num = ran.Next(1, 11);
                        Console.SetCursorPosition(2, 5);
                        Console.Write("PLAYER two: " + num);
                        if (num == 5 || num == 7)
                        {
                            if (!animationPlaying)
                            {
                                Monitor.Pulse(l);
                                animationPlaying = true;
                                comunCont--;
                                PrintNumbers(false, false);
                            }
                            else
                            {
                                comunCont -= 5;
                                PrintNumbers(false, true);
                            }
                        }
                    }
                }
                Thread.Sleep(ran.Next(100, num * 100));
            }
        }


        public static void Display()
        {
            string displayIcon = "|/-\\";
            int cont = 0;
            while (!gameFinish)
            {
                lock (l)
                {
                    if (!gameFinish)
                    {
                        if (cont == 3)
                        {
                            cont = 0;
                        }
                        else
                        {
                            cont++;
                        }
                        int j = 2;
                        for (int i = 0; i < 20; i++)
                        {
                            // TOP 
                            Console.SetCursorPosition(i, 2);
                            Console.WriteLine(displayIcon[cont]);
                            // BOTTOM
                            Console.SetCursorPosition(i, 6);
                            Console.WriteLine(displayIcon[cont]);
                            // LEFT
                            Console.SetCursorPosition(0, j);
                            Console.WriteLine(displayIcon[cont]);
                            //RIGHT
                            Console.SetCursorPosition(20, j);
                            Console.WriteLine(displayIcon[cont]);
                            if (j == 6)
                            {
                                j = 2;
                            }
                            else
                            {
                                j++;
                            }
                        }

                    }
                }
                Thread.Sleep(200);
            }
        }

        public static void PrintNumbers(bool player1, bool getLucky)
        {
            // bool player1 is for identify the player and placing the cursor in the right position
            // bool getlucky is to know if the random number is 5 or 7 so we can print +5 if not we print +1
            Console.SetCursorPosition(2, 3);
            Console.WriteLine("COMUN NUMBER: " + comunCont);
            if (player1)
            {
                Console.SetCursorPosition(17, 4);
                if (getLucky)
                {
                    Console.WriteLine("+5");
                }
                else
                {
                    Console.WriteLine("+1");
                }
            }
            else
            {
                Console.SetCursorPosition(17, 5);
                if (getLucky)
                {
                    Console.WriteLine("+5");
                }
                else
                {
                    Console.WriteLine("+1");
                }

            }
        }

        static void Main(string[] args)
        {
            Thread player1 = new Thread(Player1Function);
            Thread player2 = new Thread(Player2Function);
            Thread display = new Thread(Display);
            display.IsBackground = true;
            player1.Start();
            player2.Start();
            display.Start();

        }
    }
}