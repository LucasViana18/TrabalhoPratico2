﻿using System;
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

        protected List<Position> vectorMove;
        protected List<Position> vectorTop;
        protected List<Position> vectorBottom;
        protected List<Position> vectorLeft;
        protected List<Position> vectorRight;

        protected List<Position> vectorTopLeft;
        protected List<Position> vectorTopRight;
        protected List<Position> vectorBottomLeft;
        protected List<Position> vectorBottomRight;

        public string AgentID { get; }
        public Position AgentPosition { get { return currentPosition; } }
        public Position lastMovement { get; protected set; }
        public ControlType Control { get; set; }

        // Constructor
        public Agent
            (int startX, int startY, Params par, Board board, string agentID, ControlType control) :
            base(startX, startY)
        {
            vectorMove = new List<Position>();
            vectorTop = new List<Position>();
            vectorBottom = new List<Position>();
            vectorLeft = new List<Position>();
            vectorRight = new List<Position>();

            vectorTopLeft = new List<Position>();
            vectorTopRight = new List<Position>();
            vectorBottomLeft = new List<Position>();
            vectorBottomRight = new List<Position>();

            chosenMove = new Random();
            lastMovement = new Position(-1, -1);
            target = Type.Empty;

            agentPar = par;
            agentBoard = board;
            AgentID = agentID;
            Control = control;
            SetVectors();

        }

        private void SetVectors()
        {
            // Set VectorBottom
            for (int x = -1; x <= 1; x++)
            {
                vectorBottom.Add(new Position(x, 1));
                vectorMove.Add(new Position(x, 1));
            }

            vectorMove.Add(new Position(-1, 0));
            vectorMove.Add(new Position(0, 0));
            vectorMove.Add(new Position(1, 0));

            // Set VectorTop
            for (int x = -1; x <= 1; x++)
            {
                vectorTop.Add(new Position(x, -1));
                vectorMove.Add(new Position(x, -1));
            }

            // Set VectorLeft
            for (int y = -1; y <= 1; y++)
                vectorLeft.Add(new Position(-1, y));

            // Set VectorRight
            for (int y = -1; y <= 1; y++)
                vectorRight.Add(new Position(1, y));

            // Set VectorBottomLeft
            for (int y = 0; y <= 1; y++)
                vectorBottomLeft.Add(new Position(-1, y));
            vectorBottomLeft.Add(new Position(0, 1));

            // Set vectorBottomRight
            for (int y = 0; y <= 1; y++)
                vectorBottomRight.Add(new Position(1, y));
            vectorBottomRight.Add(new Position(0, 1));

            // Set vectorTopLeft
            for (int y = -1; y <= 0; y++)
                vectorTopLeft.Add(new Position(-1, y));
            vectorTopLeft.Add(new Position(0, -1));

            // Set vectorTopRight
            for (int y = -1; y <= 0; y++)
                vectorTopRight.Add(new Position(1, y));
            vectorTopRight.Add(new Position(0, -1));
        }

        public virtual void Move(FoundAgentDetails agent, Render render) { }

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

        public Position ManualBehavior(Render render)
        {
            int index;
            char key;
            render.Renderer(agentBoard, $"Please enter a move direction for {GetSymbol()} at numberpad: ");
            do
            {
                key = Console.ReadKey().KeyChar;
                if (key == 'q')
                {
                    render.Renderer(agentBoard, "Thanks for playing!");
                    Environment.Exit(0);
                }
                if (char.IsDigit(key))
                {
                    index = Convert.ToInt32(key);
                    if (index >= 1 && index <= 9 && index != 5)
                    {
                        return ApplyVector(vectorMove[index - 1]);
                    }
                }

            } while (true);

            //Position chosenPosition = this.AgentPosition;
            //int direction;
            //Console.Write("Please enter a move direction at numberpad: ");
            //direction = Convert.ToInt32(Console.ReadKey());
            //switch (direction)
            //{
            //    case 1:
            //        chosenPosition.X--;
            //        chosenPosition.Y++;
            //        return ApplyVector(chosenPosition);
            //    case 2:
            //        chosenPosition.Y++;
            //        return ApplyVector(chosenPosition);
            //    case 3:
            //        chosenPosition.X++;
            //        chosenPosition.Y++;
            //        return ApplyVector(chosenPosition);
            //    case 4:
            //        chosenPosition.X--;
            //        return ApplyVector(chosenPosition);
            //    case 5:
            //        return ApplyVector(chosenPosition);
            //    case 6:
            //        chosenPosition.X++;
            //        return ApplyVector(chosenPosition);
            //    case 7:
            //        chosenPosition.X--;
            //        chosenPosition.Y--;
            //        return ApplyVector(chosenPosition);
            //    case 8:
            //        chosenPosition.Y--;
            //        return ApplyVector(chosenPosition);
            //    case 9:
            //        chosenPosition.X++;
            //        chosenPosition.Y--;
            //        return ApplyVector(chosenPosition);
            //    default:
            //        Console.WriteLine("Not a valid direction, try again!");
            //        ManualBehavior(target);
            //        return ApplyVector(chosenPosition);
            //}
        }

        protected Position AutomaticBehaviour(FoundAgentDetails enemyAgent, bool attract)
        {
            Position chosenPosition;
            List<Position> toMove = new List<Position>();

            if (enemyAgent.AgentReference.X == this.currentPosition.X)
            {
                if (enemyAgent.AgentReference.Y > this.currentPosition.Y)
                {
                    toMove = attract ? vectorBottom : vectorTop;
                }
                else
                {
                    toMove = attract ? vectorTop : vectorBottom;
                }
            }
            else if (enemyAgent.AgentReference.Y == this.currentPosition.Y)
            {
                if (enemyAgent.AgentReference.X > this.currentPosition.X)
                {
                    toMove = attract ? vectorRight : vectorLeft;
                }
                else
                {
                    toMove = attract ? vectorLeft : vectorRight;
                }
            }
            else  // X and Y are different = diagonal 
            {
                if (enemyAgent.AgentReference.Y < this.currentPosition.Y)
                {
                    if (enemyAgent.AgentReference.X < this.currentPosition.X)
                        toMove = attract ?
                        vectorTopLeft : vectorBottomRight;
                    else
                        toMove = attract ?
                        vectorTopRight : vectorBottomLeft;
                }
                else
                {
                    if (enemyAgent.AgentReference.X < this.currentPosition.X)
                        toMove = attract ?
                        vectorBottomLeft : vectorTopRight;
                    else
                        toMove = attract ?
                        vectorBottomRight : vectorTopLeft;
                }
            }

            chosenPosition =
                ApplyVector(toMove[chosenMove.Next(0, toMove.Count)]);
            return chosenPosition;

        }
    }
}
