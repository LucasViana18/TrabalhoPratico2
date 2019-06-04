using System;
using System.Collections.Generic;
using System.Text;

namespace TrabalhoPratico2
{
    public class Agent : GameElement
    {
        // Variables and Properties

        protected List<Position> neighR1;
        protected int myID = 0;
        protected Params GamePar;

        public string AgentID { get { return myID.ToString("00"); } }
        public Position AgentPosition { get { return currentPosition; } }

        // Constructor
        public Agent(int startX, int startY, Params par, int lastAgentID) : base(startX, startY)
        {
            neighR1 = new List<Position>();

            GamePar = par;
            myID = lastAgentID + 1;

        }

        // Methods to identify the coordenates of the Neighbour
        protected Position GetNewNeighR1(int i)
        {
            Position localPos = new Position(currentPosition.X, currentPosition.Y);
            switch (i)
            {
                case 1:
                    localPos.X -= 1;
                    localPos.Y += 1;
                    break;
                case 2:
                    localPos.Y += 1;
                    break;
                case 3:
                    localPos.X += 1;
                    localPos.Y += 1;
                    break;
                case 4:
                    localPos.X -= 1;
                    break;
                case 6:
                    localPos.X += 1;
                    break;
                case 7:
                    localPos.X -= 1;
                    localPos.Y -= 1;
                    break;
                case 8:
                    localPos.Y -= 1;
                    break;
                case 9:
                    localPos.X += 1;
                    localPos.Y -= 1;
                    break;

                default:
                    break;
            }

            // Matrix borders control
            localPos.X = (localPos.X > GamePar.MaxX) ? localPos.X - GamePar.MaxX
                : localPos.X;
            localPos.Y = (localPos.Y > GamePar.MaxY) ? localPos.Y - GamePar.MaxY
                : localPos.Y;
            localPos.X = (localPos.X < 0) ? localPos.X + GamePar.MaxX
                : localPos.X;
            localPos.Y = (localPos.Y < 1) ? localPos.Y + GamePar.MaxY
                : localPos.Y;

            return localPos;
        }

        protected void UpdateNeighR1()
        {
            neighR1.Clear();

            for (int i = 1; i <= 9; i++)
            {
                neighR1.Add(GetNewNeighR1(i));
            }
        }


        public string Move()
        {


            UpdateNeighR1();

            return "Ola";
        }

        public Position VerifyOtherPosition() // other is placeholder
        {
            // Percorrer as 8 posições adjacentes (Moore de raio 1) ao character
            // para verificar se alguma delas contém um character de tipo "contrário",
            // aumentando o raio caso não encontre nenhum com o raio atual

            return AgentPosition;
            // Caso encontre um character de tipo "contrário", retorna a posição do
            // mesmo (? - tirar dúvida?)
        }
    }
}
