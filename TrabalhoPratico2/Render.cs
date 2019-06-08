using System;
using System.Collections.Generic;
using System.Text;

namespace TrabalhoPratico2
{
    class Render
    {
        // Variables
        private string message;
        private GameElement item;

        //Methods
        public void Renderer(Board board, string msg)
        {
            if (msg != "")
                this.message = msg;

            Console.Clear();
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("     Zombie Game!      \n");
            
            //Check and update which row is being draw
            for (int r = 0; r < board.NumberRows; r++)
            {
                if (r != 0) Console.WriteLine();

                //Draw columns per row
                for (int c = 0; c < board.NumberColumns; c++)
                {
                    item = board.GetElementInPosition(c, r);
                    if (c == board.Playing.X && r == board.Playing.Y)
                        Console.ForegroundColor = ConsoleColor.Blue;
                    else if (c == board.Enemy.X && r == board.Enemy.Y)
                        Console.ForegroundColor = ConsoleColor.Red;
                    else
                        Console.ForegroundColor = ConsoleColor.White;

                    Console.Write(" " + item.GetSymbol() + " ");
                }
            }

            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("\n_______________");
            Console.WriteLine();
            Console.WriteLine(message);
        }
    }
}
