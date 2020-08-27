using System;
using System.IO;

namespace RoboCep
{
    class Program
    {


        static void Main(string[] args)
        {
            InicializarProjeto();

            Console.WriteLine("###################### PRESSIONE ALGUMA TECLA PARA INICIAR O PROCESSO DE BUSCA DE CEP #################################");
            Console.ReadKey();
            
            SepararArquivoFaixaCep();

            Console.WriteLine("#####################################################################################################################################");
            Console.WriteLine("#####################################################################################################################################");
            Console.WriteLine("################################################## INICIANDO BUSCAS DE CEP ##########################################################");
            Console.WriteLine("#####################################################################################################################################");
            Console.WriteLine("#####################################################################################################################################");
            Threads.start();
        }

        public static void InicializarProjeto()
        {

            if (!Directory.Exists(Constantes.DiretorioArquivoResultado))
                Directory.CreateDirectory(Constantes.DiretorioArquivoResultado);

            if (!Directory.Exists(Constantes.DiretorioArquivoFaixaCep))
                Directory.CreateDirectory(Constantes.DiretorioArquivoFaixaCep);

            if (File.Exists(Constantes.ArquivoFaixasCep1) &&
                File.Exists(Constantes.ArquivoFaixasCep2) &&
                File.Exists(Constantes.ArquivoFaixasCep3))
            {
                File.Delete(Constantes.ArquivoFaixasCep1); 
                File.Delete(Constantes.ArquivoFaixasCep2);
                File.Delete(Constantes.ArquivoFaixasCep3);
            }

            if (!File.Exists(Constantes.ArquivoResultado1) &&
                !File.Exists(Constantes.ArquivoResultado2) &&
                !File.Exists(Constantes.ArquivoResultado3))
            {
                File.Create(Constantes.ArquivoResultado1); 
                File.Create(Constantes.ArquivoResultado2);
                File.Create(Constantes.ArquivoResultado3);
            }

            if (!File.Exists(Constantes.ArquivoFaixasCep1) &&
                !File.Exists(Constantes.ArquivoFaixasCep2) &&
                !File.Exists(Constantes.ArquivoFaixasCep3))
            {
                using (File.Create(Constantes.ArquivoFaixasCep1)) { };
                using (File.Create(Constantes.ArquivoFaixasCep2)) { };
                using (File.Create(Constantes.ArquivoFaixasCep3)) { };
            }

        }

        public static void SepararArquivoFaixaCep()
        {
            using (var stream = new StreamReader(Constantes.ArquivoCSVBase))
            {
                int lineIndex = 0;

                while (!stream.EndOfStream)
                {
                    if (lineIndex <= 2005)
                        CSV.EscreverStringDados(Constantes.ArquivoFaixasCep1, $"{stream.ReadLine() } \n");

                    if (lineIndex <= 4010 && lineIndex > 2005)
                        CSV.EscreverStringDados(Constantes.ArquivoFaixasCep2, $"{stream.ReadLine() } \n");

                    if (lineIndex <= 6015 && lineIndex > 4010)
                        CSV.EscreverStringDados(Constantes.ArquivoFaixasCep3, $"{stream.ReadLine() } \n");

                    Console.WriteLine($"Separando linha {lineIndex} arquivo de entrada...");
                    lineIndex++;
                }
            }
        }
    }
}
