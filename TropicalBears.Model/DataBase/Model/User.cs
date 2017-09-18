using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TropicalBears.Model.DataBase.Model
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string Senha { get; set; }
        public virtual string Email { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Sobrenome { get; set; }
        public virtual IList<Role> Roles { get; set; }

        public virtual string GetNomeCompleto()
        {
            return string.Format("{0},{1}", Nome, Sobrenome);
        }
        public virtual Boolean isAdmin()
        {
            if (Roles != null)
            {
                foreach(var role in Roles)
                {
                    if (role.Nome == "Administrador")
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
    public class UserMap : ClassMapping<User>
    {
        public UserMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Identity));
            Property(x => x.Senha, m => {
                m.NotNullable(true);
                m.Unique(true);
            });
            Property(x => x.Email, m => {
                m.NotNullable(true);
                m.Unique(true);
            });
            Property(x => x.Nome);
            Property(x => x.Sobrenome);
            
            Bag<Role>(x => x.Roles,   collectionMapping =>
            {
                collectionMapping.Table("user_role");
                collectionMapping.Cascade(Cascade.None);
                collectionMapping.Key(k => k.Column("user_id"));
                collectionMapping.Lazy(CollectionLazy.NoLazy);
            }
            , map => map.ManyToMany(p => p.Column("role_id")));
        }
        
    }
}
