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

			ExecutandoFIFO(numeroDeQuadros, paginasReferenciadas);

			ExecutandoSegundaChance(numeroDeQuadros, paginasReferenciadas);

			ExecutandoRelogio(numeroDeQuadros, paginasReferenciadas);

			ExecutandoNRU(numeroDeQuadros, paginasReferenciadas);

			ExecutandoLRU(numeroDeQuadros, paginasReferenciadas);
		}

		private static void ExecutandoFIFO(int numeroDeQuadros, List<int> paginasReferenciadas)
		{
			FIFO fifo = new FIFO(paginasReferenciadas, numeroDeQuadros);

			Stopwatch sw = new Stopwatch();
			sw.Start();
			int fifoPageFaults = fifo.ObterPageFaults();
			sw.Stop();

			Console.WriteLine("-FIFO:");
			ApresentarMetricas(paginasReferenciadas, sw, fifoPageFaults);
		}

		private static void ExecutandoSegundaChance(int numeroDeQuadros, List<int> paginasReferenciadas)
		{
			SegundaChance segundaChance = new SegundaChance(paginasReferenciadas, numeroDeQuadros);

			Stopwatch sw = new Stopwatch();
			sw.Start();
			int segundaChancePageFaults = segundaChance.ObterPageFaults();
			sw.Stop();

			Console.WriteLine("-Segunda Chance:");
			ApresentarMetricas(paginasReferenciadas, sw, segundaChancePageFaults);
		}

		private static void ExecutandoRelogio(int numeroDeQuadros, List<int> paginasReferenciadas)
		{
			Relogio relogio = new Relogio(paginasReferenciadas, numeroDeQuadros);

			Stopwatch sw = new Stopwatch();
			sw.Start();
			int relogioPageFaults = relogio.ObterPageFaults();
			sw.Stop();

			Console.WriteLine("-Relógio:");
			ApresentarMetricas(paginasReferenciadas, sw, relogioPageFaults);
		}

		private static void ExecutandoNRU(int numeroDeQuadros, List<int> paginasReferenciadas)
		{
			NRU nru = new NRU(paginasReferenciadas, numeroDeQuadros);

			Stopwatch sw = new Stopwatch();
			sw.Start();
			int nruPageFaults = nru.ObterPageFaults();
			sw.Stop();

			Console.WriteLine("-NRU:");
			ApresentarMetricas(paginasReferenciadas, sw, nruPageFaults);
		}

		private static void ExecutandoLRU(int numeroDeQuadros, List<int> paginasReferenciadas)
		{
			LRU lru = new LRU(paginasReferenciadas, numeroDeQuadros);

			Stopwatch sw = new Stopwatch();
			sw.Start();
			int lruPageFaults = lru.ObterPageFaults();
			sw.Stop();

			Console.WriteLine("-LRU:");
			ApresentarMetricas(paginasReferenciadas, sw, lruPageFaults);
		}

		private static void ApresentarMetricas(List<int> paginasReferenciadas, Stopwatch sw, int pageFaults)
		{
			float porcentagem = (pageFaults * 100.0f) / paginasReferenciadas.Count;
            Console.WriteLine($"Page Faults - {pageFaults}");
			Console.WriteLine($"Porcentagem de Page Faults - {porcentagem:F2}%");
            Console.WriteLine($"Tempo de execução total - {sw.Elapsed.TotalMilliseconds} ms \n");
		}
	}
}
