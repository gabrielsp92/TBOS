using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TropicalBears.Model.DataBase.Model
{
    public class Produto
    {
        public virtual int Id { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual Subcategoria Subcategoria { get; set; }
        public virtual Tipo Tipo { get; set; }
        public virtual double Preco { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Descricao { get; set; }

        public virtual Boolean Promocao { get; set; }
        public virtual double PrecoPromocao { get; set; }

        public virtual int Estoque { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime UpdatedAt { get; set; }

        //Public virtual Imagem ImagemPrincipal{get;set;}
        public virtual IList<Imagem> Imagens { get; set; }

    }
    public class ProdutoMap : ClassMapping<Produto>
    {
        public ProdutoMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Identity));

            ManyToOne(x => x.Categoria,m => {
                m.Cascade(Cascade.All);
                m.Column("categoria_id");
                m.Class(typeof(Categoria));
                m.Lazy(LazyRelation.NoLazy);
            });

            ManyToOne(x => x.Subcategoria, m => {
                m.Cascade(Cascade.All);
                m.Column("subcategoria_id");
                m.Class(typeof(Subcategoria));
                m.Lazy(LazyRelation.NoLazy);
            });

            ManyToOne(x => x.Tipo, m => {
                m.Cascade(Cascade.All);
                m.Column("tipo_id");
                m.Class(typeof(Tipo));
                m.Lazy(LazyRelation.NoLazy);
            }); 

            Property(x => x.Preco);
            Property(x => x.Nome);
            Property(x => x.Descricao);
            Property(x => x.Promocao);
            Property(x => x.PrecoPromocao);
            Property(x => x.Estoque);
            Property(x => x.CreatedAt);
            Property(x => x.UpdatedAt);

            Bag<Imagem>(x => x.Imagens, m =>
            {
                m.Cascade(Cascade.All);
                m.Key(k => k.Column("produto_id"));
                m.Lazy(CollectionLazy.NoLazy);
                m.Inverse(true);
            },
            r => r.OneToMany());
        }

    }
}
