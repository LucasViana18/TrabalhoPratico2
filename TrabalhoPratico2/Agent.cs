using System;
using System.Collections.Generic;
using System.Text;

namespace TrabalhoPratico2
{
    public class Agent : GameElement
    {
        // Variables and Properties
        protected Position CharPosition;


        // Constructor
        public Agent(int startX, int startY) : base(startX, startY)
        {

        }
        // Método de Movimento (raio = 1) de corpo vazio, sendo que
        // o zombie e humano herdam o método
        public string Move()
        {
            // Verificar a vizinhança de Moore; se todas tiverem characters nas 
            // mesmas, o movimento não é executado

            // Caso possa executar o movimento, move-se

            // Retorna string do Character movido (tipo de retorno sujeito a alteração)
            return " ";
        }

        // Método abstrato de Verificação da posição do humano/zombie mais próximo
        public Position VerifyOtherPosition() // other is placeholder
        {
            // Percorrer as 8 posições adjacentes (Moore de raio 1) ao character
            // para verificar se alguma delas contém um character de tipo "contrário",
            // aumentando o raio caso não encontre nenhum com o raio atual

            return CharPosition;
            // Caso encontre um character de tipo "contrário", retorna a posição do
            // mesmo (? - tirar dúvida?)
        }
    }
}
