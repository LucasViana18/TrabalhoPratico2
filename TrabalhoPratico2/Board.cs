using System;
using System.Collections.Generic;
using System.Text;

namespace TrabalhoPratico2
{
    public class Board
    {
        // Variables and Properties
        private Params boardParams;
        private GameElement[,] currentBoard;
        private List<Agent> agents;
        private Random rnd;
        private Agent agent;

        public int NumberColumns { get; private set; }
        public int NumberRows { get; private set; }
        public Position Playing { get; set; }
        public Position Enemy { get; set; }

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
        }

        public void StartBoard()
        {
            for (int r = 0; r < NumberRows; r++)
            {
                for (int c = 0; c < NumberColumns; c++)
                {
                    currentBoard[c, r] = new GameElement(c, r);
                }
            }

            CreateZombies();
            CreateHumans();
        }

        private Position FindFreeSpot()
        {
            int localCol, localRow;
            do
            {
                localCol = rnd.Next(0, NumberColumns - 1);
                localRow = rnd.Next(0, NumberRows - 1);
            } while (GetElementInPosition(localCol, localRow).
            ElementType != Type.Empty);

            return new Position(localCol, localRow);
        }

        private void CreateZombies()
        {
            Position localPosition;
            Zombie localZombie;

            for (int i = 0; i < boardParams.BotZ; i++)
            {
                localPosition = FindFreeSpot();

                localZombie = new Zombie(localPosition.X, localPosition.Y,
                    boardParams, this, NewAgentId());
                agents.Add(localZombie);
                currentBoard[localPosition.X, localPosition.Y] = localZombie;
            }

        }

        private void CreateHumans()
        {
            Position localPosition;
            Human localHuman;

            for (int i = 0; i < boardParams.BotH; i++)
            {
                localPosition = FindFreeSpot();

                localHuman = new Human(localPosition.X, localPosition.Y,
                    boardParams, this, NewAgentId());
                agents.Add(localHuman);
                currentBoard[localPosition.X, localPosition.Y] = localHuman;
            }

        }

        public GameElement GetElementInPosition(int col, int row)
        {
            return currentBoard[col, row];
        }

        public Type GetElementType(int col, int row)
        {
            return currentBoard[col, row].ElementType;
        }

        public Position ToroidalConvert(int col, int row)
        {
            Position toReturn = new Position(col, row);

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

        public Agent GetAgent(int index)
        {
            if (index < 0 || index > agents.Count - 1)
            {
                return agents[0];
            }
            else
            {
                return agents[index];
            }
        }

        public void MoveAgent(Agent whatAgent, Position newPosition)
        {
            Position currentAgentPos;

            currentAgentPos = whatAgent.AgentPosition;
            currentBoard[currentAgentPos.X, currentAgentPos.Y] =
                new GameElement(currentAgentPos.X, currentAgentPos.Y);
            currentBoard[newPosition.X, newPosition.Y] = whatAgent;
        }

        public void ChangeAgentType(Position posAgent)
        {
            Agent localAgent, whatAgent;

            whatAgent = GetElementInPosition(posAgent.X, posAgent.Y) as Agent;

            localAgent = new Zombie(whatAgent.AgentPosition.X, whatAgent.AgentPosition.Y, boardParams, this, whatAgent.AgentID);

            // copy the type of movement (auto or manual) to the new object
            agents.RemoveAll(item => item.AgentID == whatAgent.AgentID);
            agents.Add(localAgent);
            currentBoard[localAgent.AgentPosition.X, localAgent.AgentPosition.Y] = localAgent;
        }

        public string NewAgentId()
        {
            int id;
            string idHex;
            do
            {
                id = rnd.Next(1, 255);
                idHex = id.ToString("X");
                idHex = idHex.Length == 1 ? "0" + idHex : idHex;
            } while (agents.Exists(item => item.AgentID == idHex));
            return idHex;
        }
    }
}
