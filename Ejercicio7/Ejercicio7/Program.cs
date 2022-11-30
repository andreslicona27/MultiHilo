
namespace Ejercicio7
{
    internal class Program
    {
        private static readonly object l = new object();
        public static bool gameFinish = false;
        public static bool animationPlaying = false;
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
                        Console.SetCursorPosition(0, 4);
                        Console.Write("PLAYER one: " + num);
                        if (num == 5 || num == 7)
                        {
                            if (comunCont == 20)
                            {
                                gameFinish = true;
                            }
                            else
                            {
                                if (animationPlaying)
                                {
                                    Monitor.Wait(l);
                                    animationPlaying = false;
                                    comunCont++;
                                    Console.SetCursorPosition(0, 3);
                                    Console.WriteLine("COMUN NUMBER: " + comunCont);
                                    Console.SetCursorPosition(15, 4);
                                    Console.WriteLine("+1");
                                }
                                else
                                {
                                    comunCont += 5;
                                    Console.SetCursorPosition(0, 3);
                                    Console.WriteLine("COMUN NUMBER: " + comunCont);
                                    Console.SetCursorPosition(15, 4);
                                    Console.WriteLine("+5");
                                }
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
                        Console.SetCursorPosition(0, 5);
                        Console.Write("PLAYER two: " + num);
                        if (num == 5 || num == 7)
                        {
                            if (comunCont == -20)
                            {
                                gameFinish = true;
                            }
                            else
                            {
                                if (!animationPlaying)
                                {
                                    Monitor.Pulse(l);
                                    animationPlaying = true;
                                    comunCont--;
                                    Console.SetCursorPosition(0, 3);
                                    Console.WriteLine("COMUN NUMBER: " + comunCont);
                                    Console.SetCursorPosition(15,5);
                                    Console.WriteLine("-1");
                                }
                                else
                                {
                                    comunCont -= 5;
                                    Console.SetCursorPosition(0, 3);
                                    Console.WriteLine("COMUN NUMBER: " + comunCont);
                                    Console.SetCursorPosition(15, 5);
                                    Console.WriteLine("-5");
                                }
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
                        Console.SetCursorPosition(0, 0);
                        Console.WriteLine(displayIcon[cont]);
                        Console.SetCursorPosition(1, 0);
                        Console.WriteLine(displayIcon[cont]);
                        Console.SetCursorPosition(2, 0);
                        Console.WriteLine(displayIcon[cont]);
                    }
                }
                Thread.Sleep(200);
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