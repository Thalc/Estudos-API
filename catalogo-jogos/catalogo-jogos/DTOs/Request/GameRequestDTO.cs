using System.ComponentModel.DataAnnotations;
using System;

namespace catalogo_jogos.DTOs.Request
{
    public class GameRequestDTO
    {
        [Required]
        [StringLength(100,MinimumLength =3, ErrorMessage = "O nome do jogo deve conter entre 3 a 100 caracteres")]
        public string Nome { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome da produtora deve conter entre 3 a 100 caracteres")]
        public string Produtora { get; set; }
        [Required]
        [Range(1, 1000, ErrorMessage = "O preço do jogo deve ser no mínimo 1 real e máximo 100 reais")]
        public double Preco { get; set; }
    }
}
