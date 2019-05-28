using System;
using System.Collections.Generic;
using System.Text;

namespace TrabalhoPratico2
{
    public abstract class Character
    {
        // Variables and Properties
        protected Position CharPosition;

        // Método de Movimento (raio = 1) de corpo vazio, sendo que
        // o zombie e humano herdam o método
        public string Move()
        {
            // Verificar a vizinhança de Moore; se todas tiverem characters nas 
            // mesmas, o movimento não é executado

            // Caso possa executar o movimento, move-se

            // Retorna string do Character movido (tipo de retorno sujeito a alteração)
        }

        // Método abstrato de Verificação da posição do humano/zombie mais próximo
        public abstract Position VerifyOtherPosition(other) // other is placeholder
        {
            // Percorrer as 8 posições adjacentes (Moore de raio 1) ao character
            // para verificar se alguma delas contém um character de tipo "contrário",
            // aumentando o raio caso não encontre nenhum com o raio atual


            // Caso encontre um character de tipo "contrário", retorna a posição do
            // mesmo (? - tirar dúvida?)
        }
    }
}
