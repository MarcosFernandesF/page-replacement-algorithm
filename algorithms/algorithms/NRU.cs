using System;
using System.Collections.Generic;

namespace algorithms
{
    public class NRU
    {
        private int NumeroDeQuadros { get; set; }
        private List<int> PaginasReferenciadas { get; set; }
        private List<Pagina> Memoria { get; set; }
        private int PageFaults { get; set; }
        private Random random;

        public NRU(List<int> paginasReferenciadas, int numeroDeQuadros)
        {
            NumeroDeQuadros = numeroDeQuadros;
            PaginasReferenciadas = paginasReferenciadas;
            Memoria = new List<Pagina>();
            PageFaults = 0;
            random = new Random();
        }

        public int ObterPageFaults()
        {
            foreach (int pagina in PaginasReferenciadas)
            {
                if (!PaginaEstaNaMemoria(pagina))
                {
                    if (!MemoriaEstaCheia())
                    {
                        AdicionarPaginaNaMemoria(pagina, referenciada: true, modificada: false);
                    }
                    else
                    {
                        AplicarSubstituicaoNRU(pagina);
                    }

                    PageFaults++;
                }
                else
                {
                    AtualizarReferencia(pagina);
                    AtualizarModificadaAleatoriamente();
                }
            }

            return PageFaults;
        }

        private bool MemoriaEstaCheia()
        {
            return Memoria.Count == NumeroDeQuadros;
        }

        private void AtualizarModificadaAleatoriamente()
        {
            foreach (Pagina pagina in Memoria)
            {
                // Simula uma chance de 30% de uma página ser modificada
                if (random.Next(1, 101) <= 30)
                {
                    pagina.Modificada = true;
                }
            }
        }

        private bool PaginaEstaNaMemoria(int numeroPagina)
        {
            return Memoria.Exists(p => p.Numero == numeroPagina);
        }

        private void AdicionarPaginaNaMemoria(int numeroPagina, bool referenciada, bool modificada)
        {
            Memoria.Add(new Pagina { Numero = numeroPagina, Referenciada = referenciada, Modificada = modificada });
        }

        private void AtualizarReferencia(int numeroPagina)
        {
            Pagina pagina = Memoria.Find(p => p.Numero == numeroPagina);
            pagina.Referenciada = true;
        }

        private void AplicarSubstituicaoNRU(int numeroPagina)
        {
            List<Pagina> candidatasSubstituicao = EncontrarCandidatasSubstituicao();

            if (candidatasSubstituicao.Count > 0)
            {
                int indiceSubstituicao = random.Next(0, candidatasSubstituicao.Count);
                RemoverPaginaDaMemoria(candidatasSubstituicao[indiceSubstituicao]);
                AdicionarPaginaNaMemoria(numeroPagina, referenciada: true, modificada: false);
            }
        }

        private List<Pagina> EncontrarCandidatasSubstituicao()
        {
            // Dividir páginas em 4 categorias com base em Referenciada e Modificada
            List<Pagina> categoria0 = new List<Pagina>();
            List<Pagina> categoria1 = new List<Pagina>();
            List<Pagina> categoria2 = new List<Pagina>();
            List<Pagina> categoria3 = new List<Pagina>();

            foreach (Pagina pagina in Memoria)
            {
                // R = 0 && M = 0
                if (!pagina.Referenciada && !pagina.Modificada)
                    categoria0.Add(pagina);
                // R = 0 && M = 1
                else if (!pagina.Referenciada && pagina.Modificada)
                    categoria1.Add(pagina);
                // R = 1 && M = 0
                else if (pagina.Referenciada && !pagina.Modificada)
                    categoria2.Add(pagina);
                // R = 1 && M = 1
                else if (pagina.Referenciada && pagina.Modificada)
                    categoria3.Add(pagina);
            }

            // Selecionar uma página aleatória de uma categoria não vazia
            if (categoria0.Count > 0)
                return categoria0;
            else if (categoria1.Count > 0)
                return categoria1;
            else if (categoria2.Count > 0)
                return categoria2;
            else if (categoria3.Count > 0)
                return categoria3;
            else
                return new List<Pagina>();
        }

        private void RemoverPaginaDaMemoria(Pagina pagina)
        {
            Memoria.Remove(pagina);
        }
    }
}
