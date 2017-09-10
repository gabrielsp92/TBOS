using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TropicalBears.Model.DataBase.Model
{
    public class Imagem
    {
        public int Id { get; set; }
        public string Img { get; set; }
        public Produto Produto { get; set; }
        public DateTime TimeStamps { get; set; }

    }
}
