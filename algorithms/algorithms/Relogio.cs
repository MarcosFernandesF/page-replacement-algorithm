using System;
using System.Collections.Generic;
using System.Text;

namespace algorithms
{
	public class Relogio
	{
		private class Pagina
		{
			public int Numero { get; set; }
			public bool Referenciada { get; set; }
		}

		private int NumeroDeQuadros { get; set; }
		private List<int> PaginasReferenciadas { get; set; }
		private List<Pagina> Memoria { get; set; }
		private int Ponteiro { get; set; }
		private int PageFaults { get; set; }

		public Relogio(List<int> paginasReferenciadas, int numeroDeQuadros)
		{
			NumeroDeQuadros = numeroDeQuadros;
			PaginasReferenciadas = paginasReferenciadas;
			Memoria = new List<Pagina>();
			Ponteiro = 0;
			PageFaults = 0;
		}

		public int ObterPageFaults()
		{
			foreach (int pagina in PaginasReferenciadas)
			{
				if (!PaginaEstaNaMemoria(pagina))
				{
					if (!MemoriaEstaCheia())
					{
						AdicionarPaginaNaMemoria(pagina, referenciada: true);
					}
					else
					{
						while (true)
						{
							if (!Memoria[Ponteiro].Referenciada)
							{
								RemoverPaginaDaMemoria(Ponteiro);
								AdicionarPaginaNaMemoria(pagina, referenciada: true);
								break;
							}

							Memoria[Ponteiro].Referenciada = false;
							AvancarPonteiro();
						}
					}

					PageFaults++;
				}
				else
				{
					AtualizarReferencia(pagina);
				}
			}

			return PageFaults;
		}

		private bool MemoriaEstaCheia()
		{
			return Memoria.Count == NumeroDeQuadros;
		}

		private bool PaginaEstaNaMemoria(int numeroPagina)
		{
			return Memoria.Exists(p => p.Numero == numeroPagina);
		}

		private void AdicionarPaginaNaMemoria(int numeroPagina, bool referenciada)
		{
			Memoria.Add(new Pagina { Numero = numeroPagina, Referenciada = referenciada });
			AvancarPonteiro();
		}

		private void RemoverPaginaDaMemoria(int indice)
		{
			Memoria.RemoveAt(indice);
		}

		private void AtualizarReferencia(int numeroPagina)
		{
			Pagina pagina = Memoria.Find(p => p.Numero == numeroPagina);
			pagina.Referenciada = true;
		}

		private void AvancarPonteiro()
		{
			Ponteiro = (Ponteiro + 1) % NumeroDeQuadros;
		}
	}
}
