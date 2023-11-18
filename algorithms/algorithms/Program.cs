using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace algorithms
{
    public class Program
	{
		static void Main(string[] args)
		{
			int numeroDeQuadros = 5;
			List<int> paginasReferenciadas = new List<int> { 1, 3, 10, 3, 20, 245, 12, 1, 4, 5, 4, 7, 210 };

            FIFO algoritmoFifo = new FIFO(paginasReferenciadas, numeroDeQuadros);

			int fifoPageFaults = algoritmoFifo.ObterPageFaults();

            Console.WriteLine($"FIFO Page Faults: {fifoPageFaults}");
        }
	}
}
