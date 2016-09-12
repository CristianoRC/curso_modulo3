using System;
using System.Collections.Generic;
using System.IO;

namespace Cotacoes
{
    public static class Controle
    {   

        /// <summary>
    /// Preenche uma lsita com todas as linhas do arquivo CSV
    /// </summary>
    /// <returns>The cotacoes.</returns>
    /// <param name="CaminhoArquivo">Caminho arquivo.</param>
        private static List<Cotacao> ListarCotacoes(string CaminhoArquivo)
        {
            List<Cotacao> ListaInfoCotacao = new List<Cotacao>(); 
            StreamReader sr = null;

            try
            {
                sr = new StreamReader(CaminhoArquivo);

                //Lendo o valor da linha e passando para a função que tarsnforma a string nas propriedades da "cotação"
                while (!sr.EndOfStream)
                {
                    ListaInfoCotacao.Add(PreencherValoresCotacao(sr.ReadLine()));    
                }
                      
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }

            return ListaInfoCotacao;
        }

        /// <summary>
        /// Preenche a classe cotação com a linha do documento CSV.
        /// </summary>
        /// <returns>The valores cotacao.</returns>
        private static Cotacao PreencherValoresCotacao(String Linha)
        {
            string[] informacoes = Linha.Split(';'); //Colocando cada valor separado por ';' no array

            Cotacao CotacaoBase = new Cotacao();

            CotacaoBase.Dia = informacoes[0];
            CotacaoBase.Codmoeda   = int.Parse(informacoes[1]);
            CotacaoBase.Tipomoeda  = informacoes[2];
            CotacaoBase.Siglamoeda = informacoes[3];

            CotacaoBase.Taxacompra = Double.Parse(informacoes[4]);
            CotacaoBase.Taxavenda  = Double.Parse(informacoes[5]);
            CotacaoBase.Parcompra  = Double.Parse(informacoes[6]);
            CotacaoBase.Parvenda   = Double.Parse(informacoes[4]);

            return CotacaoBase;
        }

        /// <summary>
        /// Inserirs the no banco.
        /// </summary>
        /// <returns>The no banco.</returns>
        /// <param name="CaminhoArquivo">Caminho arquivo.</param>
        public static string InserirNoBanco(String CaminhoArquivo)
        {
            String Saida;

            //Preenchendo a lista com informações da cotacao
            List<Cotacao> ListaDeCotacoes = new List<Cotacao>();
            ListaDeCotacoes = ListarCotacoes(CaminhoArquivo);

            Spartacus.Database.Generic database = new Spartacus.Database.Sqlite("cotacoes.db");
            Spartacus.Database.Command cmd = new Spartacus.Database.Command();


           
            try
            {
                
                cmd.v_text = "insert into Cotacoes (dia, tipomoeda, siglamoeda, taxacompra, taxavenda, parcompra, parvenda , codmoeda)" +
                    "Values(#dia#, #tipomoeda#, #siglamoeda#, #taxacompra#, #taxavenda#, #parcompra#, #parvenda# , #codmoeda#)";

                cmd.AddParameter("dia",Spartacus.Database.Type.STRING);
                cmd.AddParameter("tipomoeda",Spartacus.Database.Type.STRING);
                cmd.AddParameter("siglamoeda",Spartacus.Database.Type.STRING);
                cmd.AddParameter("taxacompra",Spartacus.Database.Type.REAL);
                cmd.AddParameter("taxavenda",Spartacus.Database.Type.REAL);
                cmd.AddParameter("parcompra",Spartacus.Database.Type.REAL);
                cmd.AddParameter("parvenda",Spartacus.Database.Type.REAL);
                cmd.AddParameter("codmoeda",Spartacus.Database.Type.INTEGER);

                cmd.SetLocale("taxacompra",Spartacus.Database.Locale.EUROPEAN);
                cmd.SetLocale("taxavenda",Spartacus.Database.Locale.EUROPEAN);
                cmd.SetLocale("parcompra",Spartacus.Database.Locale.EUROPEAN);
                cmd.SetLocale("parvenda",Spartacus.Database.Locale.EUROPEAN);



                database.Open();


                foreach (Cotacao item in ListaDeCotacoes)
                {
                    cmd.SetValue("dia",item.Dia.ToString());
                    cmd.SetValue("tipomoeda",item.Tipomoeda.ToString());
                    cmd.SetValue("siglamoeda",item.Siglamoeda.ToString());
                    cmd.SetValue("taxacompra",item.Taxacompra.ToString());
                    cmd.SetValue("taxavenda",item.Taxavenda.ToString());
                    cmd.SetValue("parcompra",item.Parcompra.ToString());
                    cmd.SetValue("parvenda",item.Parvenda.ToString());
                    cmd.SetValue("codmoeda",item.Codmoeda.ToString());

                    database.Execute(cmd.GetUpdatedText());
                }

                database.Close();

                Saida = "Valores inseridos com sucesso!";
            }
            catch (Spartacus.Database.Exception ex)
            {
                database.Close();

                Saida = ("Ocorreu um erro ao salvar os valores: " + ex.v_message);
            }

            return Saida;
        }
    }
}

