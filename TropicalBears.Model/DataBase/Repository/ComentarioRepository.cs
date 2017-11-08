using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using TropicalBears.Model.DataBase.Model;

namespace TropicalBears.Model.DataBase.Repository
{
    public class ComentarioRepository : RepositoryBase<Comentario>
    {
        public ComentarioRepository(ISession session) : base(session)
        {
        }

        public void Deletar(Comentario comentario)
        {
            try
            {
                this.Session.Clear();
                // var transaction = this.Session.BeginTransaction();

                var stm = this.Session.CreateSQLQuery("Delete from comentario where Id = " + comentario.Id);
                stm.ExecuteUpdate();

                // transaction.Commit();

            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível excluir " + typeof(Comentario)
                    + "\nErro:" + ex.Message);
            }
        }
    }
}
