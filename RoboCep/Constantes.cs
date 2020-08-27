using System;
using System.Collections.Generic;
using System.Text;

namespace RoboCep
{
    public static class Constantes
    {


        #region Diretorios de processamento
        private static string baseDirectory = $@"{AppDomain.CurrentDomain.BaseDirectory}";

        public static string DiretorioArquivoResultado = $@"{baseDirectory}\ResultadosProcessamento";
        public static string DiretorioArquivoFaixaCep = $@"{baseDirectory}\ArquvivosFaixaCep";
        #endregion

        public static string ArquivoCSVBase = $@"{AppDomain.CurrentDomain.BaseDirectory}\ArquivoBase\Pachoalloto.csv";

        public static string ArquivoFaixasCep1 = $@"{DiretorioArquivoFaixaCep}\ArquivoBase1.csv";
        public static string ArquivoFaixasCep2 = $@"{DiretorioArquivoFaixaCep}\ArquivoBase2.csv";
        public static string ArquivoFaixasCep3 = $@"{DiretorioArquivoFaixaCep}\ArquivoBase3.csv";

        public static string ArquivoResultado1 = $@"{DiretorioArquivoResultado}\ArquivoResultado1.csv";
        public static string ArquivoResultado2 = $@"{DiretorioArquivoResultado}\ArquivoResultado2.csv";
        public static string ArquivoResultado3 = $@"{DiretorioArquivoResultado}\ArquivoResultado3.csv";


    }
}
