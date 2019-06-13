using System;

namespace TrabalhoPratico2
{
    class Program
    {
        static void Main(string[] args)
        {
            Program Prog = new Program();
            Params p = new Params();
            Game game;

            // Check the correct parameters.
            if (!p.ParseArgs(args))
            {
                Console.WriteLine("Invalid parameters. You must define, " +
                    "at least, a number of Humans, Zombies, Columns, Lines " +
                    "and Turns");
                Environment.Exit(-1);
            }

            // Call the game loop
            game = new Game(p);
            game.GameLoop();

            // End game
            Console.WriteLine("Game Over\n");
            Environment.Exit(0);
        }
    }
}
