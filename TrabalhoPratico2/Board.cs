﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TrabalhoPratico2
{
    class Board
    {
        // Variables and Properties
        private Params boardParams;
        private GameElement[,] currentBoard;
        private List<Zombie> zombies;
        private List<Human> humans;
        private Random rnd;

        public int NumberColumns { get; private set; } = 0;
        public int NumberRows { get; private set; } = 0;

        public Board(Params p)
        {
            rnd = new Random();
            boardParams = p;
            NumberColumns = boardParams.MaxX;
            NumberRows = boardParams.MaxY;
            currentBoard = new GameElement[NumberColumns, NumberRows];
            zombies = new List<Zombie>();
            humans = new List<Human>();
        }

        public void StartBoard()
        {
            for (int r = 0; r < NumberRows; r++)
            {
                for (int c = 0; c < NumberColumns; c++)
                {
                    currentBoard[c, r] = new GameElement(c, r);
                }
            }

            CreateZombies();
            CreateHumans();
        }

        private Position FindFreeSpot()
        {
            int localCol, localRow;
            do
            {
                localCol = rnd.Next(0, NumberColumns - 1);
                localRow = rnd.Next(0, NumberRows - 1);
            } while (GetElementInPosition(localCol, localRow).ElementType != Type.Empty);

            return new Position(localCol, localRow);
        }

        private void CreateZombies()
        {
            Position localPosition;
            Zombie localZombie;

            for (int i = 0; i < boardParams.BotZ; i++)
            {
                localPosition = FindFreeSpot();

                localZombie = new Zombie(localPosition.X, localPosition.Y, boardParams, i);
                zombies.Add(localZombie);
                currentBoard[localPosition.X, localPosition.Y] = localZombie;
            }

        }

        private void CreateHumans()
        {
            Position localPosition;
            Human localHuman;

            for (int i = 0; i < boardParams.BotH; i++)
            {
                localPosition = FindFreeSpot();

                localHuman = new Human(localPosition.X, localPosition.Y, boardParams, i);
                humans.Add(localHuman);
                currentBoard[localPosition.X, localPosition.Y] = localHuman;
            }

        }

        public GameElement GetElementInPosition(int col, int row)
        {
            return currentBoard[col, row];
        }
    }
}
