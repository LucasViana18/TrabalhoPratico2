using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TrabalhoPratico2
{
    class Human : Agent
    {
        // Variables/Properties

        // Constructor
        public Human
            (int startX, int startY, Params par, Board board, string agentID, ControlType control) :
            base(startX, startY, par, board, agentID, control)
        {
            this.elementType = Type.Human;
            this.target = Type.Zombie;
        }

        public override void Move(FoundAgentDetails zombie, Render render)
        {
            // Zombie found
            if (zombie.Found && agentBoard.GetElementType
                (zombie.AgentCoord.X, zombie.AgentCoord.Y) == Type.Zombie)
            {
                if (Control == ControlType.Manual)
                {
                    lastMovement = ManualBehavior(render);
                }
                else
                {
                    lastMovement = AutomaticBehaviour(zombie, false);
                }

                if (agentBoard.GetElementType(lastMovement.X, lastMovement.Y)
                    == Type.Empty)
                {
                    agentBoard.MoveAgent(this, lastMovement);
                    currentPosition.X = lastMovement.X;
                    currentPosition.Y = lastMovement.Y;
                }
            }
        }

        public override string GetSymbol()
        {
            return Control == ControlType.Automatic ? "h" + AgentID : "H" + AgentID;
        }
    }
}