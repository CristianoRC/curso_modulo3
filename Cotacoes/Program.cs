using System;
using System.Diagnostics;

namespace Cotacoes
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            
            //Mensagem de Download
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Fazendo download das informações, por favor espere...");

            String saida = ControleArquivo.AtualizarDados().ToString();
            string[] respostas = saida.Split('$'); //Separando os dois resultados, o do banco e da atualização do arquivo.
            Console.Clear();


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

            Console.Write("Você deseja gerar um relatório do dia atual?(S/N)");
            string escolha = Console.ReadLine();

            if(escolha.ToLower() == "s" || escolha.ToLower() == "sim")
            {
                string ResultadoRelatorio = ControlePDF.GerarRelatorio();

                if (ResultadoRelatorio == "Relatório gerado com sucesso!")
                {
                    Process.Start("relatorio.pdf");
                }
                else
                {
                    Console.WriteLine(ResultadoRelatorio);
                }
            }
        }
    }
}
