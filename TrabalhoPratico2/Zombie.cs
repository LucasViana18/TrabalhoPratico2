using System;
using System.Collections.Generic;
using System.Text;

namespace TrabalhoPratico2
{
    public class Zombie : Agent
    {
        // Constructor
        public Zombie
            (int startX, int startY, Params par, Board board, string agentID, 
            ControlType control) :
            base(startX, startY, par, board, agentID, control)
        {
            elementType = Type.Zombie;
            target = Type.Human;
        }

        // Methods

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

        public override void Move(FoundAgentDetails human, Render render)
        {
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
                        LastMovement = ManualBehavior(render);
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
                            (agentBoard, "Press anykey to convert human");
                        Console.ReadKey();
                    }
                    // Convert the human
                    render.Renderer(agentBoard, "The zombie " + GetSymbol() +
                        " converted " + agentBoard.GetElementInPosition
                        (human.AgentCoord.X, human.AgentCoord.Y).GetSymbol() 
                        + "!");
                    agentBoard.ChangeAgentType(human.AgentCoord);
                }
            }
        }

        public override string GetSymbol()
        {
            return Control == ControlType.Automatic ? "z" + AgentID :
                "Z" + AgentID;
        }
    }
}
