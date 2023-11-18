using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace algorithms
{
    public class Program
	{
		static void Main(string[] args)
		{
			int numeroDeQuadros = 64;
			List<int> paginasReferenciadas = Enumerable.Range(0, 100000)
                                            .Select(_ => new Random().Next(0, 256))
                                            .ToList();

            FIFO fifo = new FIFO(paginasReferenciadas, numeroDeQuadros);

			Stopwatch sw = new Stopwatch();
			sw.Start();
			int fifoPageFaults = fifo.ObterPageFaults();
			sw.Stop();

			Console.WriteLine("FIFO:");
            Console.WriteLine($"Page Faults - {fifoPageFaults}");
            Console.WriteLine($"Tempo de execução total - {sw.Elapsed.TotalMilliseconds}");
			sw.Reset();

			Relogio relogio = new Relogio(paginasReferenciadas, numeroDeQuadros);

            sw.Start();
            int relogioPageFaults = relogio.ObterPageFaults();
            sw.Stop();

            Console.WriteLine("Relógio:");
            Console.WriteLine($"Page Faults - {relogioPageFaults}");
            Console.WriteLine($"Tempo de execução total - {sw.Elapsed.TotalMilliseconds}");
            sw.Reset();
        }
	}
}
