using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TropicalBears.Model.DataBase.Model
{
    public class Endereco
    {
        public virtual int Id { get; set; }
        public virtual string Descricao { get; set; }
        public virtual string Logradouro { get; set; }
        public virtual string Cep { get; set; }
        public virtual string Bairro { get; set; }
        public virtual string Numero { get; set; }
        public virtual string Complemento { get; set; }
        public virtual User Usuario { get; set; }
    }
    public class EnderecoMap : ClassMapping<Endereco>
    {
        public EnderecoMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Identity));

            ManyToOne(x => x.Usuario, m => {
                m.Cascade(Cascade.All);
                m.Column("user_id");
                m.Class(typeof(User));
            });

            Property(x => x.Descricao);
            Property(x => x.Logradouro);
            Property(x => x.Cep);
            Property(x => x.Bairro);
            Property(x => x.Numero);
            Property(x => x.Complemento);
        }
    }
}
