using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TrabalhoPratico2
{
    /// <summary>
    /// Have every action related to a human agent
    /// </summary>
    class Human : Agent
    {
        // Constructor
        /// <summary>
        /// Human constructor
        /// </summary>
        /// <param name="startX">Start position x</param>
        /// <param name="startY">Start position y</param>
        /// <param name="board">Board instance at Game class</param>
        /// <param name="agentID">Hexadecimal ID</param>
        /// <param name="control">Automatic or manual</param>
        public Human
            (int startX, int startY, Board board, string agentID, 
            ControlType control) :
            base(startX, startY, board, agentID, control)
        {
            elementType = Type.Human;
            target = Type.Zombie;
        }

        // Methods
        /// <summary>
        /// Move human to selected position
        /// </summary>
        /// <param name="zombie">Nearest agent</param>
        /// <param name="render">Render instance at Game class</param>
        /// <param name="game">To be passed at ManualBehavior</param>
        public override void Move
            (FoundAgentDetails zombie, Render render, Game game)
        {
            // Zombie found
            if (zombie.Found && agentBoard.GetElementType
                (zombie.AgentCoord.X, zombie.AgentCoord.Y) == Type.Zombie)
            {
                // If the control is manual or automatic
                if (Control == ControlType.Manual)
                {
                    LastMovement = ManualBehavior(render, game);
                }
                else
                {
                    LastMovement = AutomaticBehaviour(zombie, false);
                }

                // If the spot to move is free
                if (agentBoard.GetElementType(LastMovement.X, LastMovement.Y)
                    == Type.Empty)
                {
                    agentBoard.MoveAgent(this, LastMovement);
                    currentPosition.X = LastMovement.X;
                    currentPosition.Y = LastMovement.Y;
                }
            }
        }
        /// <summary>
        /// Get symbol
        /// </summary>
        /// <returns>Symbol</returns>
        public override string GetSymbol()
        {
            return Control == ControlType.Automatic ? "h" + AgentID :
                "H" + AgentID;
        }
    }
}