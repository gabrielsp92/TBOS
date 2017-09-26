using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TropicalBears.Model.DataBase;
using TropicalBears.Model.DataBase.Model;

namespace TropicalBears.App.Controllers
{
    public class UserController : Controller
    {
        public User User { get; set; }

        public UserController()
        {
            this.User = DbConfig.Instance.UserRepository.isAuthenticated();
        }
        // GET: User
        public ActionResult Index()
        {
            if (this.User == null)
                return RedirectToAction("Denied", "Home");

            if (User.Enderecos == null)
            {
                User.Enderecos = new List<Endereco>();
            }
            return View(this.User);

        }
        [HttpPost]
        public ActionResult AddEndereco(FormCollection form)
        {
            if (this.User == null)
                return RedirectToAction("Denied", "Home");

            Endereco end = new Endereco()
            {
                Descricao = form["descricao"].ToString(),
                Logradouro = form["logradouro"].ToString(),
                Cep = form["cep"].ToString(),
                Bairro = form["bairro"].ToString(),
                Numero = form["numero"].ToString(),
                Complemento = form["complemento"].ToString(),
                Usuario = this.User
            };
            DbConfig.Instance.EnderecoRepository.Salvar(end);
            this.User.Enderecos.Add(end);
            return RedirectToAction("Index");



        }
    }
}