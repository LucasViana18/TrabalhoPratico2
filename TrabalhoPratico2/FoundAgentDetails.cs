
namespace TrabalhoPratico2
{
    /// <summary>
    /// Share agent details
    /// </summary>
    public class FoundAgentDetails
    {
        // Instance properties
        public bool Found { get; set; }
        public Position AgentCoord { get; set; }
        public Position AgentReference { get; set; }

        // Constructor
        /// <summary>
        /// FoundAgentDetails constructor
        /// </summary>
        /// <param name="found">If agent was found</param>
        /// <param name="coord">Agent position at currentBoard</param>
        /// <param name="reference">Possible positions at Moore 
        /// neighborhood with Toroidal effect applied</param>
        public FoundAgentDetails
            (bool found, Position coord, Position reference)
        {
            Found = found;
            AgentCoord = new Position(0, 0);
            AgentReference = new Position(0, 0);
            AgentCoord = coord;
            AgentReference = reference;
        }
    }
}
