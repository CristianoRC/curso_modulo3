using System;
using System.IO;

namespace Cotacoes
{
    public static class ControlePDF
    {
        /// <summary>
        /// Apaga o relatório antigo
        /// </summary>
        private static void ApagarRelatorio()
        {
            if(File.Exists("relatório.pdf"))
            {
                File.Delete("relatório.pdf");
            }
        }
            

        public static string GerarRelatrio()
        {
            ApagarRelatorio();

            Spartacus.Database.Generic database;
            Spartacus.Reporting.Report relatorio;
            System.Data.DataTable tabela = new System.Data.DataTable("relatorio");
            Spartacus.Database.Command cmd = new Spartacus.Database.Command();

            cmd.v_text = "select * from cotacoes where dia = #dia#";

            cmd.AddParameter("dia",Spartacus.Database.Type.STRING);
            cmd.SetValue("dia", DateTime.Now.ToString("dd/MM/yyyy"));

            try
            {
                database = new Spartacus.Database.Sqlite("cotacoes.db");
                tabela = database.Query(cmd.GetUpdatedText(),"relatorio");
               
                if(tabela.Rows.Count  != 0)
                {
                    relatorio = new Spartacus.Reporting.Report(tabela);
                    relatorio.Execute();
                    relatorio.Save("relatorio.pdf");

                    return "Relatório gerado com sucesso!";
                }
                else
                {
                    return "não foi encontrada informações sobre o dia atual!";
                }

            }
            catch (Spartacus.Database.Exception ex)
            {
                return String.Format("Ocorreu um erro ao gerar o relatório: {0}",ex.v_message);
            }
        }
    }
}

