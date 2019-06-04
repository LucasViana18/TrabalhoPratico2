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

            p.ParseArgs(args);
            game = new Game(p);
            game.GameStart();
        }
    }
}
