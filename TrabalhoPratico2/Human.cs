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

        public override void PathFinding(Board board)
        {
            base.PathFinding(board);

            foreach (Position moorePos in possibleMoves)
            {
                bool validPos = true;
                UpdateNeighR1(moorePos);

                foreach (Position flowerPos in neighR1)
                {
                    foreach (Agent agent in board.agents)
                    {
                        if (flowerPos == agent.AgentPosition && agent.ElementType == Type.Zombie)
                        {
                            validPos = false;
                        }
                    }
                }

                if (validPos)
                {
                    agentMoves.add(moorePos);
                }
            }
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
