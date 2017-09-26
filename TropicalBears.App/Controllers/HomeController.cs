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
            double precoMin;
            double precoMax;

            var min = form["min"];
            var max = form["max"];
            if (form["min"] != "")
            {
                precoMin = Convert.ToDouble(form["min"]);
            }else
            {
                precoMin = 0;
            }
            if (form["max"] != "")
            {
                precoMax = Convert.ToDouble(form["max"]);
            }
            else
            {
                precoMax = 0;
            }

            string nome = form["busca"].ToString();

            Pesquisa pesq = new Pesquisa()
            {
                Nome = nome,
                PrecoMaximo = precoMax,
                PrecoMinimo = precoMin,
                Data = DateTime.Now,
                Categoria = "Todos"
            };
            var usr = DbConfig.Instance.UserRepository.isAuthenticated();
            if (usr != null)
            {
                pesq.Usuario = usr;
            }
            DbConfig.Instance.PesquisaRepository.Salvar(pesq);
            var prods = DbConfig.Instance.ProdutoRepository.FindAll().Where(x => x.Nome.ToUpper().Contains(form["busca"].ToString().ToUpper()));

            if (precoMax > 0)
            {
                 prods = prods.Where(x => x.Preco >= precoMin).Where(x => x.Preco <= precoMax).OrderBy(x => x.Preco);
            }
            
            return View("Index", prods);
        }

        [HttpPost]
        public ActionResult BuscarCamisas(FormCollection form)
        {

            double precoMin;
            double precoMax;

            var min = form["min"];
            var max = form["max"];
            if (form["min"] != "")
            {
                precoMin = Convert.ToDouble(form["min"]);
            }
            else
            {
                precoMin = 0;
            }
            if (form["max"] != "")
            {
                precoMax = Convert.ToDouble(form["max"]);
            }
            else
            {
                precoMax = 0;
            }

            string nome = form["busca"].ToString();

            Pesquisa pesq = new Pesquisa()
            {
                Nome = nome,
                PrecoMaximo = precoMax,
                PrecoMinimo = precoMin,
                Data = DateTime.Now,
                Categoria = "Camisas"
            };
            var usr = DbConfig.Instance.UserRepository.isAuthenticated();
            if (usr != null)
            {
                pesq.Usuario = usr;
            }
            DbConfig.Instance.PesquisaRepository.Salvar(pesq);

            var prods = DbConfig.Instance.ProdutoRepository.FindAll().Where(x=> x.Categoria.Nome == "Camisas");
            prods = prods.Where(x => x.Nome.ToUpper().Contains(form["busca"].ToString().ToUpper()));

            if (precoMax > 0)
            {
                prods = prods.Where(x => x.Preco >= precoMin).Where(x => x.Preco <= precoMax).OrderBy(x => x.Preco);
            }

            return View("Camisas", prods);
        }
        [HttpPost]
        public ActionResult BuscarAcessorios(FormCollection form)
        {
            double precoMin;
            double precoMax;

            var min = form["min"];
            var max = form["max"];
            if (form["min"] != "")
            {
                precoMin = Convert.ToDouble(form["min"]);
            }
            else
            {
                precoMin = 0;
            }
            if (form["max"] != "")
            {
                precoMax = Convert.ToDouble(form["max"]);
            }
            else
            {
                precoMax = 0;
            }

            string nome = form["busca"].ToString();

            Pesquisa pesq = new Pesquisa()
            {
                Nome = nome,
                PrecoMaximo = precoMax,
                PrecoMinimo = precoMin,
                Data = DateTime.Now,
                Categoria = "Acessorios"
            };
            var usr = DbConfig.Instance.UserRepository.isAuthenticated();
            if (usr != null)
            {
                pesq.Usuario = usr;
            }
            DbConfig.Instance.PesquisaRepository.Salvar(pesq);

            var prods = DbConfig.Instance.ProdutoRepository.FindAll().Where(x => x.Categoria.Nome == "Acessorios");
            prods = prods.Where(x => x.Nome.ToUpper().Contains(form["busca"].ToString().ToUpper()));

            if (precoMax > 0)
            {
                prods = prods.Where(x => x.Preco >= precoMin).Where(x => x.Preco <= precoMax).OrderBy(x => x.Preco);
            }

            return View("Acessorios", prods);
        }
        public ActionResult Details(int id)
        {
            Produto p = DbConfig.Instance.ProdutoRepository.FindAll().Where(x => x.Id == id).FirstOrDefault();
            return View(p);
        }

        //Auth Needed
        public ActionResult SaveComment(FormCollection form)
        {
            if (this.CheckLogIn())
            {
                Comentario com = new Comentario();
                com.Avaliacao = form["Avaliacao"].ToString();
                com.Texto = form["texto"].ToString();
                com.Produto = DbConfig.Instance.ProdutoRepository.FindAll().Where(x => x.Id == Convert.ToInt32(form["produtoID"])).FirstOrDefault();
                com.Usuario = DbConfig.Instance.UserRepository.isAuthenticated();
                DbConfig.Instance.ComentarioRepository.Salvar(com);

                return View("Details",com.Produto);
            }
            return RedirectToAction("Denied");

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