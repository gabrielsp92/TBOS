using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TropicalBears.Model.DataBase.Model
{
    public class Relatorio
    {
        public Estoque Produto { get; set; }
        public int VendaMes { get; set; }
        public int Previsão { get; set; }
        public double TotalCompra { get; set; }
        public double TotalVenda { get; set; }
        public double Lucro { get; set; }

      
    }
}
