using System;
using System.Collections.Generic;
using System.Text;

namespace TrabalhoPratico2
{
    public class Zombie : Agent
    {
        // Variables/Properties

        // Constructor
        public Zombie
            (int startX, int startY, Params par, Board board, int lastAgentID):
            base(startX, startY, par, board, lastAgentID)
        { 
            this.elementType = Type.Zombie;
            this.target = Type.Human;
        }

        // Methods
        public bool HumanNear(FoundAgentDetails human)
        {
            // if the zombie detects a human in radius of 1, he attacks,
            // turning him into a zombie (maybe delete said player and replace
            // him with a zombie in the same position)

            int difX = Math.Abs(this.currentPosition.X - human.AgentReference.X);
            int difY = Math.Abs(this.currentPosition.Y - human.AgentReference.Y);
            int toCompare = Math.Max(difX, difY);

            return (toCompare == 1);
        }

        public override void Move(FoundAgentDetails human)
        {

            if (human.Found && agentBoard.GetElementType(human.AgentCoord.X, human.AgentCoord.Y) == Type.Human)  // Human  found
            {
                if (!HumanNear(human))
                {
                    LastMovement = Behaviour(human, true); // attract by the agent Human

                    if (agentBoard.GetElementType(LastMovement.X, LastMovement.Y) == Type.Empty)
                    {
                        agentBoard.MoveAgent(this, LastMovement);
                        currentPosition.X = LastMovement.X;
                        currentPosition.Y = LastMovement.Y;
                    }
                }
                else
                {
                    agentBoard.ChangeAgentType(human.AgentCoord);
                }
            }
        }

        public override string GetSymbol()
        {
            return "z" + AgentID;
        }
    }
}
