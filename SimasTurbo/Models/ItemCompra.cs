namespace SimasTurbo.Models
{
    public class ItemCompra
    {
        public Guid ItemCompraId { get; set; }
        public Guid ProdutoId { get; set; }
        public Produto? Produto { get; set; }
        public float PrecoUnit { get; set; }
        public int Quantidade { get; set; }
        public Guid CadComprasId { get; set; }
        public CadCompras? CadCompras { get; set; }
    }
}
