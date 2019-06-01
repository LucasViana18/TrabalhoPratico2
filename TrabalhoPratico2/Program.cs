using System;

namespace TrabalhoPratico2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Zombie Game!");

            Program Prog = new Program();
            Params p = new Params();

            p.ParseArgs(args);

            Console.WriteLine($"X = {p.X}\nY = {p.Y}");

            p.X += p.Y;
            p.Y += p.X;

            Console.WriteLine($"X = {p.X}\nY = {p.Y}");
        }
    }
}
