using static Ejercicio2.Program;

namespace Ejercicio2
{
    internal class Program
    {
        public delegate void MyDelegate();

        public static bool MenuGenerator(string[] options, MyDelegate[] functions)
        {
            int opcCont = 1;
            string opcMenu;
            int result;
      
            if (options.Length != functions.Length || (options == null || functions == null))
            {
                Console.WriteLine("There it's an error in the parameters.");
                return false;
            }
            else
            {
                do
                {
                    opcCont = 1;
                    Console.WriteLine("\nOPTIONS MENU");
                    Array.ForEach(options, item => Console.WriteLine("{0}) {1}", opcCont++, item));
                    Console.WriteLine("{0}) Exit", opcCont);
                    opcMenu = Console.ReadLine();
                    
                    if(int.TryParse(opcMenu, out result))
                    {
                        result = int.Parse(opcMenu);

                        if (result < options.Length + 1 && result >= 1)
                        {
                            functions[result - 1]();
                        } 
                        else if (result != options.Length + 1)
                        {
                            Console.WriteLine("That option is out of range, try again.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("The option has to be a number.");
                    }


                } while (result != options.Length + 1);
                return true;
            }

        }
        //static void f1()
        //{
        //    Console.WriteLine("A");
        //}
        //static void f2()
        //{
        //    Console.WriteLine("B");
        //}
        //static void f3()
        //{
        //    Console.WriteLine("C");
        //}
        static void Main(string[] args)
        {
            MenuGenerator(new string[] { "Op1", "Op2", "Op3" }, 
                new MyDelegate[] { 
                    () => Console.WriteLine("A"), 
                    () => Console.WriteLine("B"), 
                    () => Console.WriteLine("C") });
            Console.ReadKey();
        }



    }
}
