using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using RoboCep;


namespace RoboCep
{
    class CSV
    {
        static async void EscreverArrayDados(string arquivo, string[] dados)
        {
            using (StreamWriter stream = File.AppendText(arquivo))
            {
                stream.WriteLine($"{dados[0]};{dados[1]};{dados[2]};{dados[3]};{DateTime.Now}");
            }
        }

        public static async void EscreverStringDados(string arquivo, string dados)
        {
            using (StreamWriter stream = File.AppendText(arquivo))
            {
                stream.Write(dados);
            }
        }

        public static async void ConsumirCep(string CepInicial, string CepFinal, string arquivo)
        {
            int numeroCepInicial = int.Parse(CepInicial);
            int numeroCepFinal = int.Parse(CepFinal);


            for (int cont = numeroCepInicial; cont <= numeroCepFinal; cont++)
            {
                Cep.ProcessarDadosCep(cont.ToString(), arquivo);
                Console.WriteLine($"Processando CEP: {cont}");
            }
        }


        public  static void ProcessarArquivoEntrada1()
        {
            using (var stream = new StreamReader(Constantes.ArquivoFaixasCep1))
            {
                while (!stream.EndOfStream)
                {
                    var values = stream.ReadLine().Split(";");
                    ConsumirCep(values[1], values[2], Constantes.ArquivoResultado1);
                }
            }
        }

        public static void ProcessarArquivoEntrada2()
        {
            using (var stream = new StreamReader(Constantes.ArquivoFaixasCep2))
            {
                while (!stream.EndOfStream)
                {
                    var values = stream.ReadLine().Split(";");
                    ConsumirCep(values[1], values[2], Constantes.ArquivoResultado2);
                }
            }
        }

        public static void ProcessarArquivoEntrada3()
        {
            using (var stream = new StreamReader(Constantes.ArquivoFaixasCep3))
            {
                while (!stream.EndOfStream)
                {
                    var values = stream.ReadLine().Split(";");
                    ConsumirCep(values[1], values[2], Constantes.ArquivoResultado3);
                }
            }
        }

    }
}
