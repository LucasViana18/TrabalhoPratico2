using System;
using System.Collections.Generic;
using System.Text;

namespace TrabalhoPratico2
{
    class Game
    {
        // Variables
        private Params gameParams;
        private Board board;
        private Render render;


        // Class instances


        // Constructor
        public Game(Params par)
        {
            gameParams = par;
            board = new Board(gameParams);
            render = new Render();
        }

        private void GameStart()
        {
            board.StartBoard();
        }


        // Loop
        public void GameLoop()
        {
            GameStart();
            render.Renderer(board, "");
            for (int i = 1; i <= gameParams.MaxTurns; i++)
            {
                // Coding




            }
        }

    }
}
