using System.ComponentModel;

namespace SimasTurbo.Models
{
    public class Cliente
    {
        [DisplayName("Cliente")]
        public Guid ClienteId { get; set; }
        public string Nome { get; set; }
        [DisplayName("Endereço")]
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public string CPF { get; set; }
        [DisplayName("E-Mail")]
        public string Email { get; set; }
        [DisplayName("Data de Nascimento")]
        public DateTime DataHora { get; set; }
    }
}
