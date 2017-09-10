using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TropicalBears.Model.DataBase.Model
{
    public class Tipo
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; }
    }
    public class TipoMap : ClassMapping<Tipo>
    {
        public TipoMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.GuidComb));

            Property(x => x.Nome);
        }
    }
}
