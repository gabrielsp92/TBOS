using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TropicalBears.Model.DataBase.Model
{
    public class Role
    {
        public virtual int Id { get; set; }
        public virtual String Nome { get; set; }
    }
    public class RoleMap : ClassMapping<Role>
    {
        public RoleMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Identity));
            Property(x => x.Nome);
        }
        
    }
}
