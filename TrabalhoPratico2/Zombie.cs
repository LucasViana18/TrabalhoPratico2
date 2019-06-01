using System;
using System.Collections.Generic;
using System.Text;

namespace TrabalhoPratico2
{
    public class Zombie : Character
    {
        // Variables/Properties

        public override Position VerifyOtherPosition()
        {
            Position pos = new Position(2, 2);
            return pos;
        }

        public void TurnHuman()
        {
            // if the zombie detects a human in radius of 1, he attacks,
            // turning him into a zombie (maybe delete said player and replace
            // him with a zombie in the same position)
        }
    }
}
