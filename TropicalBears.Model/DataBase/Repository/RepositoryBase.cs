using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TropicalBears.Model.DataBase.Repository
{
    public abstract class RepositoryBase<T> where T : class
    {
        public ISession Session = null;

        public RepositoryBase(ISession session)
        {
            this.Session = session;
        }

        public T FirstOrDefault()
        {
            return Session.Query<T>().FirstOrDefault();
        }

        public IList<T> FindAll()
        {
            return Session.CreateCriteria<T>().List<T>();
        }

        public virtual T Salvar(T model)
        {
            try
            {
                this.Session.Clear();
                var transaction = this.Session.BeginTransaction();

                this.Session.SaveOrUpdate(model);

                transaction.Commit();

                return model;

            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível salvar " + typeof(T)
                    + "\nErro:" + ex.Message);
            }
        }

        public virtual void Excluir(T model)
        {
            try
            {
                this.Session.Clear();
                var transaction = this.Session.BeginTransaction();

                this.Session.Delete(model);

                transaction.Commit();

            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível excluir " + typeof(T)
                    + "\nErro:" + ex.Message);
            }
        }
    }
}
