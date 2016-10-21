using System;
using Spartacus;

namespace TamanhoBlocoDeInsercao
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Spartacus.Utils.ProgressEventClass v_progress;
            Spartacus.Utils.ErrorEventClass v_error;
            Spartacus.Database.Command v_cmd;
            Spartacus.Database.Generic database = new Spartacus.Database.Postgresql("192.168.56.2","cotacoes","psotgres","Crc19D98C");


            v_progress = new Spartacus.Utils.ProgressEventClass();
            v_error = new Spartacus.Utils.ErrorEventClass();



            int[] v_exp = { 1, 5, 10, 50, 100, 250, 500, 750, 1000, 2000, 3000 };
            System.Diagnostics.Stopwatch v_watch = new System.Diagnostics.Stopwatch();
            foreach (int v_bloco in v_exp)
            {
                database.v_blocksize = v_bloco;
                v_watch.Start;

                database.Open();
                database.TransferFromFile(
                    "Caminho Do Arquivo Aqui",
                    ";",
                    "",
                    false,
                    System.Text.Encoding.UTF8,
                    "cotacoes", // tabela existe
                    "(dia,codmoeda,tipomoeda,siglamoeda,taxacompra,taxavenda,parcompra,parvenda)",
                    v_cmd,
                    v_progress,
                    v_error
                );
                database.Close();

                v_watch.Stop;
                Console.WriteLine("Tamanho de bloco = {0}, tempo total = {1}", v_bloco, v_watch.Elapsed);
            }
        }
    }
}
