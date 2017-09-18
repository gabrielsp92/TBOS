﻿using System;
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
            p.CreatedAt = DateTime.Now;
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

            p.UpdatedAt = DateTime.Now;
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

            return RedirectToAction("ImagemProduto",p);
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