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

        public override string GetSymbol()
        {
            return "h" + AgentID;
        }

        public override void Move(FoundAgentDetails zombie, ControlType control)
        {
            // Zombie found
            if (zombie.Found && agentBoard.GetElementType
                (zombie.AgentCoord.X, zombie.AgentCoord.Y) == Type.Zombie)
            {
                if (control == ControlType.Manual)
                {
                    lastMovement = ManualBehavior(zombie);
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
    }
}