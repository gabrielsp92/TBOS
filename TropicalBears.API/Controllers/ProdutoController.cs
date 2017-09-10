using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TropicalBears.Model.DataBase;
using TropicalBears.Model.DataBase.Model;

namespace TropicalBears.API.Controllers
{
    public class ProdutoController : ApiController
    {
        public IList<Produto> produtos;

        public ProdutoController()
        {
            this.produtos = DbConfig.Instance.ProdutoRepository.FindAll();
        }
        // GET: api/Produto
        public IList<Produto> Get()
        {
            return this.produtos;
           // return new string[] { "value1", "value2" };
        }

        // GET: api/Produto/5
        public Produto Get(int id)
        {
            var prods = this.produtos;
            return prods.SingleOrDefault(x => x.Id == id);
        }
        [Route("api/Produto/{categoria}")]
        [HttpGet]
        public IEnumerable<Produto> GetByCategoria(string categoria)
        {
            var prods = this.produtos;
            return prods.Where(x => x.Categoria.Nome == categoria);
        }

        // POST: api/Produto
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Produto/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Produto/5
        public void Delete(int id)
        {
        }
    }
}
