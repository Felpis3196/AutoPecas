namespace SimasTurbo.Models
{
    public class ItemVenda
    {
        public Guid ItemVendaId { get; set; }
        public Guid ProdutoId { get; set; }
        public Produto? Produto { get; set; }
        public float PrecoUnit { get; set; }
        public int Quantidade { get; set; }
        public Guid CadVendasId { get; set; }
        public CadVendas? CadVendas { get; set; }
    }
}
