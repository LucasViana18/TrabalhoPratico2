using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TrabalhoPratico2
{
    class Human : Agent
    {
        // Constructor
        public Human
            (int startX, int startY, Params par, Board board, string agentID, 
            ControlType control) :
            base(startX, startY, par, board, agentID, control)
        {
            elementType = Type.Human;
            target = Type.Zombie;
        }

        // Methods

        public override void Move(FoundAgentDetails zombie, Render render)
        {
            // Zombie found
            if (zombie.Found && agentBoard.GetElementType
                (zombie.AgentCoord.X, zombie.AgentCoord.Y) == Type.Zombie)
            {
                // If the control is manual or automatic
                if (Control == ControlType.Manual)
                {
                    LastMovement = ManualBehavior(render);
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

        public override string GetSymbol()
        {
            return Control == ControlType.Automatic ? "h" + AgentID :
                "H" + AgentID;
        }
    }
}