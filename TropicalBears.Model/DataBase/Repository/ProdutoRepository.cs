using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using TropicalBears.Model.DataBase.Model;
using NHibernate.Linq;

namespace TropicalBears.Model.DataBase.Repository
{
    public class ProdutoRepository : RepositoryBase<Produto>
    {
        public ProdutoRepository(ISession session) : base(session)
        {
        }
        public Produto First(int id)
        {
            var produto = this.Session.Query<Produto>().FirstOrDefault(f => f.Id == id);
            return produto;
        }
    }
}
