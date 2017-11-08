using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using TropicalBears.Model.DataBase.Model;

namespace TropicalBears.Model.DataBase.Repository
{
    public class VendaRepository : RepositoryBase<Venda>
    {
        public VendaRepository(ISession session) : base(session)
        {
        }
        public void CriarVenda(Venda v)
        {
            //status 0 = undefined, 1 = confirmed, 2 = canceled
            v.Status = 0;
       
            Session.Clear();
            var transaction = Session.BeginTransaction();
            Session.Save(v);
            transaction.Commit();

            
            //adding venda to carrinho
            var cart = v.Carrinho;
            //cart.Venda = v;
            DbConfig.Instance.CarrinhoRepository.Salvar(cart);

            //Changing Estoque
            var cps = v.Carrinho.CarrinhoProduto;

            foreach(var prod in cps)
            {
                prod.Estoque.PrecoCusto -= (prod.Estoque.CustoMedio() * prod.Quantidade);

                prod.Estoque.Quantidade -= prod.Quantidade;          
                DbConfig.Instance.EstoqueRepository.Salvar(prod.Estoque);
            }
        }
        public void CancelVenda(Venda v)
        {
            v.Status = 2;

            Session.Clear();
            var transaction = Session.BeginTransaction();
            Session.Save(v);
            transaction.Commit();

            //removing venda from carrinho
            var cart = v.Carrinho;
            //cart.Venda = null;
            DbConfig.Instance.CarrinhoRepository.Salvar(cart);

            //Changing Estoque
            var cps = v.Carrinho.CarrinhoProduto;

            foreach (var prod in cps)
            {
                prod.Estoque.Quantidade += prod.Quantidade;
                prod.Estoque.PrecoCusto += (prod.Estoque.CustoMedio() * prod.Quantidade);
                DbConfig.Instance.EstoqueRepository.Salvar(prod.Estoque);
            }

        }
        public void ConfirmVenda(Venda v)
        {
            v.Status = 1;

            Session.Clear();
            var transaction = Session.BeginTransaction();
            Session.Save(v);
            transaction.Commit();
        }
    }
}
