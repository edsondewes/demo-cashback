using System;

namespace AppCashback.Core
{
    public class ValidacaoCadastroException : Exception
    {
        public object Erro { get; }

        public ValidacaoCadastroException(object erro) : base()
        {
            Erro = erro;
        }
    }
}
