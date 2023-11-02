using System.ComponentModel;

namespace SimasTurbo.Models
{
    public class RelatMov
    {
        public Guid RelatMovId { get; set; }
        [DisplayName("Nota Fiscal")]
        public string NotaFiscal { get; set; }
        [DisplayName("Data e Hora")]
        public DateTime DataEHora { get; set; }
        [DisplayName("Tipo de Movimentação")]
        public string TipoMovimentacao { get; set; }
        public string Produto { get; set; }
        public int Quantidade { get; set; }
        public float PrecoUnitario { get; set; }
    }
}
