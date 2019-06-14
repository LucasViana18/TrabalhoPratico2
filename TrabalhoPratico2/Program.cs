﻿using System;

namespace TrabalhoPratico2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Local variables
            Program Prog;
            Params p;
            Game game;

            // Instances
            Prog = new Program();
            p = new Params();

            // Call ParseArgs method
            p.ParseArgs(args);

            // Instantiate and call the game loop
            game = new Game(p);
            game.GameLoop();

            // End game
            Console.WriteLine("Game Over\n");
            Environment.Exit(0);
        }
    }
}
