using System.Collections.Generic;

namespace algorithms
{
    public class FIFO
	{
		private int NumeroDeQuadros { get; set; }
		private List<int> PaginasReferenciadas { get; set; }
		private Queue<int> OrdemDeAlocacao { get; set; }
		private int PageFaults { get; set; }

		public FIFO(List<int> paginaReferenciadas, int numeroDeQuadros)
		{
			NumeroDeQuadros = numeroDeQuadros;
			PaginasReferenciadas = paginaReferenciadas;
            OrdemDeAlocacao = new Queue<int>(numeroDeQuadros);
			PageFaults = 0;
		}

		public int ObterPageFaults()
		{
            foreach (int pagina in PaginasReferenciadas)
			{
				if (!PaginaEstaAlocada(pagina))
				{
					if (!FilaEstaCheia())
					{
						OrdemDeAlocacao.Enqueue(pagina);
					}
					else 
					{
						OrdemDeAlocacao.Dequeue();
                        OrdemDeAlocacao.Enqueue(pagina);
                    }

					PageFaults++;
				}
			}

			return PageFaults;
		}

        private bool PaginaEstaAlocada(int pagina)
        {
			return OrdemDeAlocacao.Contains(pagina);
        }

		private bool FilaEstaCheia()
		{
			return OrdemDeAlocacao.Count == NumeroDeQuadros;
		}
    }
}
