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


        // Class instances


        // Constructor
        public Game(Params par)
        {
            gameParams = par;
        }

        private void GameStart()
        {
            board = new Board(gameParams);
            board.StartBoard();

        }


        // Loop
        public void GameLoop()
        {
            GameStart();
            for (int i = 1; i <= gameParams.MaxTurns; i++)
            {
                // Coding




            }
        }

    }
}
