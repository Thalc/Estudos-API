using System;

namespace catalogo_jogos.Exceptions
{
    public class JogoJaCadastradoException : Exception
    {
        public JogoJaCadastradoException() : base("Este Jogo já esta cadastrado") { }
    }
}
