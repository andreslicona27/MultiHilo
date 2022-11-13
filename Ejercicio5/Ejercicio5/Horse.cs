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
        private string name;
        private int positionX;
        private int positionY;
        private int trackNumber;
        public bool winnerHorse = false;

        // BUILDERS
        public Horse(int trackNumber, int positionX, int positionY)
        {
            PositionX = positionX;
            PositionY = positionY;
            TrackNumber = trackNumber;
            winnerHorse = false;
        }

        // SETTERS / GETTERS
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
        public int TrackNumber
        {
            set { this.trackNumber = value; }
            get { return trackNumber; }
        }
        // FUNCTIONS
        public void Run(int gallops)
        {
            PositionX += gallops;
        }
        public void Run()
        {
            PositionX++;
        }
    }
}
