using catalogo_jogos.DTOs.Request;
using catalogo_jogos.DTOs.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace catalogo_jogos.BLL.Infra
{
    public interface IJogoService : IDisposable
    {
        Task<List<GameResponseDTO>> Obter(int pagina, int quantidade);
        Task<GameResponseDTO> ObterPorId(Guid id);
        Task<GameResponseDTO> Inserir(GameRequestDTO jogo);
        Task Atualizar(Guid id, GameRequestDTO jogo);
        Task AtualizarPreco(Guid id, double preco);
        Task Remover(Guid id);
    }
}
