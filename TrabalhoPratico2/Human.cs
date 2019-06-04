using System;
using System.Collections.Generic;
using System.Text;

namespace TrabalhoPratico2
{
    class Human : Agent
    {
        // Variables/Properties

        // Constructor
        public Human(int startX, int startY, Params par, int lastAgentID) : base(startX, startY, par, lastAgentID)
        {
            this.elementType = Type.Human;
        }

        // Methods
        /*
        public override Position VerifyOtherPosition()
        {
            throw new NotImplementedException();
        }
        */

        public override string GetSymbol()
        {
            return "h" + AgentID;
        }

    }
}
