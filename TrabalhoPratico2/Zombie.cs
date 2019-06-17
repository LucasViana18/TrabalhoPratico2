using System;
using System.Collections.Generic;
using System.Text;

namespace TrabalhoPratico2
{
    /// <summary>
    /// Have every action related to a zombie agent
    /// </summary>
    public class Zombie : Agent
    {
        // Constructor
        /// <summary>
        /// Zombie constructor
        /// </summary>
        /// <param name="startX">Start position x</param>
        /// <param name="startY">Start position y</param>
        /// <param name="board">Board instance at Game class</param>
        /// <param name="agentID">Hexadecimal ID</param>
        /// <param name="control">Automatic or manual</param>
        public Zombie
            (int startX, int startY, Board board, string agentID, 
            ControlType control) :
            base(startX, startY, board, agentID, control)
        {
            elementType = Type.Zombie;
            target = Type.Human;
        }

        // Methods
        /// <summary>
        /// Detect human in the Moore neighborhood
        /// </summary>
        /// <param name="human">Human agent to be checked</param>
        /// <returns>If there is a human in the Moore neighborhood</returns>
        public bool HumanNear(FoundAgentDetails human)
        {
            // Detects if there is a human near in a radius of 1
            int difX = Math.Abs
                (currentPosition.X - human.AgentReference.X);
            int difY = Math.Abs
                (currentPosition.Y - human.AgentReference.Y);

            int toCompare = Math.Max(difX, difY);

            return (toCompare == 1);
        }
        /// <summary>
        /// Move zombie to selected position
        /// </summary>
        /// <param name="zombie">Nearest agent</param>
        /// <param name="render">Render instance at Game class</param>
        /// <param name="game">To be passed at ManualBehavior</param>
        public override void Move
            (FoundAgentDetails human, Render render, Game game)
        {
            ToIgnore = false;
            // Human found
            if (human.Found && agentBoard.GetElementType
                (human.AgentCoord.X, human.AgentCoord.Y) == Type.Human)  
            {
                // If it isn't near
                if (!HumanNear(human))
                {
                    // Check if the control is manual or automatic
                    if (Control == ControlType.Manual)
                    {
                        LastMovement = ManualBehavior(render, game);
                    }
                    else
                    {
                        LastMovement = AutomaticBehaviour(human, true);
                    }
                    // If the spot to move is free
                    if (agentBoard.GetElementType
                        (LastMovement.X, LastMovement.Y) == Type.Empty)
                    {
                        agentBoard.MoveAgent(this, LastMovement);
                        currentPosition.X = LastMovement.X;
                        currentPosition.Y = LastMovement.Y;
                    }
                }
                // If it's near
                else
                {
                    // Check if the control is manual
                    if (Control == ControlType.Manual)
                    {
                        render.Renderer
                            (agentBoard, "Press anykey to convert human",
                            game);
                        Console.ReadKey();
                    }
                    // Convert the human
                    agentBoard.ChangeAgentType(human.AgentCoord);
                    render.Renderer(agentBoard, "The zombie " + GetSymbol() +
                        " converted " + agentBoard.GetElementInPosition
                        (human.AgentCoord.X, human.AgentCoord.Y).GetSymbol()
                        + "!", game);
                    ToIgnore = true;
                    System.Threading.Thread.Sleep(1500);
                }
            }
        }
        /// <summary>
        /// Get symbol
        /// </summary>
        /// <returns>Symbol</returns>
        public override string GetSymbol()
        {
            return Control == ControlType.Automatic ? "z" + AgentID :
                "Z" + AgentID;
        }
    }
}
