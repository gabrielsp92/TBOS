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
        public virtual int Status { get; set; }
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
                m.NotNullable(true);
            });

            Property(x => x.Status, m => {
                m.NotNullable(true);
            });
            Property(x => x.Descricao,m=>m.NotNullable(true));
            Property(x => x.Logradouro, m => m.NotNullable(true));
            Property(x => x.Cep, m => m.NotNullable(true));
            Property(x => x.Bairro, m => m.NotNullable(true));
            Property(x => x.Numero, m => m.NotNullable(true));
            Property(x => x.Complemento, m => m.NotNullable(true));
        }
    }
}
