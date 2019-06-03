using System;
using System.Collections.Generic;
using System.Text;

namespace TrabalhoPratico2
{
    public class Zombie : Agent
    {
        // Variables/Properties

        // Constructor
        public Zombie(int startX, int startY) : base(startX, startY)
        {

        }

        public void TurnHuman()
        {
            // if the zombie detects a human in radius of 1, he attacks,
            // turning him into a zombie (maybe delete said player and replace
            // him with a zombie in the same position)
        }
    }
}
