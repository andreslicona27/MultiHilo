using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio5
{
    internal class Horse
    {
        public static readonly object l = new object();
        public bool winnerHorse = false;
        private int positionX;
        private int positionY;
        private string name;

        public Horse(string name, int positionX, int positionY)
        {
            PositionX = positionX;
            PositionY = positionY;
            Name = name;
            winnerHorse = false;
        }

        public int PositionX
        {
            set { this.positionX = value; }
            get { return positionX; }
        }

        public int PositionY
        {
            set { this.positionY = value; }
            get { return positionY; }
        }
        public string Name
        {
            set { this.name = value; }
            get { return name; }
        }

        public void Posicion()
        {
            Console.SetCursorPosition(PositionX, PositionY);
        }

        public void Run(int gallops)
        {
            PositionY += gallops;
        }
    }
}
