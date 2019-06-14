using System;
using System.Collections.Generic;
using System.Text;

namespace TrabalhoPratico2
{
    public class Zombie : Agent
    {
        // Variables/Properties

        // Constructor
        public Zombie
            (int startX, int startY, Params par, Board board, string agentID, 
            ControlType control) :
            base(startX, startY, par, board, agentID, control)
        {
            this.elementType = Type.Zombie;
            this.target = Type.Human;
        }

        // Methods
        public bool HumanNear(FoundAgentDetails human)
        {
            // if the zombie detects a human in radius of 1, he attacks,
            // turning him into a zombie (maybe delete said player and replace
            // him with a zombie in the same position)

            int difX = Math.Abs
                (this.currentPosition.X - human.AgentReference.X);
            int difY = Math.Abs
                (this.currentPosition.Y - human.AgentReference.Y);
            int toCompare = Math.Max(difX, difY);

            return (toCompare == 1);
        }

        public override void Move(FoundAgentDetails human, Render render)
        {
            // Human  found
            if (human.Found && agentBoard.GetElementType
                (human.AgentCoord.X, human.AgentCoord.Y) == Type.Human)  
            {
                if (!HumanNear(human))
                {
                    if (Control == ControlType.Manual)
                    {
                        lastMovement = ManualBehavior(render);
                    }
                    else
                    {
                        lastMovement = AutomaticBehaviour(human, true);
                    }
                    if (agentBoard.GetElementType
                        (lastMovement.X, lastMovement.Y) == Type.Empty)
                    {
                        agentBoard.MoveAgent(this, lastMovement);
                        currentPosition.X = lastMovement.X;
                        currentPosition.Y = lastMovement.Y;
                    }
                }
                else
                {
                    if (Control == ControlType.Manual)
                    {
                        render.Renderer
                            (agentBoard, "Press anykey to convert human");
                        Console.ReadKey();
                    }
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
