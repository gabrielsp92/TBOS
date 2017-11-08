using System;
using System.Collections;
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

            var est = DbConfig.Instance.EstoqueRepository.FindAll().Where(x => x.Quantidade > 0);
            return View(est);
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
            var est = DbConfig.Instance.EstoqueRepository.FindAll().Where(x=>x.Produto.Categoria.Nome == "Camisas").Where(k=>k.Quantidade > 0);
            return View(est);
        }
        public ActionResult Acessorios()
        {
            var est = DbConfig.Instance.EstoqueRepository.FindAll().Where(k => k.Quantidade > 0).Where(x => x.Produto.Categoria.Nome == "Acessórios");
            return View(est);
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
            var est = DbConfig.Instance.EstoqueRepository.FindAll().Where(k => k.Quantidade > 0).Where(x => x.Produto.Nome.ToUpper().Contains(form["busca"].ToString().ToUpper()));

            if (precoMax > 0)
            {
                 est = est.Where(x => x.Preco >= precoMin).Where(x => x.Preco <= precoMax).OrderBy(x => x.Preco);
            }
            
            return View("Index", est);
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

            var est = DbConfig.Instance.EstoqueRepository.FindAll().Where(k => k.Quantidade > 0).Where(x=> x.Produto.Categoria.Nome == "Camisas");
            est = est.Where(x => x.Produto.Nome.ToUpper().Contains(form["busca"].ToString().ToUpper()));

            if (precoMax > 0)
            {
                est = est.Where(x => x.Preco >= precoMin).Where(x => x.Preco <= precoMax).OrderBy(x => x.Preco);
            }

            return View("Camisas", est);
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

            var est = DbConfig.Instance.EstoqueRepository.FindAll().Where(x => x.Produto.Categoria.Nome == "Acessorios").Where(k => k.Quantidade > 0);
            est = est.Where(x => x.Produto.Nome.ToUpper().Contains(form["busca"].ToString().ToUpper()));

            if (precoMax > 0)
            {
                est = est.Where(x => x.Preco >= precoMin).Where(x => x.Preco <= precoMax).OrderBy(x => x.Preco);
            }

            return View("Acessorios", est);
        }
        public ActionResult Details(int id)
        {
            Estoque e = DbConfig.Instance.EstoqueRepository.FindAll().Where(x => x.Id == id).FirstOrDefault();
            return View(e);
        }

        //CART METHOD
        public ActionResult Carrinho()
        {
            Carrinho car = new Carrinho();

            if (HttpContext.Session["cartID"] != null && HttpContext.Session["cartID"].ToString() != "" )
            {
                var idCart = Convert.ToInt32(HttpContext.Session["cartID"].ToString());
                car = DbConfig.Instance.CarrinhoRepository.FindAll().Where(x => x.Id == idCart).FirstOrDefault();
            }
            else
            {
                car = DbConfig.Instance.CarrinhoRepository.Salvar(car);
                HttpContext.Session["cartID"] = car.Id;
            }


            car.CarrinhoProduto = DbConfig.Instance.CarrinhoProdutoRepository.FindAll().Where(x => x.Carrinho.Id == car.Id).ToList();

            return View(car);

        }
        public ActionResult AddToCart(FormCollection form)
        {
            //get the product id
            var estoqueId = Convert.ToInt32(form["estoqueId"].ToString());
            Carrinho car = new Carrinho();

            //Check if cart exists
            if (HttpContext.Session["cartID"] != null && HttpContext.Session["cartID"].ToString() != "")
            {
                var idCart = Convert.ToInt32(HttpContext.Session["cartID"].ToString());
                car = DbConfig.Instance.CarrinhoRepository.FindAll().Where(x => x.Id == idCart).FirstOrDefault();
                car.CarrinhoProduto = DbConfig.Instance.CarrinhoProdutoRepository.FindAll().Where(x => x.Carrinho.Id == car.Id).ToList();
                
                //Check if the product is already in cart

                foreach (var cps in car.CarrinhoProduto)
                {
                    if (cps.Estoque.Id == estoqueId)
                    {
                        cps.Quantidade++;
                        return View("Carrinho", car);
                    }
                }       
            }
            else
            {
               car = DbConfig.Instance.CarrinhoRepository.Salvar(car);
            }

            
            Estoque est = DbConfig.Instance.EstoqueRepository.FindAll().Where(x => x.Id == estoqueId).FirstOrDefault();

            //Create new CP
            CarrinhoProduto cp = new CarrinhoProduto();
            cp = DbConfig.Instance.CarrinhoProdutoRepository.Salvar(cp);
            cp.Estoque = est;
            cp.Carrinho = car;
            cp.Quantidade = 1;
           
            cp = DbConfig.Instance.CarrinhoProdutoRepository.Salvar(cp);

            car.CarrinhoProduto = DbConfig.Instance.CarrinhoProdutoRepository.FindAll().Where(x => x.Carrinho.Id == car.Id).ToList();

            HttpContext.Session["cartID"] = car.Id;

            return View("Carrinho",car);
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
                com.Data = DateTime.Now;
                DbConfig.Instance.ComentarioRepository.Salvar(com);

                var est = DbConfig.Instance.EstoqueRepository.FindAll().Where(x => x.Produto.Id == com.Produto.Id).FirstOrDefault();
                return View("Details",est);
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

        public ActionResult AumentarProduto(FormCollection form)
        {
            var cpId = Convert.ToInt32(form["carrinhoProdutoID"].ToString());
            CarrinhoProduto cp = DbConfig.Instance.CarrinhoProdutoRepository.FindAll().Where(x => x.Id == cpId).FirstOrDefault();

            try
            {
                cp.Quantidade = Convert.ToInt32(form["quantidade"]);
                DbConfig.Instance.CarrinhoProdutoRepository.Salvar(cp);
            }
            catch (Exception)
            {
              //  throw;
            }
            
            return RedirectToAction("Carrinho");
        }
        public ActionResult DeletarProduto(FormCollection form)
        {
            var cpId = Convert.ToInt32(form["carrinhoProdutoID"].ToString());
            //CarrinhoProduto cp = DbConfig.Instance.CarrinhoProdutoRepository.FindAll().Where(x => x.Id == cpId).FirstOrDefault();
            DbConfig.Instance.CarrinhoProdutoRepository.Deletar(cpId);

            return RedirectToAction("Carrinho");
        }
        public ActionResult CupomDesconto(FormCollection form)
        {
            //Check if the code is valid
            var desc = DbConfig.Instance.DescontoRepository.FindAll().Where(x => x.Codigo == form["desconto"].ToString()).FirstOrDefault();
            if (desc != null)
            {
                //Attaching discount to cart
                var cart = DbConfig.Instance.CarrinhoRepository.FindAll().Where(x => x.Id == Convert.ToInt32(form["carrinhoId"].ToString())).FirstOrDefault();
                cart.Desconto = desc;
                DbConfig.Instance.CarrinhoRepository.Salvar(cart);
            }
            return RedirectToAction("Carrinho");
        }
    }
}