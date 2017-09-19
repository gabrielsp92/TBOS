using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using TropicalBears.App.security;
using TropicalBears.Model.DataBase;
using TropicalBears.Model.DataBase.Model;

namespace TropicalBears.App.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            var prods = DbConfig.Instance.ProdutoRepository.FindAll();
            return View(prods);
        }

        public ActionResult Produtos()
        {
            var prods = DbConfig.Instance.ProdutoRepository.FindAll();
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //LOGIN 
        public ActionResult Denied()
        {
            return View();
        }

        public ActionResult Authenticated()
        {
          var usr = DbConfig.Instance.UserRepository.isAuthenticated();
            if (usr != null)
            {
                    if (usr.isAdmin())
                    {
                        return RedirectToAction("Index", "Admin");
                    }         
                return RedirectToAction("Index");
            }

            return RedirectToAction("Denied");
        }

        [HttpPost]
        public ActionResult logar(FormCollection form)
        {
            string email = form["email"].ToString();
            string pass = form["pass"].ToString();

            if (DbConfig.Instance.UserRepository.Authenticate(email, pass))
            {
               return RedirectToAction("Authenticated");
            }
            return RedirectToAction("Denied");
        }

        //Get
        public ActionResult Login()
        {
            return View();
        }


        public ActionResult Logout()
        {
            DbConfig.Instance.UserRepository.Logout();
            return RedirectToAction("Index");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(FormCollection form)
        {
            User usr = new User();

            usr.Email = form["email"].ToString();
            usr.Senha = form["pass"].ToString();
            usr.Nome = form["nome"].ToString();
            usr.Sobrenome = form["sobrenome"].ToString();
            DbConfig.Instance.UserRepository.Salvar(usr);
            DbConfig.Instance.UserRepository.Authenticate(usr.Email, usr.Senha);
            return RedirectToAction("Index");
        }

        public ActionResult Camisas()
        {
            var prods = DbConfig.Instance.ProdutoRepository.FindAll().Where(x=>x.Categoria.Nome == "Camisas");
            return View(prods);
        }
        public ActionResult Acessorios()
        {
            var prods = DbConfig.Instance.ProdutoRepository.FindAll().Where(x => x.Categoria.Nome == "Acessórios");
            return View(prods);
        }
        [HttpPost]
        public ActionResult Buscar(FormCollection form)
        {
            var prods = DbConfig.Instance.ProdutoRepository.FindAll().Where(x => x.Nome.ToUpper().Contains(form["busca"].ToString().ToUpper()));
            return View("Index", prods);
        }
        [HttpPost]
        public ActionResult BuscarCamisas(FormCollection form)
        {
            var prods = DbConfig.Instance.ProdutoRepository.FindAll().Where(x => x.Categoria.Nome == "Camisas")
                            .Where(x => x.Nome.ToUpper().Contains(form["busca"].ToString().ToUpper()));
          
            return View("Camisas", prods);
        }
        [HttpPost]
        public ActionResult BuscarAcessorios(FormCollection form)
        {
            var prods = DbConfig.Instance.ProdutoRepository.FindAll().Where(x => x.Categoria.Nome == "Acessórios")
                .Where(x => x.Nome.ToUpper().Contains(form["busca"].ToString().ToUpper()));
            
            return View("Acessorios", prods);
        }
    }
}