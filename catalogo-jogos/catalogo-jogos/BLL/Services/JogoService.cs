using catalogo_jogos.BLL.Infra;
using catalogo_jogos.DTOs.Request;
using catalogo_jogos.DTOs.Response;
using catalogo_jogos.Exceptions;
using catalogo_jogos.Models;
using catalogo_jogos.Repositories.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace catalogo_jogos.BLL.Services
{
    public class JogoService : IJogoService
    { 

        private readonly IJogoRepository _jogoRepository;

        public JogoService(IJogoRepository jogoRepository)
        {
            _jogoRepository = jogoRepository;
        }

        public async Task Atualizar(Guid id, GameRequestDTO jogo)
        {
            var entidadeJogo = await _jogoRepository.ObterPorId(id);

            if (entidadeJogo == null)
                throw new JogoNaoCadastradoException();

            entidadeJogo.Nome = jogo.Nome;
            entidadeJogo.Produtora = jogo.Produtora;
            entidadeJogo.Preco = jogo.Preco;

            await _jogoRepository.Atualizar(entidadeJogo);
            
        }

        public async Task AtualizarPreco(Guid id, double preco)
        {
            var entidadeJogo = await _jogoRepository.ObterPorId(id);

            if (entidadeJogo == null)
                throw new JogoNaoCadastradoException();

            entidadeJogo.Preco = preco;

            await _jogoRepository.Atualizar(entidadeJogo);
        }

        public async Task<GameResponseDTO> Inserir(GameRequestDTO jogo)
        {
            var entidadeJogo = await _jogoRepository.ObterPorProdutora(jogo.Nome, jogo.Produtora);

            if (entidadeJogo.Count > 0)
                return null;

            var jogoInsert = new Jogo
            {
                Id = Guid.NewGuid(),
                Nome = jogo.Nome,
                Preco = jogo.Preco,
                Produtora = jogo.Produtora
            };

            await _jogoRepository.Inserir(jogoInsert);

            return new GameResponseDTO
            {
                Id = jogoInsert.Id,
                Nome = jogoInsert.Nome,
                Preco = jogoInsert.Preco,
                Produtora = jogoInsert.Produtora
            };
        }

        public async Task<List<GameResponseDTO>> Obter(int pagina, int quantidade)
        {
            var jogos = await _jogoRepository.Obter(pagina, quantidade);

            return jogos.Select(jogo => new GameResponseDTO
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            }).ToList();
        }

        public async Task<GameResponseDTO> ObterPorId(Guid id)
        {
            var jogo = await _jogoRepository.ObterPorId(id);

            if (jogo == null)
                throw new JogoNaoCadastradoException();

            return new GameResponseDTO
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco
            };
        }

        public async Task Remover(Guid id)
        {
            var jogo = await _jogoRepository.ObterPorId(id);

            if (jogo == null)
                throw new JogoNaoCadastradoException();

            await _jogoRepository.Remover(id);
        }

        public void Dispose()
        {
            _jogoRepository?.Dispose();
        }
    }
}
