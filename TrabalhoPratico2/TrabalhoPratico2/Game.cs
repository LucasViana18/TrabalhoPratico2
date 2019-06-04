using System;
using System.Collections.Generic;
using System.Text;

namespace TrabalhoPratico2
{
    class Game
    {
        // Variables
        private int currentTurn;

        // Class instances
        private Params gameParams;
        private Board board;
        private Render render;

        // Constructor
        public Game(Params par)
        {
            gameParams = par;
            board = new Board(gameParams);
            render = new Render();
        }

        public void GameStart()
        {
            board.StartBoard();
            GameLoop();
        }


        // Loop
        private void GameLoop()
        {
            while (currentTurn >= gameParams.MaxTurns)
            {
                render.Renderer(board, "");
                Console.WriteLine(board.GetElementInPosition(3, 3));
                Console.ReadLine();
                // Coding
                Console.WriteLine(board.GetElementInPosition(3, 3));
                Console.ReadLine();
            }
        }
    }
}
