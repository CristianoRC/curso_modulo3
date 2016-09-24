using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace ProjetoJSON
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            String SaidaSerializar = Serializar(GerarListarEquipamentos());
            Console.WriteLine(SaidaSerializar);

            Console.Write("");
            Console.WriteLine("_______________________________________________");
            Console.Write("");

            MostrarEquipamentos(deserializarArquivo());

        }



        private static List<Equipamento> deserializarArquivo()
        {
            List<Equipamento> ListaDeEquipamentos = new List<Equipamento>();

    
            ListaDeEquipamentos = JsonConvert.DeserializeObject<List<Equipamento>>(LerArquivoJson());
            return ListaDeEquipamentos;
        }

        private static string Serializar(List<Equipamento> Equipamentos)
        {
            List<Equipamento> Lista = new List<Equipamento>();
            Lista = Equipamentos;


            String JSONText = JsonConvert.SerializeObject(Lista);
            return EscreverArquivoJSON(JSONText);
        }

        private static string LerArquivoJson()
        {
            try
            {
                return File.ReadAllText("Equipamentos.JSON");
            }
            catch (Exception ex)
            {
                return "Erro: " + ex.Message;
            }
        }

        private static String EscreverArquivoJSON(String JSONText)
        {
            try
            {
                File.WriteAllText("Equipamentos.JSON", JSONText);

                return "Arquivo foi salvo com sucesso!";
            }
            catch (Exception ex)
            {
                return "Erro: " + ex.Message;
            }
        }


        private static List<Equipamento> GerarListarEquipamentos()
        {
            List<Equipamento> ListaDeEquipamentos = new List<Equipamento>();

            for (int i = 0; i < 50; i++)
            {
                Equipamento EquipamentoBase = new Equipamento();

                EquipamentoBase.id = i;
                EquipamentoBase.Nome = "Equipamento nº: " + i; 
                EquipamentoBase.status = GerarStatusAleatorio();

                ListaDeEquipamentos.Add(EquipamentoBase);
            }

            return ListaDeEquipamentos;
        }

        private static void MostrarEquipamentos(List<Equipamento> p_ListaDeEquipamentos)
        {
            List<Equipamento> ListaDeEquipamentos = new List<Equipamento>();
            ListaDeEquipamentos = p_ListaDeEquipamentos;


            foreach (Equipamento item in ListaDeEquipamentos)
            {
                Console.WriteLine("ID: " + item.id);
                Console.WriteLine("Nome: " + item.Nome);
                Console.WriteLine("Status: " + item.status);
                Console.WriteLine("---------------------");
                Console.WriteLine(" ");
            }
        }

        private static bool GerarStatusAleatorio()
        {
            Random RoletaBool = new Random();

            int SaidaRoleta = RoletaBool.Next();

            if (SaidaRoleta % 2 == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
