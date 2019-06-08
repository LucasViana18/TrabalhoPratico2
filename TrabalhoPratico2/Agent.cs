using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TrabalhoPratico2
{
    public class Agent : GameElement
    {
        // Variables and Properties
        protected Params agentPar;
        protected Board agentBoard;
        protected Type target;
        protected Random chosenMove;
        protected int myID = 0;

        protected List<Position> vectorMove;
        protected List<Position> vectorMoveTop;
        protected List<Position> vectorMoveBottom;
        protected List<Position> vectorMoveLeft;
        protected List<Position> vectorMoveRight;
        protected List<Position> vectorTopLeft;
        protected List<Position> vectorTopRight;
        protected List<Position> vectorBottomLeft;
        protected List<Position> vectorBottomRight;

        public string AgentID { get { return myID.ToString("00"); } }
        public Position AgentPosition { get { return currentPosition; } }
        public Position LastMovement { get; protected set; }

        // Constructor
        public Agent
            (int startX, int startY, Params par, Board board, int lastAgentID):
            base(startX, startY)
        {
            vectorMove = new List<Position>();
            vectorMoveTop = new List<Position>();
            vectorMoveBottom = new List<Position>();
            vectorTopLeft = new List<Position>();
            vectorTopRight = new List<Position>();
            vectorBottomLeft = new List<Position>();
            vectorBottomRight = new List<Position>();
            vectorMoveLeft = new List<Position>();
            vectorMoveRight = new List<Position>();

            chosenMove = new Random();
            LastMovement = new Position(-1, -1);
            target = Type.Empty;

            agentPar = par;
            agentBoard = board;
            myID = lastAgentID + 1;
            StoreVectors();

        }

        private void StoreVectors()
        {
            // Set VectorMoveTop
            for (int x = -1; x <= 1; x++)
                vectorMoveTop.Add(new Position(x, -1));

            // Set VectorMoveBottm
            for (int x = -1; x <= 1; x++)
                vectorMoveBottom.Add(new Position(x, 1));

            // Set VectorBottomLeft
            for (int y = 0; y <= 1; y++)
                vectorBottomLeft.Add(new Position(-1, y));

            // Set vectorBottomRight
            for (int y = 0; y <= 1; y++)
                vectorBottomRight.Add(new Position(1, y));

            // Set vectorTopLeft
            for (int y = -1; y <= 0; y++)
                vectorTopLeft.Add(new Position(-1, y));

            // Set vectorTopRight
            for (int y = -1; y <= 0; y++)
                vectorTopRight.Add(new Position(1, y));

            // Set VectorMove R1
            vectorMove = vectorMoveTop.Concat(vectorMoveBottom).ToList();
            vectorMove.Add(new Position(-1, 0));
            vectorMove.Add(new Position(1, 0));

            // Set VectorMoveLeft
            vectorMoveLeft = vectorTopLeft;
            vectorMoveLeft.Add(new Position(-1, 1));

            // Set VectorMoveRight
            vectorMoveRight = vectorTopRight;
            vectorMoveRight.Add(new Position(1, 1));
        }

        /*
        protected virtual void PathFinding(Board board)
        {
            UpdateNeighR1();
            foreach (Position moorePos in neighR1)
            {
                foreach (Agent agent in board.agents)
                {
                    if (moorePos != agent.AgentPosition)
                    {
                        possibleMoves.Add(moorePos);
                    }
                }
            }
        }
        */
        public virtual void Move(FoundAgentDetails agent)
        {
        }

        // Apply a vector of direction to move the Agent and consider the
        // Toroidal process.
        protected Position ApplyVector(Position vector)
        {
            int localCol, localRow;

            localCol = this.currentPosition.X + vector.X;
            localRow = this.currentPosition.Y + vector.Y;
            return agentBoard.ToroidalConvert(localCol, localRow);
        }

        public FoundAgentDetails FindNearAgent()
        {
            FoundAgentDetails toReturn = new FoundAgentDetails(false, new Position(0, 0), new Position(0, 0));
            float colLimit = agentBoard.NumberColumns / 2;

            for (int r = 1; r <= Math.Round(colLimit); r++)
            {
                for (int vx = r * -1; vx <= r; vx++)
                {
                    for (int vy = r * -1; vy <= r; vy++)
                    {
                        toReturn.AgentCoord = ApplyVector(new Position(vx, vy));

                        if (agentBoard.GetElementType(toReturn.AgentCoord.X, toReturn.AgentCoord.Y) == target)
                        {
                            toReturn.AgentReference.X = this.currentPosition.X + vx;
                            toReturn.AgentReference.Y = this.currentPosition.Y + vy;
                            toReturn.Found = true;
                            return toReturn;
                        }
                    }
                }
            }
            // if the AgentType is not found, return a position (-100, -100) as a flag error
            return toReturn;
        }
    }
}
