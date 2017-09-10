using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TropicalBears.Model.DataBase.Model
{
    public class Carrinho
    {
        public virtual int Id { get; set; }
        public virtual User Usuario { get; set; }
        public virtual Boolean Status { get; set; }
        public virtual IList<Produto> Produtos { get; set; }
    }
    public class CarrinhoMap : ClassMapping<Carrinho>
    {
        public CarrinhoMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.GuidComb));
            Property(x => x.Status);

            ManyToOne(x => x.Usuario, m =>
            {
                m.Cascade(Cascade.All);
                m.Column("user_id");
                m.Class(typeof(User));
            });

            Bag<Produto>(x => x.Produtos, collectionMapping =>
            {
                collectionMapping.Table("CarrinhoProduto");
                collectionMapping.Cascade(Cascade.None);
                collectionMapping.Key(k => k.Column("carrinho_id"));
            }
            , map => map.ManyToMany(p => p.Column("produto_id")));
        }

    }
}
