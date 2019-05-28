using System;
using System.Collections.Generic;
using System.Text;

namespace TrabalhoPratico2
{
    public class Position
    {
        // Posição (x, y)
        protected int X { get; set; }
        protected int Y { get; set; }

        // Constructor
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
