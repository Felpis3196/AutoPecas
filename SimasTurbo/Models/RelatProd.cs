namespace SimasTurbo.Models
{
    public class RelatProd
    {
        public Guid RelatProdId { get; set; }
        public Guid CategoriaId { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public int Quantidade { get; set; }
        public float PrecoUnit { get; set; }
    }
}
