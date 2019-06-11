﻿using System;
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
        private Random rnd;

        private readonly int numberAgents;
        private bool[] sequence;

        // Constructor
        public Game(Params par)
        {
            board = new Board(par);
            render = new Render();
            rnd = new Random();
            gameParams = par;
            numberAgents = par.BotH + par.BotZ;
            sequence = new bool[numberAgents];
        }

        // Methods
        private int GetNextAgent()
        {
            // Variables
            int newAgent;

            // Choose, randomly, a new agent for it to play
            do
            {
                newAgent = rnd.Next(0, numberAgents);

            } while (sequence[newAgent]);

            sequence[newAgent] = true;

            return newAgent;
        }

        private void ResetOrder()
        {
            // Resets to false every turn
            for (int i = 0; i < sequence.Length; i++)
            {
                sequence[i] = false;
            }
        }

        // Loop
        public void GameLoop()
        {
            // Variables
            FoundAgentDetails target;

            // Initialize board
            board.StartBoard();

            // Loop of turns
            for (int i = 1; i <= gameParams.MaxTurns; i++)
            {
                // Identify the number of turns
                render.Renderer(board, "Turn: " + i + ". Press Enter to continue.");
                Console.ReadLine();
                ResetOrder();

                // Loop of each agent
                for (int a = 1; a <= numberAgents; a++)
                {
                    // Get the agent to move
                    agentToMove = board.GetAgent(GetNextAgent());
                    render.Renderer(board, "Will be moved: " + agentToMove.GetSymbol());
                    System.Threading.Thread.Sleep(100);

                    // (Temporary) Case picked agent is human
                    if (agentToMove.ElementType != Type.Empty)
                    {
                        board.Playing = agentToMove.AgentPosition;
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
                            System.Threading.Thread.Sleep(100);
                            // Move the picked agent
                            agentToMove.Move(target);
                        }
                        // Post action
                        render.Renderer(board, "Agent " + agentToMove.GetSymbol() + " moved to: " + agentToMove.LastMovement.X + ", " + agentToMove.LastMovement.Y);
                        System.Threading.Thread.Sleep(100);
                    }
                }
            }
        }
    }
}
