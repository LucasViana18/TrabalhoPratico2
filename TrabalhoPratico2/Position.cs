using System;
using System.Collections.Generic;
using System.Text;

namespace TrabalhoPratico2
{
    /// <summary>
    /// Give agent position
    /// </summary>
    public class Position
    {
        // Instance properties
        public int X { get; set; }
        public int Y { get; set; }

        // Constructor
        /// <summary>
        /// Position constructor
        /// </summary>
        /// <param name="x">Position x</param>
        /// <param name="y">Position y</param>
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
