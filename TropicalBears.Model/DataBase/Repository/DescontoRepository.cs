using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using TropicalBears.Model.DataBase.Model;

namespace TropicalBears.Model.DataBase.Repository
{
    public class DescontoRepository : RepositoryBase<Desconto>
    {
        public DescontoRepository(ISession session) : base(session)
        {
        }
        public void Deletar(int id)
        {
            try
            {
                this.Session.Clear();
                var transaction = this.Session.BeginTransaction();

                Desconto d = FindAll().Where(x => x.Id == id).FirstOrDefault();

                this.Session.Delete(d);

                transaction.Commit();

            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível excluir " + typeof(CarrinhoProduto)
                    + "\nErro:" + ex.Message);
            }
        }
    }
}
