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
                LastMovement = RunAway(zombie);

                if (agentBoard.GetElementType(LastMovement.X, LastMovement.Y)
                    == Type.Empty)
                {
                    agentBoard.MoveAgent(this, LastMovement);
                    currentPosition.X = LastMovement.X;
                    currentPosition.Y = LastMovement.Y;
                }
            }

        }

        private Position RunAway(FoundAgentDetails zombie)
        {
            Position chosenPosition;
            List<Position> toMove = new List<Position>();

            if (zombie.AgentReference.X == this.currentPosition.X)
            {
                if (zombie.AgentReference.Y > this.currentPosition.Y)
                {
                    toMove = vectorMoveTop;
                }
                else
                {
                    toMove = vectorMoveBottom;
                }
            }
            else if (zombie.AgentReference.Y == this.currentPosition.Y)
            {
                if (zombie.AgentReference.X > this.currentPosition.X)
                {
                    toMove = vectorMoveLeft;
                }
                else
                {
                    toMove = vectorMoveRight;
                }
            }
            else  // X and Y are different = diagonal 
            {
                if (zombie.AgentReference.Y < this.currentPosition.Y)
                {
                    toMove = 
                        (zombie.AgentReference.X < this.currentPosition.X) ?
                        vectorMoveBottom.Concat(vectorTopRight).ToList() :
                        vectorMoveBottom.Concat(vectorTopLeft).ToList();
                }
                else
                {
                    toMove = 
                        (zombie.AgentReference.X < this.currentPosition.X) ?
                        vectorMoveTop.Concat(vectorBottomRight).ToList() :
                        vectorMoveTop.Concat(vectorBottomLeft).ToList();
                }
            }

            chosenPosition = 
                ApplyVector(toMove[chosenMove.Next(0, toMove.Count)]);

            return chosenPosition;
        }

    }
}
