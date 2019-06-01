using System;
using System.Collections.Generic;
using System.Text;

namespace TrabalhoPratico2
{
    public struct Params
    {
        public int x, y, t;
        public int z, h;
        public int Z { get; set; }
        public int H { get; set; }

        public Params(int x, int y, int z, int h, int Z, int H, int t)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.h = h;
            this.Z = Z;
            this.H = H;
            this.t = t;
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
            for (int i = 0; i < args.Length; i++)
            {

                switch (args[i])
                {
                    case "-x":
                        x = int.Parse(args[i + 1]);
                        break;
                    case "-y":
                        y = int.Parse(args[i + 1]);
                        break;
                    case "-z":
                        z = int.Parse(args[i + 1]);
                        break;
                    case "-h":
                        h = int.Parse(args[i + 1]);
                        break;
                    case "-Z":
                        Z = int.Parse(args[i + 1]);
                        break;
                    case "-H":
                        H = int.Parse(args[i + 1]);
                        break;
                    case "-t":
                        t = int.Parse(args[i + 1]);
                        break;
                    default:
                        break;
                }
            }

            return new Params(x, y, z, h, Z, H, t);
        }
    }
}
