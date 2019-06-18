using System;
using System.Collections.Generic;
using System.Text;

namespace TrabalhoPratico2
{
    /// <summary>
    /// Super class of every element in game
    /// </summary>
    public class GameElement
    {
        // Instance variables and properties
        protected Position currentPosition;
        protected Type elementType = Type.Empty;

        public Type ElementType { get { return elementType; } }

        // Constructor
        /// <summary>
        /// GameElement constructor
        /// </summary>
        /// <param name="startX">Position x</param>
        /// <param name="startY">Position y</param>
        public GameElement(int startX, int startY)
        {
            currentPosition = new Position(startX, startY);
        }

        // Methods
        /// <summary>
        /// Set position to x and y passed at body
        /// </summary>
        /// <param name="x">Position x</param>
        /// <param name="y">Position y</param>
        public void SetPosition(int x, int y)
        {
            currentPosition.X = x;
            currentPosition.Y = y;
        }
        /// <summary>
        /// Get symbol
        /// </summary>
        /// <returns>Symbol</returns>
        public virtual string GetSymbol()
        {
            return " . ";
        }
    }
}
