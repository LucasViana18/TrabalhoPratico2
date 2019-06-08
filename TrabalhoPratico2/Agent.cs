using System;
using System.Collections.Generic;
using System.Text;

namespace TrabalhoPratico2
{
    public class Agent : GameElement
    {
        // Variables and Properties
        protected List<Position> agentMoves;
        protected List<Position> possibleMoves;
        protected List<Position> neighR1;
        protected int myID = 0;
        protected Params GamePar;
        protected Board agentBoard;
        protected int[,] coordR1;
        protected int[,] vectorTop;
        protected int[,] vectorBottom;
        protected int[,] vectorTopLeft;
        protected int[,] vectorTopRight;
        protected int[,] vectorBottomLeft;
        protected int[,] vectorBottomRight;

        public string AgentID { get { return myID.ToString("00"); } }
        public Position AgentPosition { get { return currentPosition; } }

        // Constructor
        public Agent(int startX, int startY, Params par, int lastAgentID) : base(startX, startY)
        {
            neighR1 = new List<Position>();
            agentMoves = new List<Position>();
            possibleMoves = new List<Position>();

            GamePar = par;
            myID = lastAgentID + 1;
            vectorTop = new int[,] { { -1, -1 }, { 0, -1 }, { 1, -1 } };
            vectorBottom = new int[,] { { -1, 1 }, { 0, 1 }, { 1, 1 } };
            vectorTopLeft = new int[,] { { -1, 0 }, { -1, -1 } };
            vectorTopRight = new int[,] { { 1, 0 }, { 1, -1 } };
            vectorBottomLeft = new int[,] { { -1, 0 }, { -1, 1 } };
            vectorBottomRight = new int[,] { { 1, 0 }, { 1, 1 } };

        }

        // Methods to identify the coordenates of the Neighbour
        protected Position GetNewNeighR1(int i)
        {
            Position localPos = new Position(currentPosition.X, currentPosition.Y);
            switch (i)
            {
                case 1:
                    localPos.X -= 1;
                    localPos.Y += 1;
                    break;
                case 2:
                    localPos.Y += 1;
                    break;
                case 3:
                    localPos.X += 1;
                    localPos.Y += 1;
                    break;
                case 4:
                    localPos.X -= 1;
                    break;
                case 6:
                    localPos.X += 1;
                    break;
                case 7:
                    localPos.X -= 1;
                    localPos.Y -= 1;
                    break;
                case 8:
                    localPos.Y -= 1;
                    break;
                case 9:
                    localPos.X += 1;
                    localPos.Y -= 1;
                    break;

                default:
                    break;
            }

            // Matrix borders control
            localPos.X = (localPos.X > GamePar.MaxX) ? localPos.X - GamePar.MaxX
                : localPos.X;
            localPos.Y = (localPos.Y > GamePar.MaxY) ? localPos.Y - GamePar.MaxY
                : localPos.Y;
            localPos.X = (localPos.X < 0) ? localPos.X + GamePar.MaxX
                : localPos.X;
            localPos.Y = (localPos.Y < 1) ? localPos.Y + GamePar.MaxY
                : localPos.Y;

            return localPos;
        }

        protected Position GetNewNeighR1(int i, Position pos)
        {
            Position localPos = new Position(pos.X, pos.Y);
            switch (i)
            {
                case 1:
                    localPos.X -= 1;
                    localPos.Y += 1;
                    break;
                case 2:
                    localPos.Y += 1;
                    break;
                case 3:
                    localPos.X += 1;
                    localPos.Y += 1;
                    break;
                case 4:
                    localPos.X -= 1;
                    break;
                case 6:
                    localPos.X += 1;
                    break;
                case 7:
                    localPos.X -= 1;
                    localPos.Y -= 1;
                    break;
                case 8:
                    localPos.Y -= 1;
                    break;
                case 9:
                    localPos.X += 1;
                    localPos.Y -= 1;
                    break;

                default:
                    break;
            }

            // Matrix borders control
            localPos.X = (localPos.X > GamePar.MaxX) ? localPos.X - GamePar.MaxX
                : localPos.X;
            localPos.Y = (localPos.Y > GamePar.MaxY) ? localPos.Y - GamePar.MaxY
                : localPos.Y;
            localPos.X = (localPos.X < 0) ? localPos.X + GamePar.MaxX
                : localPos.X;
            localPos.Y = (localPos.Y < 1) ? localPos.Y + GamePar.MaxY
                : localPos.Y;

            return localPos;
        }

        protected void UpdateNeighR1()
        {
            neighR1.Clear();

            for (int i = 1; i <= 9; i++)
            {
                neighR1.Add(GetNewNeighR1(i));
            }
        }

        protected void UpdateNeighR1(Position pos)
        {
            neighR1.Clear();

            for (int i = 1; i <= 9; i++)
            {
                neighR1.Add(GetNewNeighR1(i, pos));
            }
        }
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
        public virtual void Move()
        {
            UpdateNeighR1();
            AgentPosition.X--;
        }

        public Position VerifyOtherPosition() // other is placeholder
        {
            // Percorrer as 8 posições adjacentes (Moore de raio 1) ao character
            // para verificar se alguma delas contém um character de tipo "contrário",
            // aumentando o raio caso não encontre nenhum com o raio atual

            return AgentPosition;
            // Caso encontre um character de tipo "contrário", retorna a posição do
            // mesmo (? - tirar dúvida?)
        }

        protected Position FindNear(Type agentType)
        {
            Position toReturn;

            int localCol, localRow;
            for (int i = 0; i < coordR1.GetLength(0); i++)
            {
                localCol = this.currentPosition.X + coordR1[i, 0];
                localRow = this.currentPosition.Y + coordR1[i, 1];
                toReturn = agentBoard.ToroidalConvert(localCol, localRow);

                if (agentBoard.GetElementType(toReturn.X, toReturn.Y) == agentType)
                {
                    return toReturn;
                }
            }

            // if the AgentType is not found, return a position (-100, -100)
            return new Position(-100, -100);
        }
    }
}
