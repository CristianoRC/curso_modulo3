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
            if (File.Exists("relatório.pdf"))
            {
                File.Delete("relatório.pdf");
            }
        }

        public static string GerarRelatorio()
        {
            ApagarRelatorio();

            Spartacus.Database.Generic database;
            Spartacus.Reporting.Report relatorio;
            System.Data.DataTable tabela = new System.Data.DataTable("relatorio");
            Spartacus.Database.Command cmd = new Spartacus.Database.Command();

            cmd.v_text = "select 'Relatório de Cotações do Dia Curso de Programação CSharp' as titulo, * from cotacoes where dia = #dia#";

            cmd.AddParameter("dia", Spartacus.Database.Type.STRING);
            cmd.SetValue("dia", DateTime.Now.ToString("dd/MM/yyyy"));

            try
            {
                database = new Spartacus.Database.Sqlite("cotacoes.db");
                tabela = database.Query(cmd.GetUpdatedText(), "relatorio");

                if (tabela.Rows.Count != 0)
                {
                    System.Collections.Generic.List<Spartacus.Reporting.Field> v_fields;
                  
                    v_fields = new System.Collections.Generic.List<Spartacus.Reporting.Field>();

                    v_fields.Add(new Spartacus.Reporting.Field("dia", "Data", Spartacus.Reporting.FieldAlignment.CENTER, 10, Spartacus.Database.Type.STRING));
                    v_fields.Add(new Spartacus.Reporting.Field("codmoeda", "Cód. Moeda", Spartacus.Reporting.FieldAlignment.CENTER, 10, Spartacus.Database.Type.STRING));
                    v_fields.Add(new Spartacus.Reporting.Field("tipomoeda", "Tipo Moeda", Spartacus.Reporting.FieldAlignment.CENTER, 10, Spartacus.Database.Type.STRING));
                    v_fields.Add(new Spartacus.Reporting.Field("siglamoeda", "Sigla Moeda", Spartacus.Reporting.FieldAlignment.CENTER, 10, Spartacus.Database.Type.STRING));
                    v_fields.Add(new Spartacus.Reporting.Field("taxacompra", "Taxa Compra", Spartacus.Reporting.FieldAlignment.RIGHT, 15, Spartacus.Database.Type.REAL));
                    v_fields.Add(new Spartacus.Reporting.Field("taxavenda", "Taxa Venda", Spartacus.Reporting.FieldAlignment.RIGHT, 15, Spartacus.Database.Type.REAL));
                    v_fields.Add(new Spartacus.Reporting.Field("parcompra", "Par. Compra", Spartacus.Reporting.FieldAlignment.RIGHT, 15, Spartacus.Database.Type.REAL));
                    v_fields.Add(new Spartacus.Reporting.Field("parvenda", "Par. Venda", Spartacus.Reporting.FieldAlignment.RIGHT, 15, Spartacus.Database.Type.REAL));

                    relatorio = new Spartacus.Reporting.Report(tabela, "titulo", v_fields);
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
                return String.Format("Ocorreu um erro ao gerar o relatório: {0}", ex.v_message);
            }
        }
    }
}

