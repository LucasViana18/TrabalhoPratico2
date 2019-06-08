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
        public Params ParseArgs(string[] args)
        {

            // if (true) // Debug
            // {
            //     MaxX = 8;
            //     MaxY = 8;
            //     BotZ = 3;
            //     BotH = 4;
            // }

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

            if (MaxX == default(int) || MaxY == default(int) ||
                BotZ == default(int) || BotH == default(int) ||
                MaxTurns == default(int))
            {
                Console.WriteLine("Parameters given were incomplete, " +
                    "shutting down.");
                System.Environment.Exit(1);
            }

            if ((BotZ + BotH + UserH + UserZ) > 
                (Math.Round((double)(MaxX * MaxY) * 0.85)))
            {
                Console.WriteLine($"Total agents: " +
                    $"{BotZ + BotH + UserH + UserZ}\nUsable Space: " +
                    $"{(Math.Round((double)(MaxX * MaxY) * 0.85))}");
                Console.WriteLine("There are too many agents on the board.\n" +
                    "Please leave some room for the agents to move.\n" +
                    "(15% of the board positions should be empty)");
                System.Environment.Exit(1);
            }
            return new Params(MaxX, MaxY, BotZ, BotH, UserZ, UserH, MaxTurns);
        }
    }
}
