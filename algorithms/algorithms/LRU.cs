using System;
using System.Collections.Generic;
using System.Linq;

namespace algorithms
{
    public class LRU
    {
        private int NumeroDeQuadros { get; set; }
        private List<int> PaginasReferenciadas { get; set; }
        private List<Pagina> Memoria { get; set; }
        private int PageFaults { get; set; }

        public LRU(List<int> paginasReferenciadas, int numeroDeQuadros)
        {
            NumeroDeQuadros = numeroDeQuadros;
            PaginasReferenciadas = paginasReferenciadas;
            Memoria = new List<Pagina>();
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
                        AdicionarPaginaNaMemoria(pagina);
                    }
                    else
                    {
                        AplicarSubstituicaoLRU(pagina);
                    }

                    PageFaults++;
                }
                else
                {
                    AtualizarReferencia(pagina);
                }

                AtualizarContadorDeInterrupcao(pagina);
            }

            return PageFaults;
        }

        private void AtualizarReferencia(int numeroPagina)
        {
            Pagina pagina = Memoria.Find(p => p.Numero == numeroPagina);
            pagina.Referenciada = true;
        }

        private bool MemoriaEstaCheia()
        {
            return Memoria.Count == NumeroDeQuadros;
        }

        private void AdicionarPaginaNaMemoria(int numeroPagina)
        {
            Memoria.Add(new Pagina { Numero = numeroPagina, Referenciada = true, ContadorInterrupcao = 0 });
        }

        private void AplicarSubstituicaoLRU(int numeroPagina)
        {
            int indiceSubstituicao = EncontrarPaginaMenosRecentementeUsada();
            RemoverPaginaDaMemoria(indiceSubstituicao);
            AdicionarPaginaNaMemoria(numeroPagina);
        }

        private int EncontrarPaginaMenosRecentementeUsada()
        {
            int indiceLRU = 0;
            int menorAcesso = Memoria[0].ContadorInterrupcao;

            for (int i = 1; i < Memoria.Count; i++)
            {
                if (Memoria[i].ContadorInterrupcao < menorAcesso)
                {
                    menorAcesso = Memoria[i].ContadorInterrupcao;
                    indiceLRU = i;
                }
            }

            return indiceLRU;
        }

        private void AtualizarContadorDeInterrupcao(int numeroPagina)
        {
            Memoria.ForEach(pagina =>
            {
                // Se a página estiver referenciada tira a referência e incrementa o contador.
                if (pagina.Referenciada)
                {
                    pagina.Referenciada = false;
                    pagina.ContadorInterrupcao++;
                }
            });
        }

        private bool PaginaEstaNaMemoria(int numeroPagina)
        {
            return Memoria.Any(p => p.Numero == numeroPagina);
        }

        private void RemoverPaginaDaMemoria(int indice)
        {
            Memoria.RemoveAt(indice);
        }
    }
}
