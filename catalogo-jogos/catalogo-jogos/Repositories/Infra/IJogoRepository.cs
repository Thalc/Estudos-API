using catalogo_jogos.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace catalogo_jogos.Repositories.Infra
{
    public interface IJogoRepository : IDisposable
    {
        Task<List<Jogo>> Obter(int pagina, int quantidade);
        Task<List<Jogo>> ObterPorProdutora(string nome, string produtora);
        Task<Jogo> ObterPorId(Guid id);
        Task Inserir(Jogo jogo);
        Task Atualizar(Jogo jogo);
        Task Remover(Guid id);
    }
}
