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

        public override void Move()
        {
            base.Move();

            Position newHumanPos;
            Position threatPos = FindNear(Type.Zombie);

            if (threatPos.X != -100)  // Zombie found
            {
                newHumanPos = RunAway(threatPos);
            }

        }

        private Position RunAway(Position threatPos)
        {
            int[,] toMove;

            if (threatPos.X == this.currentPosition.X)
            {
                if (threatPos.Y > this.currentPosition.Y)
                {
                    toMove = vectorTop;
                }
                else
                {
                    toMove = vectorBottom;
                }
            }
            else if (threatPos.Y == this.currentPosition.Y)
            {
                if (threatPos.X > this.currentPosition.X)
                {
                    int[] add;
                    toMove =
                }
                else
                {
                    toMove = new int[3, 2] { { 1, -1 }, { 1, 0 }, { 1, 1 } };
                }
            }
            else  // X and Y are different = diagonal 
            {
                if (threatPos.X < this.currentPosition.X && threatPos.Y < this.currentPosition.Y)
                {
                    toMove = new int[5, 2] { { 1, -1 }, { 1, 0 }, { 1, 1 }, { 0, 1 }, { -1, 1 } };
                }
                else if (threatPos.X > this.currentPosition.X && threatPos.Y < this.currentPosition.Y)
                {
                    toMove = new int[5, 2] { { -1, -1 }, { -1, 0 }, { -1, 1 }, { 0, 1 }, { 1, 1 } };
                }
                else if (threatPos.X > this.currentPosition.X && threatPos.Y > this.currentPosition.Y)
                {
                    toMove = new int[5, 2] { { -1, -1 }, { -1, 0 }, { -1, 1 }, { 0, -1 }, { 1, -1 } };
                }
                else
                {
                    toMove = new int[5, 2] { { 1, -1 }, { 1, 0 }, { 1, 1 }, { 0, -1 }, { -1, -1 } };
                }
            }
        }

    }
}
