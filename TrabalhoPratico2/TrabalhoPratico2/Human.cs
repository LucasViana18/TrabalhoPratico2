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

        public void AI(Board board)
        {
            foreach (Position moorePos in neighR1)
            {
                foreach (Zombie zombies in board.zombies)
                {
                    if (moorePos != zombies.AgentPosition)
                    {
                        possibleMoves.Add(moorePos);
                    }
                }

                foreach (Human human in board.humans)
                {
                    if (moorePos != human.AgentPosition)
                    {
                        possibleMoves.Add(moorePos);
                    }
                }
            }

            foreach (Position moorePos in possibleMoves)
            {
                UpdateNeighR1(moorePos);
                foreach (Position flowerPos in neighR1)
                {
                    foreach (Zombie zombies in board.zombies)
                    {
                        if (flowerPos != zombies.AgentPosition)
                        {
                            agentMoves.Add(moorePos);
                        }
                    }
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
