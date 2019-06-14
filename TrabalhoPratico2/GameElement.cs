using System;
using System.Collections.Generic;
using System.Text;

namespace TrabalhoPratico2
{
    public class GameElement
    {
        // Instance variables and properties
        protected Position currentPosition;
        protected Type elementType = Type.Empty;

        public Type ElementType { get { return elementType; } }

        // Constructor
        public GameElement(int startX, int startY)
        {
            currentPosition = new Position(startX, startY);
        }

        // Methods

        public void SetPosition(int x, int y)
        {
            currentPosition.X = x;
            currentPosition.Y = y;
        }

        public virtual string GetSymbol()
        {
            return " . ";
        }
    }
}
