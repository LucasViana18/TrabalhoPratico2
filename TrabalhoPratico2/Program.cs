using System;

namespace TrabalhoPratico2
{
    class Program
    {
        static void Main(string[] args)
        {
            Program Prog = new Program();
            Params p = new Params();

            p.ParseArgs(args);

            Console.WriteLine($"X = {p.x}\nY = {p.y}\nNum of zombies = {p.z}" +
                $"\nNum of humans = {p.h}\nNum of playable zombies = {p.Z}" +
                $"\nNum of playable humans = {p.H}\nMax turns = {p.t}");

            Console.ReadLine();

            p.z++;
            p.h--;
            p.Z++;
            p.H--;

            Console.WriteLine($"X = {p.x}\nY = {p.y}\nNum of zombies = {p.z}" +
                $"\nNum of humans = {p.h}\nNum of playable zombies = {p.Z}" +
                $"\nNum of playable humans = {p.H}\nMax turns = {p.t}");

            Console.ReadLine();

            p.z++;
            p.h--;
            p.Z++;
            p.H--;

            Console.WriteLine($"X = {p.x}\nY = {p.y}\nNum of zombies = {p.z}" +
                $"\nNum of humans = {p.h}\nNum of playable zombies = {p.Z}" +
                $"\nNum of playable humans = {p.H}\nMax turns = {p.t}");
        }
    }
}
