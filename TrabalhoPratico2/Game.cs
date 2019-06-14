using System;
using System.Collections.Generic;
using System.Text;

namespace TrabalhoPratico2
{
    class Game
    {
        // Variables and class instances
        private Params gameParams;
        private Board board;
        private Render render;
        private Agent agentToMove;

        private readonly int numberAgents;
        private int currentTurn;
        private bool game;

        // Constructor
        public Game(Params par)
        {
            board = new Board(par);
            render = new Render();
            gameParams = par;
            numberAgents = par.BotH + par.BotZ;
        }

        // Methods

        // Loop
        public void GameLoop()
        {
            // Local variables
            FoundAgentDetails target;
            Position nullPosition = new Position(-1, -1);
            currentTurn = 1;
            game = true;

            // Initialize board
            board.StartBoard();

            // Loop of turns
            while (game && currentTurn <= board.boardParams.MaxTurns)
            {
                if (!game) return;
                // Identify the number of turns
                render.Renderer(board, $"Turn: {currentTurn}. " +
                    $"Press Enter to continue.");
                Console.ReadLine();
                // Shuffle the list of agents every turn
                board.Shuffle();

                // Loop of each agent
                for (int a = 0; a < numberAgents; a++)
                {
                    game = board.WinChecker();
                    // Zombies win
                    if (!game)
                    {
                        Console.WriteLine("The horde of zombies overcame the"+
                            " humans...");
                        return;
                    }
                    board.Enemy = nullPosition;
                    // Get the agent to move
                    agentToMove = board.GetAgent(a);
                    board.Playing = agentToMove.AgentPosition;

                    render.Renderer(board, $"Will be moved: " +
                        $"{agentToMove.GetSymbol()}");
                    System.Threading.Thread.Sleep(1500);

                    // (Temporary) Case picked agent is human
                    if (agentToMove.ElementType != Type.Empty)
                    {
                        // Find the closest target
                        target = agentToMove.FindNearAgent();

                        // Case found
                        if (target.Found)
                        {
                            board.Enemy = target.AgentCoord;
                            render.Renderer(board, "Closest Enemy found: " +
                                board.GetElementInPosition
                                (target.AgentCoord.X, target.AgentCoord.Y).
                                GetSymbol());
                            System.Threading.Thread.Sleep(1500);
                            // Move the picked agent
                            agentToMove.Move(target, render);
                            board.AgentMoved(a);
                        }

                        // Post action
                        render.Renderer(board, $"Agent " +
                            $"{agentToMove.GetSymbol()} " +
                            $"moved to: {agentToMove.LastMovement.X}, " +
                            $"{agentToMove.LastMovement.Y}\n");
                        System.Threading.Thread.Sleep(1500);
                    }
                }
                // Humans win
                if (game && currentTurn == gameParams.MaxTurns)
                {
                    Console.WriteLine("The humans have escaped the horde of" +
                        " zombies!");
                }
                currentTurn++;
            }
        }
    }
}
