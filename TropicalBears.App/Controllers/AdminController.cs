using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TropicalBears.Model.DataBase;
using TropicalBears.Model.DataBase.Model;

namespace TropicalBears.App.Controllers
{
    public class AdminController : Controller
    {
        
        // GET: Admin
        public ActionResult Index()
        {
            if (!this.CheckAdmin())
                return RedirectToAction("Denied", "Home");

            //begin
            var usr = DbConfig.Instance.UserRepository.isAuthenticated();
            ViewBag.NovaSenha = usr.Senha;
                    var prods = DbConfig.Instance.ProdutoRepository.FindAll();
                    return View(prods);

            
        }


        //PRODUTO
        public ActionResult AddProduto()
        {
            if (!this.CheckAdmin())
                return RedirectToAction("Denied", "Home");

            //Begini
            var cats = DbConfig.Instance.CategoriaRepository.FindAll();

            ViewBag.Categorias = cats;
            return View();
        }
        [HttpPost]
        public ActionResult AddProduto(Produto p, FormCollection form)
        {
            if (!this.CheckAdmin())
                return RedirectToAction("Denied", "Home");

            var idcat = Convert.ToInt32(form["categorias"].ToString());
            p.Categoria = DbConfig.Instance.CategoriaRepository.FindAll().Where(x => x.Id == idcat).FirstOrDefault();
            
            DbConfig.Instance.ProdutoRepository.Salvar(p);

            return RedirectToAction("Index");
        }
        public ActionResult EditProduto(Produto p)
        {
            if (!this.CheckAdmin())
                return RedirectToAction("Denied","Home");

            var prod = DbConfig.Instance.ProdutoRepository.FindAll().Where(x => x.Id == p.Id).FirstOrDefault();

            return View(prod);
        }
        [HttpPost]
        public ActionResult SalvarProduto(Produto p)
        {
            if (!this.CheckAdmin())
                return RedirectToAction("Denied", "Home");

            
            DbConfig.Instance.ProdutoRepository.Salvar(p);

            return RedirectToAction("Index");
        }
        public ActionResult DeleteProduto(Produto p)
        {
            if (!this.CheckAdmin())
                return RedirectToAction("Denied", "Home");

            DbConfig.Instance.ProdutoRepository.Excluir(p);

            return RedirectToAction("Index");
        }
        public ActionResult DetailsProduto(Produto p)
        {
            if (!this.CheckAdmin())
                return RedirectToAction("Denied", "Home");

            var prod = DbConfig.Instance.ProdutoRepository.FindAll().Where(x => x.Id == p.Id).FirstOrDefault();

            return View(prod);
        }

            //IMAGENS DO PRODUTO
        public ActionResult ImagemProduto(Produto p)
        {
            if (!this.CheckAdmin())
                return RedirectToAction("Denied", "Home");

            var prod = DbConfig.Instance.ProdutoRepository.FindAll().Where(x => x.Id == p.Id).FirstOrDefault();

            ViewBag.produtoID = prod.Id;
            ViewBag.produtoNOME = prod.Nome;

            var imgs = DbConfig.Instance.ImagemRepository.FindAll().Where(x => x.Produto.Id == prod.Id);

            return View(imgs);
        }
        [HttpPost]
        public ActionResult SalvarImagem(FormCollection form, HttpPostedFileBase img)
        {
            if (!this.CheckAdmin())
                return RedirectToAction("Denied", "Home");

            var p = DbConfig.Instance.ProdutoRepository.FindAll().Where(x => x.Id == Convert.ToInt32(form["produtoID"])).FirstOrDefault();
            if (img != null)
            {
                var fileName = "foto" + p.Id + "_" + DateTime.Now.ToString("yyyyMMdd") + "_" + DateTime.Now.ToString("HHmmss") + "_" + Path.GetExtension(img.FileName);

                var path = HttpContext.Server.MapPath("~/Upload/");

                var file = Path.Combine(path, fileName);

                img.SaveAs(file);

                if (System.IO.File.Exists(file))
                {
                    var image = new Imagem
                    {
                        Produto = p,
                        Img = fileName
                    };
                    DbConfig.Instance.ImagemRepository.Salvar(image);
                }
            }

            return RedirectToAction("ImagemProduto", p);
        }
        public ActionResult DeleteImagem(Imagem img)
        {
            if (!this.CheckAdmin())
                return RedirectToAction("Denied", "Home");

            var pID = Request.QueryString.Get("produto");

            var imagem = DbConfig.Instance.ImagemRepository.FindAll().Where(x => x.Id == img.Id).FirstOrDefault();
            imagem.Produto = null;

            var prod = DbConfig.Instance.ProdutoRepository.FindAll().Where(x => x.Id == Convert.ToInt32(pID)).FirstOrDefault();

            DbConfig.Instance.ImagemRepository.Delete(imagem);
            return RedirectToAction("ImagemProduto", prod);
        }

        //COMENTARIOS
        public ActionResult Comentarios()
        {
            if (!this.CheckAdmin())
                return RedirectToAction("Denied", "Home");

            var coments = DbConfig.Instance.ComentarioRepository.FindAll().OrderByDescending(x => x.Data);
            return View(coments);
        }
        public ActionResult DetalhesComentarios(int id)
        {
            if (!this.CheckAdmin())
                return RedirectToAction("Denied", "Home");

            var comentario = DbConfig.Instance.ComentarioRepository.FindAll().Where(x => x.Id == id).FirstOrDefault();
            return View(comentario);
        }
        public ActionResult ProcurarComentario(FormCollection form)
        {
            if (!this.CheckAdmin())
                return RedirectToAction("Denied", "Home");

            var search = form["nome_produto"].ToString();
            var coments = DbConfig.Instance.ComentarioRepository.FindAll().Where(x => x.Produto.Nome.ToLower().Contains(search.ToLower())).OrderByDescending(x=>x.Data);
            return View("Comentarios", coments);
        }
        public ActionResult DeletarComentario(int id)
        {
            var com = DbConfig.Instance.ComentarioRepository.FindAll().Where(x => x.Id == id).FirstOrDefault();
            DbConfig.Instance.ComentarioRepository.Deletar(com);
            return RedirectToAction("Comentarios");
        }

        //ESTOQUE
            //Listagem de Compras(estoque)
        public ActionResult Compras()
        {
            var est = DbConfig.Instance.EstoqueRepository.FindAll().OrderByDescending(x => x.Quantidade);
            return View(est);
        }
            //Adicionar Produto ao Estoque form
        public ActionResult SelecionarProduto(int id)
        {
            if (!this.CheckAdmin())
                return RedirectToAction("Denied", "Home");

            var prod = DbConfig.Instance.ProdutoRepository.FindAll().Where(x => x.Id == id).FirstOrDefault();

            return View("DetailsProduto", prod);

        }
            //Metodo de busca para Adicioanr Produto ao Estoque
        public ActionResult ProcurarProduto(FormCollection form)
        {
            if (!this.CheckAdmin())
                return RedirectToAction("Denied", "Home");

            var search = form["nome_produto"].ToString();
            var prods = DbConfig.Instance.ProdutoRepository.FindAll().Where(x => x.Nome.ToLower().Contains(search.ToLower()));
            return View("ComprarProdutos", prods);
        }
        public ActionResult AddEstoque(FormCollection form)
        {
            var produtoId = Convert.ToInt32(form["produtoId"].ToString());
            var prod = DbConfig.Instance.ProdutoRepository.FindAll().Where(x => x.Id == produtoId).FirstOrDefault();
            ViewBag.Produto = prod;
            return View();
        }
        [HttpPost]
        public ActionResult CreateEstoque(FormCollection form)
        {
            //checking if the product is already registered in stock
            var produtoId = Convert.ToInt32(form["produtoId"].ToString());
            var est = DbConfig.Instance.EstoqueRepository.FindAll().Where(x => x.Produto.Id == produtoId).FirstOrDefault();
            var qtd = Convert.ToInt32(form["quantidade"].ToString());
            var pco = Convert.ToDouble(form["precoCusto"].ToString());
            //if is registered
            if (est != null)
            {
                est.Quantidade += qtd;
                est.PrecoCusto += pco;
                DbConfig.Instance.EstoqueRepository.Salvar(est);
            }
            else
            {
                est = new Estoque();
                est.Produto = DbConfig.Instance.ProdutoRepository.FindAll().Where(x => x.Id == produtoId).FirstOrDefault();
                est.Quantidade = qtd;
                est.PrecoCusto = pco;
                DbConfig.Instance.EstoqueRepository.Salvar(est);

            }
            return RedirectToAction("Compras");
        }
        public ActionResult ProcurarCompra(FormCollection form)
        {
            if (!this.CheckAdmin())
                return RedirectToAction("Denied", "Home");

            var search = form["nome_produto"].ToString();
            var est = DbConfig.Instance.EstoqueRepository.FindAll().Where(x => x.Produto.Nome.ToLower().Contains(search.ToLower()));
            return View("Compras", est);
        }
        public ActionResult ComprarProdutos()
        {
            var prods = DbConfig.Instance.ProdutoRepository.FindAll();
            return View(prods);
        }

        //CATEGORIA
        public ActionResult Categoria()
        {
            List<Categoria> cats = DbConfig.Instance.CategoriaRepository.FindAll().ToList();
            return View(cats);
        }
        public ActionResult AddCategoria(FormCollection form)
        {
            Categoria cat = new Categoria();

            if (form["nome"] != null && form["nome"].ToString() != "")
            {
                cat.Nome = form["nome"];
                DbConfig.Instance.CategoriaRepository.Salvar(cat);
            }
           
            return RedirectToAction("Categoria");
        }


        //CUPOM DE DESCONTO
        public ActionResult Desconto()
        {
            //check auth
            if (!this.CheckAdmin())
            {
                return RedirectToAction("Denied", "Home");
            }

                var desc = DbConfig.Instance.DescontoRepository.FindAll();

                return View(desc);
            
        }
        public ActionResult CreateDesconto(FormCollection form)
        {

            var desc = new Desconto();
            desc.Codigo = form["codigo"].ToString();
            desc.Tipo = Convert.ToInt32(form["tipo"].ToString());

            if (desc.Codigo != "" && desc.Tipo > 0)
            {
                DbConfig.Instance.DescontoRepository.Salvar(desc);
            }

            return RedirectToAction("Desconto");
        }
        public ActionResult DeleteDesconto(Desconto d)
        {
            if (!this.CheckAdmin())
                return RedirectToAction("Denied", "Home");

            DbConfig.Instance.DescontoRepository.Deletar(d.Id);

            return RedirectToAction("Index");
        }

        //VENDAS
        public ActionResult Vendas()
        {
            var vendas = DbConfig.Instance.VendaRepository.FindAll().OrderBy(x => x.Data);
            return View(vendas);
        }
        
        //FLUXO DE CAIXA
        public ActionResult FluxoDeCaixa()
        {
            var vendas = DbConfig.Instance.VendaRepository.FindAll().Where(x => x.Status == 1).OrderByDescending(x=>x.Data);

            return View(vendas);
        }


        //AUTH
        public Boolean CheckAdmin()
        {
            var usr = DbConfig.Instance.UserRepository.isAuthenticated();
            if (usr != null)
            {
                if (usr.isAdmin())
                {
                    return true;
                }
            }
            return false;
        }
    }
}