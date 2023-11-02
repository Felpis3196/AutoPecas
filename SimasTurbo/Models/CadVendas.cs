using System.ComponentModel;

namespace SimasTurbo.Models
{
    public class CadVendas
    {
        public Guid CadVendasId { get; set; }
        [DisplayName("Nota da Venda")]
        public string NotaVenda { get; set; }
        [DisplayName("Data da Venda")]
        public DateTime DataHora { get; set; }
        public Guid ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
        public IEnumerable<ItemVenda>? Venda { get; set; }  
    }

}
