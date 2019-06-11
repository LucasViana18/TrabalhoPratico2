using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TrabalhoPratico2
{
    class Human : Agent
    {
        // Variables/Properties

        // Constructor
        public Human
            (int startX, int startY, Params par, Board board, int lastAgentID):
            base(startX, startY, par, board, lastAgentID)
        {
            this.elementType = Type.Human;
            this.target = Type.Zombie;
        }
        
        /*
        protected override void PathFinding(Board board)
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
                    agentMoves.Add(moorePos);
                }
            }
        }
        */

        public override string GetSymbol()
        {
            return "h" + AgentID;
        }

        public override void Move(FoundAgentDetails zombie)
        {
            // Zombie found
            if (zombie.Found && agentBoard.GetElementType
                (zombie.AgentCoord.X, zombie.AgentCoord.Y) == Type.Zombie)
            {
                lastMovement = Behaviour(zombie, false);

                if (agentBoard.GetElementType(lastMovement.X, lastMovement.Y)
                    == Type.Empty)
                {
                    agentBoard.MoveAgent(this, lastMovement);
                    currentPosition.X = lastMovement.X;
                    currentPosition.Y = lastMovement.Y;
                }
            }
        }
    }
}
