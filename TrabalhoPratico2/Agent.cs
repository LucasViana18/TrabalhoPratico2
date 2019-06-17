using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TrabalhoPratico2
{
    public class Agent : GameElement
    {
        // Instance variables and properties
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
        public Position LastMovement { get; protected set; }
        public ControlType Control { get; set; }
        public bool ToIgnore { get; protected set; } = false;

        // Constructor
        public Agent
            (int startX, int startY, Params par, Board board, string agentID,
            ControlType control) :
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
            LastMovement = new Position(-1, -1);
            target = Type.Empty;

            agentPar = par;
            agentBoard = board;
            AgentID = agentID;
            Control = control;
            SetVectors();

        }

        // Methods

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

        public virtual void Move
            (FoundAgentDetails agent, Render render, Game game) { }

        // Apply a vector of direction to move the Agent and consider the
        // Toroidal process.
        protected Position ApplyVector(Position vector)
        {
            int localCol, localRow;

            localCol = currentPosition.X + vector.X;
            localRow = currentPosition.Y + vector.Y;

            return agentBoard.ToroidalConvert(localCol, localRow);
        }

        public FoundAgentDetails FindNearAgent()
        {
            FoundAgentDetails toReturn = new FoundAgentDetails
                (false, new Position(-1, -1), new Position(-1, -1));
            // Max search radius
            float colLimit = agentBoard.NumberColumns / 2;

            // Radius
            for (int r = 1; r <= Math.Round(colLimit); r++)
            {
                // Vector x
                for (int vx = r * -1; vx <= r; vx++)
                {
                    // Vector y
                    for (int vy = r * -1; vy <= r; vy++)
                    {
                        toReturn.AgentCoord = 
                            ApplyVector(new Position(vx, vy));

                        // Case it finds the target
                        if (agentBoard.GetElementType
                            (toReturn.AgentCoord.X, toReturn.AgentCoord.Y) 
                            == target)
                        {
                            toReturn.AgentReference.X = 
                                currentPosition.X + vx;

                            toReturn.AgentReference.Y = 
                                currentPosition.Y + vy;

                            toReturn.Found = true;

                            return toReturn;
                        }
                    }
                }
            }
            // if the AgentType is not found, return a position (-1, -1)
            // as a flag error
            return toReturn;
        }

        public Position ManualBehavior(Render render, Game game)
        {
            // Local variables
            int index;
            char key;

            // Show message for input
            render.Renderer(agentBoard, $"Please enter a move direction for "+
                $"{GetSymbol()} at numberpad: ", game);

            do
            {
                key = Console.ReadKey().KeyChar;

                // Quit the game
                if (key == 'q' || key == 'Q')
                {
                    render.Renderer(agentBoard, "Thanks for playing!", game);
                    Environment.Exit(0);
                }
                // Case the char is a digit, convert and apply the vectors
                if (char.IsDigit(key))
                {
                    index = int.Parse(key.ToString());
                    if (index >= 1 && index <= 9 && index != 5)
                    {
                        return ApplyVector(vectorMove[index - 1]);
                    }
                }
            } while (true);
        }

        protected Position AutomaticBehaviour
            (FoundAgentDetails enemyAgent, bool follow)
        {
            // Local variables
            Position chosenPosition;
            List<Position> toMove = new List<Position>();

            // The follow param will determine if the selected agent will
            // go after or run away from the enemy

            // Enemy in the same column
            if (enemyAgent.AgentReference.X == currentPosition.X)
            {
                if (enemyAgent.AgentReference.Y > currentPosition.Y)
                {
                    toMove = follow ? vectorBottom : vectorTop;
                }
                else
                {
                    toMove = follow ? vectorTop : vectorBottom;
                }
            }
            // Enemy in the same row
            else if (enemyAgent.AgentReference.Y == currentPosition.Y)
            {
                if (enemyAgent.AgentReference.X > currentPosition.X)
                {
                    toMove = follow ? vectorRight : vectorLeft;
                }
                else
                {
                    toMove = follow ? vectorLeft : vectorRight;
                }
            }
            // X and Y are different = diagonal
            else
            {
                if (enemyAgent.AgentReference.Y < currentPosition.Y)
                {
                    if (enemyAgent.AgentReference.X < currentPosition.X)
                        toMove = follow ?
                        vectorTopLeft : vectorBottomRight;
                    else
                        toMove = follow ?
                        vectorTopRight : vectorBottomLeft;
                }
                else
                {
                    if (enemyAgent.AgentReference.X < currentPosition.X)
                        toMove = follow ?
                        vectorBottomLeft : vectorTopRight;
                    else
                        toMove = follow ?
                        vectorBottomRight : vectorTopLeft;
                }
            }

            // Determine the final choice of movement
            chosenPosition =
                ApplyVector(toMove[chosenMove.Next(0, toMove.Count)]);

            return chosenPosition;
        }
    }
}
