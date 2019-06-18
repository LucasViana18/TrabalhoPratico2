using System;

namespace TrabalhoPratico2
{
    /// <summary>
    /// Program class
    /// </summary>
    class Program
    {
        /// <summary>
        /// Initalize program
        /// </summary>
        /// <param name="args">Arguments passed by user</param>
        static void Main(string[] args)
        {
            // Local variables
            Params p;
            Game game;

            // Instances
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
