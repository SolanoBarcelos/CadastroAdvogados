using System;

namespace Dominio
{
    [Serializable]
    public class Endereco
    {
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public Estado Estado { get; set; }
        public string Cep { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
    }
}