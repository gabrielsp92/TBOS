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