using System;

namespace Dominio
{
    [Serializable]
    public class Advogado
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Senioridade Senioridade { get; set; }
        public Endereco Endereco { get; set; }
    }
}