using System;
using System.Collections.Generic;

namespace TrabalhoPratico2
{
    /// <summary>
    /// Class that update and storage every change made in the game
    /// </summary>
    public class Board
    {
        // Instance variables and properties
        public readonly Params boardParams;
        private readonly GameElement[,] currentBoard;
        private List<Agent> agents;
        private List<int> movedAgents;
        private Random rnd;

        public int NumberColumns { get; private set; }
        public int NumberRows { get; private set; }
        public Position Playing { get; set; }
        public Position Enemy { get; set; }

        // Constructor
        /// <summary>
        /// Board constructor
        /// </summary>
        /// <param name="p">Params variable</param>
        public Board(Params p)
        {
            rnd = new Random();
            boardParams = p;
            NumberColumns = boardParams.MaxX;
            NumberRows = boardParams.MaxY;
            currentBoard = new GameElement[NumberColumns, NumberRows];
            agents = new List<Agent>();
            Playing = new Position(-1, -1);
            Enemy = new Position(-1, -1);
            movedAgents = new List<int>();
        }

        // Methods
        /// <summary>
        /// Initialize board
        /// </summary>
        public void StartBoard()
        {
            // Fill the board with GameElement instances on game start
            for (int r = 0; r < NumberRows; r++)
            {
                for (int c = 0; c < NumberColumns; c++)
                {
                    currentBoard[c, r] = new GameElement(c, r);
                }
            }
            // Create automatic agents
            CreateZombies(ControlType.Automatic);
            CreateHumans(ControlType.Automatic);
            // Create manual agents
            CreateZombies(ControlType.Manual);
            CreateHumans(ControlType.Manual);
        }
        /// <summary>
        /// Shufle agent list
        /// </summary>
        public void Shuffle()
        {
            // Local variables
            int n;
            Agent agentItem;

            n = agents.Count;
            for (int i = 0; i < n; i++)
            {
                int r = i + rnd.Next(n - i);
                agentItem = agents[r];
                agents[r] = agents[i];
                agents[i] = agentItem;
            }

            // Clear the moved agents list
            movedAgents.Clear();
        }
        /// <summary>
        /// Add to a list agents that have already been moved
        /// </summary>
        /// <param name="whatIndex">Agent that has moved</param>
        public void AgentMoved(int whatIndex)
        {
            // Add moved agents to the list
            movedAgents.Add(whatIndex);
        }
        /// <summary>
        /// Identifies if agent moved
        /// </summary>
        /// <param name="whatAgent">Agent to be checked</param>
        /// <returns>If whatAgent moved</returns>
        public bool WasMoved(GameElement whatAgent)
        {
            // Local variables
            Agent localAgent;
            int indexAgent;

            // Case the selected element is not an agent, return false
            if (!(whatAgent is Agent))
                return false;

            // Cast of variable
            localAgent = whatAgent as Agent;
            // Pick the index of the moved agent
            indexAgent = agents.FindIndex(item => item.Equals(localAgent));
            // Return a condition if exists on the moved list the agent with
            // the certain index
            return movedAgents.Exists(item => item == indexAgent);
        }
        /// <summary>
        /// Find free position at currentBoard
        /// </summary>
        /// <returns>Free position</returns>
        private Position FindFreeSpot()
        {
            // Local variables
            int localCol, localRow;

            // Loop that goes through every spot to verify if its free
            do
            {
                localCol = rnd.Next(0, NumberColumns - 1);
                localRow = rnd.Next(0, NumberRows - 1);
            } while (GetElementInPosition(localCol, localRow).
            ElementType != Type.Empty);

            return new Position(localCol, localRow);
        }
        /// <summary>
        /// Create zombie and add it to agents list
        /// </summary>
        /// <param name="control">Controlled by player or AI</param>
        private void CreateZombies(ControlType control)
        {
            // Local variables
            Position localPosition;
            Zombie localZombie;
            int nZombies;

            // Verify if the number of zombies created are automatic or manual
            nZombies = control == ControlType.Automatic ? 
                boardParams.BotZ - boardParams.UserZ : boardParams.UserZ;

            for (int i = 0; i < nZombies; i++)
            {
                // Local of the new spawn position
                localPosition = FindFreeSpot();
                // Create/Instantiate a zombie
                localZombie = new Zombie(localPosition.X, localPosition.Y,
                    this, NewAgentId(), control);
                agents.Add(localZombie);
                currentBoard[localPosition.X, localPosition.Y] = localZombie;
            }

        }
        /// <summary>
        /// Create human and add it to agents list
        /// </summary>
        /// <param name="control">Controlled by player or AI</param>
        private void CreateHumans(ControlType control)
        {
            // Local variables
            Position localPosition;
            Human localHuman;
            int nHumans;

            // Verify if the number of humans created are automatic or manual
            nHumans = control == ControlType.Automatic ? 
                boardParams.BotH - boardParams.UserH : boardParams.UserH;

            for (int i = 0; i < nHumans; i++)
            {
                // Local of the new spawn position
                localPosition = FindFreeSpot();
                // Create/Instantiate a human
                localHuman = new Human(localPosition.X, localPosition.Y,
                    this, NewAgentId(), control);
                agents.Add(localHuman);
                currentBoard[localPosition.X, localPosition.Y] = localHuman;
            }

        }
        /// <summary>
        /// Get element at passed position
        /// </summary>
        /// <param name="col">Position x</param>
        /// <param name="row">Postion y</param>
        /// <returns>Element at currentBoard</returns>
        public GameElement GetElementInPosition(int col, int row)
        {
            return currentBoard[col, row];
        }
        /// <summary>
        /// Get element Type at passed position
        /// </summary>
        /// <param name="col">Position x</param>
        /// <param name="row">Position y</param>
        /// <returns>Type at currentBoard</returns>
        public Type GetElementType(int col, int row)
        {
            return currentBoard[col, row].ElementType;
        }
        /// <summary>
        /// Convert board condinates to apply Toroidal effect
        /// </summary>
        /// <param name="col">Position x</param>
        /// <param name="row">Position y</param>
        /// <returns>Position with Toroidal effect applied</returns>
        public Position ToroidalConvert(int col, int row)
        {
            // Local variable
            Position toReturn;

            toReturn = new Position(col, row);

            toReturn.X = (toReturn.X >= NumberColumns) ?
                toReturn.X - NumberColumns : toReturn.X;
            toReturn.Y = (toReturn.Y >= NumberRows) ?
                toReturn.Y - NumberRows : toReturn.Y;
            toReturn.X = (toReturn.X < 0) ?
                toReturn.X + NumberColumns : toReturn.X;
            toReturn.Y = (toReturn.Y < 0) ?
                toReturn.Y + NumberRows : toReturn.Y;

            return toReturn;
        }
        /// <summary>
        /// Get agent at agents list
        /// </summary>
        /// <param name="index">List index</param>
        /// <returns>Selected agent</returns>
        public Agent GetAgent(int index)
        {
            // Safety measure
            if (index < 0 || index > agents.Count - 1)
            {
                return agents[0];
            }
            else
            {
                return agents[index];
            }
        }
        /// <summary>
        /// Move agent at currentBoard
        /// </summary>
        /// <param name="whatAgent">Agent to be moved</param>
        /// <param name="newPosition">Position to be moved to</param>
        public void MoveAgent(Agent whatAgent, Position newPosition)
        {
            // Local variable
            Position currentAgentPos;

            // Update of the board for the agent move
            currentAgentPos = whatAgent.AgentPosition;
            currentBoard[currentAgentPos.X, currentAgentPos.Y] =
                new GameElement(currentAgentPos.X, currentAgentPos.Y);
            currentBoard[newPosition.X, newPosition.Y] = whatAgent;
        }
        /// <summary>
        /// Convert human agent to zombie
        /// </summary>
        /// <param name="posAgent">Position of converted agent</param>
        public void ChangeAgentType(Position posAgent)
        {
            // Local variable
            Agent localAgent, whatAgent;

            // Cast of the variable
            whatAgent = GetElementInPosition(posAgent.X, posAgent.Y) as Agent;

            localAgent = new Zombie(whatAgent.AgentPosition.X, 
                whatAgent.AgentPosition.Y, this, 
                whatAgent.AgentID, ControlType.Automatic);

            // Remove the last agent(human) and adds a new agent(zombie) but
            // keeps the ID
            agents.RemoveAll(item => item.AgentID == whatAgent.AgentID);
            agents.Add(localAgent);
            currentBoard
                [localAgent.AgentPosition.X, localAgent.AgentPosition.Y] 
                = localAgent;
        }
        /// <summary>
        /// Generates new hexadecimal ID
        /// </summary>
        /// <returns>Hexadecimal ID</returns>
        public string NewAgentId()
        {
            // Local variable
            int id;
            string idHex;

            // Sorts out a different hexadecimal ID
            do
            {
                id = rnd.Next(1, 255);
                idHex = id.ToString("X");
                idHex = idHex.Length == 1 ? "0" + idHex : idHex;
            } while (agents.Exists(item => item.AgentID == idHex));

            return idHex;
        }
        /// <summary>
        /// Check for win condition
        /// </summary>
        /// <returns>If game is over</returns>
        public bool WinChecker()
        {
            foreach(Agent agent in agents)
            {
                if (agent is Human h) return true;
            }

            return false;
        }
    }
}
