using System;
using System.IO;
using System.Net;

namespace Cotacoes
{
    public static class ControleArquivo
    {
        
        /// <summary>
        /// Apaga as informações e recria a pasta do arquivo CSV
        /// </summary>
        private static void ReCriarPastaCSV()
        {
            if (!Directory.Exists("CSV"))
            {
                Directory.CreateDirectory("CSV");
            }
            else
            {
                Directory.Delete("CSV",true);
                Directory.CreateDirectory("CSV");
            }
        }

        /// <summary>
        /// Fazendo download do arquivo mais novo e chamando a função de atualizar informações no banco de dados. 
        /// OBS: Se não atualizar o arquivo, não atualiza as informações do banco.
        /// </summary>
        /// <returns>The CS.</returns>
        public static string AtualizarDados()
        {
            string Saida;
            String data = DateTime.Now.ToString("yyyyMMdd");
            string NomeArquivo = String.Format("{0}.csv",data);


            //Verifica se o arquivo da data de hoje já está na maquina do cliente.
            if(!File.Exists(String.Format(@"CSV/{0}",NomeArquivo))) 
            {
                ReCriarPastaCSV(); //Se ele tiver com algum arquivo que não seja o atualizado, é feita a limpeza da pasta.

                try
                {
                    WebClient webClient = new WebClient();

                    webClient.DownloadFile(String.Format("http://www4.bcb.gov.br/Download/fechamento/{0}",NomeArquivo), String.Format(@"CSV/{0}",NomeArquivo));
                     
                    string ResultadoBanco = Controle.InserirNoBanco(String.Format(@"CSV/{0}",NomeArquivo)); //Inserindo as informações do arquivo novo no banco.

                    Saida = String.Format("Download efetuado com sucesso !$ {0}",ResultadoBanco);           //Retorna os dois resultados, o de banco e o do download do arquivo
                }
                catch (Exception ex)
                {
                    if(ex.Message == "The remote server returned an error: (404) Not Found.")
                    {
                        Saida = "Não há arquivo da da data atual para download, atualização é feita de segunda a sexta feira as 13:00";
                    }
                    else
                    {
                        Saida = ex.Message;
                    }
                }
            }
            else
            {
                Saida = "O arquivo já está atualizado.";
            }

            return Saida;
        }

    }
}

