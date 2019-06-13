using System;
using System.Collections.Generic;
using System.Text;

namespace TrabalhoPratico2
{
    public struct Params
    {
        public int MaxTurns { get; private set; }
        public int BotZ { get; private set; }
        public int BotH { get; private set; }
        public int UserZ { get; private set; }
        public int UserH { get; private set; }
        public int MaxX { get; private set; }
        public int MaxY { get; private set; }

        public Params(int x, int y, int z, int h, int Z, int H, int t)
        {
            MaxX = x;
            MaxY = y;
            BotZ = z;
            BotH = h;
            UserZ = Z;
            UserH = H;
            MaxTurns = t;
        }

        // ParseArgs method takes the array of strings args, which is passed to
        // the Main() from the command line.It's strings are analyzed through
        // for cycle, which uses a switch(case) to verify if any of them are
        // equal to -x, for example, and equals an int X to the next string in
        // the array (parsed to int), which is x's value given in the command
        // line. This is repeated for all arguments that are given and used to
        // create the struct. The method returns a new Params struct, using the
        //values taken from the args array.
        public void ParseArgs(string[] args)
        {

            if (false) // Debug
            {
                MaxX = 8;
                MaxY = 8;
                BotZ = 3;
                BotH = 3;
                UserZ = 1;
                UserH = 1;
                MaxTurns = 10;
            }
            else
            {
                for (int i = 0; i < args.Length; i++)
                {

                    switch (args[i])
                    {
                        case "-x":
                            MaxX = int.Parse(args[i + 1]);
                            break;
                        case "-y":
                            MaxY = int.Parse(args[i + 1]);
                            break;
                        case "-z":
                            BotZ = int.Parse(args[i + 1]);
                            break;
                        case "-h":
                            BotH = int.Parse(args[i + 1]);
                            break;
                        case "-Z":
                            UserZ = int.Parse(args[i + 1]);
                            break;
                        case "-H":
                            UserH = int.Parse(args[i + 1]);
                            break;
                        case "-t":
                            MaxTurns = int.Parse(args[i + 1]);
                            break;
                        default:
                            break;
                    }
                }
            }

            if (MaxX == default(int) || MaxY == default(int) ||
                BotZ == default(int) || BotH == default(int) ||
                MaxTurns == default(int))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Parameters given were incomplete, " +
                    "shutting down.");
                Console.ForegroundColor = ConsoleColor.White;
                Environment.Exit(1);
            }

            if ((BotZ + BotH) > 
                (Math.Round((double)(MaxX * MaxY) * 0.85)))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Total agents: " +
                    $"{BotZ + BotH}\nUsable Space: " +
                    $"{(Math.Round((double)(MaxX * MaxY) * 0.85))}");
                Console.WriteLine("There are too many agents on the board.\n" +
                    "Please leave some room for the agents to move.\n" +
                    "(15% of the board positions should be empty)");
                Console.ForegroundColor = ConsoleColor.White;
                Environment.Exit(1);
            }

            if(UserH > BotH || UserZ > BotZ)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The number of humans or zombies" +
                    " controlled by the user exceeds the total number of" +
                    " humans or zombies selected.");
                Console.ForegroundColor = ConsoleColor.White;
                Environment.Exit(1);
            }
        }
    }
}
