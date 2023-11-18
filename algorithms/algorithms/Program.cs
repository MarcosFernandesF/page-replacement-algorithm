using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace algorithms
{
    public class Program
	{
		static void Main(string[] args)
		{
			int numeroDeQuadros = 64;
            int numeroDePaginas = 100000;
			List<int> paginasReferenciadas = Enumerable.Range(0, numeroDePaginas)
                                            .Select(_ => new Random().Next(0, 256))
                                            .ToList();

            FIFO fifo = new FIFO(paginasReferenciadas, numeroDeQuadros);

			Stopwatch sw = new Stopwatch();
			sw.Start();
			int fifoPageFaults = fifo.ObterPageFaults();
			sw.Stop();

			Console.WriteLine("-FIFO:");
            Console.WriteLine($"Page Faults - {fifoPageFaults}");
            Console.WriteLine($"Tempo de execução total - {sw.Elapsed.TotalMilliseconds} ms\n");
			sw.Reset();

            SegundaChance segundaChance = new SegundaChance(paginasReferenciadas, numeroDeQuadros);

            sw.Start();
            int segundaChancePageFaults = segundaChance.ObterPageFaults();
            sw.Stop();

            Console.WriteLine("-Segunda Chance:");
            Console.WriteLine($"Page Faults - {segundaChancePageFaults}");
            Console.WriteLine($"Tempo de execução total - {sw.Elapsed.TotalMilliseconds} ms\n");
            sw.Reset();

            Relogio relogio = new Relogio(paginasReferenciadas, numeroDeQuadros);

            sw.Start();
            int relogioPageFaults = relogio.ObterPageFaults();
            sw.Stop();

            Console.WriteLine("-Relógio:");
            Console.WriteLine($"Page Faults - {relogioPageFaults}");
            Console.WriteLine($"Tempo de execução total - {sw.Elapsed.TotalMilliseconds} ms\n");
            sw.Reset();

            NRU nru = new NRU(paginasReferenciadas, numeroDeQuadros);

            sw.Start();
            int nruPageFaults = nru.ObterPageFaults();
            sw.Stop();

            Console.WriteLine("-NRU:");
            Console.WriteLine($"Page Faults - {nruPageFaults}");
            Console.WriteLine($"Tempo de execução total - {sw.Elapsed.TotalMilliseconds} ms\n");
            sw.Reset();

            LRU lru = new LRU(paginasReferenciadas, numeroDeQuadros);

            sw.Start();
            int lruPageFaults = lru.ObterPageFaults();
            sw.Stop();

            Console.WriteLine("-LRU:");
            Console.WriteLine($"Page Faults - {lruPageFaults}");
            Console.WriteLine($"Tempo de execução total - {sw.Elapsed.TotalMilliseconds} ms");
            sw.Reset();
        }
    }
}
