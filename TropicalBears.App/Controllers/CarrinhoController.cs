using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TropicalBears.Model.DataBase;
using TropicalBears.Model.DataBase.Model;

namespace TropicalBears.App.Controllers
{
    public class CarrinhoController : Controller
    {
        // GET: Carrinho
        public ActionResult Index()
        {
            //check authentication
            if (this.CheckLogIn())
            {
                //Check if cart exists
                if (HttpContext.Session["cartID"] != null && HttpContext.Session["cartID"].ToString() != "")
                {
                    //getting cart from session
                    var idCart = Convert.ToInt32(HttpContext.Session["cartID"].ToString());
                    Carrinho cart = DbConfig.Instance.CarrinhoRepository.FindAll().Where(x => x.Id == idCart).FirstOrDefault();

                    //adding shipment to cart
                    cart.Entrega = 10;

                    //attaching user to cart
                    cart.Usuario = DbConfig.Instance.UserRepository.isAuthenticated();
                    DbConfig.Instance.CarrinhoRepository.Salvar(cart);

                    //sending cart to view Carrinho
                    return View(cart);
                }
            }
            //Not Authenticated
            return RedirectToAction("Denied", "Home");
        }

        //Client with new addres
        public ActionResult Adicionar(FormCollection form)
        {
            if (this.CheckLogIn())
            {
                //getting cart
                var idCart = Convert.ToInt32(HttpContext.Session["cartID"].ToString());
                Carrinho cart = DbConfig.Instance.CarrinhoRepository.FindAll().Where(x => x.Id == idCart).FirstOrDefault();

                //creating new Address
                var usr = DbConfig.Instance.UserRepository.isAuthenticated();
                Endereco end = new Endereco();
                end.Descricao = form["descricao"].ToString();
                end.Logradouro = form["logradouro"].ToString();
                end.Cep = form["cep"].ToString();
                end.Bairro = form["bairro"].ToString();
                end.Numero = form["numero"].ToString();
                end.Complemento = form["complemento"].ToString();
                end.Usuario = usr;
                DbConfig.Instance.EnderecoRepository.Salvar(end);

                //creating new Venda
                Venda venda = new Venda();
                venda.Endereco = end;
                venda.Carrinho = cart;
                venda.Data = DateTime.Now;
                venda.ValorTotal = cart.getValorTotal();

                //Clear Session's Cart
                HttpContext.Session["cartID"] = null;

                DbConfig.Instance.VendaRepository.CriarVenda(venda);


                
            }
            //If not Authenticated
            return RedirectToAction("Denied", "Home");
        }

        //Client with Adress selected
        public ActionResult SelecionarEndereco(FormCollection form)
        {
            //check auth
            if (this.CheckLogIn())
            {
                //getting cart
                var idCart = Convert.ToInt32(HttpContext.Session["cartID"].ToString());
                Carrinho cart = DbConfig.Instance.CarrinhoRepository.FindAll().Where(x => x.Id == idCart).FirstOrDefault();

                var enderecoId = Convert.ToInt32(form["enderecoId"].ToString());
                var end = DbConfig.Instance.EnderecoRepository.FindAll().Where(x => x.Id == enderecoId).FirstOrDefault();

                //creating new Venda
                Venda venda = new Venda();
                venda.Endereco = end;
                venda.Carrinho = cart;
                venda.Data = DateTime.Now;
                venda.ValorTotal = cart.getValorTotal();
                DbConfig.Instance.VendaRepository.CriarVenda(venda);


            }
            //If not Authenticated
            return RedirectToAction("Denied", "Home");

        }

        public Boolean CheckLogIn()
        {
            var usr = DbConfig.Instance.UserRepository.isAuthenticated();
            if (usr != null)
            {
                return true;
            }
            return false;
        }
    }
}