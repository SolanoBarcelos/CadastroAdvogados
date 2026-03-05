using System;
using System.ComponentModel.DataAnnotations;
using Dominio;

namespace Web.ViewModels
{
    [Serializable] 
    public class AdvogadoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o Nome")] 
        public string Nome { get; set; }

        [Required(ErrorMessage = "É obrigatório informar a Senioridade")]
        public Senioridade Senioridade { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o Logradouro")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o Bairro")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o Estado")]
        public Estado Estado { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o CEP")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "É obrigatório informar o Número")]
        public string Numero { get; set; }

        public string Complemento { get; set; }
    }
}