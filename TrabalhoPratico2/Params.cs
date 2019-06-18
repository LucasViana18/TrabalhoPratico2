using System;
using System.Collections.Generic;

namespace TrabalhoPratico2
{
    /// <summary>
    /// Storage values passed by user
    /// </summary>
    public struct Params
    {
        // Properties
        public int MaxTurns { get; private set; }
        public int BotZ { get; private set; }
        public int BotH { get; private set; }
        public int UserZ { get; private set; }
        public int UserH { get; private set; }
        public int MaxX { get; private set; }
        public int MaxY { get; private set; }

        // Methods
        /// <summary>
        /// Check if all required params were passed by user
        /// </summary>
        /// <param name="args">Arguments passed by user</param>
        private void CheckArgs(string[] args)
        {
            List<string> argsPassed = new List<string>();

            foreach (string s in args)
            {
                if (s == "-x" || s == "-y" || s == "-h" || s == "-z"
                    || s == "-H" || s == "-Z" || s == "-t")
                {
                    argsPassed.Add(s);
                }
            }

            if (argsPassed.Contains("-x") && argsPassed.Contains("-y") &&
                argsPassed.Contains("-h") && argsPassed.Contains("-z") && 
                argsPassed.Contains("-Z") && argsPassed.Contains("-H") && 
                argsPassed.Contains("-t"))
            {
                return;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Parameters given were invalid or" +
                    " incomplete, please give the program all parameters." +
                    " correctly\nExamples:\n1) dotnet run -- -x 20 -y 20 " +
                    "-z 10 -h 30 -Z 1 -H 2 -t 1000\n2) dotnet run -- -x 20 " +
                    "-y 20 -z 10 -h 30 -Z 0 -H 0 -t 1000\nShutting Down.");
                Console.ForegroundColor = ConsoleColor.White;
                Environment.Exit(1);
            }
        }
        /// <summary>
        /// Convert args into integer or shut game down if not possible
        /// </summary>
        /// <param name="args">Arguments passed by user</param>
        public void ParseArgs(string[] args)
        {
            CheckArgs(args);
            for (int i = 0; i < args.Length; i++)
            {

                switch (args[i])
                {
                    case "-x":
                        if (int.TryParse(args[i + 1], out int x))
                        {
                            MaxX = x;
                        }
                        else
                        {
                            Console.WriteLine("x couldn't be converted." +
                                "\nShutting down game.");
                            Environment.Exit(0);
                        }
                        break;
                    case "-y":
                        if (int.TryParse(args[i + 1], out int y))
                        {
                            MaxY = y;
                        }
                        else
                        {
                            Console.WriteLine("y couldn't be converted." +
                                "\nShutting down game.");
                            Environment.Exit(0);
                        }
                        break;
                    case "-z":
                        if (int.TryParse(args[i + 1], out int z))
                        {
                            BotZ = z;
                        }
                        else
                        {
                            Console.WriteLine("z couldn't be converted." +
                                "\nShutting down game.");
                            Environment.Exit(0);
                        }
                        break;
                    case "-h":
                        if (int.TryParse(args[i + 1], out int h))
                        {
                            BotH = h;
                        }
                        else
                        {
                            Console.WriteLine("h couldn't be converted." +
                                "\nShutting down game.");
                            Environment.Exit(0);
                        }
                        break;
                    case "-Z":
                        if (int.TryParse(args[i + 1], out int Z))
                        {
                            UserZ = Z;
                        }
                        else
                        {
                            Console.WriteLine("Z couldn't be converted." +
                                "\nShutting down game.");
                            Environment.Exit(0);
                        }
                        break;
                    case "-H":
                        if (int.TryParse(args[i + 1], out int H))
                        {
                            UserH = H;
                        }
                        else
                        {
                            Console.WriteLine("H couldn't be converted." +
                                "\nShutting down game.");
                            Environment.Exit(0);
                        }
                        break;
                    case "-t":
                        if (int.TryParse(args[i + 1], out int t))
                        {
                            MaxTurns = t;
                        }
                        else
                        {
                            Console.WriteLine("t couldn't be converted." +
                                "\nShutting down game.");
                            Environment.Exit(0);
                        }
                        break;
                    default:
                        break;
                }
            }

            // Rules limited conditions
            
            if (MaxX <= 0 || MaxY <= 0 || BotZ <= 0 || BotH <= 0 ||
                MaxTurns <= 0 || UserZ < 0 || UserH < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("x, y, z, h and t cannot be lesser than or" +
                    " equal to 0 and Z and H cannot be lesser than 0.");
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
                Console.WriteLine("There are too many agents on the board.\n"+
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
                    " humans or zombies selected");
                Console.ForegroundColor = ConsoleColor.White;
                Environment.Exit(1);
            }

            if (Math.Abs(MaxX - MaxY) >= 10 || 
                Math.Abs(MaxY - MaxX) >= 10)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("X and Y are too far apart, please" +
                    " maintain them with a value of 10 of eachother.");
                Console.ForegroundColor = ConsoleColor.White;
                Environment.Exit(1);
            }
        }
    }
}
