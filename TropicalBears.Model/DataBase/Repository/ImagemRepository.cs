using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using TropicalBears.Model.DataBase.Model;

namespace TropicalBears.Model.DataBase.Repository
{
    public class ImagemRepository : RepositoryBase<Imagem>
    {
        public ImagemRepository(ISession session) : base(session)
        {
        }
        public virtual void Delete(Imagem img)
        {
            try
            {
                this.Session.Clear();
                var transaction = this.Session.BeginTransaction();

                this.Session.Delete(img);

                transaction.Commit();

            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível excluir " + typeof(Imagem)
                    + "\nErro:" + ex.Message);
            }
        } 
    }
}
