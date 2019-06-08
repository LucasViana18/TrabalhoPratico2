using System;
using System.Collections.Generic;
using System.Text;

namespace TrabalhoPratico2
{
    public class FoundAgentDetails
    {
        public bool Found { get; set; }
        public Position AgentCoord { get; set; }
        public Position AgentReference { get; set; }

        public FoundAgentDetails(bool found, Position coord, Position reference)
        {
            Found = found;
            AgentCoord = new Position(0, 0);
            AgentReference = new Position(0, 0);
            AgentCoord = coord;
            AgentReference = reference;
        }
    }
}
