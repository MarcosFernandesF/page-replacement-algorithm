using System;
using System.Collections.Generic;

namespace algorithms
{
    public class SegundaChance
    {
        private int NumeroDeQuadros { get; set; }
        private List<int> PaginasReferenciadas { get; set; }
        private List<Pagina> Memoria { get; set; }
        private int Ponteiro { get; set; }
        private int PageFaults { get; set; }

        public SegundaChance(List<int> paginasReferenciadas, int numeroDeQuadros)
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
                        AplicarSubstituicaoSegundaChance(pagina);
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

        private void AdicionarPaginaNaMemoria(int numeroPagina, bool referenciada)
        {
            Memoria.Add(new Pagina { Numero = numeroPagina, Referenciada = referenciada });
        }

        private void AplicarSubstituicaoSegundaChance(int numeroPagina)
        {
            while (true)
            {
                if (!Memoria[Ponteiro].Referenciada)
                {
                    RemoverPaginaDaMemoria(Ponteiro);
                    AdicionarPaginaNaMemoria(numeroPagina, referenciada: true);
                    ZerarPonteiro();
                    break;
                }
                else
                {
                    // A página recebe uma segunda chance, é retirada do início da fila e jogada para o fim.
                    AdicionarPaginaNaMemoria(Memoria[Ponteiro].Numero, referenciada: false);
                    RemoverPaginaDaMemoria(Ponteiro);
                }
                AvancarPonteiro();
            }
        }

        private bool PaginaEstaNaMemoria(int numeroPagina)
        {
            return Memoria.Exists(p => p.Numero == numeroPagina);
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

        private void ZerarPonteiro()
        {
            Ponteiro = 0;
        }

        private void RemoverPaginaDaMemoria(int indice)
        {
            Memoria.RemoveAt(indice);
        }
    }
}
