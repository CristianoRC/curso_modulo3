using System;
using DotCEP;
using System.Diagnostics;

namespace CEP_Testes
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.Write("Digite O CEP: ");
            uint CEP = uint.Parse(Console.ReadLine()); 

            Console.WriteLine("---------------------------");
            Console.WriteLine(" ");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
           
            Endereco enderecoBase = new Endereco();
            enderecoBase = Consulta.ObterEnderecoCompleto(CEP);

            stopwatch.Stop();

            TimeSpan ts = stopwatch.Elapsed; 

            Console.WriteLine(ts.Seconds + " / " + ts.Milliseconds);

            Console.WriteLine(" ");
            Console.WriteLine("---------------------------");

            Console.WriteLine(enderecoBase.cep);
            Console.WriteLine(" ");
            Console.WriteLine(enderecoBase.uf);
            Console.WriteLine(" ");
            Console.WriteLine(enderecoBase.localidade);
            Console.WriteLine(" ");
            Console.WriteLine(enderecoBase.bairro);
            Console.WriteLine(" ");
            Console.WriteLine(enderecoBase.logradouro);
            Console.WriteLine(" ");
            Console.WriteLine(enderecoBase.complemento);
            Console.WriteLine(" ");
            Console.WriteLine(enderecoBase.ibge);
            Console.WriteLine(" ");
            Console.WriteLine(enderecoBase.gia);
            Console.WriteLine(" ");
            Console.WriteLine(enderecoBase.unidade);

            Console.ReadKey();
        }
    }
}
