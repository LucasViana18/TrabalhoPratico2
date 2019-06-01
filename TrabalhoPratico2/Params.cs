using System;
using System.Collections.Generic;
using System.Text;

namespace TrabalhoPratico2
{
    public struct Params
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Params(int x, int y)
        {
            X = x;
            Y = y;
        }

        // ParseArgs method takes the array of strings args, which is passed to
        // the Main() from the command line.It's strings are analyzed through
        // for cycle, which uses a switch(case) to verify if any of them are
        // equal to -x, for example, and equals an int X to the next string in
        // the array converted to int, which is x's value given in the command
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
                        X = Convert.ToInt32(args[i + 1]);
                        break;
                    case "-y":
                        Y = Convert.ToInt32(args[i + 1]);
                        break;
                    default:
                        break;
                }
            }

            return new Params(X, Y);
        }
    }
}
