using System;
using System.Collections.Generic;
using System.Text;

namespace TrabalhoPratico2
{
    class Board
    {
        // Variables
        private Type[,] currentBoard;
        private Type[,] background;
        private Random rnd;

        // Constructor
        public Board(int width, int height, int numOfHumans, int numOfZombies)
        {
            rnd = new Random();
            background = new Type[width, height];

            // Ciclos for para criar o tabuleiro vazio
            for (int r = 0; r < height; r++)
            {
                for (int c = 0; c < width; c++)
                {
                    // Fisher Yates Shuffle
                    // rnd.Next();
                    background[r, c] = Type.Empty;

                }
            }

            currentBoard = new Type[width, height];

            // Ciclos for para percorrer e adicionar por todo o tabuleiro os 
            // humanos e zombies propostos

        }

        /*
        public Type VerifyNeighbors()
        {
            int radius = 1;
        }
        */
    }
}
