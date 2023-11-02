using System.ComponentModel;

namespace SimasTurbo.Models
{
    public class Produto
    {
        public Guid ProdutoId { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        [DisplayName("Preço Unitário")]
        public float PreçoUnit { get; set; }
        [DisplayName("Categoria")]
        public Guid CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
        [DisplayName("Fornecedor")]
        public Guid FornecedorId { get; set; }
        public Fornecedor? Fornecedor { get; set; }

        public ICollection<CadCompras>? CadCompras { get; set; }

        public ICollection<CadVendas>? CadVendas { get; set; }
    }
    
}
