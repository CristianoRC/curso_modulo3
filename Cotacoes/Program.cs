using System;

namespace Cotacoes
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            String saida = ControleArquivo.AtualizarDados().ToString();

            string[] respostas = saida.Split('$'); //Separando os dois resultados, o do banco e da atualização do arquivo.


            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("\tAtualização do Arquivo");
            Console.WriteLine("---------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("");
            Console.WriteLine(respostas[0]);
            Console.WriteLine("");

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("\tAtualização do Banco de Dados");
            Console.WriteLine("---------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.White;


            if (respostas[0] == "Download efetuado com sucesso !")
            {
                Console.WriteLine("");
                Console.WriteLine(respostas[1]);
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine("Não foi possível atualizar as informações no banco.");
                Console.WriteLine("");
            }


            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("\t\tRelatório em PDF");
            Console.WriteLine("---------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.White;


        }
    }
}
