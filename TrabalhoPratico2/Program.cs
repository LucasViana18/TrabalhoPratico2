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

            // Call the game loop
            game = new Game(p);
            game.GameLoop();

            // End game
            Console.WriteLine("Game Over\n");
            Environment.Exit(0);
        }
    }
}
