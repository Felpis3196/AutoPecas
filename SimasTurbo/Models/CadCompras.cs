using System.ComponentModel;

namespace SimasTurbo.Models
{
    public class CadCompras
    {
        public Guid CadComprasId { get; set; }
        [DisplayName("Nota da Compra")]
        public string NotaCompra { get; set; }
        [DisplayName("Data da Compra")]
        public DateTime DataHora { get; set; }       
        public IEnumerable<ItemCompra>? itemCompras { get; set; }
    }

}
