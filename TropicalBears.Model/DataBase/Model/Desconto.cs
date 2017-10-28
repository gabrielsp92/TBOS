using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TropicalBears.Model.DataBase.Model
{
    public class Desconto
    {
        public virtual int Id { get; set; }
        public virtual String Codigo { get; set; }
        //tipos: 1 - 10%, :: 2 - 20%, :: 3 - 20,00
        public virtual int Tipo { get; set; }
        //check if the discount is active 0 = on, 1= off
       // public virtual int Status { get; set; }
    }
    public class DescontoMap : ClassMapping<Desconto>
    {
        public DescontoMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Identity));
            Property(x => x.Codigo);
            Property(x => x.Tipo);

            //Property(x => x.Status);
        }
    }
}
