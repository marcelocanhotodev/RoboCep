using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RoboCep
{
    class Threads
    {
        public static void start()
        {
            Thread[] Threads = new Thread[3];

            Threads[0] = new Thread(new ThreadStart(CSV.ProcessarArquivoEntrada1));
            Threads[1] = new Thread(new ThreadStart(CSV.ProcessarArquivoEntrada2));
            Threads[2] = new Thread(new ThreadStart(CSV.ProcessarArquivoEntrada3));

            foreach (Thread t in Threads)
                t.Start();
        }
    }
}
