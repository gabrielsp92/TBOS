using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TropicalBears.Model.DataBase.Model
{
    public class Pesquisa
    {
        public virtual int Id { get; set; }
        public virtual User Usuario { get; set; }
        public virtual String Nome { get; set; }
        public virtual Double PrecoMinimo { get; set; }
        public virtual Double PrecoMaximo { get; set; }
        public virtual String Categoria { get; set; }

        public virtual DateTime Data { get; set; }

    }
    public class PesquisaMap : ClassMapping<Pesquisa>
    {
        public PesquisaMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Identity));
            Property(x => x.Nome);
            Property(x => x.PrecoMinimo);
            Property(x => x.PrecoMaximo);

            ManyToOne(x => x.Usuario, m => {
                m.Cascade(Cascade.None);
                m.Column("user_id");
                m.Class(typeof(User));
                m.NotNullable(false);
            });

            Property(x => x.Data);
            Property(x => x.Categoria);

        }
    }
}
