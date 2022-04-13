using System;

namespace catalogo_jogos.Exceptions
{
    public class JogoNaoCadastradoException : Exception
    {
        public JogoNaoCadastradoException() : base("Este jogo nao está cadastrado") { }
    }
}
