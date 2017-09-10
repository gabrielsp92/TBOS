using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using TropicalBears.Model.DataBase.Model;

namespace TropicalBears.Model.DataBase.Repository
{
    public class UserRepository : RepositoryBase<User>
    {
        public UserRepository(ISession session) : base(session)
        {
        }

    }
}
