﻿using System;
using System.Collections.Generic;
using System.Text;

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
        /// Convert args into integer or shut game down if not possible
        /// </summary>
        /// <param name="args">Arguments passed by user</param>
        public void ParseArgs(string[] args)
        {

            if (true) // Debug
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
            }

            // Rules limited conditions

            if (MaxX == default(int) || MaxY == default(int) ||
                BotZ == default(int) || BotH == default(int) ||
                MaxTurns == default(int))
            {
                Console.WriteLine($"X:{MaxX}, Y:{MaxY}\nz:{BotZ}, h:{BotH}" +
                    $"\nt:{MaxTurns}");
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
                    " humans or zombies selected.");
                Console.ForegroundColor = ConsoleColor.White;
                Environment.Exit(1);
            }
        }
    }
}
