using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TropicalBears.Model.DataBase.Model
{
    public class CarrinhoProduto
    {
        public virtual int Id { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual Carrinho Carrinho { get; set; }
        public virtual int Quantidade { get; set; }

        public virtual Double getValor()
        {
            if (Produto.Promocao == true)
            {
                return Quantidade * Produto.PrecoPromocao;
            }
            else
            {
                return Quantidade * Produto.Preco;
            }
        }
    }
    public class CarrinhoProdutoMap : ClassMapping<CarrinhoProduto>
    {
        public CarrinhoProdutoMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Identity));
            Property(x => x.Quantidade);

            ManyToOne(x => x.Produto, m => {
                m.Cascade(Cascade.None);
                m.Column("produto_id");
                m.Class(typeof(Produto));
                m.Lazy(LazyRelation.NoLazy);
            });
            ManyToOne(x => x.Carrinho, m => {
                m.Cascade(Cascade.None);
                m.Column("carrinho_id");
                m.Class(typeof(Carrinho));
                m.Lazy(LazyRelation.NoLazy);
            });
        }
    }
}
